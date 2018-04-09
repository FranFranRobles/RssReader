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
    /// Interaction logic for AddFeed.xaml
    /// </summary>
    public partial class AddFeed : Window
    {
        RSS parent;
        public AddFeed(RSS instance)
        {
            parent = instance;
            InitializeComponent();
            this.Hide();
        }

        public void OpenWindow()
        {
            this.Show();
        }

        private void AddClick(object sender, RoutedEventArgs e)
        {
            parent.add(this.FeedName.Text, this.RSSURL.Text);
            this.Hide();
        }
    }
}
