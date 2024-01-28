using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tebbet.Models
{
    public class Races
    {
        [Key]
        public required int id {  get; set; }
        public required string Title { get; set; }
        public required DateTime Start { get; set; }
        public DateTime? End { get; set; }
        [ForeignKey("id")]
        public Circuits Circuits { get; set; }
    }
}
