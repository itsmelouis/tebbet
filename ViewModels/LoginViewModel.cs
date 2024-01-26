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
        public void Login(string mail, string password)
        {
            AuthenticationServices.Authenticate(mail, password);
        }

    }
}
