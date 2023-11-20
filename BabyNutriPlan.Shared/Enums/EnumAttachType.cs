using System.ComponentModel.DataAnnotations;

namespace BabyNutriPlan.Shared.Enums
{
    public enum EnumAttachType
    {
        [Display(Name = "Imagen")]
        Image,
        [Display(Name = "Video")]
        Video,
        [Display(Name = "Audio")]
        Audio,
        [Display(Name = "Documento")]
        Document,
        [Display(Name = "Otro")]
        Other
    }
}