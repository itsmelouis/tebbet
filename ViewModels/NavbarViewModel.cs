using Avalonia.Controls.Shapes;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Tebbet.Controls;
using Tebbet.Models;
using Tebbet.Services;

namespace Tebbet.ViewModels
{
    public class NavbarViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private double? _Credits;
        private UserService user {  get; set; }

        public double? Credits
        {
            get => _Credits;
            set
            {
                if (_Credits != value)
                {
                    _Credits = value;
                    this.RaisePropertyChanged(nameof(Credits));
                }
            }
        }

        private string _Live;

        public string Live
        {
            get => _Live;
            set
            {
                if (_Live != value)
                {
                    _Live = value;
                    this.RaisePropertyChanged(nameof(Live));
                }
            }
        }

        private LivesServices LiveService;

        public NavbarViewModel() 
        {
            LiveService = LivesServices.GetInstance();
            Live = "Direct (" + LiveService.RaceLive.ToString() + ")";
            LiveService.onRaceLive += SetLive;
            user = UserService.GetInstance();
            Credits = user.Credits;
            user.CreditsEvent += SetCredits;
        }

        private void SetLive(object sender, EventArgs args)
        {
            Live = "Direct (" + LiveService.RaceLive.ToString() + ")";
        }

        private void SetCredits(object sender, EventArgs e)
        {
            Credits = user.Credits;
        }

        public void Logout()
        {
            if (user.IsAuthentified)
            {
                user.Logout();
            }
        }
    }
}
