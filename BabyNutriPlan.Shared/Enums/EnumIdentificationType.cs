using System.ComponentModel.DataAnnotations;

namespace BabyNutriPlan.Shared.Enums
{
    public enum EnumIdentificationType
    {
        [Display(Name = "Cédula de ciudadanía"), DisplayFormat(DataFormatString = "Cédula de ciudadanía")]
        CC = 0,
        [Display(Name = "Cédula de extranjería"), DisplayFormat(DataFormatString = "Cédula de extranjería")]
        CE = 1,
        [Display(Name = "Tarjeta de identidad"), DisplayFormat(DataFormatString = "Tarjeta de identidad")]
        TI = 2,
        [Display(Name = "Registro civil"), DisplayFormat(DataFormatString = "Registro civil")]
        RC = 3,
        [Display(Name = "Pasaporte"), DisplayFormat(DataFormatString = "Pasaporte")]
        PA = 4
    }
}