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
    /// Interaction logic for Channel.xaml
    /// </summary>
    public partial class Channel : Window
    {
        private RSS parent;
        private Component_View compView = Component_View.Get_Instance();

        public Channel(RSS instance)
        {
            parent = instance;
            InitializeComponent();
            this.Hide();
        }

        public void OpenWindow()
        {
            this.Show();
        }

        private void EnterPress(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            { 
                compView.Add_Channel("/" + this.TextBox.Text);
                RSS.ComponentTreeViewItem newChannel = new RSS.ComponentTreeViewItem(this.TextBox.Text, parent);
                parent.treeView.Items.Add(newChannel);
                this.Hide();
            }
        }

        private void AddClick(object sender, RoutedEventArgs e)
        {
            compView.Add_Channel("/" + this.TextBox.Text);
            RSS.ComponentTreeViewItem newChannel = new RSS.ComponentTreeViewItem(this.TextBox.Text, parent);
            parent.treeView.Items.Add(newChannel);
            this.Hide();
        }
    }
}
