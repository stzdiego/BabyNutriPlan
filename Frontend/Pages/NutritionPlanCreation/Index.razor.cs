using BabyNutriPlan.Shared.Entities;
using Microsoft.AspNetCore.Components;

namespace Frontend.Pages.NutritionPlanCreation
{
    public partial class Index
    {
        [Inject]
        private IConfiguration? Configuration { get; set; }
        [Inject]
        private NavigationManager? NavigationManager { get; set; }
        [Inject]
        private HttpClient? Http { get; set; }

        private IEnumerable<Plan>? _plans;

        private async Task LoadPlans()
        {
            if(Http is null || Configuration is null || NavigationManager is null)
            {
                return;
            }

            _plans = await Http.GetFromJsonAsync<IEnumerable<Plan>>(Configuration["Services:NutritionPlanCreationService"] + "/plans");
        }

    }
}