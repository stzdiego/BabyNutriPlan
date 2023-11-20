using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BabyNutriPlan.Shared.Services
{
    public static class Encrypt
    {
        public static string EncryptPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public static bool ValidatePassword(string password, string hash)
        {
            return BCrypt.Net.BCrypt.Verify(password, hash);
        }
    }
}