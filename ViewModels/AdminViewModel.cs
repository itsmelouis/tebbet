using Avalonia.Controls;
using Avalonia.Platform.Storage;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Tebbet.Controls;
using Tebbet.Database;
using Tebbet.Models;

namespace Tebbet.ViewModels
{
    public class AdminViewModel : ViewModelBase, INotifyPropertyChanged
    {
        // Singleton
        private static AdminViewModel _instance;

        private AdminViewModel()
        {
            NavbarControl = new NavbarControl();
        }

        public static AdminViewModel GetInstance()
        {
            if (_instance == null)
            {
                _instance = new AdminViewModel();
            }

            return _instance;
        }
  
        private object _ContentControl;

        public object ContentControl
        {
            get => _ContentControl;
            set
            {
                if (_ContentControl != value)
                {
                    _ContentControl = value;
                    this.RaisePropertyChanged(nameof(ContentControl));
                }
            }
        }

        // route
        public override void ShowControl(Type controlType)
        {
            ContentControl = controlType.Name switch
            {
                nameof(CircuitControl) => new CircuitControl(),
                nameof(RacesControl) => new RacesControl(),
                _ => new CircuitControl()
            };
        }
    }
}
