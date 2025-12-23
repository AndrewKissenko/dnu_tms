using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace tms.Models
{
    public class City
    {
        public int Id { get; set; }
        [Required]
        public string CityName { get; set; }
        [Required]
        public string State { get; set; }
        public string AdditionalAddress { get; set; }
        public DateTime DateCreated { get; set; }
        public List<DriverCity> DriverCities { get; set; }
        public City()
        {
            DriverCities = new List<DriverCity>();
            DateCreated = DateTime.Now;
        }
    }
}
