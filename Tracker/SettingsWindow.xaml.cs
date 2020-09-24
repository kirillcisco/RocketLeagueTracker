﻿using System;
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

namespace Tracker
{
    /// <summary>
    /// Логика взаимодействия для SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {

        private SettingsManager _SettingsManager;


        public SettingsWindow()
        {
            InitializeComponent();

            _SettingsManager = new SettingsManager();

        }

        private void DataDeleteButton_Click(object sender, RoutedEventArgs e)
        {
            _SettingsManager.DataDelete();
        }

    }
}
