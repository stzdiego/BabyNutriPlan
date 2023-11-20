using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BabyNutriPlan.Shared.Class
{
    public class AuthenticationResponse
    {
        public required string Email { get; set; }
        public required string Token { get; set; }

    }
}