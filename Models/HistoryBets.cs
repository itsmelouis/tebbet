using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tebbet.Models
{
    public class HistoryBets
    {
        public string Snail_Name { get; set; }
        public string Date_Bet { get; set; }
        public bool Has_Won { get; set; }
        public bool Has_Lost { get; set; }
        public double Odds { get; set; }
        public double Gains { get; set; }
        public double Bets { get; set; }
        public string Color { get; set; }
        public string Text { get; set; }
    }
}
