using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tebbet.Database;
using Tebbet.Models;
using Tebbet.Services;
using static System.Net.Mime.MediaTypeNames;

namespace Tebbet.ViewModels
{
    public class HistoryViewModel : ViewModelBase, INotifyPropertyChanged
    {

        private ObservableCollection<HistoryBets> _HistoryBets { get; set; }
        public ObservableCollection<HistoryBets> HistoryBets
        {
            get => _HistoryBets;
            set
            {
                if (_HistoryBets != value)
                {
                    _HistoryBets = value;
                    this.RaisePropertyChanged(nameof(HistoryBets));
                }
            }
        }
        private string _Message {  get; set; }

        public string Message
        {
            get => _Message;
            set 
            {
                if (_Message != value)
                {
                    _Message = value;
                    this.RaisePropertyChanged(nameof(Message));
                }
            }
        }

        private UserService userService;

        public HistoryViewModel() 
        {
            userService = UserService.GetInstance();
            GetHistoryBets("Win");
        }

        public void GetHistoryBets(string condition)
        {
            using (var context = new DatabaseConnection())
            {
                Users user = context.Users.Single(x => x.Email == userService.Email);
                List<Bets> bets = new();
                switch (condition)
                {
                    case "Win":
                        bets = context.Bets.Where(x => x.UserId == userService.Email).Where(x => x.Has_Won == true).ToList();
                        break;
                    case "Loose":
                        bets = context.Bets.Where(x => x.UserId == userService.Email).Where(x => x.Has_Lost == true).ToList();
                        break;
                    case "All":
                        bets = context.Bets.Where(x => x.UserId == userService.Email).ToList();
                        break;
                }
                List<HistoryBets> historybetslist = GetHistoryBetsList(bets, context);
                var orderedhistorybetslist = historybetslist.OrderByDescending(x => x.Date_Bet);
                HistoryBets = new ObservableCollection<HistoryBets>(orderedhistorybetslist);
            }
        }

        private List<HistoryBets> GetHistoryBetsList(List<Bets> bets, DatabaseConnection context)
        {
            List<HistoryBets> historybetslist = new();
            foreach (var bet in bets)
            {
                Snails snail = context.Snails.Single(x => x.id == bet.SnailId);
                HistoryBets historyBets = new HistoryBets
                {
                    Snail_Name = snail.name,
                    Gains = bet.Gains,
                    Odds = bet.Odds,
                    Has_Won = bet.Has_Won,
                    Has_Lost = bet.Has_Lost,
                    Date_Bet = bet.Date_Bet.ToString("dd/MM/yyyy"),
                    Bets = Math.Round(bet.Gains / bet.Odds),
                    Color = bet.Has_Lost ? "#FF3535" : bet.Has_Won ? "#5F8C50" : "Black",
                    Text = bet.Has_Lost ? "Pertes: " : "Gains: "
                };
                historybetslist.Add(historyBets);
            }
            return historybetslist;
        } 

        public void Withdraw(double value)
        {
            using(var context = new DatabaseConnection())
            {
                if (UserService.IsAuthentified)
                {
                    Users user = context.Users.Single(x => x.Email == UserService.Email);
                    if (user.Credits - value >= 0)
                    {
                        user.Credits -= value;
                        context.Users.Update(user);
                        context.SaveChanges();
                        UserService.Credits -= value;
                        Message = "Vous avez retiré "+ value.ToString() + " credits.";
                    }
                    else
                    {
                        Message = "Vous pouvez retirer autant d'argent que vous disposez de crédits.";
                    }
                }
            }
        }

        public void Deposit(double value)
        {
            using (var context = new DatabaseConnection())
            {
                if (UserService.IsAuthentified)
                {
                    Users user = context.Users.Single(x => x.Email == UserService.Email);
                    if (user.Credits + value > 0)
                    {
                        user.Credits += value;
                        context.Users.Update(user);
                        context.SaveChanges();
                        UserService.Credits += value;
                        Message = "Vous avez déposé " + value.ToString() + " credits.";
                    }
                    else
                    {
                        Message = "L'argent déposer doit être positif.";
                    }
                }

            }
        }
    }
}
