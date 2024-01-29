using Avalonia.Controls;
using Avalonia.Media.Imaging;
using ImageExample.Helpers;
using ReactiveUI;
using System;
using System.ComponentModel;
using Tebbet.Controls;
using Tebbet.Services;


namespace Tebbet.ViewModels;

public class MainWindowViewModel : ViewModelBase, INotifyPropertyChanged
{
    private string? _HeaderComingRace;
    private string? _DateComingRace;
    private string? _AdressComingRace;
    private Bitmap? _ImageComingRace;
    private bool _IsAuthentified;
    private bool _IsAuthentifiedAsUser;
    private bool _IsAuthentifiedAsAdmin;
    private object _ContentControl;
    private double? _Credits;

    public MainWindowViewModel()
    {
        UserService.AuthenticationSucceeded += whenAuthentified;
    }

    public Bitmap? ImageComingRace
    {
        get => _ImageComingRace;
        set
        {
            if (_ImageComingRace != value)
            {
                _ImageComingRace = value;
                this.RaisePropertyChanged(nameof(ImageComingRace));
            }
        }
    }

    public object ContentControl
    {
        get => _ContentControl;
        set
        {
            if (_ContentControl != value)
            {
                _ContentControl = value;
                this.RaisePropertyChanged(nameof(ContentControl));
            }
        }
    }

    public string HeaderComingRace
    {
        get => _HeaderComingRace;

        set
        {
            if (_HeaderComingRace != value)
            {
                _HeaderComingRace = value;
                this.RaisePropertyChanged(nameof(HeaderComingRace));
            }
        }
    }

    public string DateComingRace
    {
        get => _DateComingRace;
        set
        {
            if ( _DateComingRace != value)
            {
                _DateComingRace = value;
                this.RaisePropertyChanged(nameof(DateComingRace));
            }
        }
    }

    public string AdressComingRace
    {
        get => _AdressComingRace;
        set
        {
            if (_AdressComingRace != value)
            {
                _AdressComingRace = value;
                this.RaisePropertyChanged(nameof(AdressComingRace));
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

    public double? Credits
    {
        get => _Credits;
        set
        {
            if (_Credits != value)
            {
                _Credits = value;
                this.RaisePropertyChanged(nameof(Credits));
            }
        }
    }

    public void setImageComingRace(string value)
    {
        ImageComingRace = ImageHelper.LoadFromResource(new Uri("avares://Tebbet/Assets/Images/Circuits/" + value));
    }

    public void setHeaderComingRace(string value)
    {
        HeaderComingRace = value;
    }

    public void setDateComingRace(string value)
    {
        DateComingRace = value;
    }

    public void setAdressComingRace(string value)
    {
        AdressComingRace = value;
    }

    private void whenAuthentified(object sender, EventArgs e)
    {
        IsAuthentified = true;

        if (UserService.Role == "user")
        {
            ShowControl(typeof(HomeControl));
            IsAuthentifiedAsUser = true;
            Credits = UserService.Credits;
        }
        if (UserService.Role == "admin")
        {
            ShowControl(typeof(AdminControl));
            IsAuthentifiedAsAdmin = true;
        }
    }

    // route
    public void ShowControl(Type controlType)
    {
        ContentControl = controlType.Name switch
        {
            nameof(HomeControl) => new HomeControl(),
            nameof(LoginControl) => new LoginControl(),
            nameof(RegisterControl) => new RegisterControl(),
            nameof(AdminControl) => new AdminControl(),
            _ => new HomeControl()
        };
    }
}
