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


namespace RSS_UI
{
    /// <summary>
    /// Interaction logic for RSS.xaml
    /// </summary>
    public partial class RSS : UserControl
    {
        public RSS()
        {
            InitializeComponent();

            // Format the list of articles that come from the feed selected in the tree menu
            var gridView = new GridView();
            this.articleList.View = gridView;
            gridView.Columns.Add(new GridViewColumn { Header = "Date", DisplayMemberBinding = new Binding("Date"), Width = 100 });
            gridView.Columns.Add(new GridViewColumn { Header = "Title", DisplayMemberBinding = new Binding("Title"), Width = 483 });
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
            FlowDocument newContent = new FlowDocument();
            Paragraph newP = new Paragraph();
            Run newRun = new Run("EE451 was selected, RichTextBox is kind of weird!");
            newP.Inlines.Add(newRun);
            newContent.Blocks.Add(newP);

            webBrowser.Navigate("http://www.eecs.wsu.edu/~fischer/ee451year2018.html");
            summaryBox.Document = newContent;
        }

        private void nameBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (nameBox.Text == "Feed Name")
                nameBox.Clear();
        }

        private void urlBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (urlBox.Text == "RSS URL")
                urlBox.Clear();
        }
               

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Content = new MAP();
        }
    }
}





//public partial class RSS : UserControl
//{
//    public RSS()
//    {
//        InitializeComponent();

//        var gridView = new GridView();
//        this.articleList.View = gridView;
//        gridView.Columns.Add(new GridViewColumn { Header = "Title", DisplayMemberBinding = new Binding("Title") });
//        gridView.Columns.Add(new GridViewColumn { Header = "Date", DisplayMemberBinding = new Binding("Date") });

//        webBrowser.Navigate("https://google.com");
//    }

//    private void buttonAdd_Click(object sender, RoutedEventArgs e)
//    {
//        // Click event handler
//        // Should take the contents of the textbox, which is the RSS Feed url and add it to the tree view
//        // Should give the content to some creater to create the feed object appropriately

//        // Test for populating the articleList
//        //this.articleList.Items.Add(new Article("Test", "Dummy Date"));

//        // Test for populating the treeView
//        TreeViewItem newItem = new TreeViewItem();
//        newItem.Header = "Test Header";
//        this.treeView.Items.Add(newItem);
//    }

//    private void textBox_KeyDown(object sender, KeyEventArgs e)
//    {
//        if (e.Key == Key.Enter)
//        {
//            textBox.Text = "Doug rules";
//        }
//    }
