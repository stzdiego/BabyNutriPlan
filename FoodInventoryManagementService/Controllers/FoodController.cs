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
    public class FoodController : Controller
    {
        private readonly BabyNutriPlanContext Context;
        private readonly ILogger<FoodController> Logger;
        private readonly IConfiguration Configuration;

        public FoodController(BabyNutriPlanContext context, ILogger<FoodController> logger, IConfiguration config)
        {
            Context = context;
            Logger = logger;
            Configuration = config;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Food>>> GetFood(string filter = "", int? page = null, int? pageSize = null)
        {
            try
            {
                IQueryable<Food> query = Context.Food.AsQueryable();

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
        public async Task<ActionResult<Food>> GetFood(int id)
        {
            try
            {
                var food = await Context.Food.FindAsync(id);

                if (food == null)
                {
                    return NotFound();
                }

                return food;
            }
            catch (NpgsqlException ex)
            {
                Logger.LogError(ex, "Erro to get");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error to get");
            }
        }
    
        [HttpPost]
        public async Task<ActionResult<Food>> PostFood(Food food)
        {
            try
            {
                Context.Food.Add(food);
                await Context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetFood), new { id = food.RowId }, food);
            }
            catch (NpgsqlException ex)
            {
                Logger.LogError(ex, "Error al crear el usuario");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al crear el usuario");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutFood(int id, Food food)
        {
            if (id != food.RowId)
            {
                return BadRequest();
            }

            Context.Entry(food).State = EntityState.Modified;

            try
            {
                await Context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!FoodExists(id))
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
        public async Task<IActionResult> DeleteFood(int id)
        {
            Food? foodToDelete;

            try
            {
                foodToDelete = await Context.Food.FindAsync(id);

                if (foodToDelete == null)
                {
                    return NotFound();
                }

                Context.Food.Remove(foodToDelete);
                await Context.SaveChangesAsync();

                return Ok();
            }
            catch (NpgsqlException ex)
            {
                Logger.LogError(ex, "Error al eliminar el usuario");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al eliminar el usuario");
            }
        }

        private bool FoodExists(int id)
        {
            return Context.Food.Any(e => e.RowId == id);
        }
    }
}