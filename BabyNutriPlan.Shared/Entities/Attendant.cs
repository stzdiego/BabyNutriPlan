using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using BabyNutriPlan.Shared.Bases;

namespace BabyNutriPlan.Shared.Entities
{
    [Table("ATTENDANTS")]
    public class Attendant : BasePerson
    {
        [Required, Column("children", TypeName = "smallint"), Display(Name = "Hijos")]
        public short Children { get; set; }

        [Required, Column("rowid_user", TypeName = "integer"), Display(Name = "Usuario")]
        [ForeignKey("User")]
        public int RowidUser { get; set; }

        //Relaciones
        public User? User { get; set; }

        public ICollection<Patient>? Patients { get; set; }
    }
}