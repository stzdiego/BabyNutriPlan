using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BabyNutriPlan.Shared.Class
{
    public class AuthenticationRequest
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}