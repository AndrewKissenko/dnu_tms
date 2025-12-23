using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tms_inc.Models
{
    public class OperationDetails
    {
        public int Id { get; set; }
        public int EntityId { get; set; }
        public string Message { get; set; }
        public Exception Exception { get; set; }
        public bool IsError { get; set; }
        public int StatusCode { get; set; }
        public string CreatedAt { get; set; }
    }
}
