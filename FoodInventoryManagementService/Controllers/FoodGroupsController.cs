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

namespace FoodInventoryManagementService.Controllers
{
    [Route("[controller]")]
    public class FoodGroupsController : Controller
    {
        private readonly BabyNutriPlanContext Context;
        private readonly ILogger<FoodGroupsController> Logger;
        private readonly IConfiguration Configuration;

        public FoodGroupsController(BabyNutriPlanContext context, ILogger<FoodGroupsController> logger, IConfiguration config)
        {
            Context = context;
            Logger = logger;
            Configuration = config;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FoodGroup>>> GetFoodGroups(string filter = "", int? page = null, int? pageSize = null)
        {
            try
            {
                IQueryable<FoodGroup> query = Context.FoodGroups.AsQueryable();

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
        public async Task<ActionResult<FoodGroup>> GetFoodGroup(int id)
        {
            try
            {
                var foodGroup = await Context.FoodGroups.FindAsync(id);

                if (foodGroup == null)
                {
                    return NotFound();
                }

                return foodGroup;
            }
            catch (NpgsqlException ex)
            {
                Logger.LogError(ex, "Error al obtener el usuario");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al obtener el usuario");
            }
        }
    
        [HttpPost]
        public async Task<ActionResult<FoodGroup>> PostFoodGroup(FoodGroup foodGroup)
        {
            try
            {
                Context.FoodGroups.Add(foodGroup);
                await Context.SaveChangesAsync();

                return CreatedAtAction("GetFoodGroup", new { id = foodGroup.RowId }, foodGroup);
            }
            catch (NpgsqlException ex)
            {
                Logger.LogError(ex, "Error al crear el usuario");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al crear el usuario");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutFoodGroup(int id, FoodGroup foodGroup)
        {
            if (id != foodGroup.RowId)
            {
                return BadRequest();
            }

            Context.Entry(foodGroup).State = EntityState.Modified;

            try
            {
                await Context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!FoodGroupExists(id))
                {
                    return NotFound();
                }
                else
                {
                    Logger.LogError(ex, "Error al actualizar el usuario");
                    return StatusCode(StatusCodes.Status500InternalServerError, "Error al actualizar el usuario");
                }
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFoodGroup(int id)
        {
            try
            {
                var foodGroup = await Context.FoodGroups.FindAsync(id);
                if (foodGroup == null)
                {
                    return NotFound();
                }

                Context.FoodGroups.Remove(foodGroup);
                await Context.SaveChangesAsync();

                return NoContent();
            }
            catch (NpgsqlException ex)
            {
                Logger.LogError(ex, "Error al eliminar el usuario");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al eliminar el usuario");
            }
        }

        private bool FoodGroupExists(int id)
        {
            return Context.FoodGroups.Any(e => e.RowId == id);
        }
    }
}