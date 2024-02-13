using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tebbet.Database;

namespace Tebbet.Services
{
    public class LivesServices
    {
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

        public event EventHandler onRaceLive;

        private LivesServices() 
        {
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

        private void OnRaceLive()
        {
            onRaceLive?.Invoke(this, EventArgs.Empty);
        }
    }
}
