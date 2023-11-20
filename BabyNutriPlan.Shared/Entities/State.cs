using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BabyNutriPlan.Shared.Entities
{
    [Table("STATES")]
    public class State
    {
        [Key]
        [Column("rowid", TypeName = "int"), Display(Name = "Id")]
        public required int Rowid { get; set; }
        [Required, Column("name", TypeName = "varchar(100)"), Display(Name = "Nombre")]
        public required string Name { get; set; }
        [Required, Column("code", TypeName = "varchar(10)"), Display(Name = "Código")]
        public required string Code { get; set; }

        [ForeignKey("Country")]
        [Required, Column("rowid_country", TypeName = "int"), Display(Name = "País")]
        public int RowidCountry { get; set; }

        public Country? Country { get; set; }
    }
}