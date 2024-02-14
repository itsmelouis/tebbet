using Avalonia;
using Avalonia.Controls;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Tebbet.Controls;
using Tebbet.Database;
using Tebbet.Models;
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
        private static Timer timer;
        private int _IdComingRace;
        private TimeSpan diff { get; set; }
        private ObservableCollection<SnailInRace> _SnailInRaceList;
        
        public ObservableCollection<SnailInRace> SnailInRaceList
        {
            get => _SnailInRaceList;
            set
            {
                if (value != _SnailInRaceList)
                {
                    _SnailInRaceList = value;
                    this.RaisePropertyChanged(nameof(SnailInRaceList));
                }
            }
        }

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

        private bool _ChronoVisibility;

        public bool ChronoVisibility
        {
            get => _ChronoVisibility;
            set
            {
                if (value !=  _ChronoVisibility)
                {
                    _ChronoVisibility = value;
                    this.RaisePropertyChanged(nameof(ChronoVisibility));
                }
            }
        }

        private Races Race {  get; set; }

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
            Live.onRaceLive += HandleStartRace;
            getRace();
            BetRace = ReactiveCommand.Create<int>(ToBetRace);

            if (Live.RaceLive == 1)
            {
                this.StartRace();
            }
        }

        private void ToBetRace(int id)
        {
            MainWindowViewModel main = MainWindowViewModel.GetInstance();
            main.ContentControl = new CourseControl(id);
        }

        private Races getRace()
        {
            using(var context = new DatabaseConnection())
            {
                Races race = context.Races.Where(x => x.IsEnded == false).OrderBy(x => x.Start).FirstOrDefault();
                var circuit = context.Circuits.Single(x => x.id == race.CircuitId);

                if (race != null && circuit != null)
                {
                    setRace(circuit.Image, race.Title, race.Start.ToString("dddd dd MMMM yyyy - HH:mm"), circuit.Place + " - " + circuit.City, race.id);
                }

                return race;
            }
        }

        private void HandlerLive(object sender, EventArgs args)
        {
            ChronoVisibility = true;
            string days = Live.dateDiff.Days.ToString();
            string hours = Live.dateDiff.Hours.ToString();
            string minutes = Live.dateDiff.Minutes.ToString();
            string seconds = Live.dateDiff.Seconds.ToString();

            Chrono = days + "j " + hours + "h " + minutes + "m " + seconds + "s";
        }

        private void HandleStartRace(object sender, EventArgs args)
        {
            this.StartRace();
        }

        private void StartRace()
        {
            ChronoVisibility = false;
            Race = this.getRace();
            List<SnailInRace> ListOfSnail = [];
            using(var context = new DatabaseConnection())
            {
                var SnailsInRace = context.SnailParticipatingRace.Where(x => x.RacesId == Race.id).ToList();

                foreach (var snail in SnailsInRace)
                {
                    Snails snailDetail = context.Snails.Single(x => x.id == snail.SnailsId);
                    ListOfSnail.Add(new()
                    {
                        id = snail.SnailsId,
                        name = snailDetail.name,
                        position = 0,
                        position_margin = Thickness.Parse("20, 0, 0, 5"),
                        rank = 0
                    }
                    );
                }
            }

            SnailInRaceList = new ObservableCollection<SnailInRace>(ListOfSnail);

            this.TimerLive();
        }

        public void TimerLive()
        {
            DateTime now = DateTime.Now;
            DateTime? end = Race.End;
            DateTime endTime = end.Value;
            this.diff = endTime - now;
            this.diff = diff.Add(new TimeSpan(0, 0, 0, -10));
            timer = new Timer();
            timer.Elapsed += ProceedRace;
            timer.Interval = 1000;
            timer.Enabled = true;
        }

        private void ProceedRace(object source, ElapsedEventArgs args)
        {
            this.diff = diff.Add(new TimeSpan(0, 0, 0, -1));
            if (!int.IsNegative(diff.Seconds))
            {
                foreach (var snail in SnailInRaceList)
                {
                    var culture = CultureInfo.InvariantCulture;
                    Random random = new Random();
                    double randomSpeed = Math.Round(random.NextDouble() * (0.8 - 0.1) + 0.1, 3);
                    var pos = snail.position_margin.ToString();
                    var splitpos = pos.Split(',');
                    var newpos = double.Parse(splitpos[0], culture) + randomSpeed;
                    var strpos = Math.Round(newpos, 3).ToString(culture) + ", 0, 0, 5";
                    var thickness = Thickness.Parse(strpos);
                    snail.position = Math.Round(newpos, 3);
                    snail.position_margin = thickness;
                }
                var OrderedSnailInRaceList = SnailInRaceList.OrderByDescending(x => x.position);
                var i = 0;
                foreach (var snail in OrderedSnailInRaceList)
                {
                    i++;
                    snail.rank = i;
                }
                SnailInRaceList = new ObservableCollection<SnailInRace>(OrderedSnailInRaceList);
            }
            else
            {
                timer.Stop();
                this.SaveRace();

            }
        }

        private void SaveRace()
        {
            using(var context = new DatabaseConnection())
            {
                Race.IsEnded = true;

                var snails = context.SnailParticipatingRace.Where(x => x.RacesId == Race.id);
                foreach (var snail in snails)
                {
                    snail.Ranking = SnailInRaceList.Single(x => x.id == snail.SnailsId).rank;
                    context.SnailParticipatingRace.Update(snail);
                }
                context.Races.Update(Race);
                context.SaveChanges();
            }

            var userservice = UserService.GetInstance();
            if (userservice.IsAuthentifiedAsUser)
            {
                new BetServices();
            }

            getRace();
            ChronoVisibility = true;
        }
    }
}
