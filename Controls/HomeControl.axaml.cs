using Avalonia.Controls;
using Avalonia.Interactivity;
using System;
using System.Collections.Generic;
using System.Linq;
using Tebbet.Database;
using Tebbet.Models;
using Tebbet.Services;
using Tebbet.ViewModels;


namespace Tebbet.Controls
{
    public partial class HomeControl : UserControl
    {
        private Button? lastButton;
        private readonly List<Races> Races;
        public HomeControl()
        {
            InitializeComponent();
            using (var context = new DatabaseConnection())
            {
                Races = context.Races.ToList();
            }
                Loaded += HomeLoaded;
        }

        private void ButtonClick(object sender, RoutedEventArgs args)
        {
            if (sender is Button button)
            {
                if (button.Classes.Contains("TabComingRace"))
                {
                    if (button.Name is not null)
                    {
                        string[] splitName = button.Name.Split("_");
                        int index = Int32.Parse(splitName[1]);
                        ShowIncomingRace(button, index);
                    }
                }
            }
        }

        private void HomeLoaded(object sender, EventArgs e)
        {
            ShowIncomingRace(BtnComingRace_0, 0);
            if (DataContext is MainWindowViewModel viewModel)
            {
                viewModel.WindowLoaded();
            }
        }

        private void ShowIncomingRace(Button button, int index)
        {
            if (Races.Count > 0)
            {
                Circuits circuit;
                var RacesAfterNow = Races.FindAll(x => x.Start > DateTime.Now).ToList();
                var RacesOrder = RacesAfterNow.OrderBy(x => x.Start).ToList();
                var race = RacesOrder[index];
                using(var context = new DatabaseConnection())
                {
                    circuit = context.Circuits.First(x => x.id == race.CircuitId);
                }

                if (DataContext is MainWindowViewModel viewModel)
                {
                    viewModel.setImageComingRace(circuit.Image);
                    viewModel.setHeaderComingRace(race.Title);
                    viewModel.setDateComingRace(race.Start.ToString("dddd dd MMMM yyyy - HH:mm"));
                    viewModel.setAdressComingRace(circuit.Place + " - " + circuit.City);
                    viewModel.setId(race.id);
                }

                lastButton?.Classes.Remove("active");
                button.Classes.Add("active");
                lastButton = button;
            }
        }
    }
}
