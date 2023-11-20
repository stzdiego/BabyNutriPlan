using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BabyNutriPlan.Shared.Entities;

namespace BabyNutriPlan.Shared.Dtos
{
    public class DtoHistory
    {
        public required Patient Patient { get; set; }
        public required Pediatrician Pediatrician { get; set; }
        public required IEnumerable<Attendant?> Attendants { get; set; }
        public required IEnumerable<Food?> Foods { get; set; }
        public required IEnumerable<Incident> Incidents { get; set; }

    }
}