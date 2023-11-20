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

namespace UserAdministrationService.Controllers
{
    [Route("[controller]")]
    public class UsersController : Controller
    {
        private readonly BabyNutriPlanContext Context;
        private readonly ILogger<UsersController> Logger;
        private readonly IConfiguration Configuration;

        public UsersController(BabyNutriPlanContext context, ILogger<UsersController> logger, IConfiguration config)
        {
            Context = context;
            Logger = logger;
            Configuration = config;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers(string filter = "", int? page = null, int? pageSize = null)
        {
            try
            {
                IQueryable<User> query = Context.Users;

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
                Logger.LogError(ex, "Error al obtener los usuarios");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al obtener los usuarios");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            try
            {
                var user = await Context.Users.FindAsync(id);

                if (user == null)
                {
                    return NotFound();
                }

                user.Password = "";

                return user;
            }
            catch (NpgsqlException ex)
            {
                Logger.LogError(ex, "Error al obtener el usuario");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al obtener el usuario");
            }
        }

        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            try
            {
                if (await Context.Users.AnyAsync(x => x.Email.ToLower() == user.Email.ToLower()))
                {
                    return Conflict("Ya existe un usuario con ese correo");
                }

                User userNew = new()
                {
                    Email = user.Email,
                    Password = Encrypt.EncryptPassword(Configuration.GetValue<string>("Parameters:DefaultPassword") ?? throw new Exception("The default password is not set")),
                    Status = EnumStatus.Inactive,
                    RowidRole = user.RowidRole
                };

                Context.Users.Add(userNew);
                await Context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetUser), new { id = user.RowId }, user);
            }
            catch (NpgsqlException ex)
            {
                Logger.LogError(ex, "Error al crear el usuario");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al crear el usuario");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            User? userToUpdate;

            try
            {
                userToUpdate = await Context.Users.FindAsync(id);

                if (userToUpdate == null || user.RowId != id)
                {
                    return NotFound();
                }

                userToUpdate.Email = user.Email;
                userToUpdate.Role = user.Role;

                if (userToUpdate.Role == null)
                {
                    userToUpdate.Status = EnumStatus.Inactive;
                }
                else
                {

                    switch (userToUpdate.Role.Name)
                    {
                        case "Administrator":
                            userToUpdate.Status = EnumStatus.Active;
                            break;
                        case "Pediatrician":
                            if (Context.Pediatricians.Any(x => x.RowidUser == userToUpdate.RowId))
                            {
                                userToUpdate.Status = EnumStatus.Active;
                            }
                            else
                            {
                                userToUpdate.Status = EnumStatus.Pending;
                            }
                            break;
                        case "Attendant":
                            if (Context.Attendants.Any(x => x.RowidUser == userToUpdate.RowId))
                            {
                                userToUpdate.Status = EnumStatus.Active;
                            }
                            else
                            {
                                userToUpdate.Status = EnumStatus.Pending;
                            }
                            break;
                    }
                }

                if (!string.IsNullOrEmpty(user.Password))
                {
                    userToUpdate.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
                }

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
        public async Task<IActionResult> DeleteUser(int id)
        {
            User? userToDelete;

            try
            {
                userToDelete = await Context.Users.FindAsync(id);

                if (userToDelete == null)
                {
                    return NotFound();
                }

                userToDelete.Status = EnumStatus.Deleted;
                await Context.SaveChangesAsync();

                return Ok();
            }
            catch (NpgsqlException ex)
            {
                Logger.LogError(ex, "Error al eliminar el usuario");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al eliminar el usuario");
            }
        }


        [HttpPost("changePassword")]
        public async Task<IActionResult> ChangePassword(User user, string newPassword)
        {
            User? userToUpdate;

            try
            {
                userToUpdate = await Context.Users.FindAsync(user.RowId);

                if (userToUpdate == null)
                {
                    return NotFound();
                }

                if (userToUpdate.Status == EnumStatus.Inactive || userToUpdate.Status == EnumStatus.Blocked || userToUpdate.Status == EnumStatus.Deleted)
                {
                    return Unauthorized();
                }
                else if (!BCrypt.Net.BCrypt.Verify(user.Password, userToUpdate.Password))
                {
                    return Unauthorized();
                }
                else
                {
                    userToUpdate.Password = BCrypt.Net.BCrypt.HashPassword(newPassword);
                    await Context.SaveChangesAsync();
                    return Ok();
                }
            }
            catch (NpgsqlException ex)
            {
                Logger.LogError(ex, "Error al cambiar la contraseña");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al cambiar la contraseña");
            }
        }

        [HttpPost("resetPassword")]
        public async Task<IActionResult> ResetPassword(User user)
        {
            User? userToUpdate;

            try
            {
                userToUpdate = await Context.Users.FindAsync(user.RowId);

                if (userToUpdate == null)
                {
                    return NotFound();
                }

                if (userToUpdate.Status == EnumStatus.Blocked || userToUpdate.Status == EnumStatus.Deleted)
                {
                    return Unauthorized();
                }
                else
                {
                    userToUpdate.Password = BCrypt.Net.BCrypt.HashPassword(Configuration.GetValue<string>("Parameters:DefaultPassword"));
                    userToUpdate.Attempts = 0;
                    userToUpdate.Status = EnumStatus.Active;
                    Context.Entry(userToUpdate).State = EntityState.Modified;
                    await Context.SaveChangesAsync();
                    return Ok();
                }
            }
            catch (NpgsqlException ex)
            {
                Logger.LogError(ex, "Error al reiniciar la contraseña");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al reiniciar la contraseña");
            }
        }

        [HttpPost("verifyEmail")]
        public async Task<IActionResult> VerifyEmail(string email)
        {
            try
            {
                var userFound = await Context.Users.FirstOrDefaultAsync(x => x.Email.ToLower() == email.ToLower());

                if (userFound == null)
                {
                    return Ok();
                }
                else
                {
                    return Conflict();
                }
            }
            catch (NpgsqlException ex)
            {
                Logger.LogError(ex, "Error al verificar el correo");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al verificar el correo");
            }
        }
    }
}