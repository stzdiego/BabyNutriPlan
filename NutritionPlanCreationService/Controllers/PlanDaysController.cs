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
    public class PlanDaysController : Controller
    {
        private readonly BabyNutriPlanContext Context;
        private readonly ILogger<PlansController> Logger;
        private readonly IConfiguration Configuration;

        public PlanDaysController(BabyNutriPlanContext context, ILogger<PlansController> logger, IConfiguration config)
        {
            Context = context;
            Logger = logger;
            Configuration = config;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlanDay>>> GetPlanDays(string filter = "", int? page = null, int? pageSize = null)
        {
            try
            {
                IQueryable<PlanDay> query = Context.PlanDays.AsQueryable();

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
        public async Task<ActionResult<PlanDay>> GetPlanDay(int id)
        {
            try
            {
                var planDay = await Context.PlanDays.FindAsync(id);

                if (planDay == null)
                {
                    return NotFound();
                }

                return planDay;
            }
            catch (NpgsqlException ex)
            {
                Logger.LogError(ex, "Error to get");
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<PlanDay>> PostPlanDay(PlanDay planDay)
        {
            try
            {
                planDay.CreationDate = DateTime.Now;
                planDay.ModificationDate = DateTime.Now;

                Context.PlanDays.Add(planDay);
                await Context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetPlanDay), new { id = planDay.RowId }, planDay);
            }
            catch (NpgsqlException ex)
            {
                Logger.LogError(ex, "Error to post");
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutPlanDay(int id, PlanDay planDay)
        {
            try
            {
                if (id != planDay.RowId)
                {
                    return BadRequest();
                }

                planDay.ModificationDate = DateTime.Now;

                Context.Entry(planDay).State = EntityState.Modified;
                await Context.SaveChangesAsync();

                return NoContent();
            }
            catch (NpgsqlException ex)
            {
                Logger.LogError(ex, "Error to put");
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlanDay(int id)
        {
            try
            {
                var planDay = await Context.PlanDays.FindAsync(id);

                if (planDay == null)
                {
                    return NotFound();
                }

                Context.PlanDays.Remove(planDay);
                await Context.SaveChangesAsync();

                return NoContent();
            }
            catch (NpgsqlException ex)
            {
                Logger.LogError(ex, "Error to delete");
                return BadRequest(ex.Message);
            }
        }
    }
}