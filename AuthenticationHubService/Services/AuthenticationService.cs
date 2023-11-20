using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BabyNutriPlan.Shared.Class;
using BabyNutriPlan.Shared.Data;
using BabyNutriPlan.Shared.Entities;
using BabyNutriPlan.Shared.Services;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text.Json;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationHubService.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IConfiguration Configuration;
        private readonly BabyNutriPlanContext Context;

        public AuthenticationService(IConfiguration configuration, BabyNutriPlanContext context)
        {
            Context = context;
            Configuration = configuration;
        }

        public async Task<AuthenticationResponse> Auth(AuthenticationRequest request)
        {
            AuthenticationResponse response;
            User? user;

            try
            {
                response = new AuthenticationResponse{ Email = request.Email, Token = "" };
                user = await Context.Users.Include(x => x.Role).SingleOrDefaultAsync(item => item.Email.ToLower() == request.Email.ToLower());

                if(user == null)
                {
                    return response;
                }
                
                if(Encrypt.ValidatePassword(request.Password, user.Password ?? ""))
                {
                    response.Token = GetToken(user);
                }

                return response;
            }
            catch (Exception ex)
            {
                throw new Exception("Error autentificando.", ex);
            }

        }

        public IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        {
            var claims = new List<Claim>();
            var payload = jwt.Split('.')[1];
            var jsonBytes = ParseBase64WithoutPadding(payload);
            var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);

            keyValuePairs.TryGetValue(ClaimTypes.Role, out object roles);

            if (roles != null)
            {
                if (roles.ToString().Trim().StartsWith("["))
                {
                    var parsedRoles = JsonSerializer.Deserialize<string[]>(roles.ToString());

                    foreach (var parsedRole in parsedRoles)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, parsedRole));
                    }
                }
                else
                {
                    claims.Add(new Claim(ClaimTypes.Role, roles.ToString()));
                }

                keyValuePairs.Remove(ClaimTypes.Role);
            }

            claims.AddRange(keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString())));

            return claims;

        }

        private string GetToken(User usuario)
        {
            JwtSecurityToken token;
            byte[] llave;
            DateTime dtExpiracion;
            SigningCredentials credentials;

            llave = Encoding.UTF8.GetBytes(Configuration.GetValue<string>("Authentication:Secret") ?? "");

            dtExpiracion = DateTime.UtcNow.AddDays(1);

            credentials = new SigningCredentials(
                new SymmetricSecurityKey(llave), SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.NameId, usuario.RowidRole.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, usuario.Email.ToString()),
                new Claim(ClaimTypes.Role, usuario.Role.Name.ToString()),
                new Claim("rol", usuario.Role.Name.ToString())
            };

            token = new JwtSecurityToken(claims: claims,
                                         expires: dtExpiracion,
                                         signingCredentials: credentials,
                                         issuer: Configuration.GetValue<string>("Authentication:Issuer") ?? "",
                                         audience: Configuration.GetValue<string>("Authentication:Audience") ?? "");

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private byte[] ParseBase64WithoutPadding(string base64)
        {
            switch (base64.Length % 4)
            {
                case 2: base64 += "=="; break;
                case 3: base64 += "="; break;
            }
            return Convert.FromBase64String(base64);
        }

    }
}