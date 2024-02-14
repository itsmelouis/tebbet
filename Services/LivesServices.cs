using Avalonia.Controls.Shapes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Tebbet.Database;

namespace Tebbet.Services
{
    public class LivesServices
    {
        private UserService userService;
        public int RaceLive { get; set; }
        private static LivesServices _instance;
        public static LivesServices GetInstance()
        {
            if (_instance == null)
            {
                _instance = new LivesServices();
            }
            return _instance;
        }
        public TimeSpan dateDiff { get; set; }
        public TimeSpan nextRaceDiff { get; set; }
        private static Timer timer;
        public event EventHandler timerBeforeLive;
        public event EventHandler tickRaceLive;
        public event EventHandler onRaceLive;

        private LivesServices() 
        {
            userService = UserService.GetInstance();
            RaceLive = 0;
        }

        public TimeSpan GetNextRaceDiff()
        {
            using(var context = new DatabaseConnection())
            {
                var race = context.Races.Where(x => x.IsEnded == false).OrderBy(x => x.Start).FirstOrDefault();
                DateTime dateTime = DateTime.Now;
                TimeSpan diff = race.Start - DateTime.Now;
                if (!int.IsNegative(diff.Seconds))
                {
                    RaceLive = 0;
                    OnRaceLive();
                }
                return diff;
            }
        }

        public TimeSpan GetStopLive()
        {
            using (var context = new DatabaseConnection())
            {
                var race = context.Races.Where(x => x.IsEnded == false).OrderBy(x => x.Start).FirstOrDefault();
                DateTime dateTime = DateTime.Now;
                TimeSpan diff = (TimeSpan)(race.End - DateTime.Now);
                if (!int.IsNegative(diff.Seconds))
                {
                    RaceLive = 1;
                    OnRaceLive();
                }
                return diff;
            }
        }
        private void TimerBeforeLive()
        {
            timerBeforeLive?.Invoke(this, EventArgs.Empty);
        }

        private void OnRaceLive()
        {
            onRaceLive?.Invoke(this, EventArgs.Empty);
        }

        // timer
        public void TimerLive()
        {
            new RacesServices();
            if (userService.IsAuthentifiedAsUser)
            {
                new BetServices();
            }
            nextRaceDiff = this.GetNextRaceDiff();
            dateDiff = int.IsNegative(nextRaceDiff.Seconds) && nextRaceDiff.Minutes <= 0 ? this.GetStopLive() : nextRaceDiff;
            timer = new Timer();
            timer.Elapsed += OnTimer;
            timer.Interval = 1000;
            timer.Enabled = true;
        }

        private void OnTimer(object source, ElapsedEventArgs args)
        {
            // pour enlever 1 seconde
            dateDiff = dateDiff.Add(new TimeSpan(0, 0, 0, -1));
            // compte à rebour
            if (!int.IsNegative(nextRaceDiff.Seconds))
            {
                this.TimerBeforeLive();
            }
            // vérifier si la différence de temps est négative et déclencher de nouveau le timer 
            if (int.IsNegative(dateDiff.Seconds) && dateDiff.Minutes == 0)
            {
                timer.Stop();
                TimerLive();
            }
        }
    }
}
