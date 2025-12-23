using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using tms.DataAccess;
using tms.Models;

namespace tms.Controllers
{
    public class MailController : Controller
    {
        private readonly PortalContext _context;
        private readonly IConfiguration _configuration;
        private SmtpClient client;
        public MailController(PortalContext portalContext, IConfiguration configuration, SmtpClient smtpClient)
        {
            _context = portalContext;
            _configuration = configuration;
            client = smtpClient;
        }
        // GET: MailController
        public async Task<IActionResult> Index()
        {
            var today = DateTime.Now.Date;
            var driverCities = await _context.DriverCities.Where(x => x.Date == today && x.AtHome == false && x.CityId != null).
                Include(x => x.City).Include(x => x.Driver).ThenInclude(x => x.User).
                Include(x => x.Driver).ThenInclude(x => x.Truck).ToListAsync();

            var driverCityGroups = driverCities.GroupBy(x => x.Driver.User.FirstName);
            List<List<MailTableVM>> mailTables = new List<List<MailTableVM>>();
            foreach (var group in driverCityGroups)
            {
                mailTables.Add(group.Select(x=>new MailTableVM
                {
                    UserName = x.Driver.User.FirstName,
                    UserName2 = x.Driver.User.LastName,
                    Truck = x.Driver.Truck.TruckId,
                    CityName = x.City.CityName,
                    CityState = x.City.State, 
                    Comment = x.Comment

                })?.ToList());
            }
           


            return View(mailTables);
        }

        // GET: MailController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        [HttpPost]
         public async Task<IActionResult> SendMail(EmailDataDTO emailData)
        {
            var clients =  _context.Clients.ToList();
            emailData.Body = emailData.Body + emailData.Footer;
            foreach (var client in clients)
            {
               await SendEmailAsync(emailData.Subject, emailData.Body, client.Email, "email@example.net", "NLE Inc.");
            }

            return Ok("Sent");

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
        // GET: MailController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MailController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: MailController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: MailController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: MailController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: MailController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
