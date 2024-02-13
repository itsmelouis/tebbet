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
        private static Timer timer;
        public event EventHandler timerBeforeLive;
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
            dateDiff = int.IsNegative(this.GetNextRaceDiff().Seconds) ? this.GetStopLive() : this.GetNextRaceDiff();
            timer = new Timer();
            timer.Elapsed += OnTimer;
            timer.Interval = 1000;
            timer.Enabled = true;
        }

        private void OnTimer(object source, ElapsedEventArgs args)
        {
            // way to decrease seconds
            dateDiff = dateDiff.Add(new TimeSpan(0, 0, 0, -1));
            // to handle timer
            if (!int.IsNegative(this.GetNextRaceDiff().Seconds))
            {
                this.TimerBeforeLive();
            }
            if (int.IsNegative(dateDiff.Seconds))
            {
                timer.Stop();
                TimerLive();
            }
        }
    }
}
