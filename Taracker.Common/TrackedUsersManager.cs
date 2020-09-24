using Common;
using Common.Models;
using Common.Search;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Tracker
{
    /// <summary>
    /// Object containing tracked users and is responsible for maintainging them
    /// </summary>
    public class TrackedUsersManager
    {
        private readonly string trackedUsersFile = Constants.SaveLocation + "tracked.json";
        public ObservableCollection<TrackedUser> Users = new ObservableCollection<TrackedUser>();
        private RlTracker _searcher;
        private CancellationTokenSource _tokenSource;
        private SynchronizationContext _context;

        #region Constructors

        /// <summary>
        /// Manager for tracked users
        /// </summary>
        /// <param name="searcher">Instance used to Update tracked users</param>
        public TrackedUsersManager(RlTracker searcher) : base()
        {
            _searcher = searcher;
            _tokenSource = new CancellationTokenSource();
            _context = SynchronizationContext.Current;
        }

        #endregion

        #region Public

        /// <summary>
        /// Start background refreshing
        /// </summary>
        public async void Start()
        {
            Load();
            var token = _tokenSource.Token;
            await BackgroundProcessing(token);
        }

        /// <summary>
        /// Stop background refreshing
        /// </summary>
        public void Stop()
        {
            _tokenSource.Cancel();
        }

        /// <summary>
        /// Track a new user
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="platform"></param>
        public async void Add(string userId, string platform)
        {
            var plat = RlTracker.GetPlatform(platform);

            var trackedUser = new TrackedUser()
            {
                PlatForm = plat,
                UserId = long.Parse(userId),
            };

            await RefreshUser(trackedUser);

            if (!Users.Any(x => x.UserId == trackedUser.UserId))
                Users.Add(trackedUser);

            Save();
        }

        /// <summary>
        /// Remove a tracked user
        /// </summary>
        /// <param name="userId"></param>
        public void Remove(string userId)
        {
            var l = long.Parse(userId);
            if (!Users.Any(x => x.UserId == l))
                return;

            Users.Remove(Users.First(x => x.UserId == l));

            Save();
        }

        /// <summary>
        /// For retrieving the latest info from TRN
        /// </summary>
        public void ForceRefresh()
        {
            RefreshTrackedUsers(true);
        }

        /// <summary>
        /// Check if a user is already being tracked based on the userId
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool IsTracked(string userId)
        {
            var l = long.Parse(userId);
            return Users.Any(x => x.UserId == l);
        }

        #endregion

        #region Persistence

        private void Load()
        {
            if (!System.IO.File.Exists(trackedUsersFile))
                return;
            var settingString = System.IO.File.ReadAllText(trackedUsersFile);

            if (string.IsNullOrEmpty(settingString))
                return;

            Users.Clear();

            var users = JsonConvert.DeserializeObject<List<TrackedUser>>(settingString);

            foreach (var user in users)
            {
                Users.Add(user);
            }
        }

        private void Save()
        {
            try
            {
                var stringInfo = JsonConvert.SerializeObject(Users);
                System.IO.File.WriteAllText(trackedUsersFile, stringInfo);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
        }

        #endregion

        #region Private

        private void RefreshTrackedUsers(bool force = false)
        {
            var temp = Users.ToArray();

            for (int i = 0; i < temp.Length - 1; i++)
            {
                var user = temp[i];
                if (user.LastUpdate.HasValue && user.LastUpdate.Value <= DateTime.Now.AddMinutes(-5) || force)
                {
                    System.Diagnostics.Debug.WriteLine($"{user.LastUpdate} - {DateTime.Now.AddMinutes(-5)}");
                    try
                    {
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
                        Task.Run(async () =>
                        {
                            await RefreshUser(user);
                        });
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine($"Error refreshing user: {ex.ToString()}");
                    }

                    _context.Send(x => Users[i] = user, null);
                    Save();
                }

            }
        }

        private Task BackgroundProcessing(CancellationToken token)
        {
            return Task.Run(async () =>
            {
                while (!token.IsCancellationRequested)
                {
                    RefreshTrackedUsers();
                    await Task.Delay(1000);
                }
            }, token);
        }

        private async Task RefreshUser(TrackedUser user)
        {
            user.Data = await _searcher.Get(user.UserId, user.PlatForm);
            user.LastUpdate = DateTime.Now;
        }

        #endregion
    }



}
