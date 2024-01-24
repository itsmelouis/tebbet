using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tebbet.Models;

namespace Tebbet.ViewModels
{
    public class ComingRacesViewModel : ViewModelBase
    {
        public List<ComingRaces> ComingRaces { get; set; }

        public ComingRacesViewModel()
        {
            ComingRaces = new List<ComingRaces>
            {
                new()
                {
                    Image = "valenciennes.jpg",
                    Title = "Valenciesnails",
                    Date = "Mardi 10 Février 2024 - 02:00",
                    Adress = "Quartier Le Vignole - Valenciennes"
                },
                new()
                {
                    Image = "tokyo.jpg",
                    Title = "Escargokyo",
                    Date = "Jeudi 12 Février 2024 - 14:20",
                    Adress = "Chuo-dori - Tokyo"
                },
                new()
                {
                    Image = "lille.jpg",
                    Title = "Bave à lille",
                    Date = "Samedi 02 Mars 2024 - 10:30",
                    Adress = "Euralille - Lille"
                },
                new()
                {
                    Image = "newyork.jpeg",
                    Title = "Tour du Central Park",
                    Date = "Dimanche 03 Mars 2024 - 05:45",
                    Adress = "Central Park - New York"
                }
            };
        }
    }
}
