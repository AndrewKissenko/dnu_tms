using tms_inc.Models.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tms_inc.Models.ViewModels
{
    public class UnprocessedNumsViewModel
    {
        public int ApplicantsNum { get; set; }
        public int ContactMeRequestsNum { get; set; }
        public int QuoteRequestsNum { get; set; }
        public int Total { get; set; }
    }
}
