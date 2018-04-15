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
    public partial class AddToChannelWindow : Window
    {
        private Component_View compView = Component_View.Get_Instance();

        private ComponentTreeViewItem sourceComponent;
        private string oldPath;
        private string newPath;
        public event EventHandler OnComponentMoved;

        public AddToChannelWindow() => InitializeComponent();

        public void OpenWindow(ComponentTreeViewItem movedComp)
        {
            sourceComponent = movedComp;
            this.oldPath = sourceComponent.Path;
        }

        private void EnterPressed(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                string childPath;
                this.newPath = "/" + this.TextBox.Text + sourceComponent.Path;  
                sourceComponent.Path = this.newPath;

                // Need to iterate through each path to find proper level

                foreach (ComponentTreeViewItem c in sourceComponent.Items)
                {
                    childPath = c.Path;
                    c.Path = "/" + this.TextBox.Text + c.Path;
                }

                compView.Move_Component(this.oldPath, this.newPath);
                this.Close();
                OnComponentMoved(sourceComponent, new EventArgs());
            }

        }

        private void AddClicked(object sender, RoutedEventArgs e)
        {
            string childPath;
            this.newPath = "/" + this.TextBox.Text + "/";
            sourceComponent.Path = this.newPath;

            foreach (ComponentTreeViewItem c in sourceComponent.Items)
            {
                childPath = c.Path;
                c.Path = "/" + this.TextBox.Text + c.Path;
            }

            compView.Move_Component(this.oldPath, this.newPath);
            this.Close();
            OnComponentMoved(sourceComponent, new EventArgs());
        }
    }
}
