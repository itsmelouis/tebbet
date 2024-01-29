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
        private bool _ModalAddCircuit;
        public ReactiveCommand<int, Unit> DeleteCommand { get; }

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
        public bool ModalAddCircuit
        {
            get => _ModalAddCircuit;
            set
            {
                if (_ModalAddCircuit != value)
                {
                    _ModalAddCircuit = value;
                    this.RaisePropertyChanged(nameof(ModalAddCircuit));
                }
            }
        }
        public AdminViewModel() 
        {
            this.GetCircuits();
            DeleteCommand = ReactiveCommand.Create<int>(DeleteCircuit);
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

        public void AddCircuit(string name, string filePath, string place, string city, string country)
        {
            using(var context = new DatabaseConnection())
            {
                Circuits circuits = new()
                {
                    Name = name,
                    Image = File.ReadAllBytes(filePath),
                    Place = place,
                    City = city,
                    Country = country,
                };
                context.Circuits.Add(circuits);
                context.SaveChanges();
                this.GetCircuits();
                ModalAddCircuit = false;
            }
        }
    }
}
