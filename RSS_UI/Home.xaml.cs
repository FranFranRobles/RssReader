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

namespace RSS_UI
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : Window
    {
        public Home()
        {
            InitializeComponent();
            this.myContent.Content = new RSS();
            //Content RSS_C = new RSS();


        }

        //
        // Content Control Click Events
        //
        private void mnuRSS_Click(object sender, RoutedEventArgs e)
        {
            this.myContent.Content = new RSS();
        }

        private void mnuMAP_Click(object sender, RoutedEventArgs e)
        {
            this.myContent.Content = new MAP();
        }

        private void mnuTOPIC_Click(object sender, RoutedEventArgs e)
        {
            this.myContent.Content = new TOPIC();
        }

    }
}
