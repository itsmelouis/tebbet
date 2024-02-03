using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using System.Globalization;
using Tebbet.Services;
using Tebbet.ViewModels;
using Tebbet.Views;

namespace Tebbet;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            var Route = RouteService.GetInstance();
            Route.ChangePage("MainWindow");
            /*desktop.MainWindow = new MainWindow
            {
                DataContext = new MainWindowViewModel(),
            };*/
        }

        base.OnFrameworkInitializationCompleted();
    }
}