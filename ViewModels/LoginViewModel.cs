using ReactiveUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tebbet.Database;
using Tebbet.Services;

namespace Tebbet.ViewModels
{
    public class LoginViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private AuthenticationServices? authenticationServices;
        public bool Login(string mail, string password)
        {
            authenticationServices = new AuthenticationServices();
            var Authenticate = authenticationServices.Authenticate(mail, password);

            if (Authenticate.Count == 1)
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
