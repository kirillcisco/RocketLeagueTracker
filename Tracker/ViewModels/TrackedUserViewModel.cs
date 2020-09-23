using Common.Search;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Common.Models
{
    public class TrackedUserViewModel : INotifyPropertyChanged
    {
        private string name;
        private long userId;
        private Platform platform;
        private Uri avatar;
        private Uri threesUri;
        private byte[] threesPic;
        private string threesTitle;
        private string threesMmr;
        private double? threesMatchesPlayed;
        private Uri twosUri;
        private byte[] twosPic;
        private string twosTitle;
        private string twosMmr;
        private double? twosMatchesPlayed;
        private Uri onesUri;
        private byte[] onesPic;
        private string onesTitle;
        private string onesMmr;
        private double? onesMatchesPlayed;

        public string Name { get => name; set { if (name != value) { name = value; NotifyPropertyChanged(); } } }
        public long UserId { get => userId; set { if (userId != value) { userId = value; NotifyPropertyChanged(); } } }
        public Platform Platform { get => platform; set { if (platform != value) { platform = value; NotifyPropertyChanged(); } } }
        public Uri Avatar { get => avatar; set { if (avatar != value) { avatar = value; NotifyPropertyChanged(); } } }

        public Uri ThreesUri { get => threesUri; set { if (threesUri != value) { threesUri = value; NotifyPropertyChanged(); } } }
        public byte[] ThreesPic { get => threesPic; set { if (threesPic != value) { threesPic = value; NotifyPropertyChanged(); } } }
        public string ThreesTitle { get => threesTitle; set { if (threesTitle != value) { threesTitle = value; NotifyPropertyChanged(); } } }
        public string ThreesMmr { get => threesMmr; set { if (threesMmr != value) { threesMmr = value; NotifyPropertyChanged(); } } }
        public double? ThreesMatchesPlayed { get => threesMatchesPlayed; set { if (threesMatchesPlayed != value) { threesMatchesPlayed = value; NotifyPropertyChanged(); } } }

        public Uri TwosUri { get => twosUri; set { if (twosUri != value) { twosUri = value; NotifyPropertyChanged(); } } }
        public byte[] TwosPic { get => twosPic; set { if (twosPic != value) { twosPic = value; NotifyPropertyChanged(); } } }
        public string TwosTitle { get => twosTitle; set { if (twosTitle != value) { twosTitle = value; NotifyPropertyChanged(); } } }
        public string TwosMmr { get => twosMmr; set { if (twosMmr != value) { twosMmr = value; NotifyPropertyChanged(); } } }
        public double? TwosMatchesPlayed { get => twosMatchesPlayed; set { if (twosMatchesPlayed != value) { twosMatchesPlayed = value; NotifyPropertyChanged(); } } }

        public Uri OnesUri { get => onesUri; set { if (onesUri != value) { onesUri = value; NotifyPropertyChanged(); } } }
        public byte[] OnesPic { get => onesPic; set { if (onesPic != value) { onesPic = value; NotifyPropertyChanged(); } } }
        public string OnesTitle { get => onesTitle; set { if (onesTitle != value) { onesTitle = value; NotifyPropertyChanged(); } } }
        public string OnesMmr { get => onesMmr; set { if (onesMmr != value) { onesMmr = value; NotifyPropertyChanged(); } } }
        public double? OnesMatchesPlayed { get => onesMatchesPlayed; set { if (onesMatchesPlayed != value) { onesMatchesPlayed = value; NotifyPropertyChanged(); } } }

        public Uri TournamentsUri { get; set; }
        public byte[] TournamentsPic { get; set; }
        public string TournamentsTitle { get; set; }
        public double? TournamentsMmr { get; set; }



        public event PropertyChangedEventHandler PropertyChanged;
        // This method is called by the Set accessor of each property.  
        // The CallerMemberName attribute that is applied to the optional propertyName  
        // parameter causes the property name of the caller to be substituted as an argument.  
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
