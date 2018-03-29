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
using System.Windows.Shapes;
using RSS_UI;

namespace RSS_UI
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    /// 
    


    public partial class Home : Window
    {
        private UserControl myMap   = new MAP();
        private UserControl myRSS   = new RSS();
        private UserControl myTopic = new TOPIC();

        public Home()
        {
            InitializeComponent();
            this.myContent.Content = myRSS;

        }

        //
        // Content Control Click Events
        //
        private void mnuRSS_Click(object sender, RoutedEventArgs e)
        {
            this.myContent.Content = myRSS;
        }

        private void mnuMAP_Click(object sender, RoutedEventArgs e)
        {
            this.myContent.Content = myMap;
        }

        private void mnuTOPIC_Click(object sender, RoutedEventArgs e)
        {
            this.myContent.Content = myTopic;
        }

        private void mnuIncrease_Click(object sender, RoutedEventArgs e)
        {

            //myRSS.SetTextSize_Up();
            myContent.FontSize++;

        }

        private void mnuDecrease_Click(object sender, RoutedEventArgs e)
        {
            myContent.FontSize--;
        }





    }
}
