﻿using Avalonia.Controls;
using Avalonia.Media.Imaging;
using ImageExample.Helpers;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using Tebbet.Controls;
using Tebbet.Database;
using Tebbet.Models;
using Tebbet.Services;


namespace Tebbet.ViewModels;

public class MainWindowViewModel : ViewModelBase, INotifyPropertyChanged
{
    // Singleton
    private static MainWindowViewModel _instance;

    private MainWindowViewModel()
    {
        NavbarControl = new NavbarControl();
    }

    public static MainWindowViewModel GetInstance()
    {
        if (_instance == null)
        {
            _instance = new MainWindowViewModel();
        }

        return _instance;
    }

    private object _ContentControl;
    private string? _HeaderComingRace;
    private string? _DateComingRace;
    private string? _AdressComingRace;
    private byte[] _ImageComingRace;
    private ObservableCollection<RacesCards> _RacesCards;

    public ObservableCollection<RacesCards> RacesCards
    {
        get => _RacesCards;
        set
        {
            if (_RacesCards != value)
            {
                _RacesCards = value;
                this.RaisePropertyChanged(nameof(RacesCards));
            }
        }
    }

    public byte[] ImageComingRace
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

    public void setImageComingRace(byte[] value)
    {
        ImageComingRace = value;
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

    public void WindowLoaded()
    {
        using (var context = new DatabaseConnection())
        {
            var races = context.Races.ToList();
            var racesNow = races.FindAll(x => x.Start > DateTime.Now).ToList();
            var racesAfter4 = racesNow.GetRange(4, racesNow.Count - 4).ToList();
            foreach (var race in racesAfter4)
            {
                var circuit = context.Circuits.First(x => x.id == race.CircuitId);
                RacesCards = new ObservableCollection<RacesCards>();
                RacesCards.Add(new RacesCards
                {
                    City = circuit.City,
                    Country = circuit.Country,
                    id = race.id,
                    Place = circuit.Place,
                    Start = race.Start,
                    Title = race.Title
                });
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

    // route
    public override void ShowControl(Type controlType)
    {
        ContentControl = controlType.Name switch
        {
            nameof(HomeControl) => new HomeControl(),
            nameof(LoginControl) => new LoginControl(),
            nameof(RegisterControl) => new RegisterControl(),
            _ => new HomeControl()
        };
    }

}
