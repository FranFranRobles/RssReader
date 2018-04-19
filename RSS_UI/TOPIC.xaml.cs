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

        public TOPIC()
        {
            InitializeComponent();

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

        private void TopicBox_KeyDown(object sender, KeyEventArgs e)
        {
            // Add new topic
        }

        private void AddTopicButton_Click(object sender, RoutedEventArgs e)
        {
            // Add new topic
        }

        private void Open_AddTopicWindow(object sender, RoutedEventArgs e)
        {
            // Open the Popup window
        }
    }
}
