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
using Tebbet.Database;
using Tebbet.Models;

namespace Tebbet.ViewModels
{
    public class AdminViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private ObservableCollection<Circuits> _Circuits;
        private bool _ModalCircuit;
        public ReactiveCommand<int, Unit> DeleteCommand { get; }
        public ReactiveCommand<int, Unit> UpdateCommand {  get; }
        private int? _IdUpdateCircuit { get; set; }
        private string _ContentAddCircuit;
        private string _Name;
        private string _Place;
        private string _City;
        private string _Country;
        public string? FilePath { get; set; }

        public string Place
        {
            get => _Place;
            set
            {
                if (_Place != value)
                {
                    _Place = value;
                    this.RaisePropertyChanged(nameof(Place));
                }
            }
        }

        public string Name
        {
            get => _Name;
            set
            {
                if (_Name != value)
                {
                    _Name = value;
                    this.RaisePropertyChanged(nameof(Name));
                }
            }
        }

        public string City
        {
            get => _City;
            set
            {
                if (_City != value)
                {
                    _City = value;
                    this.RaisePropertyChanged(nameof(City));
                }
            }
        }

        public string Country
        {
            get => _Country;
            set
            {
                if (_Country != value)
                {
                    _Country = value;
                    this.RaisePropertyChanged(nameof(Country));
                }
            }
        }

        public string ContentAddCircuit
        {
            get => _ContentAddCircuit;
            set
            {
                if (_ContentAddCircuit != value)
                {
                    _ContentAddCircuit = value;
                    this.RaisePropertyChanged(nameof(ContentAddCircuit));
                }
            }
        }

        public ObservableCollection<Circuits> Circuits { 
            get => _Circuits;
            set { 
                if (_Circuits != value) 
                {
                    _Circuits = value;
                    this.RaisePropertyChanged(nameof(Circuits));
                } 
            }
        }
        public bool ModalCircuit
        {
            get => _ModalCircuit;
            set
            {
                if (_ModalCircuit != value)
                {
                    _ModalCircuit = value;
                    this.RaisePropertyChanged(nameof(ModalCircuit));
                }
            }
        }
        public AdminViewModel() 
        {
            this.GetCircuits();
            ContentAddCircuit = "Ajouter un circuit";
            DeleteCommand = ReactiveCommand.Create<int>(DeleteCircuit);
            UpdateCommand = ReactiveCommand.Create<int>(UpdateCircuit);
        }

        private void UpdateCircuit(int id)
        {
            ContentAddCircuit = "Modifier un circuit";
            _IdUpdateCircuit = id;
            using(var context = new DatabaseConnection())
            {
                Circuits circuit = context.Circuits.Single(c => c.id == id);
                Name = circuit.Name;
                Place = circuit.Place;
                City = circuit.City;
                Country = circuit.Country;
            }
            ModalCircuit = true;
        }

        public void AddCircuit()
        {
            ContentAddCircuit = "Ajouter un circuit";
            Name = "";
            City = "";
            Country = "";
            Place = "";
            ModalCircuit = true;
        }

        private void DeleteCircuit(int id)
        {
            using(var context = new DatabaseConnection())
            {
                var circuit = context.Circuits.Single(c => c.id == id);
                context.Remove(circuit);
                context.SaveChanges();
                this.GetCircuits();
            }
        }

        private void GetCircuits()
        {
            using (var context = new DatabaseConnection())
            {
                var circuits = context.Circuits.ToList();
                Circuits = new ObservableCollection<Circuits>(circuits);
            }
        }

        public void SaveCircuit(string name, string place, string city, string country)
        {
            if (_IdUpdateCircuit == null && FilePath != null)
            {
                using (var context = new DatabaseConnection())
                {
                    Circuits circuits = new()
                    {
                        Name = name,
                        Image = File.ReadAllBytes(FilePath),
                        Place = place,
                        City = city,
                        Country = country,
                    };
                    context.Circuits.Add(circuits);
                    context.SaveChanges();
                    this.GetCircuits();
                    ModalCircuit = false;
                    FilePath = null;
                }                
            } 
            else
            {
                using (var context = new DatabaseConnection())
                {
                    var circuit = context.Circuits.Single(c => c.id == _IdUpdateCircuit);

                    circuit.Name = name;
                    if (FilePath != null)
                    {
                        circuit.Image = File.ReadAllBytes(FilePath);
                    }
                    circuit.Place = place;
                    circuit.City = city;
                    circuit.Country = country;
                    context.Circuits.Update(circuit);
                    context.SaveChanges();
                    this.GetCircuits();
                    ModalCircuit = false;
                    _IdUpdateCircuit = null;
                }
            }
        }
    }
}
