using Avalonia.Controls;
using Avalonia.Interactivity;
using System;
using Tebbet.Controls;

namespace Tebbet.Views;

public partial class MainWindow : Window
{

    public MainWindow()
    {
        InitializeComponent();
        ShowControl(typeof(HomeControl));
    }


    private void ButtonClick(object sender, RoutedEventArgs args)
    {
        if (sender is Button button)
        {
            if (button.Classes.Contains("Login"))
            {
                ShowControl(typeof(LoginControl));
            }
            if (button.Classes.Contains("Home"))
            {
                ShowControl(typeof(HomeControl));
            }
        }
    }

    private void ShowControl(Type controlType)
    {
        contentControl.Content = controlType.Name switch
        {
            nameof(HomeControl) => new HomeControl(),
            nameof(LoginControl) => new LoginControl(),
            _ => new HomeControl(),
        };
    }
}