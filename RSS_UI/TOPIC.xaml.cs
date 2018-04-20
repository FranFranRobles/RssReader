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
using RSS_LogicEngine;

namespace RSS_UI
{
    /// <summary>
    /// Interaction logic for TOPIC.xaml
    /// </summary>
    public partial class TOPIC : UserControl
    {
        private Component_View compView;
        private Update_Manager updateManager;
        private ComponentTreeViewItem selectedItem;
        private List<TopicItem> topics;

        public TOPIC()
        {
            InitializeComponent();

            topics = new List<TopicItem>();

            compView = Component_View.Get_Instance();   // Get the reference to the Component View item
            updateManager = Update_Manager.Get_Instance();
            updateManager.Set_Update_Period(5);      // Initialized to 5 sec refresh rate

            // Format the list of articles that come from the feed selected in the tree menu
            var gridView = new GridView();
            gridView.Columns.Add(new GridViewColumn { Header = "Read", DisplayMemberBinding = new Binding("Read"), Width = 50 });
            gridView.Columns.Add(new GridViewColumn { Header = "Date", DisplayMemberBinding = new Binding("Date"), Width = 125 });
            gridView.Columns.Add(new GridViewColumn { Header = "Title", DisplayMemberBinding = new Binding("Title"), Width = 600 });
            this.articleList.View = gridView;
            articleList.SelectionChanged += ArticleListItem_Clicked;    // Maps the list view being clicked to a handler
            treeView.AllowDrop = true;

            summaryBox.IsReadOnly = true;               // Making sure the user can't edit the summary shown in UI
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

        private void TopicButton_Click(object sender, EventArgs e)
        {
            // Add new topic
            string newTopicName = topicBox.Text;
            List<Article> allArticles = compView.Get_Articles_Under("/");
            List<Article> topicArticleList = new List<Article>();

            foreach (Article a in allArticles)
            {
                if (a.Title.Contains(newTopicName) || a.Summary.Contains(newTopicName))
                    topicArticleList.Add(a);
            }
            // Need to have some catch for a topic that doesn't contain any articles
            TopicItem newTopic = new TopicItem(newTopicName, topicArticleList);
            this.topics.Add(newTopic);
            AddToTreeView(newTopic);
            topicBox.Clear();
        }

        private void TopicBox_Enter(object sender, EventArgs e)
        {
            KeyEventArgs pressed = (KeyEventArgs)e;
            if (pressed.Key == Key.Enter)
                TopicButton_Click(sender, e);
        }

        private void Open_AddTopicWindow(object sender, RoutedEventArgs e)
        {
            // Open the Popup window
        }

        private void AddToTreeView(TopicItem newTopic)
        {
            // Need to map the click functions for the newly added items 
            TopicTreeViewItem newItem = new TopicTreeViewItem(newTopic.name);
            newItem.MouseLeftButtonUp += TopicClicked;
            treeView.Items.Add(newItem);
        }

        private void TopicClicked(object sender, EventArgs e)
        {
            // Show all articles contained in the topic
            TopicTreeViewItem selectedTopic = (TopicTreeViewItem)sender;
            List<Article> articles = new List<Article>();

            foreach (TopicItem t in topics)
            {
                if (t.name == (string)selectedTopic.Header)
                    articles = t.articleList;
            }

            articleList.Items.Clear();
            

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
    }
}
