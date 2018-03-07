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
            gridView.Columns.Add(new GridViewColumn { Header = "Title", DisplayMemberBinding = new Binding("Title") });
            gridView.Columns.Add(new GridViewColumn { Header = "Date", DisplayMemberBinding = new Binding("Date") });

            webBrowser.Navigate("https://google.com");
        }

        private void buttonAdd_Click(object sender, RoutedEventArgs e)
        {
            // Click event handler
            // Should take the contents of the textbox, which is the RSS Feed url and add it to the tree view
            // Should give the content to some creater to create the feed object appropriately

            // Test for populating the articleList
            //this.articleList.Items.Add(new Article("Test", "Dummy Date"));

            // Test for populating the treeView
            TreeViewItem newItem = new TreeViewItem();
            newItem.Header = "Test Header";
            this.treeView.Items.Add(newItem);
        }

        private void textBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                textBox.Text = "Doug rules";
            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow newWindow = new RSS_UI.MainWindow();
            newWindow.Show();
            this.Close();
        }
    }
}
