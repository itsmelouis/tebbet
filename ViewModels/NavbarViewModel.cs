using ReactiveUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tebbet.Controls;
using Tebbet.Services;

namespace Tebbet.ViewModels
{
    public class NavbarViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private double? _Credits;

        public double? Credits
        {
            get => _Credits;
            set
            {
                if (_Credits != value)
                {
                    _Credits = value;
                    this.RaisePropertyChanged(nameof(Credits));
                }
            }
        }

        public NavbarViewModel() 
        {
            Credits = UserService.Credits;
        }
    }
}
