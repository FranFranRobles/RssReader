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
using System.IO;
using RSS_LogicEngine;



namespace RSS_UI
{
    /// <summary>
    /// Interaction logic for RSS.xaml
    /// </summary>
    public partial class RSS : UserControl
    {
        private Component_View compView;    // Reference to the single Component View item that exists in the project
        private Update_Manager updateManager;
        private int unnamedFeeds = 1;


        public RSS()
        {
            InitializeComponent();
            compView = Component_View.Get_Instance();   // Get the reference to the Component View item
            updateManager = Update_Manager.Get_Instance();
            updateManager.Set_Update_Period(5);      // Initialized to 5 sec refresh rate

            // Format the list of articles that come from the feed selected in the tree menu
            var gridView = new GridView();
            gridView.Columns.Add(new GridViewColumn { Header = "Date", DisplayMemberBinding = new Binding("Date"), Width = 100 });
            gridView.Columns.Add(new GridViewColumn { Header = "Title", DisplayMemberBinding = new Binding("Title"), Width = 483 });
            this.articleList.View = gridView;
            articleList.SelectionChanged += ArticleListItem_Clicked;    // Maps the list view being clicked to a handler
            treeView.AllowDrop = true;

            summaryBox.IsReadOnly = true;               // Making sure the user can't edit the summary shown in UI
        }

        public void Load(FileStream stream)
        {
            // Clear all of the current content in the UI
            treeView.Items.Clear();
            articleList.Items.Clear();
            FlowDocument clear = new FlowDocument();
            summaryBox.Document = clear;

            compView.Load_Components(stream);
            List<String> newTree = compView.Get_Children_Of("/");   // Returns name of Channels or Feeds below the root

            foreach (String i in newTree)
            {
                if (compView.Is_Channel(i)) // Buggy?
                {
                    ComponentTreeViewItem newFeed = new ComponentTreeViewItem(i, this);    // Create item to be displayed in left hand menu
                    treeView.Items.Add(newFeed);
                }

                else
                    treeView.Items.Add(i); // We have a feed that we can place into the TreeView
            }
        }

