using System.Windows;
using Common.Models;
using MahApps.Metro.Controls;
using Microsoft.Extensions.DependencyInjection;

namespace Tracker
{

    public partial class SettingsWindow : MetroWindow
    {
        private AppSettings _settings;
        public SettingsViewModel vm;

        public SettingsWindow(AppSettings settings)
        {
            _settings = settings;
            InitializeComponent();
            vm = new SettingsViewModel();
            ViewModelMapper.SettingsViewModel(_settings, vm);
            this.DataContext = vm;
        }

        private void DataDeleteButton_Click(object sender, RoutedEventArgs e)
        {
        }

        private void SaveSettingsButton_Click(object sender, RoutedEventArgs e)
        {
            _settings.AutoUpdate = vm.AutoUpdate;
            _settings.SaveFolderLocation = vm.SaveLocation;
            _settings.RefreshMins = vm.RefreshMinutes;
            _settings.MinimizeToTray = vm.MinimizeToTray;
        }
    }
}
