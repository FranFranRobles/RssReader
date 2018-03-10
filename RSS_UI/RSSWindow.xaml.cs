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
using RSS_LogicEngine;

namespace RSS_UI
{
    /// <summary>
    /// Interaction logic for RSSWindow.xaml
    /// </summary>
    public partial class RSSWindow : Window
    {
        private Component_View compView;    // Reference to the single Component View item that exists in the project

        public RSSWindow()
        {
            InitializeComponent();
            compView = Component_View.Get_Instance();   // Get the reference to the Component View item

            // Format the list of articles that come from the feed selected in the tree menu
            var gridView = new GridView();
            this.articleList.View = gridView;
            gridView.Columns.Add(new GridViewColumn { Header = "Date", DisplayMemberBinding = new Binding("Date"), Width = 100});
            gridView.Columns.Add(new GridViewColumn { Header = "Title", DisplayMemberBinding = new Binding("Title"), Width = 483});     
            articleList.SelectionChanged += articleList_SelectionChanged;  // Giving the articles an appropriate method to show in browser

            summaryBox.IsReadOnly = true;   // Making sure the user can't edit the summary shown in UI

            webBrowser.Navigate("https://google.com");
        }

        public class ArticleListItem
        {
            // Property definitions so that the articleList can bind to an ArticleListItem and organize properly
            public String Title { get; set; }
            public String Date { get; set; }
        }

        private void buttonAdd_Click(object sender, RoutedEventArgs e)
        {
            string rssURL = urlBox.Text;        // Get names listed in the textboxes
            string feedName = nameBox.Text;
            TreeViewItem newFeed = new TreeViewItem();  // Create item to be displayed in left hand menu
            newFeed.Header = feedName;                  // Reflect the name in the menu properly 
            newFeed.MouseLeftButtonUp += component_MouseLeftButtonUp;   // Link the event to the proper handler

            // Call the Component_View's Add_Feed function to pass proper info to the logic engine to create feed
            compView.Add_Feed("/" + feedName, rssURL);
            this.treeView.Items.Add(newFeed);   // Show the new feed in the TreeView menu
            urlBox.Text = "RSS URL";        // Restore default text values in the textboxes
            nameBox.Text = "Feed Name";
        }

        private void component_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ArticleListItem newArticle = new ArticleListItem();
            newArticle.Title = "test";
            newArticle.Date = "test date";

            articleList.Items.Add(newArticle);
        }

        private void articleList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FlowDocument newContent = new FlowDocument();       // Keep
            Paragraph newP = new Paragraph();                   // Keep
            Run newRun = new Run("EE451 was selected, RichTextBox is kind of weird!");  // Change to description, string return from engine
            newP.Inlines.Add(newRun);   // Keep
            newContent.Blocks.Add(newP);    // Keep

            webBrowser.Navigate("http://www.eecs.wsu.edu/~fischer/ee451year2018.html"); // Change to article's url
            summaryBox.Document = newContent;       // Keep
        }

        private void nameBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (nameBox.Text == "Feed Name")    // If we have default text when clicked, clear it
                nameBox.Clear();
        }

        private void urlBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (urlBox.Text == "RSS URL")   // If we have default text when clicked, clear it
                urlBox.Clear();
        }

        private void button_Click(object sender, RoutedEventArgs e)     // Need this?
        {
            MainWindow newWindow = new RSS_UI.MainWindow();
            newWindow.Show();
            this.Close();
        }
    }
}
