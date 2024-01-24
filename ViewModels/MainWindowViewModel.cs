using Avalonia.Media.Imaging;
using ImageExample.Helpers;
using ReactiveUI;
using System;
using System.ComponentModel;


namespace Tebbet.ViewModels;

public class MainWindowViewModel : ViewModelBase, INotifyPropertyChanged
{
    private string _HeaderComingRace;
    private string _DateComingRace;
    private string _AdressComingRace;
    private Bitmap? _ImageComingRace;
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

    public string HeaderComingRace
    {
        get => _HeaderComingRace;
        set
        {
            if ( _HeaderComingRace != value)
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
}
