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
    public class PediatriciansController : Controller
    {
        private readonly BabyNutriPlanContext Context;
        private readonly ILogger<AttendantsController> Logger;
        private readonly IConfiguration Configuration;

        public PediatriciansController(BabyNutriPlanContext context, ILogger<AttendantsController> logger, IConfiguration config)
        {
            Context = context;
            Logger = logger;
            Configuration = config;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pediatrician>>> Get(string filter = "", int? page = null, int? pageSize = null)
        {
            try
            {
                IQueryable<Pediatrician> query = Context.Pediatricians;

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
                Logger.LogError(ex, "Error al obtener los pediatras");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al obtener los pediatras");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Pediatrician>> Get(int id)
        {
            try
            {
                var pediatrician = await Context.Pediatricians.FindAsync(id);

                if (pediatrician == null)
                {
                    return NotFound();
                }

                return pediatrician;
            }
            catch (NpgsqlException ex)
            {
                Logger.LogError(ex, "Error al obtener el pediatra");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al obtener el pediatra");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Pediatrician>> Post(Pediatrician pediatrician)
        {
            try
            {
                if(Context.Pediatricians.Any(p => p.Id == pediatrician.Id))
                    return Conflict("Ya existe un pediatra registrado con la misma identificación.");

                Context.Pediatricians.Add(pediatrician);
                await Context.SaveChangesAsync();

                return Ok();
            }
            catch (NpgsqlException ex)
            {
                Logger.LogError(ex, "Error al crear el pediatra");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al crear el pediatra");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutPediatrician(int id, Pediatrician pediatrician)
        {
            if (id != pediatrician.RowId)
            {
                return BadRequest();
            }

            try
            {
                Context.Entry(pediatrician).State = EntityState.Modified;
                await Context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PediatricianExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            catch (NpgsqlException ex)
            {
                Logger.LogError(ex, "Error al actualizar el pediatra");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al actualizar el pediatra");
            }

            return NoContent();
        }

        private bool PediatricianExists(int id)
        {
            return Context.Pediatricians.Any(e => e.RowId == id);
        }
    }
}