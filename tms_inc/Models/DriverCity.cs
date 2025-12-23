using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tms.Models
{
    public class DriverCity
    {
        public int Id { get; set; }
        public int DriverId { get; set; }
        public Driver Driver { get; set; }
        public int? CityId { get; set; }
        public City City { get; set; }
        public DateTime Date { get; set; }
        public bool AtHome { get; set; }
        public string Comment { get; set; }
    }
}
