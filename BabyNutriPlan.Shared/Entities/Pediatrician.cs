using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using BabyNutriPlan.Shared.Bases;

namespace BabyNutriPlan.Shared.Entities
{
    [Table("PEDIATRICIANS")]
    public class Pediatrician : BasePerson
    {
        [Required, Column("university", TypeName = "varchar(255)"),
        Display(Name = "Universidad"), DataType(DataType.Text),
        StringLength(255, MinimumLength = 2, ErrorMessage = "La universidad debe tener entre {2} y {1} caracteres")]
        public required string University { get; set; }

        [Required, Column("promotion_date", TypeName = "timestamp with time zone"), Display(Name = "Fecha de promoción")]
        public required DateTime PromotionDate { get; set; }

        [Required, Column("id_professional", TypeName = "int"), Range(10000000, 9999999999),
        Display(Name = "Identificación profesional")]
        public required int IdProfessional { get; set; }

        [Column("description", TypeName = "varchar(500)"), Display(Name = "Descripción"), DataType(DataType.Text),
        StringLength(500, MinimumLength = 2, ErrorMessage = "La descripción debe tener entre {2} y {1} caracteres")]
        public string? Description { get; set; }

        [Required, Column("rowid_user", TypeName = "int"), Display(Name = "Usuario")]
        [ForeignKey("User")]
        public int RowidUser { get; set; }

        [Column("rowid_attach_photo", TypeName = "int"), Display(Name = "Foto")]
        [ForeignKey("AttachPhoto")]
        public int? RowidAttachPhoto { get; set; }


        //Relaciones
        public User? User { get; set; }
        public Attach? AttachPhoto { get; set; }

        public ICollection<Patient>? Patients { get; set; }
    }
}