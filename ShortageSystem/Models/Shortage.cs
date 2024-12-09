using ShortageSystem.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ShortageSystem.Models
{
    public class Shortage
    {
        public string Title { get; set; }
        public string Name { get; set; }
        public Category Category { get; set; }
        public Room Room { get; set; }
        [Range(1,10)]
        public int Priority { get; set; }
        public DateOnly CreatedOn { get; set; }
        public string CreatedBy {  get; set; }

        [JsonConstructor]
        public Shortage(string Title, string Name, Category Category, Room Room, int Priority, DateOnly CreatedOn, string CreatedBy)
        {
            this.Title = Title;
            this.Name = Name;
            this.Category = Category;
            this.Room = Room;
            this.Priority = Priority;
            this.CreatedOn = CreatedOn;
            this.CreatedBy = CreatedBy;
        }

        public Shortage() { }

        public override bool Equals(object? obj)
        {
            if (obj is not Shortage other) return false;

            return Title == other.Title &&
                   Name == other.Name &&
                   Category == other.Category &&
                   Room == other.Room &&
                   Priority == other.Priority &&
                   CreatedBy == other.CreatedBy &&
                   CreatedOn == other.CreatedOn;
        }
    }

    public enum Room
    {
        MeetingRoom,
        Kitchen,
        Bathroom
    }

    public enum Category
    {
        Electronics,
        Food,
        Other
    }
}
