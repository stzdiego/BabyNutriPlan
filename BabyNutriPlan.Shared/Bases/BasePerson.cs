using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using BabyNutriPlan.Shared.Enums;
using Microsoft.EntityFrameworkCore;

namespace BabyNutriPlan.Shared.Bases
{
    [Index(nameof(Id), nameof(IdentificationType), IsUnique = true)]
    public class BasePerson : BaseAudit
    {
        [Required, Column("id_type", TypeName = "varchar(20)"), Display(Name = "Tipo de identificación")]
        public EnumIdentificationType IdentificationType { get; set; }

        [Required, Column("id", TypeName = "int"), Range(10000000, 9999999999),
        Display(Name = "Identificación")]
        public int Id { get; set; }

        [Required, Column("name", TypeName = "varchar(50)"), MinLength(2), MaxLength(50), RegularExpression(@"^[a-zA-ZÀ-ÿ\s]+$"), 
        Display(Name = "Nombre"), DataType(DataType.Text), 
        StringLength(50, MinimumLength = 2, ErrorMessage = "El nombre debe tener entre {2} y {1} caracteres")]
        public string Name { get; set; } = null!;

        [Required, Column("last_name", TypeName = "varchar(50)"), MinLength(2), MaxLength(50), RegularExpression(@"^[a-zA-ZÀ-ÿ\s]+$"),
        Display(Name = "Apellido"), DataType(DataType.Text),
        StringLength(50, MinimumLength = 2, ErrorMessage = "El apellido debe tener entre {2} y {1} caracteres")]
        public string LastName { get; set; } = null!;

        [Required, Column("birth_date", TypeName = "timestamp with time zone"), Display(Name = "Fecha de nacimiento")]
        public DateTime BirthDate { get; set; }

        [Column("tel", TypeName = "varchar(50)"), Display(Name = "Teléfono"), DataType(DataType.PhoneNumber)]
        public string? Tel { get; set; }

        [Required, Column("cel", TypeName = "varchar(50)"), Display(Name = "Celular"), DataType(DataType.PhoneNumber)]
        public string Cel { get; set; } = null!;

        [Column("occupation", TypeName = "varchar(255)"), Display(Name = "Ocupación"), DataType(DataType.Text), 
        StringLength(255, MinimumLength = 2, ErrorMessage = "La ocupación debe tener entre {2} y {1} caracteres")]
        public string? Occupation { get; set; }

        [Required, Column("address", TypeName = "varchar(255)"), Display(Name = "Dirección"), DataType(DataType.Text),
        StringLength(255, MinimumLength = 2, ErrorMessage = "La dirección debe tener entre {2} y {1} caracteres")]
        public string Address { get; set; } = null!;

        [Required, Column("country", TypeName = "varchar(255)"), Display(Name = "País"), DataType(DataType.Text),
        StringLength(255, MinimumLength = 2, ErrorMessage = "El país debe tener entre {2} y {1} caracteres")]
        public string Country { get; set; } = null!;

        [Required, Column("state", TypeName = "varchar(255)"), Display(Name = "Departamento"), DataType(DataType.Text),
        StringLength(255, MinimumLength = 2, ErrorMessage = "El departamento debe tener entre {2} y {1} caracteres")]
        public string State { get; set; } = null!;

        [Required, Column("city", TypeName = "varchar(255)"), Display(Name = "Ciudad"), DataType(DataType.Text),
        StringLength(255, MinimumLength = 2, ErrorMessage = "La ciudad debe tener entre {2} y {1} caracteres")]
        public string City { get; set; } = null!;

        [Column("neighborhood", TypeName = "varchar(255)"), Display(Name = "Barrio"), DataType(DataType.Text),
        StringLength(255, MinimumLength = 2, ErrorMessage = "El barrio debe tener entre {2} y {1} caracteres")]
        public string? Neighborhood { get; set; }

        [Required, Column("gender", TypeName = "varchar(20)"), Display(Name = "Género")]
        public EnumGender Gender { get; set; }

        //Propiedades no mapeadas
        [NotMapped]
        public string FullName => $"{Name} {LastName}";
        [NotMapped]
        public int Age => DateTime.UtcNow.Year - BirthDate.Year;
    }
}