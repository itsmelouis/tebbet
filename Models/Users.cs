using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tebbet.Models
{
    public class Users
    {
        [Key]
        public required string Email {  get; set; } 
        public required string Lastname { get; set; }
        public required string Firstname { get; set; }
        public required DateTime Birthdate { get; set; }
        public required string Address { get; set; }
        public required string Postalcode { get; set; }
        public required string City { get; set; }
        public required string Password { get; set; }
        public int? Credits { get; set; }
        public string? Phone { get; set; }
        public string? Role { get; set;}
    }
}

