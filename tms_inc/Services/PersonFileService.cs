using Amazon.Runtime.Internal;
using Azure.Core;
using Microsoft.EntityFrameworkCore;
using tms_inc.Models.File;
using tms.DataAccess;
using System;
using System.Threading.Tasks;
using tms.AppConfiguration;

namespace tms.Services
{
    public class PersonFileService
    {
        private readonly PortalContext _context;
        private readonly S3Service _s3Service;
        private readonly AppSettings _appSettings;
        public PersonFileService(PortalContext portalContext, S3Service s3Service, AppSettings appSettings)
        {
            _context = portalContext;
            _s3Service = s3Service;
            _appSettings = appSettings;
        }

        public async Task<(int fileId, string presignedUrl)> CreateIfNotExistsAsync(string name, string personName, string personLastName, int personId)
        {
            var file = await _context.PersonFiles
                .FirstOrDefaultAsync(x => x.Name == name && x.FileType == FileType.CV);

            var direcrory = "CV_Other_" + personName + "_" + personLastName;
            var filePath = direcrory + "/" + name;

            if (file == null)
            {

                file = new PersonFile
                {
                    Name = name,
                    FileType = FileType.CV,
                    Path = filePath,
                    CreatedAt = DateTime.Now,
                    PersonId = personId,
                };
                await _context.PersonFiles.AddAsync(file);
                await _context.SaveChangesAsync();
            }

            var url = await _s3Service.GeneratePreSignedUrl(_appSettings.CVBucket, filePath, Amazon.S3.HttpVerb.PUT);

            return (file.Id, url);
        }

        public async Task MarkUploadedAsync(int fileId)
        {
            var file = await _context.PersonFiles.FirstOrDefaultAsync(x => x.Id == fileId);
            if (file != null)
            {
                file.IsUploaded = true;
                _context.PersonFiles.Update(file);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("File not found");
            }
        }
    }
}
