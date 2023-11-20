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
    [Table("FOOD_GROUPS")] // Nombre de la tabla para la entidad FoodGroup
    public class FoodGroup : BaseAudit
    {
        [Required, Column("name", TypeName = "varchar(100)"),
        Display(Name = "Nombre"), DataType(DataType.Text),
        StringLength(100, MinimumLength = 2, ErrorMessage = "El nombre debe tener entre {2} y {1} caracteres")]
        public required string Name { get; set; }

        [Required, Column("color", TypeName = "varchar(50)"),
        Display(Name = "Color"), DataType(DataType.Text),
        StringLength(50, MinimumLength = 2, ErrorMessage = "El color debe tener entre {2} y {1} caracteres")]
        public required string Color { get; set; }
    }
}
