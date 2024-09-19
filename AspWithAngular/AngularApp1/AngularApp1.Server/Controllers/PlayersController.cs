using AngularApp1.Server.Data;
using AngularApp1.Server.Models;
using AngularApp1.Server.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AngularApp1.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayersController : ControllerBase
    {
        private readonly IPlayerService _playerService;

        public PlayersController(IPlayerService playerService)
        {
            _playerService = playerService;
        }

        // GET: api/Players
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Player>>> GetPlayers()
        {
            var players = await _playerService.GetPlayersAsync();
            return Ok(new { Message = "Data pemain berhasil diambil.", Data = players });
        }

        // GET: api/Players/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Player>> GetPlayer(int id)
        {
            var player = await _playerService.GetPlayerByIdAsync(id);
            if (player == null)
            {
                return NotFound(new { Message = "Pemain tidak ditemukan." });
            }
            return Ok(new { Message = "Detail pemain berhasil diambil.", Data = player });
        }

        // POST: api/Players
        [HttpPost]
        public async Task<ActionResult<Player>> CreatePlayer(Player player)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newPlayer = await _playerService.AddPlayerAsync(player);
            return CreatedAtAction(nameof(GetPlayer), new { id = newPlayer.Id }, new { Message = "Pemain berhasil ditambahkan.", Data = newPlayer });
        }

        // PUT: api/Players/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePlayer(int id, Player player)
        {
            if (id != player.Id)
            {
                return BadRequest(new { Message = "ID pemain tidak cocok." });
            }

            var success = await _playerService.UpdatePlayerAsync(player);
            if (!success)
            {
                return NotFound(new { Message = "Pemain tidak ditemukan." });
            }

            return Ok(new { Message = "Pemain berhasil diperbarui." });
        }

        // DELETE: api/Players/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlayer(int id)
        {
            var success = await _playerService.DeletePlayerAsync(id);
            if (!success)
            {
                return NotFound(new { Message = "Pemain tidak ditemukan." });
            }

            return Ok(new { Message = "Pemain berhasil dihapus." });
        }
    }
}
