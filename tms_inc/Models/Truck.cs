using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tms.Models
{
    public class Truck
    {
        public int Id { get; set; }
        public string TruckId { get; set; }
        public virtual Driver Driver { get; set; }
        public bool IsAvailable { get; set; }
        public DateTime DateCreated { get; set; }
        public Truck()
        {
            IsAvailable = true;
            DateCreated = DateTime.Now;
        }
    }
}
