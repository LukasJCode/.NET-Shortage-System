using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortageSystem.Models
{
    public class DateInterval
    {
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }

        public DateInterval() { }
        public DateInterval(DateOnly startDate, DateOnly endDate)
        {
            this.StartDate = startDate;
            this.EndDate = endDate;
        }
    }
}
