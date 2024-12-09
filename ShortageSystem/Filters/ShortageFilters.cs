using ShortageSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortageSystem.Filters
{
    public class ShortageFilters
    {
        public List<Shortage> FilterByTitle(List<Shortage> shortages, string input)
        {
            shortages = shortages.Where(s => s.Title.ToLower().Contains(input.ToLower())).OrderByDescending(s => s.Priority).ToList();
            return shortages;
        }

        public List<Shortage> FilterByCreatedOn(List<Shortage> shortages, DateInterval dateInterval)
        { 
            if(dateInterval.StartDate >= dateInterval.EndDate)
                return new List<Shortage>();
            shortages = shortages.Where(s => s.CreatedOn >= dateInterval.StartDate && s.CreatedOn <= dateInterval.EndDate).OrderByDescending(s => s.Priority).ToList();
            return shortages;
        }

        public List<Shortage> FilterByCategory(List<Shortage> shortages, Category category)
        {
            shortages = shortages.Where(s => s.Category == category).OrderByDescending(s => s.Priority).ToList();
            return shortages;
        }
        public List<Shortage> FilterByRoom(List<Shortage> shortages, Room room)
        {
            shortages = shortages.Where(s => s.Room == room).OrderByDescending(s => s.Priority).ToList();
            return shortages;
        }
    }
}
