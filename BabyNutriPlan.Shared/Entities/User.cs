using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BabyNutriPlan.Shared.Bases;
using BabyNutriPlan.Shared.Enums;

namespace BabyNutriPlan.Shared.Entities
{
    [Table("USERS")]
    [Index(nameof(Email), IsUnique = true)]
    public class User : BaseAudit
    {
        [Required, Column("email", TypeName = "varchar(255)"), MinLength(2), MaxLength(255), RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$"),
        Display(Name = "Correo"), DataType(DataType.EmailAddress),
        StringLength(255, MinimumLength = 2, ErrorMessage = "El correo debe tener entre {2} y {1} caracteres")]
        public required string Email { get; set; }

        [Column("password", TypeName = "varchar(255)"), MinLength(2), MaxLength(255), RegularExpression(@"^[a-zA-Z0-9_.+-]+$"),
        Display(Name = "Contraseña"), DataType(DataType.Password),
        StringLength(24, MinimumLength = 2, ErrorMessage = "La contraseña debe tener entre {2} y {1} caracteres")]
        public string? Password { get; set; }

        [Required, Column("attempts", TypeName = "int"), Display(Name = "Intentos")]
        public int Attempts { get; set; }

        [Required, Column("status", TypeName = "varchar(20)"), Display(Name = "Activo")]
        public EnumStatus Status {get; set; }
        
        [ForeignKey("Role")]
        [Required, Column("rowid_role", TypeName = "int"), Display(Name = "Rol")]
        public int? RowidRole { get; set; }

        public Role? Role { get; set; }
        
    }
}