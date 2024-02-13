using BCrypt.Net;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Tebbet.Database;
using Tebbet.Models;

namespace Tebbet.Services
{
    public class AuthenticationServices
    {
        public static void Authenticate(string mail, string password)
        {
            using (var context = new DatabaseConnection())
            {
                var User = context.Users
                    .Where(p => p.Email.Equals(mail))
                    .ToList();

                if (User.Count > 0)
                {
                    if (BCrypt.Net.BCrypt.Verify(password, User[0].Password))
                    {
                        ProcessAuthentication(User);
                    }
                }
            }
        }

        private static void ProcessAuthentication(List<Users> user)
        {
            if (user.Count == 1)
            {
                var user_service = UserService.GetInstance();
                user_service.SetUserInfo(user);
                if (user_service.IsAuthentifiedAsUser)
                {
                    new BetServices();
                }
            }
        }
    }
}
