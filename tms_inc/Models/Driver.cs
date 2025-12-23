using tms_inc.Models.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tms.Models
{
    public class Driver
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public bool IsAvailable { get; set; } = false;
        public int? TruckId  { get; set; }
        public virtual Truck Truck { get; set; }
        public int? TrailerId { get; set; }
        public virtual Trailer Trailer { get; set; }
        public string UserId { get; set; }
        public virtual User User { get; set; }
        public int? ApplicantId { get; set; }
        public Applicant Applicant { get; set; }
        public List<DriverCity> DriverCities { get; set; }
        public Driver()
        {
            DriverCities = new List<DriverCity>();
        }

    }
}
