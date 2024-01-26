using Avalonia.Controls;
using Avalonia.Interactivity;
using System;
using System.Linq;
using Tebbet.Database;
using Tebbet.Services;
using Tebbet.ViewModels;


namespace Tebbet.Controls
{
    public partial class HomeControl : UserControl
    {
        private Button? lastButton;
        private readonly ComingRacesViewModel comingRacesViewModel;
        public HomeControl()
        {
            InitializeComponent();
            Loaded += HomeLoaded;
            comingRacesViewModel = new ComingRacesViewModel();
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
                        // Permet de modifier la couleur du background, on utilise Color.Parse pour intégrer des couleurs spécifique.
                        ShowIncomingRace(button, index);
                    }
                }
            }
        }

        private void HomeLoaded(object sender, EventArgs e)
        {
            ShowIncomingRace(BtnComingRace_0, 0);
        }

        private void ShowIncomingRace(Button button, int index)
        {
            var race = comingRacesViewModel.ComingRaces[index];

            if (DataContext is MainWindowViewModel viewModel)
            {
                viewModel.setImageComingRace(race.Image);
                viewModel.setHeaderComingRace(race.Title);
                viewModel.setDateComingRace(race.Date);
                viewModel.setAdressComingRace(race.Adress);
            }

            lastButton?.Classes.Remove("active");
            button.Classes.Add("active");
            lastButton = button;
        }
    }
}
