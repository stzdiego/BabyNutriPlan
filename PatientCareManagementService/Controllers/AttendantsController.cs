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
using BabyNutriPlan.Shared.Services;

namespace PatientCareManagement.Controllers
{
    [Route("[controller]")]
    public class AttendantsController : Controller
    {
        private readonly BabyNutriPlanContext Context;
        private readonly ILogger<AttendantsController> Logger;
        private readonly IConfiguration Configuration;

        public AttendantsController(BabyNutriPlanContext context, ILogger<AttendantsController> logger, IConfiguration config)
        {
            Context = context;
            Logger = logger;
            Configuration = config;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Attendant>>> GetAttendants(string filter = "", int? page = null, int? pageSize = null)
        {
            try
            {
                IQueryable<Attendant> query = Context.Attendants;

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
                Logger.LogError(ex, "Error al obtener los acudientes");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al obtener los acudientes");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Attendant>> GetAttendant(int id)
        {
            try
            {
                var attendant = await Context.Attendants.FindAsync(id);

                if (attendant == null)
                {
                    return NotFound();
                }

                return attendant;
            }
            catch (NpgsqlException ex)
            {
                Logger.LogError(ex, "Error al obtener el acudiente");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al obtener el acudiente");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Attendant>> PostAttendant(Attendant attendant)
        {
            try
            {
                if(await Context.Attendants.AnyAsync(a => a.Id == attendant.Id))
                {
                    return Conflict("Ya existe un acudiente con la identificación ingresada.");
                }

                Context.Attendants.Add(attendant);
                await Context.SaveChangesAsync();

                return CreatedAtAction("GetAttendant", new { id = attendant.RowId }, attendant);
            }
            catch (NpgsqlException ex)
            {
                Logger.LogError(ex, "Error al crear el acudiente");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al crear el acudiente");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAttendant(int id, Attendant attendant)
        {
            if (id != attendant.RowId)
            {
                return BadRequest();
            }

            Context.Entry(attendant).State = EntityState.Modified;

            try
            {
                await Context.SaveChangesAsync();
            }
            catch (NpgsqlException ex)
            {
                if (!AttendantExists(id))
                {
                    return NotFound();
                }
                else
                {
                    Logger.LogError(ex, "Error al actualizar el acudiente");
                    return StatusCode(StatusCodes.Status500InternalServerError, "Error al actualizar el acudiente");
                }
            }

            return NoContent();
        }

        private bool AttendantExists(int id)
        {
            return Context.Attendants.Any(e => e.RowId == id);
        }
    }
}