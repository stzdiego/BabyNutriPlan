using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BabyNutriPlan.Shared.Entities
{
    [Table("CITIES")]
    [Index(nameof(Code), IsUnique = true)]
    public class City
    {
        [Key]
        [Column("rowid", TypeName = "int"), Display(Name = "Id")]
        public required int Rowid { get; set; }
        [Required, Column("name", TypeName = "varchar(100)"), Display(Name = "Nombre")]
        public required string Name { get; set; }
        [Required, Column("code", TypeName = "varchar(10)"), Display(Name = "Código")]
        public required string Code { get; set; }

        [ForeignKey("State")]
        [Required, Column("rowid_state", TypeName = "int"), Display(Name = "Estado")]
        public int RowidState { get; set; }

        public State? State { get; set; }
    }
}