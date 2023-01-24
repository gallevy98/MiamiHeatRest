using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiamiHeatRest.Models;
using System.Numerics;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace MiamiHeatRest.Controllers
{
    [Route("api")]
    [ApiController]
    public class CustomController : ControllerBase
    {
        private readonly ScoutingReportsContext _context;

        public CustomController(ScoutingReportsContext context)
        {
            _context = context;
        }

        // 3.a.ii: Create an endpoint that retrieves the list of teams filtered by league id
        // GET: api/TeamsByLeagueId/1
        [HttpGet("TeamsByLeagueId/{leagueId}")]
        public IEnumerable<Team> GetTeamsByLeagueId(int leagueId)
        {
            return _context.Teams.FromSqlRaw("SELECT * FROM Team WHERE LeagueKey={0}",leagueId).ToList();
        }

        // 2.a.i: Create an endpoint that allow to search active players by name in a given season
        // GET: api/ActivePlayersBySeason/2021
        [HttpGet("ActivePlayersBySeason/{season}")]
        public IActionResult GetActivePlayersBySeason(int season)
        {
            var query = from player in _context.Players
                        join teamPlayer in _context.TeamPlayers
                        on player.PlayerKey equals teamPlayer.PlayerKey
                        where teamPlayer.SeasonKey == season && teamPlayer.ActiveTeamFlg == true
                        orderby player.LastName
                        select new
                        {
                            firstName = player.FirstName,
                            lastName = player.LastName,
                            season = teamPlayer.SeasonKey,
                            active = teamPlayer.ActiveTeamFlg
                        };
            return Ok(query);
        }

        // 3.a.iii: Create an endpoint that retrieves the list of players filtered by team id and season
        // GET: api/PlayersByTeamIdAndSeason/1/2021
        [HttpGet("PlayersByTeamIdAndSeason/{teamId}/{season}")]
        public IActionResult GetPlayersByTeamIdAndSeason(int teamId, int season)
        {
            var query = from player in _context.Players
                        join teamPlayer in _context.TeamPlayers
                        on player.PlayerKey equals teamPlayer.PlayerKey
                        where teamPlayer.TeamKey == teamId && teamPlayer.SeasonKey == season
                        select player;
            return Ok(query);
        }

        // 3.c.i: Create an endpoint that retrieves the list of active scouts
        // GET: api/ActiveScouts/
        [HttpGet("ActiveScouts")]
        public IEnumerable<User> GetActiveScouts()
        {
            return _context.Users.FromSqlRaw("SELECT * FROM \"User\" WHERE ActiveFlag=1").ToList();
        }

        // 2.c.i: Create an endpoint that retrieves the reports filtered by scout id and grouped by team
        // GET: api/ScoutingReportsByScoutIdGroupedByTeam/10
        [HttpGet("ScoutingReportsByScoutIdGroupedByTeam/{scoutId}")]
        public IActionResult GetScoutingReportsByScoutIdGroupedByTeam(int scoutId)
        {
            //1st query joins reports with players and filters by scoutId
            var query = from scoutingReport in _context.ScoutingReports
                        join player in _context.Players
                        on scoutingReport.PlayerKey equals player.PlayerKey
                        where scoutingReport.ScoutId == scoutId
                        select new
                        {
                            teamId = scoutingReport.TeamKey,
                            playerId = player.PlayerKey,
                            firstName = player.FirstName,
                            lastName = player.LastName,
                            dob = player.BirthDate,
                            scoutingReport
                        };

            //2nd query separated group clause out to make it enumerable
            var query2 = from x in query.AsEnumerable()
                      group new { x.playerId, x.firstName, x.lastName, x.dob, x.scoutingReport } by x.teamId into g
                      select new
                      {
                          teamId = g.Key,
                          players = g.ToList()
                      };

            //3rd query grabs team name, nickname, and conference
            var query3 = from x in query2.AsEnumerable()
                         join team in _context.Teams
                         on x.teamId equals team.TeamKey
                         select new
                         {
                             x.teamId,
                             team.TeamName,
                             team.TeamNickname,
                             team.Conference,
                             x.players
                         };

            return Ok(query3);
        }
    }
}
