using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Dynamic;
using System.Runtime.CompilerServices;
using System.Security.Permissions;

namespace Common.Models
{
    //Todo create specific objects for the various related game modes
    public class TrackedUserViewModel : INotifyPropertyChanged, IComparable
    {
        private string name;
        private long userId;
        private Platform platform;
        private Uri avatar;
        private Uri playerUri;

        public RankViewModel ThreesModel { get; set; }
        public RankViewModel TwosModel { get; set; }
        public RankViewModel OnesModel { get; set; }
        public RankViewModel CasualModel { get; set; }
        public RankViewModel TournamentModel { get; set; }

        public string Name { get => name; set { if (name != value) { name = value; NotifyPropertyChanged(); } } }
        public long UserId { get => userId; set { if (userId != value) { userId = value; NotifyPropertyChanged(); } } }
        public Platform Platform { get => platform; set { if (platform != value) { platform = value; NotifyPropertyChanged(); } } }
        public Uri Avatar { get => avatar; set { if (avatar != value) { avatar = value; NotifyPropertyChanged(); } } }

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

    public class RankViewModel : INotifyPropertyChanged
    {
        private string title;
        private double? mmr;
        private string division;
        private double? divUp;
        private double? divDown;
        private string imageUrl;
        private double? matchesPlayed;

        public string Title { get => title; set { if (title != value) { title = value; NotifyPropertyChanged(nameof(Title)); } } }
        public double? Mmr { get => mmr; set { if (mmr != value) { mmr = value; NotifyPropertyChanged(nameof(Mmr)); } } }
        public string Division { get => division; set  { if (division != value) { division = value; NotifyPropertyChanged(nameof(Division)); } } }
        public double? DivUp { get => divUp; set { if (divUp != value) { divUp = value; NotifyPropertyChanged(nameof(DivUp)); } } }
        public double? DivDown { get => divDown; set  { if (divDown != value) { divDown = value; NotifyPropertyChanged(nameof(DivDown)); } } }
        public string ImageUrl { get => imageUrl; set  { if (imageUrl != value) { imageUrl = value; NotifyPropertyChanged(nameof(ImageUrl)); } } }
        public double? MatchesPlayed { get => matchesPlayed; set { if (matchesPlayed != value) { matchesPlayed = value; NotifyPropertyChanged(nameof(MatchesPlayed)); } } }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            System.Diagnostics.Debug.WriteLine($"{mmr}");
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
