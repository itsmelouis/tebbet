using Avalonia.Controls;
using Avalonia.Interactivity;
using Tebbet.ViewModels;

namespace Tebbet.Controls
{
    public partial class NavbarControl : UserControl
    {
        NavbarViewModel viewModel;
        public NavbarControl()
        {
            InitializeComponent();
            viewModel = new NavbarViewModel();
            DataContext = viewModel;
        }

        private void ButtonClick(object sender, RoutedEventArgs args)
        {
            if (sender is Button button)
            {
                var MainWindow = MainWindowViewModel.GetInstance();
                // route
                if (button.Classes.Contains("Login"))
                {
                    MainWindow.ShowControl(typeof(LoginControl));
                }
                if (button.Classes.Contains("Home"))
                {
                    MainWindow.ShowControl(typeof(HomeControl));
                }
                if (button.Classes.Contains("Register"))
                {
                    MainWindow.ShowControl(typeof(RegisterControl));
                }
                if (button.Classes.Contains("Ranking"))
                {
                    MainWindow.ShowControl(typeof(RankingControl));
                }
                if (button.Classes.Contains("History"))
                {
                    MainWindow.ShowControl(typeof(HistoryControl));
                }
                if (button.Classes.Contains("Logout"))
                {
                    viewModel.Logout();
                }
            }
        }
    }
}
