using System.Windows;
using Common.Models;
using MahApps.Metro.Controls;
using ControlzEx.Theming;

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
            vm.PropertyChanged += Vm_PropertyChanged;
            ViewModelMapper.SettingsViewModel(_settings, vm);

            var appTheme = ThemeManager.Current.DetectTheme(Application.Current);
            vm.SelectedColor = appTheme.ColorScheme;
            vm.UseDarkMode = appTheme.BaseColorScheme == "Dark";

            this.DataContext = vm;
        }

        private void Vm_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if(e.PropertyName == "SelectedColor")
            {
                foreach(Window win in Application.Current.Windows)
                {
                    ThemeManager.Current.ChangeThemeColorScheme(win, vm.SelectedColor);
                }
            }
            if(e.PropertyName == "UseDarkMode")
            {
                foreach (Window win in Application.Current.Windows)
                {
                    ThemeManager.Current.ChangeThemeBaseColor(win, vm.UseDarkMode ? "Dark" : "Light");
                }
            }
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
            _settings.UseDarkMode = vm.UseDarkMode;
            _settings.Color = vm.SelectedColor;
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void AccentColorComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
        }

    }
}
