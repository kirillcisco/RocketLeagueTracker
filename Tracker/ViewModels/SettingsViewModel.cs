using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Tracker
{
    public class SettingsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public bool MinimizeToTray { get; set; }
        public string SaveLocation { get; set; }
        public int RefreshMinutes { get; set; }
        public bool AutoUpdate { get; set; }

        private bool _useDarkMode;
        public bool UseDarkMode
        {
            get => _useDarkMode;
            set
            {
                {
                    if (_useDarkMode != value)
                    {
                        _useDarkMode = value;
                        NotifyPropertyChanged(nameof(UseDarkMode));
                    }
                }
            }
        }

        private string _selectedColor;
        public string SelectedColor
        {
            get => _selectedColor;
            set
            {
                if (_selectedColor != value)
                {
                    _selectedColor = value;
                    NotifyPropertyChanged(nameof(SelectedColor));
                }
            }
        }

        // This method is called by the Set accessor of each property.  
        // The CallerMemberName attribute that is applied to the optional propertyName  
        // parameter causes the property name of the caller to be substituted as an argument.  
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
