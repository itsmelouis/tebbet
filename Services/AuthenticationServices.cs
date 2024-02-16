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
                var user = context.Users
                    .FirstOrDefault(p => p.Email.Equals(mail));


                if (user != null)
                {
                    if (BCrypt.Net.BCrypt.Verify(password, user.Password))
                    {
                        ProcessAuthentication(user);
                    }
                }
            }
        }

        private static void ProcessAuthentication(Users user)
        {
            if (user != null)
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
