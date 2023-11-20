using Radzen;
using BabyNutriPlan.Shared.Entities;

namespace Frontend.Pages.Attendants
{
    public partial class Attendants
    {
        private List<Attendant> _attendants { get; set; } = new List<Attendant>();
        private List<Attendant>? _selectedAttendants { get; set; }
        private Attendant? _attendant { get; set; }
        private bool _attendantIsLoading { get; set; }
        private int _attendantsCount { get; set; }
        private int _incidentsToday { get; set; }

        private async Task OnClickAddPatient()
        {
            await DialogService.OpenAsync<NewAttendant>("New Attendant", new Dictionary<string, object?>(),
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
    }
}