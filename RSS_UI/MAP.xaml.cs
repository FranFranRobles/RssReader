using System;
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

            for(double i = 47.97898; i < 100; i = i +5)
            {
                for(double j = -122.20208; j < 0; j = j + 5)
                {
                    addPin(i, j);
                }
            }

            myMap.Mode = new AerialMode(true);
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Content = new RSS();
        }

        private void addPin(Double latitude, Double longitude)
        {
            Pushpin pin = new Pushpin();
            pin.Location = new Location(latitude, longitude);
            this.myMap.Children.Add(pin);
        }
    }
}

