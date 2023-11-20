using BabyNutriPlan.Shared.Entities;
using BabyNutriPlan.Shared.Enums;
using Microsoft.JSInterop;
using Radzen;
using Radzen.Blazor;
using Radzen.Blazor.Rendering;

namespace Frontend.Pages.Pediatricians
{
    public partial class NewPediatrician
    {
        private Pediatrician? _pediatrician;
        private IEnumerable<EnumIdentificationType> _identificationsTypes;
        private IEnumerable<EnumGender> _genders;
        private string? _filePhoto;
        private string? _fileDocument;
        private long? _fileSizePhoto;
        private long? _fileSizeDocument;

        public NewPediatrician()
        {
            _identificationsTypes = GetIdentificationsTypes();
            _genders = GetGenders();
        }

        private IEnumerable<EnumIdentificationType> GetIdentificationsTypes()
        {
            return Enum.GetValues(typeof(EnumIdentificationType)).Cast<EnumIdentificationType>();
        }

        private IEnumerable<EnumGender> GetGenders()
        {
            return Enum.GetValues(typeof(EnumGender)).Cast<EnumGender>();
        }

        void OnSubmit(Pediatrician model)
        {
        }

        void OnInvalidSubmit(FormInvalidSubmitEventArgs args)
        {
        }

    }
}