        private void textBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                add();
            }

        }

        // This can probably be merged to the function above, textBox_KeyDown
        private void buttonAdd_Click(object sender, RoutedEventArgs e)  
        {
            add();
        }

        private void add()
        {
            string rssURL = urlBox.Text;                                  // Get user provided URL
            string feedName = nameBox.Text;                                 // Get user provided name            

            if (!IsValidURL(rssURL))                                         // Check if URL is valid web address and contains .rss or .xml
            {
                InvalidURL();                                                   // If not valid, reset input box and pop error 
                return;                                                         // Exit add function
            }

            if (feedName.Equals(""))                                        // If user doesn't provide a name provide a default.
            {
                feedName = "New Feed" + unnamedFeeds.ToString();                // Default base name is New FeedX   
                unnamedFeeds++;                                                 // Increment 'X'
            }

            ComponentTreeViewItem newFeed = new ComponentTreeViewItem(feedName, this);    // Create item to be displayed in left hand menu
           
            // Call the Component_View's Add_Feed function to pass proper info to the logic engine to create feed
            compView.Add_Feed("/" + feedName, rssURL);
            this.treeView.Items.Add(newFeed);                               // Show the new feed in the TreeView menu
            urlBox.Text = "";                                               // Restore default text values in the textboxes
            nameBox.Text = "";

        }

        // Check if the url string is valid
        private bool IsValidURL(string url)
        {
            bool valid = true;
            Uri uri = null;                                                  // Create U.niform r.esouce i.dentifier 

            if (!Uri.TryCreate(url, UriKind.Absolute, out uri) || null == uri)  // Check if the string is a valid web address
            {
                valid = false;                                                  // if not set valid to false
            }


            string validURL = url.ToLower();                                    // Cast to lower to ensure .XmL, .rsS, etc... == true                          
            if (!validURL.Contains(".xml") && !validURL.Contains(".rss"))       // If user provided address doesn't contain .rss or .xml
            {
                valid = false;                                                  // if not set valid to false
            }

            return valid;                                                       // return boolean 'valid'
        }

        // Function to display Invalid URL Message Box
        private void InvalidURL()
        {
            MessageBox.Show("Invalid URL Entered", "URL Entry", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
            urlBox.Text = "";
            return;
        }

        public void SetTextSize_Up()
        {
            treeView.FontSize++;
            articleList.FontSize++;
            summaryBox.FontSize++;
        }

        public void SetTextSize_Down()
        {
            treeView.FontSize--;
            articleList.FontSize--;
            summaryBox.FontSize--;
        }


        public class ArticleListItem
        {
            // Property definitions so that the articleList can bind to an ArticleListItem and organize properly
            public String Date { get; set; }        // Allows the user to see the date the article was published in the UI, binding property
            public String Title { get; set; }       // Allows the user to see the title of the article in the UI, binding property
            public String URL { get; set; }         // Allows the UI to navigate to the appropriate page in the browser 
            public String Description { get; set; } // Allows the user to see the summary of the article in the UI

        }

        private class ComponentTreeViewItem : TreeViewItem
        {
            // Constructor was added because it was being used in multiple places
            public ComponentTreeViewItem(string feedName, RSS parent)   // parent parameter allows the events to be mapped
            {
                this.Header = feedName;                                      // Reflect the name in the menu properly 
                this.MouseLeftButtonUp += parent.treeComp_MouseLeftButtonUp;        // Link the event to the proper handler
                this.ContextMenu = new ContextMenu();                        // Create a right click menu for the TreeViewItem
                this.ContextMenu.StaysOpen = true;                           // Make sure that it doesn't close immediately
                this.Title = feedName;                                       // Give the feed a title for the UI's use
                this.Path = "/" + feedName;                                  // Give the feed a proper path for the UI's use

                //
                // Manage Right Click Menu Options
                //
                MenuItem addChannel = new MenuItem();                        // Creating options for the right click menu
                MenuItem removeChannel = new MenuItem();
                MenuItem renameFeed = new MenuItem();

                addChannel.Header = "Add to Channel";                      // Giving text to the options
                removeChannel.Header = "Remove from Channel";
                renameFeed.Header = "Rename Channel";

                addChannel.MouseLeftButtonUp += parent.addToChannel;                // Routing events to proper handlers
                removeChannel.MouseLeftButtonUp += parent.removeFromChannel;
                renameFeed.MouseLeftButtonUp += parent.renameChannel;

                this.ContextMenu.Items.Add(addChannel);                      // Placing these items in the right click menu
                this.ContextMenu.Items.Add(removeChannel);
                this.ContextMenu.Items.Add(renameFeed);

            }
            public String Title { get; set; }
            public String Path { get; set; }
        }

    
        private void treeComp_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            articleList.Items.Clear();
            ComponentTreeViewItem senderComp = (ComponentTreeViewItem)sender;  // Typecast so we can figure out the sender
            List<Article> articles;                                             // Create a new list of articles for the return
            articles = compView.Get_Articles_Under(senderComp.Path);            // Get the list of articles below the current selection

            foreach (Article i in articles)
            {
                ArticleListItem articleListItem = new ArticleListItem();        // Create a new ArticleListItem to be displayed
                articleListItem.Title = i.Title;                                // Get the correct title
                articleListItem.Date = i.Publication_Date;                      // Get the correct date
                articleListItem.URL = i.URL;
                articleListItem.Description = i.Summary;

                articleList.Items.Add(articleListItem);                         // Place in the UI
            }
        }

        private void ArticleListItem_Clicked(object sender, SelectionChangedEventArgs e)
        {
            ListView currentList = (ListView)sender;
            ArticleListItem currentArticle = (ArticleListItem)currentList.SelectedItem;

            // Verify if the selection has articles
            if (currentArticle == null)
            {
                return;
            }

            // Need to have function return string containing information from description tag in RSS XML
            FlowDocument newContent = new FlowDocument();
            Paragraph newP = new Paragraph();

            Run newRun = new Run(currentArticle.Description);       // Getting the Summary attibute
            newP.Inlines.Add(newRun);
            newContent.Blocks.Add(newP);                            // Placing the summary into the newContent which will be displayed in the summaryBox


            summaryBox.Document = newContent;       // Display summary
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

        private void addToChannel(object sender, RoutedEventArgs e)
        {
            ;   // Needs some work
            // Probably will need to create another window for the user to interact with
        }

        private void removeFromChannel(object sender, RoutedEventArgs e)
        {
            ;   // Needs some work
        }

        private void renameChannel(object sender, MouseEventArgs e)
        {

            //compView.Add_Feed("/" + feedName, rssURL);
            //this.treeView.Items.R;                               // Show the new feed in the TreeView menu
            //this.
            //this.Header = "test";
            bool test = false;
            test = true;
        }

        private void nameBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }
    }
}




