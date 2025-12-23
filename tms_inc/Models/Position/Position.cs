using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tms_inc.Models.Position
{
    public enum PositionType
    {
        Driver = 0,
        Office,
        Other
    }
    public class Position: EntityBase
    {
        public string PositionName { get; set; }
        public PositionType PositionType { get; set; }
        public string Location { get; set; }
        public DateTime EndDate { get; set; }
        public string JobSummary { get; set; }
        public string JobResponsibilitiesString { get; set; }
        public string JobResponsibilityIntro { get; set; }

    }
}
