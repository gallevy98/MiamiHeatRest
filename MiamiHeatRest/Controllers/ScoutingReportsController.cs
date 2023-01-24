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
    public class ScoutingReportsController : ControllerBase
    {
        private readonly ScoutingReportsContext _context;

        public ScoutingReportsController(ScoutingReportsContext context)
        {
            _context = context;
        }

        // GET: api/ScoutingReports
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ScoutingReport>>> GetScoutingReports()
        {
            return await _context.ScoutingReports.ToListAsync();
        }

        // GET: api/ScoutingReports/20/16/10
        [HttpGet("{playerId}/{teamId}/{scoutId}")]
        public async Task<ActionResult<ScoutingReport>> GetScoutingReport(int playerId, int teamId, int scoutId)
        {
            var scoutingReport = await _context.ScoutingReports.FindAsync(playerId, teamId, scoutId);

            if (scoutingReport == null)
            {
                return NotFound();
            }

            return scoutingReport;
        }

        // 3.b.i: Create an endpoint that updates the scouting report
        // PUT: api/ScoutingReports/20/16/10
        [HttpPut("{playerId}/{teamId}/{scoutId}")]
        public async Task<IActionResult> PutScoutingReport(int playerId, int teamId, int scoutId, ScoutingReport scoutingReport)
        {
            if (playerId != scoutingReport.PlayerKey || teamId != scoutingReport.TeamKey || scoutId != scoutingReport.ScoutId)
            {
                return BadRequest();
            }

            _context.Entry(scoutingReport).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ScoutingReportExists(playerId, teamId, scoutId))
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

        // 2.b.ii: Create an endpoint that creates new scouting reports
        // POST: api/ScoutingReports
        [HttpPost]
        public async Task<ActionResult<ScoutingReport>> PostScoutingReport(ScoutingReport scoutingReport)
        {
            _context.ScoutingReports.Add(scoutingReport);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ScoutingReportExists(scoutingReport.PlayerKey, scoutingReport.TeamKey, scoutingReport.ScoutId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetScoutingReport", new { playerId = scoutingReport.PlayerKey, teamId = scoutingReport.TeamKey, scoutId = scoutingReport.ScoutId }, scoutingReport);
        }

        // 3.b.ii: Create an endpoint that deletes the scouting report
        // DELETE: api/ScoutingReports/20/16/10
        [HttpDelete("{playerId}/{teamId}/{scoutId}")]
        public async Task<ActionResult<ScoutingReport>> DeleteScoutingReport(int playerId, int teamId, int scoutId)
        {
            var scoutingReport = await _context.ScoutingReports.FindAsync(playerId, teamId, scoutId);
            if (scoutingReport == null)
            {
                return NotFound();
            }

            _context.ScoutingReports.Remove(scoutingReport);
            await _context.SaveChangesAsync();

            return scoutingReport;
        }

        private bool ScoutingReportExists(int playerId, int teamId, int scoutId)
        {
            return _context.ScoutingReports.Any(e => e.PlayerKey == playerId && e.TeamKey == teamId && e.ScoutId == scoutId);
        }
    }
}
