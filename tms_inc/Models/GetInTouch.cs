using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tms_inc.Models
{
    public class GetInTouch: EntityBase
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string Message { get; set; }

        public string Subject { get; set; }
    }
}
