using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;
using Tebbet.Database;
using Tebbet.Models;

namespace Tebbet.ViewModels
{
    public class AdminRacesViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private ObservableCollection<Races> _Races;
        private bool _ModalRace;
        public ReactiveCommand<int, Unit> DeleteCommand { get; }
        public ReactiveCommand<int, Unit> UpdateCommand { get; }
        private int? _IdUpdateRace { get; set; }
        private string _ContentAddRace;
        private string _Title;
        private DateTimeOffset _Start;
        private DateTimeOffset? _End;
        private Circuits _Circuits;

        public string Title
        {
            get => _Title;
            set
            {
                if (_Title != value)
                {
                    _Title = value;
                    this.RaisePropertyChanged(nameof(Title));
                }
            }
        }

        public DateTimeOffset Start
        {
            get => _Start;
            set
            {
                if (_Start != value)
                {
                    _Start = value;
                    this.RaisePropertyChanged(nameof(Start));
                }
            }
        }

        public DateTimeOffset? End
        {
            get => _End;
            set
            {
                if (_End != value)
                {
                    _End = value;
                    this.RaisePropertyChanged(nameof(End));
                }
            }
        }

        public Circuits Circuits
        {
            get => _Circuits;
            set
            {
                if (_Circuits != value)
                {
                    _Circuits = value;
                    this.RaisePropertyChanged(nameof(Circuits));
                }
            }
        }

        public string ContentAddRace
        {
            get => _ContentAddRace;
            set
            {
                if (_ContentAddRace != value)
                {
                    _ContentAddRace = value;
                    this.RaisePropertyChanged(nameof(ContentAddRace));
                }
            }
        }

        public ObservableCollection<Races> Races
        {
            get => _Races;
            set
            {
                if (_Races != value)
                {
                    _Races = value;
                    this.RaisePropertyChanged(nameof(Races));
                }
            }
        }
        public bool ModalRace
        {
            get => _ModalRace;
            set
            {
                if (_ModalRace != value)
                {
                    _ModalRace = value;
                    this.RaisePropertyChanged(nameof(ModalRace));
                }
            }
        }

        public AdminRacesViewModel()
        {
            this.GetRaces();
            ContentAddRace = "Ajouter une course";
            DeleteCommand = ReactiveCommand.Create<int>(DeleteRace);
            UpdateCommand = ReactiveCommand.Create<int>(UpdateRace);
        }

        private void UpdateRace(int id)
        {
            ContentAddRace = "Modifier un circuit";
            _IdUpdateRace = id;
            using (var context = new DatabaseConnection())
            {
                Races race = context.Races.Single(c => c.id == id);
                Title = race.Title;
                Start = new DateTimeOffset(race.Start);
                End = new DateTimeOffset((DateTime)race.End);
            }
            ModalRace = true;
        }

        public void AddRace()
        {
            ContentAddRace = "Ajouter une course";
            Title = "";
            Start = new DateTimeOffset(DateTime.Now);
            End = new DateTimeOffset(DateTime.Now);
            ModalRace = true;
        }

        private List<Races> GetRaces()
        {
            using (var context = new DatabaseConnection())
            {
                var races = context.Races.ToList();
                Races = new ObservableCollection<Races>(races);
                return races;
            }
        }

        private void DeleteRace(int id)
        {
            using (var context = new DatabaseConnection())
            {
                var race = context.Races.Single(c => c.id == id);
                context.Remove(race);
                context.SaveChanges();
                this.GetRaces();
            }
        }

        public void SaveRace(string title, DateTime start, DateTime? end)
        {
            var circuits_array =  new AdminCircuitViewModel().GetCircuits().ToArray();
            Random random = new Random();
            var randomNext = random.Next(0, circuits_array.Length);
            var random_circuit = circuits_array[randomNext];
            if (_IdUpdateRace == null)
            {
                using (var context = new DatabaseConnection())
                {
                    Races races = new()
                    {
                        Title = title,
                        Start = start,
                        End = end,
                        CircuitId = random_circuit.id
                    };
                    context.Races.Add(races);
                    context.SaveChanges();
                    this.GetRaces();
                    ModalRace = false;
                }
            }
            else
            {
                using (var context = new DatabaseConnection())
                {
                    var race = context.Races.Single(c => c.id == _IdUpdateRace);

                    race.Title = title;
                    race.Start = start;
                    race.End = end;
                    context.Races.Update(race);
                    context.SaveChanges();
                    this.GetRaces();
                    ModalRace = false;
                    _IdUpdateRace = null;
                }
            }
        }
    }
}
