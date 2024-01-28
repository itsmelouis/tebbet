using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tebbet.Models
{
    public class Circuits
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id {  get; set; }
        [StringLength(50)]
        public required string Name { get; set; }
        [StringLength(30)]
        public required string City {  get; set; }
        [StringLength(30)]
        public required string Country { get; set; }
        [StringLength(50)]
        public required string Place { get; set; }
        public required byte[] Image { get; set; }
        public ICollection<Races> Races { get; set; }
    }
}
