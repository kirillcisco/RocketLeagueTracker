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
        public static AppSettings Load()
        {
            AppSettings output = null;
            if (System.IO.File.Exists(settingsFilePath))
            {
                var text = System.IO.File.ReadAllText(settingsFilePath);
                try
                {

                    JObject allJson = JObject.Parse(text);
                    var section = allJson["AppSettings"].ToString();
                    output = JsonConvert.DeserializeObject<AppSettings>(section);
                }
                catch (Exception)
                {
                    //error parsing the settings.  Wipe it and rebuild.
                    output = new AppSettings();
                    System.IO.File.WriteAllText(settingsFilePath, JsonConvert.SerializeObject(output));
                }
            }

            return output;
        }

        private void SetDefaults()
        {
            CacheFolderLocation = Path.Combine(defaultFilePath, "Cache");
            SaveFolderLocation = defaultFilePath;
            AutoUpdate = false;
            RefreshMins = 5;
            MinimizeToTray = false;
        }

        private void TrySave<T>(Expression<Func<T>> expression)
        {
            PropertyInfo property = ((MemberExpression)expression.Body).Member as PropertyInfo;
            if (property != null)
            {
                //If the settings file doesn't exist, create it.
                if (!System.IO.File.Exists(settingsFilePath))
                {
                    System.IO.File.WriteAllText(settingsFilePath, JsonConvert.SerializeObject(this));               
                }

                string json = File.ReadAllText(settingsFilePath);
                dynamic jsonObj = Newtonsoft.Json.JsonConvert.DeserializeObject(json);
                var section = jsonObj["AppSettings"];

                if (section == null)
                {
                    jsonObj.AppSettings = new JObject();
                    section = jsonObj.AppSettings;
                }

                var current = section[property.Name];
                if (current == null)
                {
                    section[property.Name] = new JObject();
                    current = section[property.Name];
                }
                var currentValue = current.Value;

                var newValue = property.GetValue(this, null) as dynamic;
                if (!object.Equals(currentValue, newValue))
                {
                    section[property.Name] = newValue;

                    string output = JsonConvert.SerializeObject(jsonObj, Formatting.Indented);
                    System.IO.File.WriteAllText(settingsFilePath, output);
                    System.Diagnostics.Debug.WriteLine($"Save");
                }
            }
        }

        private string _cacheFolderLocation;
        public string CacheFolderLocation
        {
            get
            {
                if (string.IsNullOrEmpty(_cacheFolderLocation))
                {
                    CacheFolderLocation = defaultFilePath;
                }

                return _cacheFolderLocation;
            }
            set
            {
                if (_cacheFolderLocation != value)
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
                if (_autoUpdate.HasValue == false)
                {
                    return false;
                }
                return _autoUpdate.Value;
            }
            set
            {
                if (_autoUpdate != value)
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
                if (_refreshMins != value)
                {
                    _refreshMins = value;
                    TrySave(() => RefreshMins);
                }
            }
        }

        public bool? _minimizedToTray;
        public bool? MinimizeToTray
        {
            get
            {
                if (_minimizedToTray.HasValue == false)
                {
                    return false;
                }
                return _minimizedToTray;
            }
            set
            {
                if (_minimizedToTray != value)
                {
                    _minimizedToTray = value;
                    TrySave(() => MinimizeToTray);
                }
            }
        }

        public bool? _useDarkMode;
        public bool? UseDarkMode
        {
            get
            {
                if(_useDarkMode.HasValue == false)
                {
                    return true;
                }
                return _useDarkMode;
            }
            set
            {
                if(_useDarkMode != value)
                {
                    _useDarkMode = value;
                    TrySave(() => UseDarkMode);
                }
            }
        }

        public string _color;
        public string Color
        {
            get
            {
                if (string.IsNullOrEmpty(_color))
                {
                    return "Cobalt";
                }
                return _color;
            }
            set
            {
                if (_color != value)
                {
                    _color = value;
                    TrySave(() => Color);
                }
            }
        }
    }
}
