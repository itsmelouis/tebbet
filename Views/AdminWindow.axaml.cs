using Avalonia.Controls;
using Avalonia.Interactivity;
using Tebbet.Controls;
using Tebbet.ViewModels;

namespace Tebbet.Views
{
    public partial class AdminWindow : Window
    {
        public AdminWindow()
        {
            InitializeComponent();
            DataContext = AdminViewModel.GetInstance();
            Circuits.Click += Handler;
            Races.Click += Handler;
        }

        private void Handler(object sender, RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                if (DataContext is AdminViewModel viewModel)
                {
                    switch (button.Name)
                    {
                        case "Circuits":
                            viewModel.ShowControl(typeof(CircuitControl));
                            break;
                        case "Races":
                            break;
                    }
                }
            }
        }
    }
}
