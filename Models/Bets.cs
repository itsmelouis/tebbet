using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tebbet.Models
{
    [PrimaryKey(nameof(UserId), nameof(RaceId))]
    public class Bets
    {
        [Column(Order = 1)]
        public string UserId { get; set; }

        [Column(Order = 2)]
        public int RaceId { get; set; }
        public int SnailId { get; set; }
        public double Odds { get; set; }
        public double Gains { get; set; }
        public bool Has_Won { get; set; }
        public bool Has_Lost { get; set; }
        public DateTime Date_Bet { get; set; }
    }
}
