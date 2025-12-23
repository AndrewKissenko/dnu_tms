using tms_inc.Models.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tms_inc.Models.File
{
    public enum FileType
    {
        LicenseFront = 1,
        LicenseBack,
        Signature,
        CV
    }
    public class PersonFile: EntityBase
    {
        public string Name { get; set; }
        public FileType FileType { get; set; }
        public string Extension { get; set; }
        public string Path { get; set; }
        public Applicant Person { get; set; }
        public int PersonId { get; set; }
        public string ContentType { get; set; }
        public bool IsUploaded { get; set; }
        public byte[] File { get; set; }
    }
}
