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
using RSS_UI;
using Microsoft.Win32;
using System.Xml;

namespace RSS_UI
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    /// 


    public partial class Home : Window
    {
        private MAP myMap = new MAP();     // Window has one 'MAP' User Control
        private RSS myRSS = new RSS();     // Window has one 'RSS' User Control
        private TOPIC myTopic = new TOPIC();   // Window has one ''TOPIC' User Control

        public Home()
        {
            InitializeComponent();
            this.myContent.Content = myRSS;     // Load RSS User Control by default
        }

        //
        // Menu/Control Functions
        //

        // Save File Dialog 
        private void mnu_SAVE(object sender, RoutedEventArgs e)
        {
            //configure save file dialog box
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.FileName = "config"; //default file name
            dlg.DefaultExt = ".xml"; //default file extension
            dlg.Filter = "XML documents (.xml)|*.xml"; //filter files by extension

            // Show save file dialog box
            Nullable<bool> result = dlg.ShowDialog();

            // Process save file dialog box results
            if (result == true)
            {
                Stream save_stream = dlg.OpenFile();
                RSS_LogicEngine.Component_View component_view;
                component_view = RSS_LogicEngine.Component_View.Get_Instance();
                component_view.Save_Components(save_stream);
                save_stream.Close();
            }
        }


        // Load File Dialog
        private void mnu_LOAD(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.DefaultExt = ".txt";

            Nullable<bool> result = openFileDialog.ShowDialog();

            string fileName = openFileDialog.FileName;

            FileStream fileStream = new FileStream(fileName, FileMode.Open);

            myRSS.Load(fileStream);         // Need to have this function implemented for all window types
        }

        // Exit Program
        private void mnu_EXIT(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }



        //
        // Content Control Click Events
        //


        //
        // Custom Commands 
        //

        // Increase Font Size Personal Command
        private void UpCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
        private void UpCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            myRSS.SetTextSize_Up();
        }


        // Decrease Font Size Personal Command
        private void DownCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
        private void DownCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            myRSS.SetTextSize_Down();
        }
        // Open RSS interface Personal Command
        private void RSSCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
        private void RSSCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            myContent.Content = myRSS;
        }

        // Open MAP interface Personal Command
        private void MAPCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
        private void MAPCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            myContent.Content = myMap;
        }

        // Open Topic interface Personal Command
        private void TOPICCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
        private void TOPICCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            myContent.Content = myTopic;
        }
    }

    public static class CCs
    {
        public static readonly RoutedUICommand Up = new RoutedUICommand
            (
                "Up", "Up",
                typeof(CCs), new InputGestureCollection()
                {
                        new KeyGesture(Key.Up, ModifierKeys.Alt)
                }

            );

        public static readonly RoutedUICommand Down = new RoutedUICommand
            (
                "Down", "Down",
                typeof(CCs), new InputGestureCollection()
                {
                        new KeyGesture(Key.Down, ModifierKeys.Alt)
                }

            );

        public static readonly RoutedUICommand Map = new RoutedUICommand
            (
                "Map", "Map",
                typeof(CCs), new InputGestureCollection()
                {
                        new KeyGesture(Key.M, ModifierKeys.Alt)
                }

            );

        public static readonly RoutedUICommand RSS = new RoutedUICommand
            (
                "RSS", "RSS",
                typeof(CCs), new InputGestureCollection()
                {
                        new KeyGesture(Key.R, ModifierKeys.Alt)
                }

            );

        public static readonly RoutedUICommand Topic = new RoutedUICommand
            (
                "Topic", "Topic",
                typeof(CCs), new InputGestureCollection()
                {
                        new KeyGesture(Key.T, ModifierKeys.Alt)
                }

            );
    }

}
