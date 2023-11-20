using BabyNutriPlan.Shared.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<BabyNutriPlanContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"), 
    b => b.MigrationsAssembly("Migrations"));
});

var app = builder.Build();

//Se aplican migraciones a la base de datos configurada
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<BabyNutriPlanContext>();
    context.Database.Migrate();
}

app.Run();
