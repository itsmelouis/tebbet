using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tebbet.Models;

namespace Tebbet.Services
{
    public sealed class UserService
    {
        // Singleton
        private UserService() { }

        private static UserService _instance;
        public static UserService GetInstance()
        {
            if (_instance == null)
            {
                _instance = new UserService();
            }
            return _instance;
        }

        public string? Email { get; set; }
        public string? Lastname { get; set; }
        public string? Firstname { get; set; }
        public DateTime Birthdate { get; set; }
        public string? Address { get; set; }
        public string? Postalcode { get; set; }
        public string? City { get; set; }
        public string? Password { get; set; }
        private double? _Credits;
        public double? Credits 
        {
            get => _Credits;
            set
            {
                _Credits = value;
                OnCreditsEvent();
            }
        }
        public string? Phone { get; set; }
        public string? Role { get; set; }
        public bool IsAuthentified { get; set; }
        public bool IsAuthentifiedAsAdmin { get; set; }
        public bool IsAuthentifiedAsUser { get; set; }
        public event EventHandler AuthenticationSucceeded;
        public event EventHandler LogoutSucceeded;
        public event EventHandler CreditsEvent;


        public void SetUserInfo(List<Users> user)
        {
            if (user != null)
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
                IsAuthentified = true;

                if (Role == "admin")
                {
                    IsAuthentifiedAsAdmin = true;
                }
                else
                {
                    IsAuthentifiedAsUser = true;
                }

                OnAuthenticationSucceeded();
            }
        }

        public void Logout()
        {
            Email = null;
            Lastname = null;
            Firstname = null;
            Birthdate = DateTime.MinValue;
            Address = null;
            Postalcode = null;
            City = null;
            Password = null;
            Credits = null;
            Phone = null;
            Role = null;
            IsAuthentified = false;
            IsAuthentifiedAsUser = false;
            IsAuthentifiedAsAdmin = false;
            OnLogoutSucceeded();
        }

        private void OnLogoutSucceeded()
        {
            LogoutSucceeded?.Invoke(this, EventArgs.Empty);
        }

        private void OnAuthenticationSucceeded()
        {
            AuthenticationSucceeded?.Invoke(this, EventArgs.Empty);
        }

        private void OnCreditsEvent()
        {
            CreditsEvent?.Invoke(this, EventArgs.Empty);
        }
    }
}
