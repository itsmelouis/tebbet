using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Numerics;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;
using Tebbet.Controls;
using Tebbet.Database;
using Tebbet.Models;
using Tebbet.Services;

namespace Tebbet.ViewModels
{
    public class CourseViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private Races _Race { get; set; }
        private Circuits _Circuit { get; set; }
        private BetSnailsRaces _Bet {  get; set; }
        private bool _HasBet { get; set; }
        private ObservableCollection<BetSnailsRaces> _Snails { get; set; }
        private double _Gain { get; set; }
        private int PlacedMoney { get; set; }
        private string _Alert { get; set; }

        public string Alert
        {
            get => _Alert;
            set
            {
                if (_Alert != value)
                {
                    _Alert = value;
                    this.RaisePropertyChanged(nameof(Alert));
                }
            }
        }

        public double Gain
        {
            get => _Gain;
            set
            {
                if (_Gain != value)
                {
                    _Gain = value;
                    this.RaisePropertyChanged(nameof(Gain));
                }
            }
        }

        public ReactiveCommand<int, Unit> BetCommand { get; }

        public bool HasBet
        {
            get => _HasBet;
            set
            {
                if (_HasBet != value)
                {
                    _HasBet = value;
                    this.RaisePropertyChanged(nameof(HasBet));
                }
            }
        }

        public double TextBoxValue {  get; set; }

        public BetSnailsRaces Bet
        {
            get => _Bet;
            set
            {
                if (_Bet != value)
                {
                    _Bet = value;
                    this.RaisePropertyChanged(nameof(Bet));
                    this.SetGain(TextBoxValue);
                }
            }
        }

        public ObservableCollection<BetSnailsRaces> Snails
        {
            get => _Snails;
            set
            {
                if (_Snails != value)
                {
                    _Snails = value;
                    this.RaisePropertyChanged(nameof(Snails));
                }
            }
        }
        public Races Race
        {
            get => _Race;
            set
            {
                if (_Race != value)
                {
                    _Race = value;
                    this.RaisePropertyChanged(nameof(Race));
                }
            }
        }
        public Circuits Circuit
        {
            get => _Circuit;
            set
            {
                if (_Circuit != value)
                {
                    _Circuit = value;
                    this.RaisePropertyChanged(nameof(Circuit));
                }
            }
        }
        public CourseViewModel(int id) 
        {
            Race = GetRace(id);
            Circuit = GetCircuit(Race.CircuitId);
            Snails = new ObservableCollection<BetSnailsRaces>(GetSnails(Race.id));
            BetCommand = ReactiveCommand.Create<int>(BetOnSnail);
            TextBoxValue = 0;
        }

        private void BetOnSnail(int id)
        {
            HasBet = true;
            Bet = Snails.First(x => x.idSnail == id);
        }

        private Races GetRace(int id)
        {
            using(var context = new DatabaseConnection())
            {
                return context.Races.First(x => x.id == id);
            }
        }

        private Circuits GetCircuit(int id)
        {
            using (var context = new DatabaseConnection())
            {
                return context.Circuits.First(x => x.id == id);
            }
        }

        private List<BetSnailsRaces> GetSnails(int id)
        {
            List<BetSnailsRaces> snails = new();
            using (var context = new DatabaseConnection())
            {
                var snailsParticipating = context.SnailParticipatingRace.Where(x => x.RacesId == id).ToList();
                foreach (var snail in snailsParticipating)
                {
                    var a = context.Snails.Single(x => x.id == snail.SnailsId);
                    var b = context.Snails.Where(x => x.id != snail.SnailsId).ToList();
                    var averageRanks = AverageRanks(b);
                    var Odds = GetOdds(a.general_rank, averageRanks);
                    var betsnailsraces = new BetSnailsRaces { idSnail = a.id, name = a.name, general_rank = a.general_rank, RankingRace = snail.Ranking, BetOdds = Odds };
                    snails.Add(betsnailsraces);
                }
            }
            return snails;
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

        public void SetGain(double value)
        {
            double ValueAndOdd = value * Bet.BetOdds;
            PlacedMoney = Convert.ToInt32(value);
            Gain = Math.Round(ValueAndOdd, 2);
        }

        public void SaveBet()
        {
            var user = UserService.GetInstance();
            if (user.IsAuthentifiedAsUser && user.Credits - PlacedMoney >= 0 && PlacedMoney > 0)
            {
                try
                {
                    using(var context = new DatabaseConnection())
                    {
                        DateTime DateTimeNow = DateTime.Now;
                        DateTime Datetime = DateTimeNow.AddHours(1);
                        Bets bets = new Bets
                        {
                            Date_Bet = Datetime.ToUniversalTime(),
                            Gains = Gain,
                            Has_Won = false,
                            Odds = Bet.BetOdds,
                            SnailId = Bet.idSnail,
                            UserId = UserService.Email,
                            RaceId = Race.id
                        };
                        context.Bets.Add(bets);
                        context.SaveChanges();

                        Users users = context.Users.First(x => x.Email == UserService.Email);
                        if (users != null)
                        {
                            users.Credits -= PlacedMoney;
                            user.Credits -= PlacedMoney;
                            context.Update(users);
                            context.SaveChanges();
                        }
                    }
                    Alert = "Votre pari a bien été pris en compte.";
                }
                catch
                {
                    Alert = "Vous avez déjà parié sur un escargot de cette course.";
                }
            }
            else
            {
                if (!user.IsAuthentifiedAsUser)
                {
                    MainWindowViewModel.GetInstance().ShowControl(typeof(LoginControl));
                }
                if (PlacedMoney <= 0)
                {
                    Alert = "Le montant du pari doit être positif.";
                }
                else
                {
                    Alert = "Vous n'avez pas assez de crédits.";
                }
            }
        }
    }
}
