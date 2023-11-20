using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using BabyNutriPlan.Shared.Bases;
using BabyNutriPlan.Shared.Enums;

namespace BabyNutriPlan.Shared.Entities
{
    [Table("INCIDENTS")]
    public class Incident : BaseAudit
    {
        [Required, Column("type", TypeName = "varchar(255)"), 
        Display(Name = "Tipo"), DataType(DataType.Text),
        StringLength(255, MinimumLength = 2, ErrorMessage = "El tipo debe tener entre {2} y {1} caracteres)")]
        public EnumIncidentType Type { get; set; }

        [Required, Column("description", TypeName = "varchar(500)"),
        Display(Name = "Descripción"), DataType(DataType.Text),
        StringLength(500, MinimumLength = 2, ErrorMessage = "La descripción debe tener entre {2} y {1} caracteres")]
        public required string Description { get; set; }

        [ForeignKey("Attendant")]
        [Column("rowid_attendant", TypeName = "int"), Display(Name = "Acudiente")]
        public required int RowidAttendant { get; set; }

        [ForeignKey("Patient")]
        [Column("rowid_patient", TypeName = "int"), Display(Name = "Paciente")]
        public required int RowidPatient { get; set; }

        [ForeignKey("PlanDay")]
        [Column("rowid_plan_day", TypeName = "int"), Display(Name = "Plan del día")]
        public required int RowidPlanDay { get; set; }

        public Attendant? Attendant { get; set; }
        public Patient? Patient { get; set; }
        public PlanDay? PlanDay { get; set; }
    }
}