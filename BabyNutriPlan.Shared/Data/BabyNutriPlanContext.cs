using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BabyNutriPlan.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace BabyNutriPlan.Shared.Data
{
    public class BabyNutriPlanContext : DbContext
    {
        public BabyNutriPlanContext(DbContextOptions<BabyNutriPlanContext> options) : base(options)
        {
        }

        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Attach> Attaches { get; set; }
        public DbSet<Attendant> Attendants { get; set; }
        public DbSet<AttendantPatient> AttendantPatients { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Pediatrician> Pediatricians { get; set; }
        public DbSet<Incident> Incidents { get; set; }
        public DbSet<Plan> Plans { get; set; }
        public DbSet<PlanDay> PlanDays { get; set; }
        public DbSet<Food> Food { get; set; }
        public DbSet<FoodGroup> FoodGroups { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<City> Cities { get; set; }
    }
}