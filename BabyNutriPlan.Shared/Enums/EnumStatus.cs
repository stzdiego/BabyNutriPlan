using System.ComponentModel.DataAnnotations;

namespace BabyNutriPlan.Shared.Enums
{
    public enum EnumStatus
    {
        [Display(Name = "Activo"), DisplayFormat(DataFormatString = "Activo")]
        Active,
        [Display(Name = "Inactivo"), DisplayFormat(DataFormatString = "Inactivo")]
        Inactive,
        [Display(Name = "Eliminado"), DisplayFormat(DataFormatString = "Eliminado")]
        Deleted,
        [Display(Name = "Bloqueado"), DisplayFormat(DataFormatString = "Bloqueado")]
        Blocked,
        [Display(Name = "Pendiente"), DisplayFormat(DataFormatString = "Pendiente")]
        Pending
    }
}