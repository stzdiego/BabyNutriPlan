using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BabyNutriPlan.Shared.Enums;
using BabyNutriPlan.Shared.Bases;

namespace BabyNutriPlan.Shared.Entities
{
    [Table("ATTACHS")]
    [Display(Name = "Adjuntos")]
    [Index(nameof(Path), IsUnique = true)]
    [Index(nameof(Name), IsUnique = false)]
    public class Attach : BaseAudit
    {
        [Column("name", TypeName = "varchar(255)"), MinLength(2), MaxLength(255), RegularExpression(@"^[a-zA-ZÀ-ÿ\s]+$"),
        Display(Name = "Nombre"), DataType(DataType.Text),
        StringLength(255, MinimumLength = 2, ErrorMessage = "El nombre debe tener entre {2} y {1} caracteres")]
        public string? Name { get; set; }

        [Required, Column("path", TypeName = "varchar(255)"), MinLength(2), MaxLength(255), RegularExpression(@"^[a-zA-ZÀ-ÿ\s]+$"),
        Display(Name = "Ruta"), DataType(DataType.Text),
        StringLength(255, MinimumLength = 2, ErrorMessage = "La ruta debe tener entre {2} y {1} caracteres")]
        public string? Path { get; set; }

        [Required, Column("type", TypeName = "varchar(50)"),
        Display(Name = "Tipo"), DataType(DataType.Text)]
        public EnumAttachType Type { get; set; }
    }
}