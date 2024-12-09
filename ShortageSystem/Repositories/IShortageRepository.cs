using ShortageSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortageSystem.Repositories
{
    public interface IShortageRepository
    {
        Task AddShortageAsync(Shortage shortage);
        Task DeleteShortageAsync(Shortage shortage);
        Task<List<Shortage>> GetAllShortagesAsync();
        Task<List<Shortage>> GetShortagesByUserAsync(string userName);
    }
}
