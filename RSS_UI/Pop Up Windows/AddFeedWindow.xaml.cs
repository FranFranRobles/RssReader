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
    public partial class AddFeedWindow : Window
    {
        //RSS parent;
        public event EventHandler OnFeedCreated;
        private List<string> feedInfo;

        public AddFeedWindow()  // Previous parameter was "RSS instance"
        {
            //parent = instance;
            InitializeComponent();
        }

        public void OpenWindow()
        {
            this.Show();
        }

        private void AddClick(object sender, RoutedEventArgs e)
        {
            feedInfo = new List<string>();
            feedInfo.Add(this.FeedName.Text);
            feedInfo.Add(this.RSSURL.Text);
            OnFeedCreated(feedInfo, new EventArgs());
            this.Close();
        }

        private void AddFeedEnterPressed(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                feedInfo = new List<string>();
                feedInfo.Add(this.FeedName.Text);
                feedInfo.Add(this.RSSURL.Text);
                OnFeedCreated(feedInfo, new EventArgs());
                this.Close();
            }
        }
    }
}
