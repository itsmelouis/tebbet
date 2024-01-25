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
        }
    }

    private void ShowControl(Type controlType)
    {
        switch(controlType.Name)
        {
            case nameof(HomeControl):
                contentControl.Content = new HomeControl();
                break;
            case nameof(LoginControl):
                contentControl.Content = new LoginControl();
                break;
            default:
                contentControl.Content = new HomeControl();
                break;
        }
    }
}