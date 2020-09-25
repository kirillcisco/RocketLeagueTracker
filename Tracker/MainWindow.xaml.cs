using Common.Models;
using Common.Models.Search;
using Common.Search;
using MahApps.Metro.Controls;
using Squirrel;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Tracker
{
    public partial class MainWindow : MetroWindow
    {
        private TrackedUsersManager _trackedUsersManager;
        private RlTracker _tracker;
        public MainViewModel vm = new MainViewModel();

        public MainWindow()
        {
            InitializeComponent();

            _tracker = new RlTracker();

            _trackedUsersManager = new TrackedUsersManager(_tracker);
            _trackedUsersManager.Users.CollectionChanged += Users_CollectionChanged;
            _trackedUsersManager.Start();

            CachedImage.FileCache.AppCacheMode = CachedImage.FileCache.CacheMode.Dedicated;

            //set viewmodel 
            this.DataContext = vm;

            CheckForUpdates();
        }

        private void CheckForUpdates()
        {
            //https://github.com/Squirrel/Squirrel.Windows/blob/develop/docs/using/github.md
            Task.Run(async () =>
            {
                using (var mgr = UpdateManager.GitHubUpdateManager("https://github.com/kevinLay7/RocketLeagueTracker"))
                {
                    var a = await mgr.Result.CheckForUpdate();
                    await mgr.Result.UpdateApp();
                }
            });
        }

        //todo move the searchdata to a viewmodel

        //I'm sure theres a better place for this
        private void Users_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine($"Event fired {e.Action.ToString()}");
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
                    var newInstance = new TrackedUserViewModel();
                    ViewModelMapper.TrackedUser(item, ref newInstance);
                    vm.Users.Add(newInstance);
                }
            }
            if (e.Action == NotifyCollectionChangedAction.Replace) 
            {
                foreach (TrackedUser item in e.NewItems)
                {
                    var instance = vm.Users.FirstOrDefault(vm => vm.UserId == item.UserId);
                    if(instance == null)
                    {
                        instance = new TrackedUserViewModel();
                        vm.Users.Add(instance);
                    }
                    ViewModelMapper.TrackedUser(item, ref instance);
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
            var userModel = ((FrameworkElement)sender).DataContext as TrackedUserViewModel;
            _trackedUsersManager.Remove(userModel.UserId.ToString());
        }

        private void ForceRefreshButton_Click(object sender, RoutedEventArgs e)
        {
            _trackedUsersManager.ForceRefresh();
        }
    }
}
