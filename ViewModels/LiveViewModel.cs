using Avalonia.Controls;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;
using Tebbet.Controls;
using Tebbet.Database;
using Tebbet.Services;

namespace Tebbet.ViewModels
{
    public class LiveViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private string _Chrono;
        private byte[] _ImageComingRace;
        private string? _HeaderComingRace;
        private string? _DateComingRace;
        private string? _AdressComingRace;
        private int _IdComingRace;

        public string Chrono
        {
            get => _Chrono;
            set
            {
                if (value != _Chrono)
                {
                    _Chrono = value;
                    this.RaisePropertyChanged(nameof(Chrono));
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
                if (_DateComingRace != value)
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

        public int IdComingRace
        {
            get => _IdComingRace;
            set
            {
                if (_IdComingRace != value)
                {
                    _IdComingRace = value;
                    this.RaisePropertyChanged(nameof(IdComingRace));
                }
            }
        }

        private void setRace(byte[] image, string header, string date, string adress, int id)
        {
            ImageComingRace = image;
            HeaderComingRace = header;
            DateComingRace = date;
            AdressComingRace = adress;
            IdComingRace = id;
        }

        private LivesServices Live;
        public ReactiveCommand<int, Unit> BetRace { get; }

        public LiveViewModel() 
        {
            Live = LivesServices.GetInstance();
            Live.timerBeforeLive += HandlerLive;
            getRace();
            BetRace = ReactiveCommand.Create<int>(ToBetRace);
        }

        private void ToBetRace(int id)
        {
            MainWindowViewModel main = MainWindowViewModel.GetInstance();
            main.ContentControl = new CourseControl(id);
        }

        private void getRace()
        {
            using(var context = new DatabaseConnection())
            {
                var race = context.Races.Where(x => x.IsEnded == false).OrderBy(x => x.Start).FirstOrDefault();
                var circuit = context.Circuits.Single(x => x.id == race.CircuitId);

                if (race != null && circuit != null)
                {
                    setRace(circuit.Image, race.Title, race.Start.ToString("dddd dd MMMM yyyy - HH:mm"), circuit.Place + " - " + circuit.City, race.id);
                }
            }
        }

        private void HandlerLive(object sender, EventArgs args)
        {
            string days = Live.dateDiff.Days.ToString();
            string hours = Live.dateDiff.Hours.ToString();
            string minutes = Live.dateDiff.Minutes.ToString();
            string seconds = Live.dateDiff.Seconds.ToString();

            Chrono = days + "j " + hours + "h " + minutes + "m " + seconds + "s";
        }
    }
}
