using Avalonia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tebbet.Models
{
    public class SnailInRace
    {
        public int id {  get; set; }
        public string name { get; set; }
        public double position { get; set; }
        public Thickness position_margin { get; set; }
        public int rank { get; set; }
    }
}
