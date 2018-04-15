using RSS_LogicEngine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;


namespace RSS_UI
{
    /// <summary>
    /// Interaction logic for RSS.xaml
    /// </summary>
    public partial class RSS : UserControl
    {
        private Component_View compView;    // Reference to the single Component View item that exists in the project
        private Update_Manager updateManager;
        private AddToChannelWindow addToChannelWindow;
        private CreateChannelWindow createChannelWindow;
        private AddFeedWindow addFeedWindow;
        private ComponentTreeViewItem selectedItem;
        private int unnamedFeeds = 1;

        public RSS()
        {
            InitializeComponent();
            compView = Component_View.Get_Instance();   // Get the reference to the Component View item
            updateManager = Update_Manager.Get_Instance();
            //addToChannelWindow = AddToChannel.GetInstance();
            //channelWindow = new CreateChannelWindow();
            //addFeedWindow = new AddFeedWindow();
            updateManager.Set_Update_Period(5);      // Initialized to 5 sec refresh rate

            // Format the list of articles that come from the feed selected in the tree menu
            var gridView = new GridView();
            gridView.Columns.Add(new GridViewColumn { Header = "Read", DisplayMemberBinding = new Binding("Read"), Width = 25 });
            gridView.Columns.Add(new GridViewColumn { Header = "Date", DisplayMemberBinding = new Binding("Date"), Width = 125 });
            gridView.Columns.Add(new GridViewColumn { Header = "Title", DisplayMemberBinding = new Binding("Title"), Width = 433 });
            this.articleList.View = gridView;
            articleList.SelectionChanged += ArticleListItem_Clicked;    // Maps the list view being clicked to a handler
            treeView.AllowDrop = true;

            summaryBox.IsReadOnly = true;               // Making sure the user can't edit the summary shown in UI
            //addToChannelWindow.OnComponentMoved += this.OnAddToChannelWindowClose;
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
            PopulateOnLoad(newTree);
        }

        private void PopulateOnLoad(List<String> newTree)
        {
            foreach (String i in newTree)
            {
                if (compView.Is_Channel(i)) // We know there are either more channels or feeds internal, need to recursively call
                {
                    List<String> components = compView.Get_Children_Of(i);
                    PopulateOnLoad(components);
                }

                else
                {
                    ComponentTreeViewItem newFeed = new ComponentTreeViewItem(i, this);    // Create item to be displayed in left hand menu
                    treeView.Items.Add(newFeed);
                }
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

            ComponentTreeViewItem newFeed = new ComponentTreeViewItem(feedName, this);    // Create item to be displayed in left hand 
            newFeed.MouseLeftButtonUp += treeComp_MouseLeftButtonUp;        // Link the event to the proper handler
            newFeed.PreviewMouseRightButtonDown += treeView_PreviewMouseRightButtonDown;
            newFeed.addChannel.Click += AddComponentToChannel;                // Routing events to proper handlers
            newFeed.removeChannel.MouseLeftButtonUp += removeFromChannel;
            newFeed.renameFeed.MouseLeftButtonUp += renameChannel;

            // Call the Component_View's Add_Feed function to pass proper info to the logic engine to create feed
            compView.Add_Feed("/" + feedName, rssURL);
            this.treeView.Items.Add(newFeed);                               // Show the new feed in the TreeView menu
            urlBox.Text = "";                                               // Restore default text values in the textboxes
            nameBox.Text = "";
        }

        public void add(string feedName, string rssURL)
        {
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
                articleListItem.Read = "";                                      // Setting default to unread

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

            Run newRun = new Run(currentArticle.Description);   // Getting the Summary attibute
            newP.Inlines.Add(newRun);
            newContent.Blocks.Add(newP);                        // Placing the summary into the newContent which will be displayed in the summaryBox
            summaryBox.Document = newContent;                   // Display summary
            currentArticle.Read = "X";                          // Reflecting that the article has been read
        }

        private void AddComponentToChannel(object sender, RoutedEventArgs e)
        {
            ComponentTreeViewItem movingComp = (ComponentTreeViewItem)treeView.SelectedItem;
            addToChannelWindow = new AddToChannelWindow();
            addToChannelWindow.Show();
            addToChannelWindow.OpenWindow(movingComp);
            addToChannelWindow.OnComponentMoved += this.OnAddToChannelWindowClose;
        }

        private void removeFromChannel(object sender, RoutedEventArgs e)
        {
            ;
        }

        private void OpenAddFeedWindow(object sender, RoutedEventArgs e)
        {
            addFeedWindow = new AddFeedWindow();
            addFeedWindow.OpenWindow();
        }

        private void OpenCreateChannelWindow(object sender, RoutedEventArgs e)
        {
            createChannelWindow = new CreateChannelWindow();
            createChannelWindow.Show();
            createChannelWindow.OnChannelCreated += this.OnCreateChannelWindowClose;
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
            ;
        }

        public void OnClosed()      // Do we still need this? Used in Home.xaml.cs Line 151
        {
            this.addToChannelWindow.Close();
            this.createChannelWindow.Close();
            this.addFeedWindow.Close();
        }

        // Ensures that we do not have a null value when using Context Menus
        private void treeView_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            selectedItem = (ComponentTreeViewItem)sender;
        }

        private void OnAddToChannelWindowClose(object sender, EventArgs e)  // Handler for when adding Component to a Channel
        {
            ComponentTreeViewItem movedComponent = (ComponentTreeViewItem)sender;
            this.treeView.Items.Remove(movedComponent);  // Remove the moved component from the UI
            string childPath = movedComponent.Path;    // Need to figure out container

            int childSlash = childPath.LastIndexOf('/');
            int parentSlash = childPath.LastIndexOf("/", childSlash-1);
            string parentPath = childPath.Substring(parentSlash, childSlash - parentSlash);
            parentPath = parentPath.Substring(1);

            for (int i = 0; i < this.treeView.Items.Count; i++)
            {
                ComponentTreeViewItem currentItem = (ComponentTreeViewItem)this.treeView.Items.GetItemAt(i);
                if (parentPath == (string)currentItem.Header)
                {
                    currentItem.Items.Add(movedComponent);
                    break;
                }
            }
        }

        private void OnCreateChannelWindowClose(object sender, EventArgs e) // Handler for when Creating a new Channel
        {
            // Need to have channel have the same click properties as the other feeds in base
            CreateChannelWindow source = (CreateChannelWindow)sender;
            string channelName = source.TextBox.Text;
            ComponentTreeViewItem newChannel = new ComponentTreeViewItem(channelName, this);    // Create item to be displayed in left hand 

            newChannel.PreviewMouseRightButtonDown += treeView_PreviewMouseRightButtonDown;
            newChannel.addChannel.Click += AddComponentToChannel;                // Routing events to proper handlers
            newChannel.removeChannel.MouseLeftButtonUp += removeFromChannel;
            newChannel.renameFeed.MouseLeftButtonUp += renameChannel;
            this.treeView.Items.Add(newChannel);
        }
    }
}




