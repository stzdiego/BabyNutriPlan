using System.ComponentModel.DataAnnotations;

namespace BabyNutriPlan.Shared.Enums
{
    public enum EnumAttendantType
    {
        [Display(Name = "Familiar")]
        Family,
        [Display(Name = "Cuidador")]
        Carer
    }
}