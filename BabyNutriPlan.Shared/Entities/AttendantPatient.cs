using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BabyNutriPlan.Shared.Enums;

namespace BabyNutriPlan.Shared.Entities
{
    [Table("ATTENDANTS_PATIENTS")]
    [Index(nameof(RowidAttendant), nameof(RowidPatient), IsUnique = true)]
    public class AttendantPatient
    {
        [Key]
        [Column("rowid", TypeName = "integer")]
        public int Rowid { get; set; }

        [Column("attendant_type", TypeName = "varchar(20)"), Display(Name = "Tipo de acompañante")]
        public EnumAttendantType AttendantType { get; set; }

        [Column("rowid_attendant")]
        [ForeignKey("Attendant")]
        public int RowidAttendant { get; set; }
        [Column("rowid_patient")]
        [ForeignKey("Patient")]
        public int RowidPatient { get; set; }

        //Relationships]
        public Attendant? Attendant { get; set; }
        public Patient? Patient { get; set; }
    }
}