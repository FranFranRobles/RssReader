using RSS_LogicEngine;
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
    /// textBox
    /// treeView
    /// 
    /// </summary>
    public partial class RSSWindow : Window
    {
        Feed test = new Feed();
        Channel testChan = new Channel();
        public RSSWindow()
        {
            InitializeComponent();
         
            var gridView = new GridView();
            this.articleList.View = gridView;
            gridView.Columns.Add(new GridViewColumn { Header = "Title", DisplayMemberBinding = new Binding("Title") });
            gridView.Columns.Add(new GridViewColumn { Header = "Date", DisplayMemberBinding = new Binding("Date") });

            webBrowser.Navigate("https://google.com");
        }

        private void buttonAdd_Click(object sender, RoutedEventArgs e)  //Add Feed Button
        {
            // Click event handler
            // Should take the contents of the textbox, which is the RSS Feed url and add it to the tree view
            // Should give the content to some creater to create the feed object appropriately

            // Test for populating the articleList
            //this.articleList.Items.Add(new Article("Test", "Dummy Date"));

            // Test for populating the treeView
            TreeViewItem newItem = new TreeViewItem();

            newItem.MouseLeftButtonUp += left_Button_Up;

           // testChan.Add_Child(textBox.Text, test);
             newItem.Header = textBox.Text;
             this.treeView.Items.Add(newItem);
            int x = 9;
        }

        private void textBox_KeyDown(object sender, KeyEventArgs e)     //Enter Key for textBox
        {
            
            if (e.Key == Key.Enter)
            {
                this.treeView.Items.Add(test);
            }
        }

        private void button_Click(object sender, RoutedEventArgs e)     //Add Feed button click
        {
            MainWindow newWindow = new RSS_UI.MainWindow();
            newWindow.Show();
            this.Close();
        }

        private void left_Button_Up(object sender, MouseButtonEventArgs e)
        {
            ArticleListView test = new ArticleListView();
            test.Title = "CNN, the World is Smaller";
            test.Date = "January, 5th, 2000";
            

            articleList.Items.Add(test);
            int x = 0;

            //articleList.Items.;
 
            //articleList.Items.(s);
        }

        private void articleList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int x = 0;
        }

        public class ArticleListView
        {
            public String Title { get; set; }
            public String Date { get; set; }

        }
            
    }
}
