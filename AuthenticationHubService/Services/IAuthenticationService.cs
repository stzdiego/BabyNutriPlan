using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BabyNutriPlan.Shared.Class;

namespace AuthenticationHubService.Services
{
    public interface IAuthenticationService
    {
        Task<AuthenticationResponse> Auth(AuthenticationRequest model);
        IEnumerable<Claim> ParseClaimsFromJwt(string jwt);

    }
}