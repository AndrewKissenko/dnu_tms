using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using tms_inc.Models;
using tms_inc.Models.RequestedQuote;
using tms.DataAccess;
using tms.Models;

namespace tms_inc.Areas.Site.Controllers
{
    [Area("Tms")]
    public class HomeController : Controller
    {
        private PortalContext _context;
        private SmtpClient client;
        public HomeController(PortalContext context, SmtpClient smtpClient)
        {
            _context = context;
            client = smtpClient;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult RequestQuote()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RequestQuote(RequestedQuote requestedQuote)
        {
            var uri = Request.Headers["Referer"].ToString();
            if (!ShouldSave(requestedQuote.Message))
                return Redirect(uri);

            if (ModelState.IsValid)
            {
                requestedQuote.CreatedAt = DateTime.Now;
                await _context.RequestedQuotes.AddAsync(requestedQuote);
                await _context.SaveChangesAsync();
            }
            return Redirect(uri);
        }

        public IActionResult ContactUs()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ContactUs(GetInTouch getInTouch)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (!ShouldSave(getInTouch.Message))
                        return View(getInTouch);
                    getInTouch.CreatedAt = DateTime.Now;
                    
                    await _context.GetInTouches.AddAsync(getInTouch);
                    await _context.SaveChangesAsync();
                    if (!String.IsNullOrEmpty(getInTouch.Email))
                    {
                        await SendEmailAsync(getInTouch.Subject, GetBodyTemplate(getInTouch),
                            "email@example.net", "email@example.net", "CONTACT REQUEST");
                    }
                }
                catch (Exception ex)
                {

                    return Ok(ex.Message);
                }
               
            }
            return View(getInTouch);
        }
      
        private async Task SendEmailAsync(string subject, string body, string sendTo, string sendFrom, string header)
        {

            MailMessage message = new MailMessage();
            message.From = new MailAddress(sendFrom, header);
            message.IsBodyHtml = true;
            message.To.Add(new MailAddress(sendTo));

            message.Subject = subject;
            message.Body = body;

            await client.SendMailAsync(message);


        }

        private string GetBodyTemplate(GetInTouch getInTouch)
        {
            var body = new StringBuilder();
            body.Append("<div style='padding:15px; width: 250px; background-color: #f2f2f2;'>");
            body.Append("<label>Name:</label>");
            body.Append("<div><input readonly value='" + getInTouch.Name + "' style='width: 250px'></div>");
            body.Append("<label>Email:</label>");
            body.Append("<div><input readonly value='" + getInTouch.Email + "' style='width: 250px'></div>");
            body.Append("<label>Phone:</label>");
            body.Append("<div><input readonly value='" + getInTouch.Phone + "' style='width: 250px'></div>");
            body.Append("<label>Message:</label>");
            body.Append("<div><textarea style='width: 250px' rows='7'>" + getInTouch.Message+"</textarea></div>");
            return body.ToString();
        }

        [HttpGet]
        public async Task<ActionResult> AvailableTrucks()
        {
            var today = DateTime.Now.Date;
            var driverCities = await _context.DriverCities.Where(x => x.Date == today && x.AtHome == false && x.CityId != null).
                Include(x => x.City).Include(x => x.Driver).ThenInclude(x => x.User).
                Include(x => x.Driver).ThenInclude(x => x.Truck).ToListAsync();

            var driverCityGroups = driverCities.GroupBy(x => x.Driver.User.FirstName);
            List<List<MailTableVM>> mailTables = new List<List<MailTableVM>>();
            foreach (var group in driverCityGroups)
            {
                mailTables.Add(group.Select(x => new MailTableVM
                {
                    UserName = x.Driver.User.FirstName,
                    UserName2 = x.Driver.User.LastName,
                    Truck = x.Driver.Truck.TruckId,
                    CityName = x.City.CityName,
                    CityState = x.City.State,
                    Comment = x.Comment

                }).ToList());
            }



            return View(mailTables);
        }
        private bool ShouldSave(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return false;

            string input = text.ToLowerInvariant();

            // Common spam keywords
            string[] spamKeywords =
            {
                "click here",
                "free",
                "winner",
                "prize",
                "offer",
                "sms",
                "subscribe",
                "urgent"
            };

            // Check keyword matches
            foreach (var keyword in spamKeywords)
            {
                if (input.Contains(keyword))
                    return false;
            }

            // Detect URLs (http, https, www, .com, etc.)
            if (Regex.IsMatch(input, @"(http|https|www\.|\.(com|net|org|io))"))
                return false;

            // Detect excessive punctuation (!!! $$$)
            if (Regex.IsMatch(input, @"([!$])\1{2,}"))
                return false;

            return true;
        }

    }

   

}
