using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using tms_inc.Models;
using tms_inc.Models.Employees;
using tms_inc.Models.File;
using tms.DataAccess;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using tms_inc.Services;
using tms.Services;
using Microsoft.Extensions.DependencyInjection;
using tms.AppConfiguration;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Authorization;
using tms.Requests;
using tms.Responses;

namespace tms_inc.Areas.tms.Controllers
{
    [Area("Tms")]
    public class CareerController: Controller
    {

        IHostingEnvironment _appEnvironment;
        PortalContext _context;
        private readonly AppSettings _appSettings;
        private readonly ApplicatService _applicantService;
        private readonly PersonFileService _personFileService;
        public CareerController(IHostingEnvironment appEnvironment, PortalContext context, AppSettings appSettings, ApplicatService applicantService, PersonFileService personFileService)
        {

            _appEnvironment = appEnvironment;
            _context = context;
            _appSettings = appSettings;
            _applicantService = applicantService;
            _personFileService = personFileService;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.Positions.Where(x=>!x.IsDeleted).ToListAsync());
        }

        public ActionResult DriverApplication()
        {
            return View();
        }

        public ActionResult OtherApplication()
        {
            return View();
        }

        public ActionResult DriverApplication2()
        {
            return View();
        }

        public ActionResult _GetContractInformation()
        {
            return PartialView();
        }

        public ActionResult _GetStateList()
        {
            return PartialView();
        }
        public ActionResult _GetSchoolInformation()
        {
            return PartialView();
        }

        public ActionResult EmploymentInformation()
        {
            return PartialView();
        }
        public ActionResult _GetMotorVehicleRecord()
        {
            return PartialView();
        }

        public ActionResult _GetIncidentDetails()
        {
            return PartialView();
        }

        public ActionResult _GetAccidentDetails()
        {
            return PartialView();
        }
        public ActionResult _GetCriminalRecord()
        {
            return PartialView();
        }


        //partial view for one small table
        public ActionResult _GetTable()
        {
            return PartialView();
        }

        public ActionResult _GetConfirmMessage()
        {
            return PartialView();
        }
        public ActionResult _GetStepper2Step1()
        {
              return PartialView();
        }
        public ActionResult _GetStepper2Step2()
        {
            return PartialView();
        }

        public ActionResult _GetStepper2Step3()
        {
            return PartialView();
        }

        [HttpPost]
        public async Task<OperationDetails> UploadStep1Data(Applicant candidate)
        {
            candidate.ApplicationType = ApplicationType.Driver;
            return await CreateOrUpdatePersonAsync(candidate.Id.ToString(), candidate);
        }

        [HttpPost]
        public async Task<IActionResult> UploadCV(ApplyForNonDriverPositionRequest request)
        {
            var validationRes = await new ApplyForNonDriverPositionRequestValidator()
                .ValidateAsync(request);

            if (!validationRes.IsValid && validationRes.Errors.Count > 0)
            {
                return BadRequest(string.Join(",", validationRes.Errors));
            }

            using var transaction = await _context.Database.BeginTransactionAsync();

            var id = await _applicantService.CreateNonDriverApplicatIfNotExistsAsync(request);
            var fileRes =  await _personFileService.CreateIfNotExistsAsync(request.FileName, request.FirstName, request.LastName, id);

            await transaction.CommitAsync();

            return Ok(new ApplyForNonDriverPositionResponse
            {
                PreSignedUrl = fileRes.presignedUrl,
                FileID = fileRes.fileId  
            });
        }

        [HttpPost]
        public async Task<IActionResult> MarkFileAsUploaded(int fileId)
        {
            await _personFileService.MarkUploadedAsync(fileId);
            return Ok();
        }
        public ActionResult RenderView()
        {
            return View();
        }

        public IActionResult Success()
        {
            return View();
        }

        [HttpPost]
        public async Task<OperationDetails> AddLicenseFront()
        {
            var res =  await AddFileToDb(FileType.LicenseFront);
            return res;
        }

