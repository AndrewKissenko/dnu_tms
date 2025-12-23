using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tms_inc.Models.RequestedQuote
{
    public class RequestedQuote : EntityBase
    {
        public string FreightType { get; set; }
        public string DepartureCity { get; set; }
        public string DeliveryCity { get; set; }
        public string TotalGrossWeight { get; set; }
        public string Dimention { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Message { get; set; }
    }
}
