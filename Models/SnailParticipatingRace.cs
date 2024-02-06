using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Tebbet.Models
{
    [PrimaryKey(nameof(RacesId), nameof(SnailsId))]
    public class SnailParticipatingRace
    {
        [Column(Order = 1)]
        public int RacesId { get; set; }

        [Column(Order = 2)] 
        public int SnailsId { get; set; }
        public int Ranking { get; set; }
    }
}
