using Common;
using Common.Models;
using ControlzEx.Theming;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;

namespace Tracker
{
    /// <summary>
    /// Interaction logic for RankDispaly.xaml
    /// </summary>
    public partial class RankDispaly : UserControl, INotifyPropertyChanged
    {
        public static readonly DependencyProperty RankProperty =
            DependencyProperty.Register("Rank", typeof(RankViewModel), typeof(RankDispaly));

        public event PropertyChangedEventHandler PropertyChanged;

        public RankDispaly()
        {
            InitializeComponent();
        }

        public RankViewModel Rank
        {
            get
            {
                return (RankViewModel)GetValue(RankProperty);
            }
            set
            {
                SetValue(RankProperty, value);
                NotifyPropertyChanged(nameof(Rank));
            }
        }

        private byte[] _pic;
        public byte[] Pic

        {
            get
            {
                return _pic;
            }
            set
            {
                if (_pic != value)
                {
                    _pic = value;
                    Img.Source = LoadImage(Pic);
                    NotifyPropertyChanged(nameof(Pic));
                }
            }
        }

        private string _hoverText;
        public string HoverText
        {
            get
            {
                return _hoverText;
            }
            set
            {
                _hoverText = $"MP: {Rank?.MatchesPlayed.ToString()}  |  {Rank?.Division}";
                NotifyPropertyChanged(nameof(HoverText));
            }
        }

        private void TargetUpdated(object sender, DataTransferEventArgs e)
        {
            var accentColor = ThemeManager.Current.DetectTheme().PrimaryAccentColor;
            dynamic item = sender;

            var color = Colors.White;

            if (Rank?.MatchesPlayed < 10)
                color = Colors.Gray;

            if (Rank?.DivDown.HasValue == false || Rank?.DivDown.Value == 0)
                RankDownArrow.Visibility = Visibility.Hidden;
            else
                RankDownArrow.Visibility = Visibility.Visible;

            if (Rank?.DivUp.HasValue == false || Rank?.DivUp.Value == 0)
                RankUpArrow.Visibility = Visibility.Hidden;
            else
                RankUpArrow.Visibility = Visibility.Visible;

            ColorAnimation ca = new ColorAnimation(color, new Duration(TimeSpan.FromSeconds(10)));
            item.Foreground = new SolidColorBrush(accentColor);
            item.Foreground.BeginAnimation(SolidColorBrush.ColorProperty, ca);
        }

        private static BitmapImage LoadImage(byte[] imageData)
        {
            if (imageData == null || imageData.Length == 0) return null;
            var image = new BitmapImage();
            using (var mem = new MemoryStream(imageData))
            {
                mem.Position = 0;
                image.BeginInit();
                image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.UriSource = null;
                image.StreamSource = mem;
                image.EndInit();
            }
            image.Freeze();
            return image;
        }

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void RankDisplay_Loaded(object sender, RoutedEventArgs e)
        {
            if(Rank != null)
                Pic = ImageManager.Instance().GetImageFromUri(Rank.ImageUrl);
            HoverText = "";
        }

        private void RankDisplay_SourceUpdated(object sender, DataTransferEventArgs e)
        {

        }
    }
}
