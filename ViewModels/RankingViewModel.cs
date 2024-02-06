using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tebbet.Database;
using Tebbet.Models;

namespace Tebbet.ViewModels
{
    public class RankingViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private ObservableCollection<Snails> _Snails {  get; set; }
        public ObservableCollection<Snails> Snails
        {
            get => _Snails;
            set
            {
                if (_Snails != value)
                {
                    _Snails = value;
                    this.RaisePropertyChanged(nameof(Snails)); 
                }
            }
        }

        public RankingViewModel()
        {
            this.GetSnails();
        }

        private List<Snails> GetSnails()
        {
            using(var context = new DatabaseConnection())
            {
                var snails = context.Snails.OrderBy(x => x.general_rank).ToList();
                Snails = new ObservableCollection<Snails>(snails);
                return snails;
            }
        }
    }
}
