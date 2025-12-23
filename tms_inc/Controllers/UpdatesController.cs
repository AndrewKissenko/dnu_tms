using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using tms_inc.Models.Employees;
using tms_inc.Models.RequestedQuote;
using tms_inc.Models.ViewModels;
using tms_inc.Services;
using tms.DataAccess;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using tms.AppConfiguration;
using tms.Services;

namespace tms_inc.Controllers
{
    public class UpdatesController : Controller
    {
        private PortalContext _context;
       
        private UnprocessedNumsViewModel unprocessedNums;
        IHostingEnvironment _appEnvironment;
        private readonly UpdatesService _updatesService;
        private readonly AppSettings _appSettings;
        public UpdatesController(IHostingEnvironment appEnvironment, PortalContext portalContext, UpdatesService updatesService, AppSettings appSettings)
        {
            _appEnvironment = appEnvironment;
            _context = portalContext;
            _updatesService = updatesService;

            unprocessedNums = new UnprocessedNumsViewModel
            {
                ApplicantsNum = UpdatesService.ApplicantsNum,
                ContactMeRequestsNum = UpdatesService.ContactMeRequestsNum,
                QuoteRequestsNum = UpdatesService.QuoteRequestsNum
            };

            _appSettings = appSettings;
        }
        public async Task<IActionResult> Index()
        {
            await _updatesService.UpdateNumbers();
            return View(unprocessedNums);
        }
        [HttpGet]
        public async Task<IActionResult> _GetActiveApplications()
        {
            var people =  await _context.People
                .Where(x => !x.IsProcessed && !x.IsDeleted && x.Status == Status.Pending)
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();
            return View("HireMe/Index", people);
        }

        [HttpGet]
        public async Task<IActionResult> GetActiveApplicationsNoLayout()
        {
            var people = await _context.People
                .Where(x => !x.IsProcessed && !x.IsDeleted && x.Status == Status.Pending)
                .OrderByDescending(x=>x.CreatedAt)
                .ToListAsync();
            return View("HireMe/Table", people);
        }

        [HttpGet]
        public async Task<IActionResult> GetProcessedApplications()
        {
            var people = await _context.People
                .Where(x => x.IsProcessed || (!x.IsDeleted && x.Status != Status.Pending && x.Status != Status.Deleted))
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();
            return View("HireMe/Table", people);
        }

        [HttpGet]
        public async Task<IActionResult> GetDeletedApplications()
        {
            var people = await _context.People
                .Where(x => x.IsDeleted || x.Status == Status.Deleted)
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();
            return View("HireMe/Table", people);
        }

        [HttpGet]
        public async Task<IActionResult> GetActiveQuoteRequests()
        {
            var quoteRequests = await _context.RequestedQuotes
                .Where(x => !x.IsProcessed && !x.IsDeleted)
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();
            return View("GiveQuote/Index", quoteRequests);
        }

        [HttpGet]
        public async Task<IActionResult> GetActiveQuoteRequestsNoLayout()
        {
            var quoteRequests = await _context.RequestedQuotes
                .Where(x => !x.IsProcessed && !x.IsDeleted)
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();
            return View("GiveQuote/Table", quoteRequests);
        }

        [HttpGet]
        public async Task<IActionResult> GetProcessedQuoteRequests()
        {
            var quoteRequests = await _context.RequestedQuotes
                .Where(x => x.IsProcessed && !x.IsDeleted)
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();
            return View("GiveQuote/Table", quoteRequests);
        }

        [HttpGet]
        public async Task<IActionResult> GetDeletedQuoteRequests()
        {
            var quoteRequests = await _context.RequestedQuotes
                .Where(x => x.IsDeleted)
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();
            return View("GiveQuote/Table", quoteRequests);
        }

        [HttpGet]
        public async Task<IActionResult> GetActiveContactRequests()
        {
            var contactRequests = await _context.GetInTouches
                .Where(x => !x.IsProcessed && !x.IsDeleted)
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();
            return View("ContactMe/Index", contactRequests);
        }

        [HttpGet]
        public async Task<IActionResult> GetActiveContactRequestsNoLayout()
        {
            var contactRequests = await _context.GetInTouches
                .Where(x => !x.IsProcessed && !x.IsDeleted)
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();
            return View("ContactMe/Table", contactRequests);
        }

        [HttpGet]
        public async Task<IActionResult> GetProcessedContactRequests()
        {
            var contactRequests = await _context.GetInTouches
                .Where(x => x.IsProcessed && !x.IsDeleted)
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();
            return View("ContactMe/Table", contactRequests);
        }

        [HttpGet]
        public async Task<IActionResult> GetDeletedContactRequests()
        {
            var contactRequests = await _context.GetInTouches
                .Where(x => x.IsDeleted)
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();
            return View("ContactMe/Table", contactRequests);
        }


        public async Task<IActionResult> EditContactRequest(int id)
        {
            var contactRequest = await _context.GetInTouches.FirstOrDefaultAsync(x => x.Id == id);
            if (contactRequest != null)
            {
                if(contactRequest.IsProcessed) return RedirectToAction(nameof(GetActiveContactRequests));
                contactRequest.IsProcessed = true;
                await _context.SaveChangesAsync();
                UpdatesService.ContactMeRequestsNum--;
                return RedirectToAction(nameof(GetActiveContactRequests));
            }
            return BadRequest();
        }

