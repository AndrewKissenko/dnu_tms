using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace tms.Models
{
    public class User: IdentityUser
    {
       // public int Id { get; set; }
        [Required]
        public string Login { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public virtual List<Driver> Drivers { get; set; }
        
        public User()
        {
            Drivers = new List<Driver>();
        }
    }
}
