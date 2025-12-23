using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tms_inc.Models
{
    public class EntityBase
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsProcessed { get; set; }
    }
}
