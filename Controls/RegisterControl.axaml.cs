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
        private readonly RegisterViewModel viewModel;
        public RegisterControl()
        {
            InitializeComponent();
            viewModel = new RegisterViewModel();
            DataContext = viewModel;
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
                            viewModel.IsBirthdateValid(textbox.Text);
                            break;
                        case "Mail":
                            viewModel.IsMailValid(textbox.Text);
                            break;
                        case "PostalCode":
                            viewModel.IsPostalCodeValid(textbox.Text);
                            break;
                    }
                }
            }

            if (sender is Button button && button.Name == "Register")
            {
                if (new[] {Lastname.Text, Firstname.Text, Mail.Text, Address.Text, PostalCode.Text, City.Text, Birthdate.Text, Password.Text, PasswordConfirmation.Text } is string[] formData && formData.All(s => !string.IsNullOrEmpty(s)))
                {
                    viewModel.VerifyRegister(formData);
                }                
            }
        }
    }
}
