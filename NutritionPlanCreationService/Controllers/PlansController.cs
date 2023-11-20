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

namespace NutritionPlanCreationService.Controllers
{
    [Route("[controller]")]
    public class PlansController : Controller
    {
        private readonly BabyNutriPlanContext Context;
        private readonly ILogger<PlansController> Logger;
        private readonly IConfiguration Configuration;

        public PlansController(BabyNutriPlanContext context, ILogger<PlansController> logger, IConfiguration config)
        {
            Context = context;
            Logger = logger;
            Configuration = config;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Plan>>> GetPlans(string filter = "", int? page = null, int? pageSize = null)
        {
            try
            {
                IQueryable<Plan> query = Context.Plans.AsQueryable();

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
        public async Task<ActionResult<Plan>> GetPlan(int id)
        {
            try
            {
                var plan = await Context.Plans.FindAsync(id);

                if (plan == null)
                {
                    return NotFound();
                }

                return plan;
            }
            catch (NpgsqlException ex)
            {
                Logger.LogError(ex, "Error to get");
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Plan>> PostPlan([FromBody] Plan plan)
        {
            try
            {
                plan.CreationDate = DateTime.Now;
                plan.ModificationDate = DateTime.Now;
                Context.Plans.Add(plan);
                await Context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetPlan), new { id = plan.RowId }, plan);
            }
            catch (NpgsqlException ex)
            {
                Logger.LogError(ex, "Error to post");
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutPlan(int id, [FromBody] Plan plan)
        {
            if (id != plan.RowId)
            {
                return BadRequest();
            }

            try
            {
                plan.ModificationDate = DateTime.Now;
                Context.Entry(plan).State = EntityState.Modified;
                await Context.SaveChangesAsync();
            }
            catch (NpgsqlException ex)
            {
                Logger.LogError(ex, "Error to put");
                return BadRequest(ex.Message);
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlan(int id)
        {
            try
            {
                var plan = await Context.Plans.FindAsync(id);
                if (plan == null)
                {
                    return NotFound();
                }

                Context.Plans.Remove(plan);
                await Context.SaveChangesAsync();
            }
            catch (NpgsqlException ex)
            {
                Logger.LogError(ex, "Error to delete");
                return BadRequest(ex.Message);
            }

            return NoContent();
        }

        [HttpGet("GetPlanByPatientId/{patientId}")]
        public async Task<ActionResult<Plan>> GetPlanByPatientId(int patientId)
        {
            try
            {
                var plan = await Context.Plans.Where(x => x.RowidPatient == patientId).FirstOrDefaultAsync();

                if (plan == null)
                {
                    return NotFound();
                }

                return plan;
            }
            catch (NpgsqlException ex)
            {
                Logger.LogError(ex, "Error to get");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetPlanByPatientIdAndDate/{patientId}/{date}")]
        public async Task<ActionResult<Plan>> GetPlanByPatientIdAndDate(int patientId, DateTime date)
        {
            try
            {
                var plan = await Context.Plans.Where(x => x.RowidPatient == patientId && x.CreationDate.Date == date.Date).FirstOrDefaultAsync();

                if (plan == null)
                {
                    return NotFound();
                }

                return plan;
            }
            catch (NpgsqlException ex)
            {
                Logger.LogError(ex, "Error to get");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetPlanByPatientIdAndDateRange/{patientId}/{startDate}/{endDate}")]
        public async Task<ActionResult<Plan>> GetPlanByPatientIdAndDateRange(int patientId, DateTime startDate, DateTime endDate)
        {
            try
            {
                var plan = await Context.Plans.Where(x => x.RowidPatient == patientId && x.CreationDate.Date >= startDate.Date && x.CreationDate.Date <= endDate.Date).FirstOrDefaultAsync();

                if (plan == null)
                {
                    return NotFound();
                }

                return plan;
            }
            catch (NpgsqlException ex)
            {
                Logger.LogError(ex, "Error to get");
                return BadRequest(ex.Message);
            }
        }


    }
}