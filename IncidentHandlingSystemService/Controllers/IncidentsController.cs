using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BabyNutriPlan.Shared.Data;
using BabyNutriPlan.Shared.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq.Dynamic.Core;
using Npgsql;
using Microsoft.EntityFrameworkCore;
using BabyNutriPlan.Shared.Enums;

namespace IncidentHandlingSystem.Controllers
{
    [Route("[controller]")]
    public class IncidentsController : Controller
    {
        private readonly BabyNutriPlanContext Context;
        private readonly ILogger<IncidentsController> Logger;
        private readonly IConfiguration Configuration;

        public IncidentsController(BabyNutriPlanContext context, ILogger<IncidentsController> logger, IConfiguration config)
        {
            Context = context;
            Logger = logger;
            Configuration = config;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Incident>>> GetIncidents(string filter = "", int? page = null, int? pageSize = null)
        {
            try
            {
                IQueryable<Incident> query = Context.Incidents
                .Include(x => x.Attendant)
                .Include(x => x.Patient)
                .Include(x => x.PlanDay)
                .AsQueryable();

                if (!string.IsNullOrEmpty(filter))
                {
                    query = query.Where(filter);
                }

                if (page.HasValue && pageSize.HasValue)
                {
                    query = query.Skip(page.Value * pageSize.Value).Take(pageSize.Value);
                }

                return await query.ToListAsync();
            }
            catch (NpgsqlException ex)
            {
                Logger.LogError(ex, "Error to get");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Incident>> GetIncident(int id)
        {
            try
            {
                var incident = await Context.Incidents.FindAsync(id);

                if (incident == null)
                {
                    return NotFound();
                }

                return incident;
            }
            catch (NpgsqlException ex)
            {
                Logger.LogError(ex, "Error to get");
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Incident>> PostIncident(Incident incident)
        {
            try
            {
                Context.Incidents.Add(incident);
                await Context.SaveChangesAsync();

                return CreatedAtAction("GetIncident", new { id = incident.RowId }, incident);
            }
            catch (NpgsqlException ex)
            {
                Logger.LogError(ex, "Error to post");
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutIncident(int id, Incident incident)
        {
            if (id != incident.RowId)
            {
                return BadRequest();
            }

            try
            {
                Context.Entry(incident).State = EntityState.Modified;
                await Context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!IncidentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    Logger.LogError(ex, "Error to put");
                    return BadRequest(ex.Message);
                }
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIncident(int id)
        {
            try
            {
                var incident = await Context.Incidents.FindAsync(id);
                if (incident == null)
                {
                    return NotFound();
                }

                Context.Incidents.Remove(incident);
                await Context.SaveChangesAsync();

                return NoContent();
            }
            catch (NpgsqlException ex)
            {
                Logger.LogError(ex, "Error to delete");
                return BadRequest(ex.Message);
            }
        }

        private bool IncidentExists(int id)
        {
            return Context.Incidents.Any(e => e.RowId == id);
        }
    }
}