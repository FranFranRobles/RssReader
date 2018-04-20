﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Maps.MapControl.WPF;
using RSS_LogicEngine;

namespace RSS_UI
{
    /// <summary>
    /// Interaction logic for MAP.xaml
    /// </summary>
    public partial class MAP : UserControl
    {
        public MAP()
        {
            InitializeComponent();
            myMap.Focus();
            //47.97898 -122.20208   Everett, Wa

            //addPin(47.97898, - 122.20208);

            Feed_Manager feed = Feed_Manager.Get_Instance();
            List<string[]> test = feed.GetAllTitlesAndCordinates();

            myMap.Mode = new AerialMode(true);

        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Content = new RSS();
        }

        public void addPin(Double latitude, Double longitude)
        {
            Pushpin pin = new Pushpin();
            pin.Location = new Location(latitude, longitude);

            Feed_Manager feed = Feed_Manager.Get_Instance();
            List<string[]> test = feed.GetAllTitlesAndCordinates();
            this.myMap.Children.Add(pin);
        }

    }
}

