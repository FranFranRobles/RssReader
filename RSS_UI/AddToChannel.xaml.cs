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
    /// Interaction logic for AddToChannel.xaml
    /// </summary>
    public partial class AddToChannel : Window
    {
        private static AddToChannel instance;
        private Component_View compView;
        private string oldPath;
        private string newPath;

        public static AddToChannel GetInstance()
        {
            if (instance == null)
            {
                instance = new AddToChannel();
                instance.compView = Component_View.Get_Instance();
            }
            return instance;
        }

        private AddToChannel() => InitializeComponent();

        public void OpenWindow(RSS.ComponentTreeViewItem movedComp)
        {
            this.oldPath = movedComp.Path;
            instance.Show();
        }

        private void EnterPressed(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                this.newPath = this.TextBox.Text;
                compView.Move_Component(this.oldPath, this.newPath);
                this.Hide();   // Close the window if the addition was done correctly
            }

        }

        private void AddClicked(object sender, RoutedEventArgs e)
        {
            this.newPath = this.TextBox.Text;
            compView.Move_Component(this.oldPath, this.newPath);
            this.Hide();   // Close the window if the addition was done correctly
        }
    }
}
