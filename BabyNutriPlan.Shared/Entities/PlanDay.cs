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
    [Table("PLAN_DAY")] // Nombre de la tabla para la entidad FoodPlan
    public class PlanDay : BaseAudit
    {
        [Required, Column("day", TypeName = "date"), Display(Name = "Día")]
        public required DateOnly Day { get; set; }

        [Required, Column("rowid_food", TypeName = "int"), Display(Name = "Comida")]
        [ForeignKey("Food")]
        public int RowidFood { get; set; }

        [Required, Column("rowid_plan", TypeName = "int"), Display(Name = "Plan")]
        [ForeignKey("Plan")]
        public int RowidPlan { get; set; }

        public Food? Food { get; set; }
        public Plan? Plan { get; set; }
    }
}
