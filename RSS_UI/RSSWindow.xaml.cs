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

            var gridView = new GridView();
            this.articleList.View = gridView;
            gridView.Columns.Add(new GridViewColumn { Header = "Date", DisplayMemberBinding = new Binding("Date") });
            gridView.Columns.Add(new GridViewColumn { Header = "Title", DisplayMemberBinding = new Binding("Title") });

            webBrowser.Navigate("https://google.com");
        }

        private void buttonAdd_Click(object sender, RoutedEventArgs e)
        {
            // Test for populating the treeView
            TreeViewItem newItem = new TreeViewItem();
            newItem.Header = "Test Header";
            newItem.MouseLeftButtonUp += component_MouseLeftButtonUp;
            this.treeView.Items.Add(newItem);   
        }

        private void urlBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                urlBox.Text = "Doug rules";
            }
        }

        private void urlBox_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                urlBox.Text = "";
            }
            else
                urlBox.Text = "False";
        }

        private void component_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ArticleListItem newArticle = new ArticleListItem();
            newArticle.Title = "test";
            newArticle.Date = "test date";
            
            articleList.Items.Add(newArticle);
        }

        public class ArticleListItem
        {
            public String Title { get; set; }
            public String Date { get; set; }


        }
    }
}
