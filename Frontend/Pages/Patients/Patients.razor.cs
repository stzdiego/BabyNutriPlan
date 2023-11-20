using BabyNutriPlan.Shared.Entities;
using Radzen;

namespace Frontend.Pages.Patients
{
    public partial class Patients
    {
        private List<Patient> _patients { get; set; } = new List<Patient>();
        private List<Patient>? _selectedPatients { get; set; }
        private Patient? _patient { get; set; }
        private bool _patiendIsLoading { get; set; }
        private int _patientsCount { get; set; }
        private int _incidentsToday { get; set; }

        private async Task OnClickAddPatient()
        {
            await DialogService.OpenAsync<NewPatient>("New Patient", new Dictionary<string, object?>(),
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