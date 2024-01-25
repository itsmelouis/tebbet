 using Avalonia.Controls;
using Avalonia.Interactivity;
using Tebbet.ViewModels;

namespace Tebbet.Controls
{
    public partial class LoginControl : UserControl
    {
        private readonly LoginViewModel loginViewModel;
        public LoginControl()
        {
            InitializeComponent();
            loginViewModel = new LoginViewModel();
        }

        public void ButtonClick(object sender, RoutedEventArgs args)
        {
            if (sender is Button button)
            {
                if (button.Name == "Connection")
                {
                    var mail = Mail.Text;
                    var password = Password.Text;
                    if (mail != null && password != null)
                    {
                        loginViewModel.Login(mail, password);
                    }
                }
            }
        }
    }
}
