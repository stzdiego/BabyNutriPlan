using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BabyNutriPlan.Shared.Entities;

[Table("COUNTRIES")]
public class Country 
{
    [Key]
    [Column("rowid", TypeName = "int"), Display(Name = "Id")]
    public required int Rowid { get; set; }
    [Required, Column("name", TypeName = "varchar(100)"), Display(Name = "Nombre")]
    public required string Name { get; set; }
    [Required, Column("code", TypeName = "varchar(10)"), Display(Name = "Código")]
    public required string Code { get; set; }
}