        public async Task<IActionResult> DeleteContactRequest(int id)
        {
            var contactRequest = await _context.GetInTouches.FirstOrDefaultAsync(x => x.Id == id);
            if (contactRequest != null)
            {
                if (contactRequest.IsDeleted) return RedirectToAction(nameof(GetActiveContactRequests));
                contactRequest.IsDeleted = true;
                await _context.SaveChangesAsync();
                UpdatesService.ContactMeRequestsNum--;
                return RedirectToAction(nameof(GetActiveContactRequests));
            }
            return BadRequest();
        }

        public async Task<IActionResult> EditQuoteRequest(int id)
        {
            var quote =  await _context.RequestedQuotes.FirstOrDefaultAsync(x => x.Id == id);
            if (quote != null)
            {
                if (quote.IsProcessed) return RedirectToAction(nameof(GetActiveQuoteRequests));
                quote.IsProcessed = true;
                await _context.SaveChangesAsync();
                UpdatesService.QuoteRequestsNum--;
                return RedirectToAction(nameof(GetActiveQuoteRequests));
            }
            return BadRequest();
        }

        public async Task<IActionResult> DeleteQuoteRequest(int id)
        {
            var quote = await _context.RequestedQuotes.FirstOrDefaultAsync(x => x.Id == id);
            if (quote != null)
            {
                if(quote.IsDeleted) return RedirectToAction(nameof(GetActiveQuoteRequests));
                quote.IsDeleted = true;
                await _context.SaveChangesAsync();
                UpdatesService.QuoteRequestsNum--;
                return RedirectToAction(nameof(GetActiveQuoteRequests));
            }
            return BadRequest();
        }

        public async Task<IActionResult> GetApplication(int id)
        {
            var application = await _context.People.Include(x => x.Files)
                .FirstOrDefaultAsync(x => x.Id == id);

            //Driver file is in the DB. Redirect to Details view, so it will handle the Model
            if (application.ApplicationType == ApplicationType.Driver) 
                return PartialView("HireMe/Details", application);

            if (application.Files == null || application.Files.Count == 0 || string.IsNullOrEmpty(application.Files[0].Path))
                return BadRequest("No file found");

            var file = application.Files[0];

            var s3Service = HttpContext.RequestServices.GetRequiredService<S3Service>();

            var downloadUrl = await s3Service.GeneratePreSignedUrl(_appSettings.CVBucket, file.Path, Amazon.S3.HttpVerb.GET);
            //return direct file
            return Redirect(downloadUrl);
        }

        public async Task<UnprocessedNumsViewModel> GetUnprocessedEntetiesNumber()
        {
            if (UpdatesService.ApplicantsNum == -1 && UpdatesService.ContactMeRequestsNum == -1 && UpdatesService.QuoteRequestsNum == -1)
            {
              await _updatesService.UpdateNumbers();
            }
            return new UnprocessedNumsViewModel {
                ApplicantsNum = UpdatesService.ApplicantsNum,
                ContactMeRequestsNum = UpdatesService.ContactMeRequestsNum,
                QuoteRequestsNum = UpdatesService.QuoteRequestsNum,
                Total = UpdatesService.ApplicantsNum + UpdatesService.ContactMeRequestsNum + UpdatesService.QuoteRequestsNum
            };
        }

       

        [HttpGet]
        public async Task<IActionResult> EditApplicant(int id)
        {
            var res =  await _context.People.FirstOrDefaultAsync(x => x.Id == id);
            var statuses = new List<Status>
            {
                Status.Pending, Status.Hired, Status.Rejected, Status.Deleted
            };
            ViewBag.Statuses = new SelectList(statuses);
            return View("HireMe/Edit", res);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditApplicant(int id , Applicant person)
        {
            await UpdateApplicant(id , person);
            return RedirectToAction(nameof(_GetActiveApplications));
        }

        [HttpGet]
        public async Task<IActionResult> DeleteApplicant(int id)
        {
            var applicant = await _context.People.FirstOrDefaultAsync(x => x.Id == id);
            if (applicant == null) throw new System.Exception("not found");
            applicant.IsDeleted = true;
           // applicant.Status = Status.Deleted;
            _context.People.Remove(applicant);
            UpdatesService.ApplicantsNum--;
            //_context.Update(applicant);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(_GetActiveApplications));

        }

       

        private async Task UpdateApplicant(int id , Applicant person)
        {
            var foundPerson = await _context.People.FirstOrDefaultAsync(x=>x.Id == id);
           
            if (foundPerson == null) throw new System.Exception("no entity found");
            if (person.IsDeleted || person.IsProcessed || person.Status != Status.Pending) UpdatesService.ApplicantsNum--;
            if(person.Status == Status.Hired)
            {
                var driver = await _context.Drivers.FirstOrDefaultAsync(x => x.Phone.Contains(person.Phone.Trim()));
                if (driver == null)
                {
                    _context.Drivers.Add(new tms.Models.Driver
                    {
                        ApplicantId = foundPerson.Id,
                        Name = person.ApplicantFirstName + " " + person.ApplicantSecondName,
                        Phone = person.Phone
                    });
                }
            }
            foundPerson.IsDeleted = person.IsDeleted;
            foundPerson.IsProcessed = person.IsProcessed;
            foundPerson.ApplicantFirstName = person.ApplicantFirstName;
            foundPerson.ApplicantSecondName = person.ApplicantSecondName;
            foundPerson.Phone = person.Phone;
            foundPerson.Status = person.Status;
            await _context.SaveChangesAsync();
        }


    }
}
