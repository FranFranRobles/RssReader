using System;
using System.IO;
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
using Microsoft.Win32;

namespace RSS_UI
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : Window
    {
        public Home()
        {
            InitializeComponent();
            this.myContent.Content = new RSS();
            //Content RSS_C = new RSS();
        }


        private void mnuRSS_Click(object sender, RoutedEventArgs e)
        {
            this.myContent.Content = new RSS();
        }

        private void mnuMAP_Click(object sender, RoutedEventArgs e)
        {
            this.myContent.Content = new MAP();
        }

        private void mnuTOPIC_Click(object sender, RoutedEventArgs e)
        {
            this.myContent.Content = new TOPIC();
        }

        private void mnu_SAVE(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            if(saveFileDialog.ShowDialog() == true)
            {
                File.WriteAllText(saveFileDialog.FileName, txtEditor.Text);
            }
        }

        private void mnu_LOAD(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.DefaultExt = ".txt";

            Nullable<bool> result = openFileDialog.ShowDialog();

            string fileName = openFileDialog.FileName;

        }


    }
}
