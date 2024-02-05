using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tebbet.Models
{
    public class RacesCards
    {
        public int id { get; set; }
        public string Title { get; set; }
        public DateTime Start { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Place { get; set; }
    }
}
