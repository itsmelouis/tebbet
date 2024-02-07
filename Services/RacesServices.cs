using DynamicData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tebbet.Database;
using Tebbet.Models;

namespace Tebbet.Services
{
    public class RacesServices
    {
        public RacesServices()
        {
            var races = FindRacesBeforeNow();
            CheckRanksSnails(races);
        }

        private List<Races> FindRacesBeforeNow()
        {
            DateTime dateTime = DateTime.Now;
            dateTime = dateTime.AddHours(1);
            using (var context = new DatabaseConnection())
            {
                return context.Races.Where(x => x.End <=  dateTime.ToUniversalTime()).Where(x => x.IsEnded == false).ToList();
            }
        }

        private void CheckRanksSnails(List<Races> races)
        {
            using(var context = new DatabaseConnection())
            {
                foreach (var race in races)
                {
                    List<SnailsParkours> snailsParkour = new List<SnailsParkours>();
                    var snailsParticipating = context.SnailParticipatingRace.Where(x => x.RacesId == race.id).ToList();
                    foreach (var snail in snailsParticipating)
                    {
                        var a = context.Snails.Single(x => x.id == snail.SnailsId);
                        var b = context.Snails.Where(x => x.id != snail.SnailsId).ToList();
                        var averageRanks = AverageRanks(b);
                        double Odds = GetOdds(a.general_rank, averageRanks) > 1.5 ? GetOdds(a.general_rank, averageRanks) : 1.5;
                        double totalTraveled = Math.Round(GetListTraveled(Odds, race).Sum(),2);
                        snailsParkour.Add(new() { Snail = snail, TotalParkour = totalTraveled });
                    }
                    // Vérifier le total parcourus de chacuns des escargots.
                    List<SnailsParkours> ranks = snailsParkour.OrderByDescending(x => x.TotalParkour).ToList();
                    SaveRank(ranks);
                }
            }
        }

        private void SaveRank(List<SnailsParkours>  ranks)
        {
            using(var context = new DatabaseConnection())
            {
                var i = 0;
                foreach (var rank in ranks)
                {
                    i++;
                    var snail = context.SnailParticipatingRace.Where(x => x.SnailsId == rank.Snail.SnailsId).Where(x => x.RacesId == rank.Snail.RacesId).First();
                    snail.Ranking = i;
                    context.Update(snail);
                    context.SaveChanges();
                }
                var race = context.Races.Where(x => x.id == ranks[0].Snail.RacesId).First();
                race.IsEnded = true;
                context.Update(race);
                context.SaveChanges();
            }
        }

        private List<double> GetListTraveled(double odds, Races race)
        {
            List<double> Traveled = [];
            double minutes = GetDifferenceMinutes(race);
            var x = minutes / 2;
            List<int> ListTwoMinutes = [];
            for (int i = 0; i < x; i++)
            {
                ListTwoMinutes.Add(2);
            }

            foreach (var twoMinutes in ListTwoMinutes)
            {
                Random random = new Random();
                int random_numb = random.Next(3, 6);
                double dividedOdds = 1 / odds;
                double randomOdds = (dividedOdds / 2) + (random.NextDouble() * dividedOdds);
                double speed = random_numb + randomOdds;
                Traveled.Add(Math.Round(speed, 2));
            }
            return Traveled;
        }

        private double GetDifferenceMinutes(Races race)
        {
            DateTime? B = race.End;
            DateTime UpdatedTime = race.End ?? DateTime.Now;
            if (B.HasValue)
            {
                DateTime x = B.Value;
                DateTime y = race.Start;
                TimeSpan dateDifference = x - y;
                return dateDifference.TotalMinutes;
            }
            return 0;
        }

        private double GetOdds(int SnailRank, double averageRanks)
        {
            var a = 9 / averageRanks;
            var b = SnailRank * a;
            return Math.Round(b, 2);
        }

        private double AverageRanks(List<Snails> Snails)
        {
            List<double> rank = new List<double>();
            foreach (var snail in Snails)
            {
                rank.Add(snail.general_rank);
            }
            return rank.Average();
        }
    }
}