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


        public void SetUserInfo(Users user)
        {
            if (user != null)
            {
                Email = user.Email;
                Lastname = user.Lastname;
                Firstname = user.Firstname;
                Birthdate = user.Birthdate;
                Address = user.Address;
                Postalcode = user.Postalcode;
                City = user.City;
                Password = user.Password;
                Credits = user.Credits;
                Phone = user.Phone;
                Role = user.Role;
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
