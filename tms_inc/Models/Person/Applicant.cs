using tms_inc.Models.File;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tms_inc.Models.Employees
{
    public enum ApplicationType
    {
        Driver = 1,
        Other
    }
    public enum Status
    {
        Pending, 
        Rejected,
        Hired,
        Deleted
    }
    public class Applicant: EntityBase
    {
        public string ApplicantFirstName { get; set; }
        public string ApplicantSecondName { get; set; }
        public string Phone { get; set; }
        public string TableHtml { get; set; }
        public string Signature { get; set; }
        public ApplicationType ApplicationType { get; set; }
        public Status Status { get; set; }
        public List<PersonFile> Files { get; set; }

        public Applicant()
        {
            Files = new List<PersonFile>();
            Status = Status.Pending;
        }
    }
}
