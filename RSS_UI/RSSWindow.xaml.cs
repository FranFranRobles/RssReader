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
    /// Interaction logic for RSSWindow.xaml
    /// </summary>
    public partial class RSSWindow : Window
    {
        public RSSWindow()
        {
            InitializeComponent();

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
            // Test for populating the treeView
            TreeViewItem newItem = new TreeViewItem();
            newItem.Header = "Test Header";
            newItem.MouseLeftButtonUp += component_MouseLeftButtonUp;
            this.treeView.Items.Add(newItem);

            // Need to send information from the Feed Name and RSS URL textboxes pass data to engine
            // Get string from Feed Name
            // Get string from RSS URL
            // Receive the updated path that is reflected in TreeView
            // Clear the textboxes and repopulate with Default names


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
