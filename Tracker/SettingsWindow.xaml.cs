using System;
using Common;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Configuration;

namespace Tracker
{
    /// <summary>
    /// Логика взаимодействия для SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        private readonly string dataLocation = Constants.SaveLocation; // TODO: Dont use Constants!

        private SettingsManager _SettingsManager;

        static void ReadAppSettings() // i dont fucking know, why i cant use this in SettingsManager.cs, i have error CS1069. ("System.Configuration;" is used)
        {
            try
            {
                System.Configuration.Configuration appConfig = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None); // get config file
                System.Configuration.AppSettingsSection appSettings = (System.Configuration.AppSettingsSection)appConfig.GetSection("trackerSettings"); // get the config section "trackerSettings"

                if (appSettings.Settings.Count != 0)
                {
                    foreach (string key in appSettings.Settings.AllKeys)
                    {
                        string settingsValue = appSettings.Settings[key].Value;
                    }
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("Exception raised: {0}", e.Message);
            }
        }

        public SettingsWindow()
        {
            InitializeComponent();
            FilesPathBox.Text = dataLocation;
            _SettingsManager = new SettingsManager();

        }

        private void DataDeleteButton_Click(object sender, RoutedEventArgs e)
        {
            _SettingsManager.DataDelete();
        }

    }
}
