using Microsoft.EntityFrameworkCore;
using tms_inc.Models.Employees;
using tms_inc.Models.File;
using tms.DataAccess;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Threading.Tasks;
using tms.Requests;

namespace tms.Services
{
    public class ApplicatService
    {
        private readonly PortalContext _context;
        public ApplicatService(PortalContext portalContext)
        {
            _context = portalContext;
        }
        public async Task<int> CreateNonDriverApplicatIfNotExistsAsync(ApplyForNonDriverPositionRequest request)
        {
            var applicant = await _context.People
               .AsNoTracking()
               .FirstOrDefaultAsync(x => x.ApplicantFirstName == request.FirstName &&
                                    x.ApplicantSecondName == request.LastName &&
                                    x.Phone == request.Phone &&
                                    x.ApplicationType == ApplicationType.Other);

            if (applicant == null)
            {
                applicant = new Applicant
                {
                    CreatedAt = DateTime.UtcNow,
                    ApplicantFirstName = request.FirstName,
                    ApplicantSecondName = request.LastName,
                    Phone = request.Phone,
                    ApplicationType = ApplicationType.Other,
                };

                var created = await _context.People.AddAsync(applicant);
                await _context.SaveChangesAsync();
            }
            return applicant.Id;
        }
    }
}
