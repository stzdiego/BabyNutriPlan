using System.ComponentModel.DataAnnotations;

namespace BabyNutriPlan.Shared.Enums
{
    public enum EnumIncidentType
    {
        [Display(Name = "Alergia")]
        Allegry = 0,
        [Display(Name = "Intolerancia")]
        Intolerance = 1
    }
}