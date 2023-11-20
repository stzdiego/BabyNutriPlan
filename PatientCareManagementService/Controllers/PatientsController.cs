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
    public class PatientsController : Controller
    {
        private readonly BabyNutriPlanContext Context;
        private readonly ILogger<AttendantsController> Logger;
        private readonly IConfiguration Configuration;

        public PatientsController(BabyNutriPlanContext context, ILogger<AttendantsController> logger, IConfiguration config)
        {
            Context = context;
            Logger = logger;
            Configuration = config;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Patient>>> Get(string filter = "", int? page = null, int? pageSize = null)
        {
            try
            {
                IQueryable<Patient> query = Context.Patients;

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
                Logger.LogError(ex, "Error al obtener los pacientes");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al obtener los pacientes");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Patient>> Get(int id)
        {
            try
            {
                var patient = await Context.Patients.FindAsync(id);

                if (patient == null)
                {
                    return NotFound();
                }

                return patient;
            }
            catch (NpgsqlException ex)
            {
                Logger.LogError(ex, "Error al obtener el paciente");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al obtener el paciente");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Patient>> Post(Patient patient)
        {
            try
            {
                if(await Context.Patients.AnyAsync(x => x.Id == patient.Id))
                {
                    return Conflict("Ya existe un paciente con el número de identificación ingresado.");
                }

                Context.Patients.Add(patient);
                await Context.SaveChangesAsync();

                return Ok();
            }
            catch (NpgsqlException ex)
            {
                Logger.LogError(ex, "Error al crear el paciente");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al crear el paciente");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Patient patient)
        {
            if (id != patient.RowId)
            {
                return BadRequest();
            }

            Context.Entry(patient).State = EntityState.Modified;

            try
            {
                await Context.SaveChangesAsync();
            }
            catch (NpgsqlException ex)
            {
                if (!PatientExists(id))
                {
                    return NotFound();
                }
                else
                {
                    Logger.LogError(ex, "Error al actualizar el paciente");
                    return StatusCode(StatusCodes.Status500InternalServerError, "Error al actualizar el paciente");
                }
            }

            return NoContent();
        }

        [HttpPost("addAttendant")]
        public async Task<ActionResult<Patient>> AddAttendant(int rowidPatient, int rowidAttendant, int type)
        {
            try
            {
                var patient = await Context.Patients.FindAsync(rowidPatient);

                if (patient == null)
                {
                    return NotFound();
                }

                var attendant = await Context.Attendants.FindAsync(rowidAttendant);

                if (attendant == null)
                {
                    return NotFound();
                }

                if(await Context.AttendantPatients.AnyAsync(x => x.RowidAttendant == attendant.RowId && x.RowidPatient == patient.RowId))
                {
                    return BadRequest("El acudiente ya se encuentra asignado al paciente");
                }

                Context.AttendantPatients.Add(new AttendantPatient
                {
                    RowidAttendant = attendant.RowId,
                    RowidPatient = patient.RowId,
                    AttendantType = (EnumAttendantType)type
                });
                await Context.SaveChangesAsync();

                return Ok();
            }
            catch (NpgsqlException ex)
            {
                Logger.LogError(ex, "Error al agregar el acudiente");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al agregar el acudiente");
            }
        }

        [HttpDelete("delAttendant")]
        public async Task<ActionResult<Patient>> DeleteAttendant(int rowidPatient, int rowidAttendant)
        {
            try
            {
                var patient = await Context.Patients.FindAsync(rowidPatient);

                if (patient == null)
                {
                    return NotFound();
                }

                var attendant = await Context.Attendants.FindAsync(rowidAttendant);

                if (attendant == null)
                {
                    return NotFound();
                }

                var attendantPatient = await Context.AttendantPatients.FirstOrDefaultAsync(x => x.RowidAttendant == attendant.RowId && x.RowidPatient == patient.RowId);

                if (attendantPatient == null)
                {
                    return BadRequest("El acudiente no se encuentra asignado al paciente");
                }
                else
                {
                    Context.AttendantPatients.Remove(attendantPatient);
                    await Context.SaveChangesAsync();

                    return Ok();
                }
            }
            catch (NpgsqlException ex)
            {
                Logger.LogError(ex, "Error al agregar el acudiente");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al agregar el acudiente");
            }
        }

        private bool PatientExists(int id)
        {
            return Context.Patients.Any(e => e.RowId == id);
        }
    }
}