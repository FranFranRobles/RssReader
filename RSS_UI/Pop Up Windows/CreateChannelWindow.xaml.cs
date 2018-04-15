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
    public partial class CreateChannelWindow : Window
    {
        private Component_View compView = Component_View.Get_Instance();
        public event EventHandler OnChannelCreated;

        public CreateChannelWindow() => InitializeComponent();

        public void OpenWindow() => this.Show();

        private void EnterPress(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            { 
                compView.Add_Channel("/" + this.TextBox.Text);
                this.Hide();
                OnChannelCreated(this, new EventArgs());
            }
        }

        private void AddClick(object sender, RoutedEventArgs e)
        {
            compView.Add_Channel("/" + this.TextBox.Text);
            this.Hide();
            OnChannelCreated(this, new EventArgs());
        }
    }
}
