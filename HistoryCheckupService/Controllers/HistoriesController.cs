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
using BabyNutriPlan.Shared.Dtos;

namespace HistoryCheckupService.Controllers
{
    [Route("[controller]")]
    public class HistoriesController : Controller
    {
        private readonly BabyNutriPlanContext Context;
        private readonly ILogger<HistoriesController> Logger;
        private readonly IConfiguration Configuration;

        public HistoriesController(BabyNutriPlanContext context, ILogger<HistoriesController> logger, IConfiguration config)
        {
            Context = context;
            Logger = logger;
            Configuration = config;
        }

        [HttpPost]
        public async Task<ActionResult<DtoHistory>> GetHistory(Patient patient)
        {
            DtoHistory history;
            IEnumerable<Food?> foods;
            Plan plan;

            try
            {
                plan = await Context.Plans.SingleAsync(x => x.RowidPatient == patient.RowId);
                foods = await Context.PlanDays.Include(x => x.Food).Where(x => x.RowidPlan == plan.RowId).Select(x => x.Food).ToListAsync();

                history = new DtoHistory
                {
                    Patient = await Context.Patients.SingleAsync(x => x.RowId == patient.RowId),
                    Pediatrician = await Context.Pediatricians.SingleAsync(x => x.RowId == patient.RowidPediatrician),
                    Attendants = await Context.AttendantPatients.Include(x => x.Attendant).Where(x => x.RowidPatient == patient.RowId).Select(x => x.Attendant).ToListAsync(),
                    Foods = foods,
                    Incidents = await Context.Incidents.Where(x => x.RowidPatient == patient.RowId).ToListAsync()
                };

                return Ok(history);
            }
            catch (NpgsqlException ex)
            {
                Logger.LogError(ex, "Error to get");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error to get");
                return BadRequest(ex.Message);
            }
        }
    }
}