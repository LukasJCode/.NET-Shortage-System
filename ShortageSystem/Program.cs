using ShortageSystem.Controller;
using ShortageSystem.Models;
using ShortageSystem.Repositories;
using ShortageSystem.Views;

namespace ShortageSystem
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            IShortageRepository shortageRepository = new ShortageRepository();
            IShortageView view = new ShortageView();
            ShortageController controller = new ShortageController(shortageRepository, view);
            controller.GetUserName();
            await controller.MainMenu();
        }
    }
}
