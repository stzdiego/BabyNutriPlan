using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BabyNutriPlan.Shared.Entities
{
    [Table("ROLES")]
    public class Role
    {
        [Key]
        [Column("rowid", TypeName = "int"), Display(Name = "Id")]
        public int Rowid { get; set; }

        [Required, Column("name", TypeName = "varchar(100)"), Display(Name = "Nombre")]
        public string Name { get; set; } = null!;

        [Required, Column("description", TypeName = "varchar(255)"), Display(Name = "Descripción")]
        public string Description { get; set; } = null!;
    }
}