        [HttpPost]
        public async Task<OperationDetails> AddLicenseBack()
        {
            return await AddFileToDb(FileType.LicenseBack);
        }

        [HttpPost]
        public async Task<OperationDetails> AddSignature()
        {
            return await AddFileToDb(FileType.Signature);
        }

        [HttpPost]
        public async Task<OperationDetails> AddCVFile()
        {
            var res = await AddCVFileToDb(FileType.CV);
            return res;
        }

        private async Task<OperationDetails> AddCVFile(FileType fileType)
        {
            if (Request.Form.Files.Count == 0) return new OperationDetails() { IsError = true, Message = "No file specified" };
            var person = new Applicant();
            for (int i = 0; i < Request.Form.Files.Count; i++)
            {

                var file = Request.Form.Files[i]; //Uploaded file

                string path = "/Files/" + file.FileName;
                // сохраняем файл в папку Files в каталоге wwwroot
                var personFile = new PersonFile() { Name = file.FileName, FileType = fileType, Path = path, CreatedAt = DateTime.Now, ContentType = file.ContentType};

                try
                {
                    using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }

                    person.Files.Add(personFile);

                    return await CreateOrUpdatePersonAsync(file.Name, person);
                }
                catch (Exception ex)
                {
                    return new OperationDetails()
                    {
                        IsError = true,
                        Message = ex.Message
                    };
                }

            }
            return new OperationDetails()
            {
                IsError = false
            };
        }

        private async Task<OperationDetails> AddCVFileToDb(FileType fileType)
        {
            if (Request.Form.Files.Count == 0) return new OperationDetails() { IsError = true, Message = "No file specified" };
            var person = new Applicant();
            for (int i = 0; i < Request.Form.Files.Count; i++)
            {

                var file = Request.Form.Files[i]; //Uploaded file

                string path = "/Files/" + file.FileName;
                // сохраняем файл в папку Files в каталоге wwwroot
                var personFile = new PersonFile() { Name = file.FileName, FileType = fileType, Path = path, CreatedAt = DateTime.Now, ContentType = file.ContentType };

                try
                {
                    byte[] fileData = null;
                    using (var binaryReader = new BinaryReader(file.OpenReadStream()))
                    {
                        fileData = binaryReader.ReadBytes((int)file.Length);
                    }
                    personFile.File = fileData;
                    person.Files.Add(personFile);

                    return await CreateOrUpdatePersonAsync(file.Name, person);
                }
                catch (Exception ex)
                {
                    return new OperationDetails()
                    {
                        IsError = true,
                        Message = ex.Message
                    };
                }

            }
            return new OperationDetails()
            {
                IsError = false
            };
        }

        private async Task<OperationDetails> AddFileToDb(FileType fileType)
        {
            if (Request.Form.Files.Count == 0) return new OperationDetails() { IsError = true, Message = "No file specified" };
            var person = new Applicant();
            
            for (int i = 0; i < Request.Form.Files.Count; i++)
            {

                var file = Request.Form.Files[i]; //Uploaded file

                string mimeType = file.ContentType;
                if (mimeType.Split('/')[0] != "image" && mimeType != "application/pdf") return new OperationDetails()
                {
                    IsError = true,
                    Message = "File is not image"
                };


                

                string path = "/Files/" + file.FileName;
                // сохраняем файл в папку Files в каталоге wwwroot
                var personFile = new PersonFile() { Name = file.FileName, FileType = fileType, Path = path, CreatedAt = DateTime.Now, ContentType = mimeType };

                try
                {
                    byte[] fileData = null;
                    using (var binaryReader = new BinaryReader(file.OpenReadStream()))
                    {
                        fileData = binaryReader.ReadBytes((int)file.Length);
                    }
                    personFile.File = fileData;
                    person.Files.Add(personFile);

                    return await CreateOrUpdatePersonAsync(file.Name, person);
                }
                catch (Exception ex)
                {
                    return new OperationDetails()
                    {
                        IsError = true,
                        Message = ex.Message
                    };
                }

            }
            return new OperationDetails()
            {
                IsError = false
            };

        }

        private async Task<OperationDetails> AddFile(FileType fileType)
        {
            if(Request.Form.Files.Count == 0 ) return new OperationDetails() { IsError = true, Message = "No file specified" };
            var person = new Applicant();
            for (int i = 0; i < Request.Form.Files.Count; i++)
            {

                var file = Request.Form.Files[i]; //Uploaded file
                    
                var files = Directory.GetFiles(_appEnvironment.WebRootPath + "/Files/");
               // if(files.Any(x=>x.Contains(file.FileName))) return new OperationDetails() { IsError = true, Message = "You have already submitted this file previously. Try another one" };

                string mimeType = file.ContentType;
                if(mimeType.Split('/')[0] != "image" && mimeType != "application/pdf") return new OperationDetails() {
                    IsError = true, Message = "File is not image" };

               


                string path = "/Files/" + file.FileName;
                // сохраняем файл в папку Files в каталоге wwwroot
                var personFile = new PersonFile() { Name = file.FileName, FileType = fileType, Path = path, CreatedAt = DateTime.Now, ContentType = mimeType };
              
                try
                {
                    using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }

                    person.Files.Add(personFile);
                   
                    return await CreateOrUpdatePersonAsync(file.Name, person);
                }
                catch (Exception ex)
                {
                    return new OperationDetails()
                    {
                        IsError = true,
                        Message = ex.Message
                    };
                }
             
            }
            return new OperationDetails()
            {
                IsError = false
            };

           }

        private async Task<OperationDetails> CreateOrUpdatePersonAsync(string id, Applicant person)
        {
            EntityEntry<Applicant> entityEntry = null;
            int  parsedId = 0;
            if (string.IsNullOrEmpty(id))
            {
                person.CreatedAt = DateTime.Now;
                entityEntry = await _context.People.AddAsync(person);
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {

                    throw;
                }
               
                UpdatesService.ApplicantsNum++;
            }
            else
            {
                var res = int.TryParse(id, out parsedId);

                if(!res) return new OperationDetails { IsError = true, Message = "Error: wrong person id" };

                var foundPerson =  await _context.People.Include(x => x.Files).FirstOrDefaultAsync(x => x.Id == parsedId);
                person.CreatedAt = foundPerson.CreatedAt;
                if (foundPerson != null) {

                    if(!String.IsNullOrEmpty(person.Signature))
                    foundPerson.Signature = person.Signature;
                    if (!String.IsNullOrEmpty(person.TableHtml))
                        foundPerson.TableHtml = person.TableHtml;
                    foundPerson.ApplicationType = person.ApplicationType;
                    if (!String.IsNullOrEmpty(person.ApplicantFirstName))
                        foundPerson.ApplicantFirstName = person.ApplicantFirstName;
                    if (!String.IsNullOrEmpty(person.ApplicantSecondName))
                        foundPerson.ApplicantSecondName = person.ApplicantSecondName;
                    if (!String.IsNullOrEmpty(person.Phone))
                        foundPerson.Phone = person.Phone;
                    _context.People.Update(foundPerson);
                    await _context.SaveChangesAsync();

                    if (person.Files.Count > 0)
                    {                        
                        foreach (var item in person.Files)
                        {
                            if (foundPerson.Files.Any(x => x.Name == item.Name)) 
                                return new OperationDetails { IsError = true, Message = "This file has previously been uploaded" };

                            foundPerson.Files.Add(item);
                        }
                    }
                    _context.People.Update(foundPerson);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    person.CreatedAt = DateTime.Now;
                    entityEntry = await _context.People.AddAsync(person);
                    _context.People.Update(foundPerson);
                    await _context.SaveChangesAsync();
                    UpdatesService.ApplicantsNum++;
                }
            }

            return new OperationDetails
            {
                IsError = false,
                EntityId = entityEntry != null ? entityEntry.Entity.Id : parsedId,
                CreatedAt = person.CreatedAt.ToShortDateString()
            };
            
        }



    }
}
