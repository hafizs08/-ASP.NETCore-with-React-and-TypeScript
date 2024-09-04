using AngularApp1.Server.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AngularApp1.Server.Services
{
    public interface IPlayerService
    {
        Task<IEnumerable<Player>> GetPlayersAsync();
        Task<Player> GetPlayerByIdAsync(int id);
        Task<Player> AddPlayerAsync(Player player);
        Task<Player> UpdatePlayerAsync(Player player);
        Task DeletePlayerAsync(int id);
    }
}
