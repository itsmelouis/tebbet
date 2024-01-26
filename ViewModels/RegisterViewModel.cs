using Avalonia.Controls;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Tebbet.ViewModels
{
    public class RegisterViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private string _Error;
        public string Error
        {
            get => _Error;
            set 
            {
                if (_Error != value)
                {
                    _Error = value;
                    this.RaisePropertyChanged(nameof(Error));
                }
            }
        }
        public RegisterViewModel()
        {
            Error = "test";
        }
        public void IsBirthdateValid(string value)
        {
            int date = DateTime.Now.Year - 18;
            string year = date.ToString();
            char year_decimal = year[2];
            char year_unit = year[3];
            string regex = $@"^(0[1-9]|1[0-9]|2[0-9]|3[0-1])/(0[1-9]|1[0-2])/(19[0-9][0-9]|20[0-{year_decimal}][0-{year_unit}])$";

            if (!Regex.IsMatch(value, regex))
            {
                Error = "Format de date non valide. Format: jj/mm/aaaa";
            }
        }
    }
}
