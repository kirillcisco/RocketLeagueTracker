using Common;
using Common.Models;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Tracker
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public MainViewModel()
        {
            _users = new ObservableCollection<TrackedUserViewModel>() { };
        }

        private DateTime? lastUpdate;
        public DateTime? LastUpdate { get => lastUpdate; set { if (lastUpdate != value) { lastUpdate = value; NotifyPropertyChanged(); } } }

        private ObservableCollection<TrackedUserViewModel> _users;
        public ObservableCollection<TrackedUserViewModel> Users
        {
            get
            {
                _users.Sort();
                return _users;
            }
            set
            {
                _users = value;
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        private TrackedUserViewModel LoadTestData()
        {
            var user = new TrackedUserViewModel()
            {
                Avatar = new Uri("https://steamcdn-a.akamaihd.net/steamcommunity/public/images/avatars/72/72b95f57694cf85050eb243fb798265221eb8600_full.jpg"),
                Name = "W33p",
                OnesMatchesPlayed = 6,
                OnesMmr = "993",
                OnesPic = ImageManager.Instance().GetImageFromUri("https://trackercdn.com/cdn/tracker.gg/rocket-league/ranks/s4-18.png"),
                OnesTitle = "Unranked",
                OnesUri = new Uri("https://trackercdn.com/cdn/tracker.gg/rocket-league/ranks/s4-18.png"),
                Platform = Platform.Pc,
                PlayerUri = new Uri("https://rocketleague.tracker.network/rocket-league/profile/steam/76561198030712005"),
                ThreesMatchesPlayed = 1291,
                ThreesMmr = "1620",
                ThreesPic = ImageManager.Instance().GetImageFromUri("https://trackercdn.com/cdn/tracker.gg/rocket-league/ranks/s4-18.png"),
                ThreesUri = new Uri("https://trackercdn.com/cdn/tracker.gg/rocket-league/ranks/s4-18.png"),
                ThreesTitle = "Grand Champion 1",
                TwosMatchesPlayed = 138,
                TwosMmr = "1524",
                TwosPic = ImageManager.Instance().GetImageFromUri("https://trackercdn.com/cdn/tracker.gg/rocket-league/ranks/s4-18.png"),
                TwosTitle = "Grand Champion 1",
                TwosUri = new Uri("https://trackercdn.com/cdn/tracker.gg/rocket-league/ranks/s4-18.png"),
                UserId = 76561198030712005

            };

            return user;
        }


    }

}
