using Common.Models;
using MahApps.Metro.Controls;
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
            Users = new ObservableCollection<TrackedUserViewModel>() { };
        }

        private DateTime? lastUpdate;
        public DateTime? LastUpdate { get => lastUpdate; set { if (lastUpdate != value) { lastUpdate = value; NotifyPropertyChanged(); } } }

        public ObservableCollection<TrackedUserViewModel> Users { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
