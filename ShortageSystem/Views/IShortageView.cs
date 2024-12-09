using ShortageSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortageSystem.Views
{
    internal interface IShortageView
    {
        public int DisplayMainMenu();
        public void DisplayShortages(List<Shortage> shortages);
        public int DisplayDeleteShotage(List<Shortage> shortages);
        public Shortage DisplayCreateShortage();
        public int DisplayFilters();
        public Category ChooseCategory();
        public Room ChooseRoom();
        public int ChoosePriority();
        public DateInterval ChooseDateInterval();
        public string ChooseTitle();
        public string ChooseName();
        public string GetUserName();
    }
}
