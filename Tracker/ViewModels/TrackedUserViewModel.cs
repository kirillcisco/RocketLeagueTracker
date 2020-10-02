using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Dynamic;
using System.Runtime.CompilerServices;

namespace Common.Models
{
    //Todo create specific objects for the various related game modes
    public class TrackedUserViewModel : INotifyPropertyChanged, IComparable
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
        private Uri playerUri;
        private string casualMmr;
        private double? casualMatchesPlayed;

        public string Name { get => name; set { if (name != value) { name = value; NotifyPropertyChanged(); } } }
        public long UserId { get => userId; set { if (userId != value) { userId = value; NotifyPropertyChanged(); } } }
        public Platform Platform { get => platform; set { if (platform != value) { platform = value; NotifyPropertyChanged(); } } }
        public Uri Avatar { get => avatar; set { if (avatar != value) { avatar = value; NotifyPropertyChanged(); } } }

        public Uri CasualUri { get => new Uri("https://trackercdn.com/cdn/tracker.gg/rocket-league/ranks/s4-0.png"); }
        public byte[] CasualPic { get => ImageManager.Instance().GetImageFromUri(CasualUri.ToString()); }
        public string CasualTitle { get => "Un-Ranked"; }
        public string CasualMmr { get => casualMmr; set { if (casualMmr != value) { casualMmr = value; NotifyPropertyChanged(nameof(CasualMmr)); } } }
        public double? CasualMatchesPlayed { get => casualMatchesPlayed; set { if (casualMatchesPlayed != value) { casualMatchesPlayed = value; NotifyPropertyChanged(nameof(CasualMatchesPlayed)); } } }

        public Uri ThreesUri { get => threesUri; set { if (threesUri != value) { threesUri = value; NotifyPropertyChanged(nameof(ThreesUri)); } } }
        public byte[] ThreesPic { get => threesPic; set { if (threesPic != value) { threesPic = value; NotifyPropertyChanged(nameof(ThreesPic)); } } }
        public string ThreesTitle { get => threesTitle; set { if (threesTitle != value) { threesTitle = value; NotifyPropertyChanged(nameof(ThreesTitle)); } } }
        public string ThreesMmr { get => threesMmr; set { if (threesMmr != value) { threesMmr = value; NotifyPropertyChanged(nameof(ThreesMmr)); } } }
        public double? ThreesMatchesPlayed { get => threesMatchesPlayed; set { if (threesMatchesPlayed != value) { threesMatchesPlayed = value; NotifyPropertyChanged(nameof(ThreesMatchesPlayed)); } } }

        public Uri TwosUri { get => twosUri; set { if (twosUri != value) { twosUri = value; NotifyPropertyChanged(nameof(TwosUri)); } } }
        public byte[] TwosPic { get => twosPic; set { if (twosPic != value) { twosPic = value; NotifyPropertyChanged(nameof(TwosPic)); } } }
        public string TwosTitle { get => twosTitle; set { if (twosTitle != value) { twosTitle = value; NotifyPropertyChanged(nameof(TwosTitle)); } } }
        public string TwosMmr { get => twosMmr; set { if (twosMmr != value) { twosMmr = value; NotifyPropertyChanged(nameof(TwosMmr)); } } }
        public double? TwosMatchesPlayed { get => twosMatchesPlayed; set { if (twosMatchesPlayed != value) { twosMatchesPlayed = value; NotifyPropertyChanged(nameof(TwosMatchesPlayed)); } } }

        public Uri OnesUri { get => onesUri; set { if (onesUri != value) { onesUri = value; NotifyPropertyChanged(nameof(OnesUri)); } } }
        public byte[] OnesPic { get => onesPic; set { if (onesPic != value) { onesPic = value; NotifyPropertyChanged(nameof(OnesPic)); } } }
        public string OnesTitle { get => onesTitle; set { if (onesTitle != value) { onesTitle = value; NotifyPropertyChanged(nameof(OnesTitle)); } } }
        public string OnesMmr { get => onesMmr; set { if (onesMmr != value) { onesMmr = value; NotifyPropertyChanged(nameof(OnesMmr)); } } }
        public double? OnesMatchesPlayed { get => onesMatchesPlayed; set { if (onesMatchesPlayed != value) { onesMatchesPlayed = value; NotifyPropertyChanged(nameof(OnesMatchesPlayed)); } } }

        public Uri TournamentsUri { get; set; }
        public byte[] TournamentsPic { get; set; }
        public string TournamentsTitle { get; set; }
        public double? TournamentsMmr { get; set; }
        public Uri PlayerUri { get => playerUri; set { if (playerUri != value) { playerUri = value; } } }

        private int? sortOrder;
        public int? SortOrder { get { if (sortOrder.HasValue == false) { return 99; } return sortOrder; } set { if (sortOrder != value) { sortOrder = value; NotifyPropertyChanged(nameof(SortOrder)); } } }

        public event PropertyChangedEventHandler PropertyChanged;
        // This method is called by the Set accessor of each property.  
        // The CallerMemberName attribute that is applied to the optional propertyName  
        // parameter causes the property name of the caller to be substituted as an argument.  
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        public int CompareTo(object obj)
        {
            TrackedUserViewModel a = this;
            TrackedUserViewModel b = (TrackedUserViewModel)obj;

            if (a.SortOrder == b.SortOrder)
                return this.UserId.CompareTo(b.UserId);

            return a.SortOrder.Value.CompareTo(b.SortOrder.Value);
        }
    }
}
