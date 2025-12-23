using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tms.Models
{
    public class Trailer
    {
        public int Id { get; set; }
        public string TrailerNumber { get; set; }
        public virtual Driver Driver { get; set; }
        public bool IsAvailable { get; set; }
        public DateTime DateCreated { get; set; }
        public Trailer()
        {
            IsAvailable = true;
            DateCreated = DateTime.Now;
        }
    }
}
