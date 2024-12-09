using ShortageSystem.Filters;
using ShortageSystem.Identity;
using ShortageSystem.Models;
using ShortageSystem.Repositories;
using ShortageSystem.Views;

namespace ShortageSystem.Controller
{
    internal class ShortageController
    {
        IShortageRepository _repo;
        IShortageView _view;
        ShortageFilters _filters;

        public ShortageController(IShortageRepository repo, IShortageView view)
        {
            _repo = repo;
            _view = view;
            _filters = new ShortageFilters();
        }

        public async Task MainMenu()
        {
            Console.WriteLine();
            Console.WriteLine($"Logged in as: {ApplicationUser.UserName}");
            switch(_view.DisplayMainMenu())
            {
                case 1: await GetAllShortages();
                    break;
                case 2: await AddShortage();
                    break;
                case 3: await RemoveShortage();
                    break;
                case 4: await Filter();
                    break;
                default: _view.DisplayMainMenu(); 
                    break;

            }
        }

        public void GetUserName()
        {
            ApplicationUser.UserName = _view.GetUserName();
        }

        public async Task Filter()
        {
            List<Shortage> list = new List<Shortage>();
            list = await _repo.GetAllShortagesAsync();
            switch(_view.DisplayFilters())
            {
                case 1: _view.DisplayShortages(_filters.FilterByTitle(list, _view.ChooseTitle()));
                    break;
                case 2: _view.DisplayShortages(_filters.FilterByCreatedOn(list, _view.ChooseDateInterval()));
                    break;
                case 3: _view.DisplayShortages(_filters.FilterByCategory(list, _view.ChooseCategory()));
                    break;
                case 4: _view.DisplayShortages(_filters.FilterByRoom(list, _view.ChooseRoom()));
                    break;
                default:
                    Console.WriteLine("Invalid selection. Please try again.");
                    break;
            }
            await MainMenu();
        }

        public async Task AddShortage()
        {
            Shortage shortage = _view.DisplayCreateShortage();
            await _repo.AddShortageAsync(shortage);
            await MainMenu();
        }

        public async Task RemoveShortage()
        {

            var shortages = await _repo.GetAllShortagesAsync();
            var index = _view.DisplayDeleteShotage(shortages) - 1;
            if (index < 0 || index >= shortages.Count)
            {
                Console.WriteLine("Invalid index.");
                return;
            }
            await _repo.DeleteShortageAsync(shortages.ElementAt(index));
            await MainMenu();
        }

        public async Task GetAllShortages()
        {
            var shortages = await _repo.GetShortagesByUserAsync(ApplicationUser.UserName);
            if (shortages != null)
                _view.DisplayShortages(shortages);
            await MainMenu();
        }

    }
}
