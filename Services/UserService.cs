using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tebbet.Models;

namespace Tebbet.Services
{
    public class UserService
    {
        public static string Email { get; set; }
        public static string Lastname { get; set; }
        public static string Firstname { get; set; }
        public static DateTime Birthdate { get; set; }
        public static string Address { get; set; }
        public static string Postalcode { get; set; }
        public static string City { get; set; }
        public static string Password { get; set; }
        public static int? Credits { get; set; }
        public static string? Phone { get; set; }
        public static string? Role { get; set; }

        public static void SetUserInfo(List<Users> user)
        {
            Email = user[0].Email;
            Lastname = user[0].Lastname;
            Firstname = user[0].Firstname;
            Birthdate = user[0].Birthdate;
            Address = user[0].Address;
            Postalcode = user[0].Postalcode;
            City = user[0].City;
            Password = user[0].Password;
            Credits = user[0].Credits;
            Phone = user[0].Phone;
            Role = user[0].Role;
        }
    }
}
