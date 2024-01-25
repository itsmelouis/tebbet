using System.Collections.Generic;
using System.Linq;
using Tebbet.Database;
using Tebbet.Models;

namespace Tebbet.Services
{
    public class AuthenticationServices
    {
        public List<Users> Authenticate(string mail, string password)
        {
            using (var context = new DatabaseConnection())
            {
                var User = context.Users
                    .Where(p => p.Email.Equals(mail))
                    .Where(p => p.Password.Equals(password))
                    .ToList();

                IsAuthenticated(User);

                return User;
            }
        }

        public bool IsAuthenticated(List<Users> user)
        {

            if (user.Count == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
