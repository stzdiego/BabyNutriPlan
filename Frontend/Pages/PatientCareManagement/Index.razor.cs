using BabyNutriPlan.Shared.Entities;
using Microsoft.AspNetCore.Components;

namespace Frontend.Pages.PatientCareManagement
{
    public partial class Index
    {
        [Inject]
        private IConfiguration? Configuration { get; set; }
        [Inject]
        private NavigationManager? NavigationManager { get; set; }
        [Inject]
        private HttpClient? Http { get; set; }

        private IEnumerable<Patient>? _patients;
        private IEnumerable<Attendant>? _attendants;

        private async Task LoadPatients()
        {
            if(Http is null || Configuration is null || NavigationManager is null)
            {
                return;
            }

            _patients = await Http.GetFromJsonAsync<IEnumerable<Patient>>(Configuration["Services:PatientCareManagementService"] + "/patients");
        }

        private async Task LoadAttendants()
        {
            if(Http is null || Configuration is null || NavigationManager is null)
            {
                return;
            }

            _attendants = await Http.GetFromJsonAsync<IEnumerable<Attendant>>(Configuration["Services:PatientCareManagementService"] + "/attendants");
        }
    }
}