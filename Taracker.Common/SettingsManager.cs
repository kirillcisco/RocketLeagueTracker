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
    public class SettingsManager
    {
        private readonly string CacheFolder = Constants.SaveLocation + "Cache";
        private readonly string trackedUsersFile = Constants.SaveLocation + "tracked.json";
        private readonly string userSettingsFile = Constants.SaveLocation + "userSettings.json";


        public async void Start()
        {
            Load();
        }

        public async void DataDelete()
        {
            Delete();
        }


        private void Load()
        {
            if (!System.IO.File.Exists(userSettingsFile))
                return;
            var SettingsString = System.IO.File.ReadAllText(userSettingsFile);

            if (string.IsNullOrEmpty(SettingsString))
                return;
        }

        private void Save()
        {
            try
            {
                string SettingsString = null; // TODO
                System.IO.File.WriteAllText(trackedUsersFile, SettingsString);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
        }
        private void Delete()
        {
            try
            {
                System.IO.File.Delete(trackedUsersFile);
                System.IO.File.Delete(CacheFolder);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
        }
    }
}
