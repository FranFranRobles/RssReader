﻿using System;
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
    /// Interaction logic for RenameComponentWindow.xaml
    /// </summary>
    public partial class RenameComponentWindow : Window
    {
        public RenameComponentWindow()
        {
            InitializeComponent();
        }

        private void RenameTextBox_Enter(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                ;
            }
        }

        private void RenameButton_Click(object sender, RoutedEventArgs e)
        {
            ;
        }
    }
}
