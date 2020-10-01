using System;
using System.Collections.Generic;
using System.Text;

namespace Tracker
{
    public class SettingsViewModel
    {
        public bool MinimizeToTray { get; set; }
        public string SaveLocation { get; set; }
        public int RefreshMinutes { get; set; }
        public bool AutoUpdate { get; set; }
    }
}
