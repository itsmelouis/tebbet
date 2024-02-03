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
        DataContext = MainWindowViewModel.GetInstance();
        Opened += WindowOpened;
    }

    private void WindowOpened(object sender, EventArgs e)
    {
        if (DataContext is MainWindowViewModel viewModel)
        {
            viewModel.ShowControl(typeof(HomeControl));
        }
    }
}