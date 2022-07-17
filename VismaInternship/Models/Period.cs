using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VismaInternship.Models
{
    /// <summary>
    /// Class for checking DateTime periods if they intersect with each other
    /// </summary>
    public class Period
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; } 
        public bool IntersectsWith(Period otherPeriod)
        {
            return !(this.Start > otherPeriod.End || this.End < otherPeriod.Start);
        }
    }
}
