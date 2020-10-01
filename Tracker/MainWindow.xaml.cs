using Common.Models;
using Common.Models.Search;
using Common.Search;
using ControlzEx.Theming;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Onova;
using Onova.Services;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Tracker
{
    public partial class MainWindow : MetroWindow
    {
        private TrackedUsersManager _trackedUsersManager;
        private RlTracker _tracker;
        public MainViewModel vm = new MainViewModel();
        private AppSettings _settings;
        private UpdateManager _manager;

        public MainWindow(IOptions<AppSettings> settings)
        {
            _settings = settings.Value;

            InitializeComponent();
            GithubButton.Content = "v" + Assembly.GetExecutingAssembly().GetName().Version.ToString();

            _tracker = new RlTracker();
            _trackedUsersManager = new TrackedUsersManager(_tracker, _settings);
            _trackedUsersManager.Users.CollectionChanged += Users_CollectionChanged;
            _trackedUsersManager.Start();

            CachedImage.FileCache.AppCacheMode = CachedImage.FileCache.CacheMode.Dedicated;

            this.DataContext = vm;
            CheckForUpdates();
        }

        private async void CheckForUpdates()
        {
            if (!Debugger.IsAttached)
            {
                //todo modify so that we check for updates on a cycle and enable an update button to launch the update when its ready
                _manager = new Onova.UpdateManager(new GithubPackageResolver("kevinlay7", "RocketLeagueTracker", "*.zip"), new ZipPackageExtractor());
                var check = await _manager.CheckForUpdatesAsync();

                if (!check.CanUpdate)
                {
                    return;
                }

                UpdateButton.Visibility = Visibility.Visible;
            }
        }

        private async void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            await this.ShowProgressAsync("Updating", "The window will disapper and it may take a minute beofre it re-opens.  Do not manually re-launch!");
            await _manager.CheckPerformUpdateAsync();
        }

        private void Users_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (TrackedUser item in e.OldItems)
                {
                    var instance = vm.Users.FirstOrDefault(vm => vm.UserId == item.UserId);
                    if (instance != null)
                    {
                        vm.Users.Remove(instance);
                    }
                }
            }
            else if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (TrackedUser item in e.NewItems)
                {
                    var instance = vm.Users.FirstOrDefault(vm => vm.UserId == item.UserId);
                    if (instance != null)
                        return;

                    var newInstance = new TrackedUserViewModel();
                    ViewModelMapper.TrackedUser(item, newInstance);
                    vm.Users.Add(newInstance);
                }
            }
            if (e.Action == NotifyCollectionChangedAction.Replace)
            {
                foreach (TrackedUser item in e.NewItems)
                {
                    var instance = vm.Users.FirstOrDefault(vm => vm.UserId == item.UserId);
                    if (instance == null)
                    {
                        instance = new TrackedUserViewModel();
                        vm.Users.Add(instance);
                    }
                    ViewModelMapper.TrackedUser(item, instance);
                }
            }
            if(e.Action == NotifyCollectionChangedAction.Move)
            {
                foreach (TrackedUser item in e.NewItems)
                {
                    var instance = vm.Users.FirstOrDefault(vm => vm.UserId == item.UserId);
                    if (instance == null)
                    {
                        instance = new TrackedUserViewModel();
                        vm.Users.Add(instance);
                    }
                    ViewModelMapper.TrackedUser(item, instance);
                }
            }
            vm.LastUpdate = DateTime.Now;

        }

        private async void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            SearchButton.IsEnabled = false;
            Spinner.IsActive = true;

            var userOrId = UsernameTextBox.Text.Trim();
            var selectedText = ((System.Windows.Controls.ComboBoxItem)PlatformDropdown.SelectedValue).Content.ToString();

            try
            {
                var users = await _tracker.Search(userOrId, RlTracker.GetPlatform(selectedText));

                foreach (var user in users)
                {
                    user.IsTracked = !_trackedUsersManager.IsTracked(user.PlatformUserIdentifier);
                }

                SearchResults.ItemsSource = users;
            }
            catch (Exception)
            {

            }
            finally
            {
                SearchResults.Visibility = Visibility.Visible;
                TrackedGrid.Visibility = Visibility.Hidden;
                SearchButton.IsEnabled = true;
                Spinner.IsActive = false;
            }
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            SettingsWindow settingswindow = new SettingsWindow(_settings);
            settingswindow.Owner = GetWindow(this);
            try
            {
                settingswindow.Show();
            }
            catch (Exception)
            {

            }
            finally
            {

            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var searchData = ((FrameworkElement)sender).DataContext as SearchData;

            if (searchData != null && searchData.PlatformSlug != null && searchData.PlatformUserIdentifier != null)
                _trackedUsersManager.Add(searchData.PlatformUserIdentifier, searchData.PlatformSlug);

            ((System.Windows.Controls.Button)sender).IsEnabled = false;
        }

        private void SearchCloseButton_Click(object sender, RoutedEventArgs e)
        {
            SearchResults.ItemsSource = new List<SearchData>();
            SearchResults.Visibility = Visibility.Hidden;
            TrackedGrid.Visibility = Visibility.Visible;
        }

        private void RemoveTrackedUserButton_Click(object sender, RoutedEventArgs e)
        {
            var selected = TrackedUserGrid.SelectedItem as TrackedUserViewModel;
            _trackedUsersManager.Remove(selected.UserId.ToString());
        }

        private void ForceRefreshButton_Click(object sender, RoutedEventArgs e)
        {
            _trackedUsersManager.ForceRefresh();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = TrackedUserGrid.SelectedItem as TrackedUserViewModel;

            if (selectedItem != null)
            {
                var url = selectedItem.PlayerUri.ToString();
                try
                {
                    System.Diagnostics.Process.Start(url);
                }
                catch (System.Exception other)
                {
                    // hack because of this: https://github.com/dotnet/corefx/issues/10361
                    if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                    {
                        url = url.Replace("&", "^&");
                        Process.Start(new ProcessStartInfo("cmd", $"/c start {url}") { CreateNoWindow = true });
                    }
                    else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                    {
                        Process.Start("xdg-open", url);
                    }
                    else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                    {
                        Process.Start("open", url);
                    }
                    else
                    {
                        MessageBox.Show(other.ToString());
                    }
                }
            }
        }

        private void LaunchGitHubSite(object sender, RoutedEventArgs e)
        {
            var url = "https://github.com/kevinLay7/RocketLeagueTracker";
            try
            {
                System.Diagnostics.Process.Start(url);
            }
            catch (System.Exception other)
            {
                // hack because of this: https://github.com/dotnet/corefx/issues/10361
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    url = url.Replace("&", "^&");
                    Process.Start(new ProcessStartInfo("cmd", $"/c start {url}") { CreateNoWindow = true });
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    Process.Start("xdg-open", url);
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                {
                    Process.Start("open", url);
                }
                else
                {
                    MessageBox.Show(other.ToString());
                }
            }
        }



        private void MoveUp_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = TrackedUserGrid.SelectedItem as TrackedUserViewModel;
            if (selectedItem != null)
                _trackedUsersManager.ShiftUp(selectedItem.UserId);
        }

        private void MoveDown_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = TrackedUserGrid.SelectedItem as TrackedUserViewModel;
            if (selectedItem != null)
                _trackedUsersManager.ShiftDown(selectedItem.UserId);
        }

        private void TargetUpdated(object sender, DataTransferEventArgs e)
        {
            var accentColor = ThemeManager.Current.DetectTheme().PrimaryAccentColor;
            dynamic item = sender;
            ColorAnimation ca = new ColorAnimation(Colors.White, new Duration(TimeSpan.FromSeconds(10)));
            item.Foreground = new SolidColorBrush(accentColor);
            item.Foreground.BeginAnimation(SolidColorBrush.ColorProperty, ca);
        }
    }
}
