using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiamiHeatRest.Models;

namespace MiamiHeatRest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamPlayersController : ControllerBase
    {
        private readonly ScoutingReportsContext _context;

        public TeamPlayersController(ScoutingReportsContext context)
        {
            _context = context;
        }

        // GET: api/TeamPlayers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TeamPlayer>>> GetTeamPlayers()
        {
            return await _context.TeamPlayers.ToListAsync();
        }

        // GET: api/TeamPlayers/20/16/2022
        [HttpGet("{playerId}/{teamId}/{seasonId}")]
        public async Task<ActionResult<TeamPlayer>> GetTeamPlayer(int playerId, int teamId, int seasonId)
        {
            var teamPlayer = await _context.TeamPlayers.FindAsync(playerId, teamId, seasonId);

            if (teamPlayer == null)
            {
                return NotFound();
            }

            return teamPlayer;
        }

        // PUT: api/TeamPlayers/20/16/2022
        [HttpPut("{playerId}/{teamId}/{seasonId}")]
        public async Task<IActionResult> PutTeamPlayer(int playerId, int teamId, int seasonId, TeamPlayer teamPlayer)
        {
            if (playerId != teamPlayer.PlayerKey || teamId != teamPlayer.TeamKey || seasonId != teamPlayer.SeasonKey)
            {
                return BadRequest();
            }

            _context.Entry(teamPlayer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TeamPlayerExists(playerId, teamId, seasonId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/TeamPlayers
        [HttpPost]
        public async Task<ActionResult<TeamPlayer>> PostTeamPlayer(TeamPlayer teamPlayer)
        {
            _context.TeamPlayers.Add(teamPlayer);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TeamPlayerExists(teamPlayer.PlayerKey, teamPlayer.TeamKey, teamPlayer.SeasonKey))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetTeamPlayer", new { playerId = teamPlayer.PlayerKey, teamId = teamPlayer.TeamKey, seasonId = teamPlayer.SeasonKey }, teamPlayer);
        }

        // DELETE: api/TeamPlayers/20/16/2022
        [HttpDelete("{playerId}/{teamId}/{seasonId}")]
        public async Task<ActionResult<TeamPlayer>> DeleteTeamPlayer(int playerId, int teamId, int seasonId)
        {
            var teamPlayer = await _context.TeamPlayers.FindAsync(playerId, teamId, seasonId);
            if (teamPlayer == null)
            {
                return NotFound();
            }

            _context.TeamPlayers.Remove(teamPlayer);
            await _context.SaveChangesAsync();

            return teamPlayer;
        }

        private bool TeamPlayerExists(int playerId, int teamId, int seasonId)
        {
            return _context.TeamPlayers.Any(e => e.PlayerKey == playerId && e.TeamKey == teamId && e.SeasonKey == seasonId);
        }
    }
}
