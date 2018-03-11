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
        private Update_Manager updateManager;

        public RSSWindow()
        {
            InitializeComponent();
            compView = Component_View.Get_Instance();   // Get the reference to the Component View item
            updateManager = Update_Manager.Get_Instance();
            updateManager.Set_Update_Period(1);      // Initialized to an hour update period

            // Format the list of articles that come from the feed selected in the tree menu
            var gridView = new GridView();
            this.articleList.View = gridView;
            gridView.Columns.Add(new GridViewColumn { Header = "Date", DisplayMemberBinding = new Binding("Date"), Width = 100});
            gridView.Columns.Add(new GridViewColumn { Header = "Title", DisplayMemberBinding = new Binding("Title"), Width = 483});     
            //articleList.SelectionChanged += articleList_SelectionChanged;  // Giving the articles an appropriate method to show in browser

            summaryBox.IsReadOnly = true;               // Making sure the user can't edit the summary shown in UI

            webBrowser.Navigate("https://google.com");
        }

        public class ArticleListItem
        {
            // Property definitions so that the articleList can bind to an ArticleListItem and organize properly
            public String Title { get; set; }
            public String Date { get; set; }
            public String URL { get; set; }
            public String Description { get; set; }
            public MouseButtonEventHandler click;
        }

        private class ComponentTreeViewItem : TreeViewItem
        {
            public String Title { get; set; }
            public String Path { get; set; }
        }

        private void buttonAdd_Click(object sender, RoutedEventArgs e)
        {
            string rssURL = urlBox.Text;                                    // Get names listed in the textboxes
            string feedName = nameBox.Text;

            ComponentTreeViewItem newFeed = new ComponentTreeViewItem();    // Create item to be displayed in left hand menu
            newFeed.Header = feedName;                                      // Reflect the name in the menu properly 
            newFeed.MouseLeftButtonUp += treeComp_MouseLeftButtonUp;        // Link the event to the proper handler
            newFeed.ContextMenu = new ContextMenu();                        // Create a right click menu for the TreeViewItem
            newFeed.ContextMenu.StaysOpen = true;                           // Make sure that it doesn't close immediately
            newFeed.Title = feedName;                                       // Give the feed a title for the UI's use
            newFeed.Path = "/" + feedName;                                  // Give the feed a proper path for the UI's use

            MenuItem addChannel = new MenuItem();                           // Creating options for the right click menu
            MenuItem removeChannel = new MenuItem();
            addChannel.Header = "Add to Channel";                       // Giving text to the options
            removeChannel.Header = "Remove from Channel";
            addChannel.MouseLeftButtonUp += addToChannel;               // Routing events to proper handlers
            removeChannel.MouseLeftButtonUp += removeFromChannel;
            newFeed.ContextMenu.Items.Add(addChannel);                  // Placing these items in the right click menu
            newFeed.ContextMenu.Items.Add(removeChannel);
            
            // Call the Component_View's Add_Feed function to pass proper info to the logic engine to create feed
            compView.Add_Feed("/" + feedName, rssURL);
            this.treeView.Items.Add(newFeed);                           // Show the new feed in the TreeView menu
            urlBox.Text = "RSS URL";                                    // Restore default text values in the textboxes
            nameBox.Text = "Feed Name";
        }

        private void treeComp_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ComponentTreeViewItem senderComp = (ComponentTreeViewItem) sender;  // Typecast so we can figure out the sender
            List<Article> articles;                                             // Create a new list of articles for the return
            articles = compView.Get_Articles_Under(senderComp.Path);            // Get the list of articles below the current selection

            foreach (Article i in articles)
            {
                ArticleListItem articleListItem = new ArticleListItem();        // Create a new ArticleListItem to be displayed
                articleListItem.Title = i.Title;                                // Get the correct title
                articleListItem.Date = i.Publication_Date;                      // Get the correct date
                articleListItem.URL = i.URL;
                articleListItem.Description = i.Summary;

                articleListItem.click += ArticleListItem_MouseLeftButtonUp; // Triggers event for actual item

                articleList.Items.Add(articleListItem);                         // Place in the UI
            }
        }

        private void ArticleListItem_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            // Need to have function return string containing information from description tag in RSS XML
            ArticleListItem currentArticle = (ArticleListItem)sender;           // Typecase to get the functionality of an Article from sender obj

            FlowDocument newContent = new FlowDocument();       // Keep
            Paragraph newP = new Paragraph();                   // Keep
            //Run newRun = new Run(currentArticle.Description);       // Getting the Summary attibute
            Run newRun = new Run("Suck");       // Getting the Summary attibute
            newP.Inlines.Add(newRun);   // Keep
            newContent.Blocks.Add(newP);    // Keep

            webBrowser.Navigate("http://www.eecs.wsu.edu/~fischer/ee451year2018.html"); // Navigate to the article's URL
            summaryBox.Document = newContent;       // Keep
        }

        private void articleList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Need to have function return string containing information from description tag in RSS XML
            ArticleListItem currentArticle = (ArticleListItem)sender;           // Typecase to get the functionality of an Article from sender obj

            FlowDocument newContent = new FlowDocument();       // Keep
            Paragraph newP = new Paragraph();                   // Keep
            //Run newRun = new Run(currentArticle.Description);       // Getting the Summary attibute
            Run newRun = new Run("Suck");       // Getting the Summary attibute
            newP.Inlines.Add(newRun);   // Keep
            newContent.Blocks.Add(newP);    // Keep

            webBrowser.Navigate("http://www.eecs.wsu.edu/~fischer/ee451year2018.html"); // Navigate to the article's URL
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

        private void addToChannel(object sender, MouseEventArgs e)
        {
            ;   // Needs some work
            // Probably will need to create another window for the user to interact with
        }

        private void removeFromChannel(object sender, MouseEventArgs e)
        {
            ;   // Needs some work
        }
    }
}
