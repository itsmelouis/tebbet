using Avalonia.Controls;
using ReactiveUI;
using System;
using System.ComponentModel;
using Tebbet.Controls;
using Tebbet.Services;

namespace Tebbet.ViewModels;

public interface InterfaceShowControl
{
    void ShowControl(Type ControlType);
}

public class ViewModelBase : ReactiveObject, INotifyPropertyChanged, InterfaceShowControl
{
    public UserService UserService;

    public virtual void ShowControl(Type ControlType) { }

    private bool _IsAuthentified;
    private bool _IsAuthentifiedAsUser;
    private bool _IsAuthentifiedAsAdmin;

    private object _NavbarControl;


    public object NavbarControl
    {
        get => _NavbarControl;
        set
        {
            if (_NavbarControl != value)
            {
                _NavbarControl = value;
                this.RaisePropertyChanged(nameof(NavbarControl));
            }
        }
    }

    public bool IsAuthentified
    {
        get => _IsAuthentified;
        set
        {
            if (_IsAuthentified != value)
            {
                _IsAuthentified = value;
                this.RaisePropertyChanged(nameof(IsAuthentified));
            }
        }
    }

    public bool IsAuthentifiedAsUser
    {
        get => _IsAuthentifiedAsUser;
        set
        {
            if (_IsAuthentifiedAsUser != value)
            {
                _IsAuthentifiedAsUser = value;
                this.RaisePropertyChanged(nameof(IsAuthentifiedAsUser));
            }
        }
    }

    public bool IsAuthentifiedAsAdmin
    {
        get => _IsAuthentifiedAsAdmin;
        set
        {
            if (_IsAuthentifiedAsAdmin != value)
            {
                _IsAuthentifiedAsAdmin = value;
                this.RaisePropertyChanged(nameof(IsAuthentifiedAsAdmin));
            }
        }
    }
    public ViewModelBase()
    {
        UserService = UserService.GetInstance();
        UserService.AuthenticationSucceeded += whenAuthentified;
        UserService.LogoutSucceeded += whenLogout;
        IsAuthentified = UserService.IsAuthentified;
        IsAuthentifiedAsAdmin = UserService.IsAuthentifiedAsAdmin;
        IsAuthentifiedAsUser = UserService.IsAuthentifiedAsUser;
    }

    private void whenLogout(object sender, EventArgs e)
    {
        NavbarControl = new NavbarControl();
        this.ShowControl(typeof(HomeControl));
    }

    private void whenAuthentified(object sender, EventArgs e)
    {
        if (UserService.Role == "user")
        {
            NavbarControl = new NavbarControl();
            this.ShowControl(typeof(HomeControl));
        }
        if (UserService.Role == "admin")
        {
            var route = RouteService.GetInstance();
            route.ChangePage("AdminWindow");
        }
    }
}
