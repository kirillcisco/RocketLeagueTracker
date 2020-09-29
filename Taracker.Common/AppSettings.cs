using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Tracker
{
    public class AppSettings
    {
        private string filePath = Path.Combine(AppContext.BaseDirectory, "appSettings.json");

        private void TrySave<T>(Expression<Func<T>> expression)
        {
            PropertyInfo property = ((MemberExpression)expression.Body).Member as PropertyInfo;
            if(property != null)
            {
                string json = File.ReadAllText(filePath);
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
                    System.IO.File.WriteAllText(filePath, output);
                }
            }
        }

        private string _cacheFolderLocation;
        public string CacheFolderLocation {
            get => _cacheFolderLocation;
            set
            {
                if(_cacheFolderLocation != value)
                {
                    _cacheFolderLocation = value;
                    TrySave(() => CacheFolderLocation);
                }
            }
        }



    }
}
