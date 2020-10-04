using Common;
using Common.Models;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    }

}
