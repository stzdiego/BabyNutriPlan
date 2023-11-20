using BabyNutriPlan.Shared.Entities;
using Microsoft.AspNetCore.Components;

namespace Frontend.Pages.FoodInventoryManagement
{
    public partial class Index
    {
        [Inject]
        private IConfiguration? Configuration { get; set; }
        [Inject]
        private NavigationManager? NavigationManager { get; set; }
        [Inject]
        private HttpClient? Http { get; set; }

        private IEnumerable<FoodGroup>? _foodgroups;
        private IEnumerable<Food>? _food;

        private async Task LoadFoodGroups()
        {
            if(Http is null || Configuration is null || NavigationManager is null)
            {
                return;
            }

            _foodgroups = await Http.GetFromJsonAsync<IEnumerable<FoodGroup>>(Configuration["Services:FoodInventoryManagementService"] + "/foodgroups");
        }

        private async Task LoadFood()
        {
            if(Http is null || Configuration is null || NavigationManager is null)
            {
                return;
            }

            _food = await Http.GetFromJsonAsync<IEnumerable<Food>>(Configuration["Services:FoodInventoryManagementService"] + "/food");
        }
    }
}