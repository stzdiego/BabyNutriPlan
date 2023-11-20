using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BabyNutriPlan.Shared.Bases;

namespace BabyNutriPlan.Shared.Entities
{
    [Table("FOODS")] // Nombre de la tabla para la entidad Food
    public class Food : BaseAudit
    {
        [Required, Column("name", TypeName = "varchar(100)"),
        Display(Name = "Nombre"), DataType(DataType.Text),
        StringLength(100, MinimumLength = 2, ErrorMessage = "El nombre debe tener entre {2} y {1} caracteres")]
        public required string Name { get; set; }

        [Column("description", TypeName = "varchar(50)"),
        Display(Name = "Descripción"), DataType(DataType.Text),
        StringLength(50, MinimumLength = 2, ErrorMessage = "La descripción debe tener entre {2} y {1} caracteres")]
        public string? Icon { get; set; }

        [Required, Column("is_allergeneric", TypeName = "bool"),
        Display(Name = "¿Es alergénico?")]
        public required bool IsAllergeneric { get; set; }

        [Required, Column("rowid_food_group", TypeName = "int"), Display(Name = "Grupo de alimento")]
        [ForeignKey("FoodGroup")]
        public int RowidFoodGroup { get; set; }

        public FoodGroup? FoodGroup { get; set; }
    }
}
