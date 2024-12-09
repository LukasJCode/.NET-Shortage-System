using ShortageSystem.Identity;
using ShortageSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortageSystem.Views
{
    internal class ShortageView:IShortageView
    {
        public Shortage DisplayCreateShortage()
        {
            Console.WriteLine();

            var title = ChooseTitle();

            var name = ChooseName();

            var category = ChooseCategory();

            Console.WriteLine();

            var room = ChooseRoom();

            var priority = ChoosePriority();

            var date = DateOnly.FromDateTime(DateTime.Now);

            var creator = ApplicationUser.UserName;
            Shortage shortage = new Shortage(title, name, category, room, priority, date, creator);

            return shortage;

        }

        public int DisplayDeleteShotage(List<Shortage> shortages)
        {
            Console.WriteLine();
            DisplayShortages(shortages);
            Console.WriteLine("Which Shortage to remove (specify number):");
            var index = Convert.ToInt32(Console.ReadLine());
            if (index < 0 && index > shortages.Count - 1)
            {
                Console.WriteLine("Please Enter Valid number");
                DisplayDeleteShotage(shortages);
            }
            return index;
        }

        public void DisplayShortages(List<Shortage> shortages)
        {
            if (shortages.Count > 0)
            {
                Console.WriteLine();
                for (int i = 0; i < shortages.Count; i++)
                {
                    Console.WriteLine($"{i + 1}");
                    Console.WriteLine("Shortage Title: " + shortages[i].Title);
                    Console.WriteLine("Shortage Name: " + shortages[i].Name);
                    Console.WriteLine("Shortage Category: " + shortages[i].Category);
                    Console.WriteLine("Shortage Room: " + shortages[i].Room);
                    Console.WriteLine("Shortage Priority: " + shortages[i].Priority);
                    Console.WriteLine("Shortage CreatedOn: " + shortages[i].CreatedOn);
                    Console.WriteLine("Shortage Created by: " + shortages[i].CreatedBy);
                }
            }
            else
                Console.WriteLine("No Shortages found");

        }

        public int DisplayMainMenu()
        {
            Console.WriteLine("To list all shortages press 1");
            Console.WriteLine("To add a shortage press 2");
            Console.WriteLine("To delete a shortage press 3");
            Console.WriteLine("To filter shortages press 4");
            var selection = Convert.ToInt32(Console.ReadLine());
            return selection;
        }

        public int DisplayFilters()
        {
            Console.WriteLine();
            Console.WriteLine("To filter by title press 1");
            Console.WriteLine("To filter by date press 2");
            Console.WriteLine("To filter by category press 3");
            Console.WriteLine("To filter by room press 4");
            var selection = Convert.ToInt32(Console.ReadLine());
            return selection;
        }

        public Category ChooseCategory()
        {
            Console.WriteLine("Shortage Categories: ");
            var categories = Enum.GetValues(typeof(Category));
            for (int i = 0; i < categories.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {(Category)i}");
            }
            Console.WriteLine("Select category number:");
            var category = (Category)(Convert.ToInt32(Console.ReadLine()) - 1);
            if (!Enum.IsDefined(typeof(Category), category))
            {
                Console.WriteLine("Please select valid category");
                category = ChooseCategory();
            }
            return category;
        }

        public Room ChooseRoom()
        {
            Console.WriteLine("Shortage Rooms: ");
            var rooms = Enum.GetValues(typeof(Room));
            for (int i = 0; i < rooms.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {(Room)i}");
            }
            Console.WriteLine("Select room number:");
            var room = (Room)(Convert.ToInt32(Console.ReadLine()) - 1);
            if (!Enum.IsDefined(typeof(Room), room))
            {
                Console.WriteLine("Please select valid room");
                room = ChooseRoom();
            }
            return room;
        }

        public DateInterval ChooseDateInterval()
        {
            DateInterval dateInterval = new DateInterval();
            Console.WriteLine("Choose start date (yyyy/mm/dd):");
            dateInterval.StartDate = ChooseDate();

            Console.WriteLine("Choose end date (yyyy/mm/dd):");
            dateInterval.EndDate = ChooseDate();

            if (dateInterval.StartDate > dateInterval.EndDate)
            {
                Console.WriteLine("Start date cant be more recent than end date");
                ChooseDateInterval();
            }

            return dateInterval;
        }

        public string ChooseTitle()
        {
            Console.WriteLine("Shortage Title: ");
            string title = Console.ReadLine();
            return title;
        }

        public string ChooseName()
        {
            Console.WriteLine("Shortage Name: ");
            string name = Console.ReadLine();
            return name;
        }

        public int ChoosePriority()
        {

            Console.WriteLine("Shortage Priority: ");
            var priority = Convert.ToInt32(Console.ReadLine());
            if(priority <= 0 || priority > 10)
            {
                Console.WriteLine("Choose valid priority (1-10)");
                priority = ChoosePriority();
            }
            return priority;
        }

        private DateOnly ChooseDate()
        {
            Console.WriteLine("Select year (yyyy):");
            var yyyy = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Select month (mm):");
            var mm = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Select day (dd):");
            var dd = Convert.ToInt32(Console.ReadLine());

            return new DateOnly(yyyy, mm, dd);
        }

        public string GetUserName()
        {
            Console.WriteLine("Who is using the application?: ");
            string userName = Console.ReadLine();
            return userName.ToLower();
        }
    }
}
