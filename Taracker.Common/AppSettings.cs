using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Linq.Expressions;
using System.Reflection;

namespace Tracker
{
    public class AppSettings
    {
        private static string defaultFilePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\RLTracker\\";
        public static readonly string settingsFilePath = Path.Combine(defaultFilePath, "appSettings.json");

        public AppSettings()
        {
            //If the settings file doesn't exist, create it.
            if (!System.IO.File.Exists(settingsFilePath))
            {
                SetDefaults();
                System.IO.File.WriteAllText(settingsFilePath, JsonConvert.SerializeObject(this));
            }
        }
        
        private void SetDefaults()
        {
            CacheFolderLocation = Path.Combine(defaultFilePath, "Cache");
            SaveFolderLocation = defaultFilePath;
            AutoUpdate = false;
            RefreshMins = 5;
        }


        private void TrySave<T>(Expression<Func<T>> expression)
        {
            PropertyInfo property = ((MemberExpression)expression.Body).Member as PropertyInfo;
            if(property != null)
            {
                string json = File.ReadAllText(settingsFilePath);
                dynamic jsonObj = Newtonsoft.Json.JsonConvert.DeserializeObject(json);
                var section = jsonObj["AppSettings"];

                if(section == null)
                {
                    jsonObj.AppSettings = new JObject();
                    section = jsonObj.AppSettings;
                }

                var current = section[property.Name];
                if(current == null)
                {
                    section[property.Name] = new JObject();
                    current = section[property.Name];
                }
                var currentValue = current.Value;

                var newValue = property.GetValue(this, null) as dynamic;
                if(object.Equals(currentValue, newValue))
                {
                    section[property.Name] = newValue;

                    string output = JsonConvert.SerializeObject(jsonObj, Formatting.Indented);
                    System.IO.File.WriteAllText(settingsFilePath, output);
                }
            }
        }

        private string _cacheFolderLocation;
        public string CacheFolderLocation {
            get {
                if (string.IsNullOrEmpty(_cacheFolderLocation))
                {
                    CacheFolderLocation = defaultFilePath;
                }

                return _cacheFolderLocation;
            }
            set
            {
                if(_cacheFolderLocation != value)
                {
                    _cacheFolderLocation = value;
                    TrySave(() => CacheFolderLocation);
                }
            }
        }

        private string _saveFolderLocation;
        public string SaveFolderLocation
        {
            get
            {
                if (string.IsNullOrEmpty(_saveFolderLocation))
                {
                    SaveFolderLocation = defaultFilePath;
                }

                return _saveFolderLocation;
            }
            set
            {
                if (_saveFolderLocation != value)
                {
                    _saveFolderLocation = value;
                    TrySave(() => SaveFolderLocation);
                }
            }
        }

        private bool? _autoUpdate;
        public bool AutoUpdate
        {
            get
            {
                if(_autoUpdate.HasValue == false)
                {
                    return false;
                }
                return _autoUpdate.Value;
            }
            set
            {
                if(_autoUpdate != value)
                {
                    _autoUpdate = value;
                    TrySave(() => AutoUpdate);
                }
            }
        }

        public int? _refreshMins;
        public int? RefreshMins
        {
            get
            {
                if (_refreshMins.HasValue == false)
                {
                    return 5;
                }
                return _refreshMins;
            }
            set
            {
                if(_refreshMins != value)
                {
                    _refreshMins = value;
                    TrySave(() => RefreshMins);
                }
            }
        }
    }
}
