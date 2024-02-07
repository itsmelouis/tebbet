using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tebbet.Database;
using Tebbet.Models;

namespace Tebbet.Services
{
    public class BetServices
    {
        private UserService userService;
        public BetServices() 
        {
            userService = UserService.GetInstance();
            ProcessFindBets();
        }

        private void ProcessFindBets()
        {
            if (userService.IsAuthentified)
            {
                using (var context = new DatabaseConnection())
                {
                    var query = from x in context.Bets
                                join y in context.Races
                                on x.RaceId equals y.id
                                join w in context.SnailParticipatingRace
                                on x.RaceId equals w.RacesId
                                where x.UserId == userService.Email
                                where x.SnailId == w.SnailsId
                                where x.Has_Lost == false
                                where x.Has_Won == false
                                where y.IsEnded == true
                                select new
                                {
                                    Bets = x,
                                    Races = y,
                                    Snails = w,
                                };
                    var bets = query.ToList();

                    foreach (var bet in bets)
                    {
                        if (bet.Snails.Ranking == 1)
                        {
                            // win
                            bet.Bets.Has_Won = true;
                            var user = context.Users.Single(x => x.Email == userService.Email);
                            user.Credits += bet.Bets.Gains;
                            userService.Credits += bet.Bets.Gains;
                            context.Bets.Update(bet.Bets);
                            context.Users.Update(user);
                            context.SaveChanges();
                        }
                        else
                        {
                            // loose
                            bet.Bets.Has_Lost = true;
                            context.Bets.Update(bet.Bets);
                            context.SaveChanges();
                        }
                    }
                }
            }
        }

    }
}
