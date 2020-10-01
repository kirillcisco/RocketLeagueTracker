using System.Windows;
using Microsoft.Extensions.DependencyInjection;

namespace Tracker
{

    public partial class SettingsWindow : Window
    {
        private AppSettings _settings;

        [ActivatorUtilitiesConstructor]
        public SettingsWindow(AppSettings settings)
        {
            _settings = settings;

            InitializeComponent();
            FilesPathBox.Text = _settings.SaveFolderLocation;
        }

        private void DataDeleteButton_Click(object sender, RoutedEventArgs e)
        {
        }

    }
}
