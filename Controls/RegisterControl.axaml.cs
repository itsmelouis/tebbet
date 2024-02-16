using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using System;
using System.Linq;
using System.Reactive.Joins;
using System.Text.RegularExpressions;
using Tebbet.Models;
using Tebbet.ViewModels;

namespace Tebbet.Controls
{
    public partial class RegisterControl : UserControl
    {
        private readonly RegisterViewModel registerViewModel;
        private readonly LoginViewModel loginViewModel;
        public RegisterControl()
        {
            InitializeComponent();
            registerViewModel = new RegisterViewModel();
            DataContext = registerViewModel;
            Birthdate.LostFocus += Handler;
            Mail.LostFocus += Handler;
            PostalCode.LostFocus += Handler;
            Register.Click += Handler;
        }

        private void Handler(object sender, RoutedEventArgs args)
        {
            if (sender is TextBox textbox)
            {
                if (!string.IsNullOrEmpty(textbox.Text))
                {
                    switch (textbox.Name) 
                    {
                        case "Birthdate":
                            registerViewModel.IsBirthdateValid(textbox.Text);
                            break;
                        case "Mail":
                            registerViewModel.IsMailValid(textbox.Text);
                            break;
                        case "PostalCode":
                            registerViewModel.IsPostalCodeValid(textbox.Text);
                            break;
                    }
                }
            }

            if (sender is Button button && button.Name == "Register")
            {
                if (new[] {Lastname.Text, Firstname.Text, Mail.Text, Address.Text, PostalCode.Text, City.Text, Birthdate.Text, Password.Text, PasswordConfirmation.Text } is string[] formData && formData.All(s => !string.IsNullOrEmpty(s)))
                {
                    registerViewModel.VerifyRegister(formData);
                }                
            }
        }
    }
}
