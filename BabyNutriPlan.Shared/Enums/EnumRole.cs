using System.ComponentModel.DataAnnotations;

namespace BabyNutriPlan.Shared.Enums
{
    public enum EnumRole
    {
        [Display(Name = "Indefinido")]
        Undefined = 0,
        [Display(Name = "Administrador")]
        Administrator = 1,
        [Display(Name = "Pediatra")]
        Pediatrician = 2,
        [Display(Name = "Acudiente")]
        Attendant = 3
    }
}