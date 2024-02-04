using Avalonia.Controls;
using Avalonia.Interactivity;
using System;
using System.Linq;
using Tebbet.Models;
using Tebbet.ViewModels;

namespace Tebbet.Controls
{
    public partial class RacesControl : UserControl
    {
        private AdminRacesViewModel viewModel;
        public RacesControl()
        {
            InitializeComponent();
            viewModel = new AdminRacesViewModel();
            DataContext = viewModel;
            AddRace.Click += Handler;
            SaveRace.Click += Handler;
            CancelRace.Click += Handler;
        }

        private void Handler(object sender, RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                switch (button.Name)
                {
                    case "AddRace":
                        viewModel.AddRace();
                        break;
                    case "CancelRace":
                        viewModel.ModalRace = false;
                        break;
                    case "SaveRace":
                        CheckForm();
                        break;
                }
            }
        }

        private void CheckForm()
        {
            string title = TitleRace.Text;

            var timeStart = TimeStartRace.SelectedTime.Value;
            var dateStart = DateStartRace.SelectedDate.Value.DateTime;
            dateStart = dateStart.AddHours(timeStart.Hours + 1);
            dateStart = dateStart.AddMinutes(timeStart.Minutes);

            var timeEnd = TimeEndRace.SelectedTime.Value;
            var dateEnd = DateEndRace.SelectedDate.Value.DateTime;
            dateEnd = dateEnd.AddHours(timeEnd.Hours + 1);
            dateEnd = dateEnd.AddMinutes(timeEnd.Minutes);
     
            DateTime start = dateStart.ToUniversalTime();
            DateTime? end = dateEnd.ToUniversalTime();
            if (!string.IsNullOrEmpty(title) && start != null && end != null)
            {
                viewModel.SaveRace(title, start, end);
            }
            else
            {
                // Ajouter une liste d'erreurs.
            }
        }
    }
}
