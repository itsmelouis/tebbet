using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using System;
using System.Reactive.Joins;
using System.Text.RegularExpressions;
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
            Birthdate.LostFocus += Handler;
        }

        private void Handler(object sender, RoutedEventArgs args)
        {
            if (sender is TextBox textbox)
            {
                if (textbox.Text != null)
                {
                    viewModel.IsBirthdateValid(textbox.Text);
                }
            }
        }
    }
}
