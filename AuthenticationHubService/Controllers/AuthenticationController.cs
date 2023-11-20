using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AuthenticationHubService.Services;
using BabyNutriPlan.Shared.Class;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AuthenticationHubService.Controllers
{
    [Route("[controller]")]
    public class AuthenticationController : Controller
    {
        private readonly ILogger<AuthenticationController> Logger;
        private readonly IAuthenticationService AuthenticationService;

        public AuthenticationController(ILogger<AuthenticationController> logger, IAuthenticationService autenticacion)
        {
            Logger = logger;
            AuthenticationService = autenticacion;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(AuthenticationRequest request)
        {
            AuthenticationResponse response;

            try
            {
                response = await AuthenticationService.Auth(request);

                if(response.Token == "")
                {
                    return BadRequest("User/Password incorrect.");
                }
                else
                {
                    return Ok(response);
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error autentificando.");
                return BadRequest("Error autentificando.");
            }
        }

    }
}