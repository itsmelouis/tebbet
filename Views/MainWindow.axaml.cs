using Avalonia.Controls;
using Avalonia.Interactivity;
using System;
using Tebbet.Controls;
using Tebbet.Services;
using Tebbet.ViewModels;

namespace Tebbet.Views;

public partial class MainWindow : Window
{

    public MainWindow()
    {
        InitializeComponent();
        Opened += WindowOpened;
    }

    private void WindowOpened(object sender, EventArgs e)
    {
        if (DataContext is MainWindowViewModel viewModel)
        {
            viewModel.ShowControl(typeof(HomeControl));
        }
    }

    private void ButtonClick(object sender, RoutedEventArgs args)
    {
        if (sender is Button button && DataContext is MainWindowViewModel viewModel)
        {
            // route
            if (button.Classes.Contains("Login"))
            {
                viewModel.ShowControl(typeof(LoginControl));
            }
            if (button.Classes.Contains("Home"))
            {
                viewModel.ShowControl(typeof(HomeControl));
            }
            if (button.Classes.Contains("Register"))
            {
                viewModel.ShowControl(typeof(RegisterControl));
            }
        }
    }
}