using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using BabyNutriPlan.Shared.Bases;

namespace BabyNutriPlan.Shared.Entities
{
    [Table("PATIENTS")]
    public class Patient : BasePerson
    {
        [Required, Column("weight", TypeName = "double precision"), Display(Name = "Peso")]
        public required double Weight { get; set; }

        [Required, Column("height", TypeName = "double precision"), Display(Name = "Altura")]
        public required double Height { get; set; }

        [Required, Column("rowid_pediatrician"), Display(Name = "Pediatra")]
        [ForeignKey("Pediatrician")]
        public required int RowidPediatrician { get; set; }

        public Pediatrician? Pediatrician { get; set; }
    }
}