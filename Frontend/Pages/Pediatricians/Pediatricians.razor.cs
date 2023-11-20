using BabyNutriPlan.Shared.Entities;
using Radzen;

namespace Frontend.Pages.Pediatricians
{
    public partial class Pediatricians
    {
        private List<Pediatrician> _pediatricians { get; set; } = new List<Pediatrician>();
        private List<Pediatrician>? _selectedPediatricians { get; set; }
        private Pediatrician? _pediatrician { get; set; }
        private bool _pediatricianIsLoading { get; set; }
        private int _pediatriciansCount { get; set; }
        private int _incidentsToday { get; set; }

        private async Task OnClickAddPatient()
        {
            await DialogService.OpenAsync<NewPediatrician>("New Pediatrician", new Dictionary<string, object?>(),
            new DialogOptions()
            {
                Width = "80%",
                Height = "80%",
                ShowClose = true,
                CloseDialogOnEsc = true,
                AutoFocusFirstElement = true,
                Draggable = true
            });
        }

        private void OnSubmit(Patient model)
        {
        }

        private void OnInvalidSubmit(FormInvalidSubmitEventArgs args)
        {
        }
    }
}