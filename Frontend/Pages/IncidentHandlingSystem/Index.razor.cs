using BabyNutriPlan.Shared.Entities;
using Microsoft.AspNetCore.Components;

namespace Frontend.Pages.IncidentHandlingSystem
{
    public partial class Index
    {
        [Inject]
        private IConfiguration? Configuration { get; set; }
        [Inject]
        private NavigationManager? NavigationManager { get; set; }
        [Inject]
        private HttpClient? Http { get; set; }

        private IEnumerable<Incident>? _incidents;

        private async Task LoadIncidents()
        {
            if(Http is null || Configuration is null || NavigationManager is null)
            {
                return;
            }

            _incidents = await Http.GetFromJsonAsync<IEnumerable<Incident>>(Configuration["Services:IncidentHandlingSystemService"] + "/incidents");
        }

    }
}