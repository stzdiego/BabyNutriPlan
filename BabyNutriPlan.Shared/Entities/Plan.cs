using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using BabyNutriPlan.Shared.Bases;

namespace BabyNutriPlan.Shared.Entities
{
    [Table("PLANS")]
    public class Plan : BaseAudit
    {
        [Required, Column("initial_day", TypeName = "date"), Display(Name = "Día inicial")]
        public DateOnly InitialDay { get; set; }

        [Required, Column("rowid_patient", TypeName = "int"), Display(Name = "Paciente")]
        [ForeignKey("Patient")]
        public int RowidPatient { get; set; }

        public Patient? Patient { get; set; }
    }
}
