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

namespace UserAdministrationService.Controllers
{
    [Route("[controller]")]
    public class RolesController : Controller
    {
        private readonly BabyNutriPlanContext Context;
        private readonly ILogger<UsersController> Logger;
        private readonly IConfiguration Configuration;

        public RolesController(BabyNutriPlanContext context, ILogger<UsersController> logger, IConfiguration config)
        {
            Context = context;
            Logger = logger;
            Configuration = config;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Role>>> GetRoles(string filter = "", int? page = null, int? pageSize = null)
        {
            try
            {
                IQueryable<Role> query = Context.Roles.AsQueryable();

                if (!string.IsNullOrEmpty(filter))
                {
                    query = query.Where(filter);
                }

                if (page.HasValue && pageSize.HasValue)
                {
                    query = query.Skip(page.Value * pageSize.Value).Take(pageSize.Value);
                }

                return await query.Select(x => new Role
                {
                    Rowid = x.Rowid,
                    Name = x.Name,
                    Description = x.Description,
                }).ToListAsync();
            }
            catch (NpgsqlException ex)
            {
                Logger.LogError(ex, "Error al obtener los usuarios");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al obtener los usuarios");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Role>> GetRole(int id)
        {
            try
            {
                var role = await Context.Roles.FindAsync(id);

                if (role == null)
                {
                    return NotFound();
                }

                return role;
            }
            catch (NpgsqlException ex)
            {
                Logger.LogError(ex, "Error al obtener el usuario");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al obtener el usuario");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Role>> PostRole(Role role)
        {
            try
            {
                if (await Context.Roles.AnyAsync(x => x.Name.ToLower() == role.Name.ToLower()))
                {
                    return Conflict("The role already exists");
                }

                Context.Roles.Add(role);
                await Context.SaveChangesAsync();

                return CreatedAtAction(nameof(PostRole), role.Rowid, role);
            }
            catch (NpgsqlException ex)
            {
                Logger.LogError(ex, "Error to create the user");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error to create the user");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutRole(int id, Role role)
        {
            Role? roleToUpdate;

            try
            {
                roleToUpdate = await Context.Roles.FindAsync(id);

                if (roleToUpdate == null || role.Rowid != id)
                {
                    return NotFound();
                }

                Context.Entry(roleToUpdate).State = EntityState.Detached;
                Context.Roles.Update(role);
                await Context.SaveChangesAsync();
                return Ok();
            }
            catch (NpgsqlException ex)
            {
                Logger.LogError(ex, "Error al actualizar el usuario");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al actualizar el usuario");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRol(int id)
        {
            Role? roleToDelete;

            try
            {
                roleToDelete = await Context.Roles.FindAsync(id);

                if (roleToDelete == null)
                {
                    return NotFound();
                }

                Context.Roles.Remove(roleToDelete);
                await Context.SaveChangesAsync();

                return Ok();
            }
            catch (NpgsqlException ex)
            {
                Logger.LogError(ex, "Error to delete");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error to delete");
            }
        }


    }
}