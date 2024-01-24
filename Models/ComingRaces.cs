using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tebbet.Models
{
    public class ComingRaces
    {
        public required string Image { get; set; }
        public required string Title { get; set; }

        public required string Date { get; set; }

        public required string Adress { get; set; }
    }
}
