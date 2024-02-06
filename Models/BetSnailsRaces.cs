using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tebbet.Models
{
    public class BetSnailsRaces
    {
        public int idSnail { get; set; }
        public string name { get; set; }
        public int general_rank { get; set; }
        public int RankingRace { get; set; }
        private double _betOdds;

        public double BetOdds
        {
            get { return _betOdds; }
            set
            {
                if (value >= 1)
                {
                    _betOdds = value;
                }
                else
                {
                    _betOdds = 1.05;
                }
            }
        }
    }
}
