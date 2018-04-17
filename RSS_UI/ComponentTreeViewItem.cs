using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using RSS_UI;

namespace RSS_UI
{
    public class ComponentTreeViewItem : TreeViewItem
    {
        public MenuItem addChannel;
        public MenuItem removeChannel;
        public MenuItem renameFeed;

        // Constructor was added because it was being used in multiple places
        public ComponentTreeViewItem(string feedName, RSS parent)   // parent parameter allows the events to be mapped
        {
            this.Header = feedName;                                      // Reflect the name in the menu properly 
            //this.MouseLeftButtonUp += parent.treeComp_MouseLeftButtonUp;        // Link the event to the proper handler
            //this.PreviewMouseRightButtonDown += parent.treeView_PreviewMouseRightButtonDown;
            this.ContextMenu = new ContextMenu();                        // Create a right click menu for the TreeViewItem
            this.ContextMenu.StaysOpen = true;                           // Make sure that it doesn't close immediately
            this.Title = feedName;                                       // Give the feed a title for the UI's use
            this.Path = "/" + feedName;                                  // Give the feed a proper path for the UI's use

            //
            // Manage Right Click Menu Options
            //
            addChannel = new MenuItem();                        // Creating options for the right click menu
            removeChannel = new MenuItem();
            renameFeed = new MenuItem();

            addChannel.Header = "Add to Channel";                      // Giving text to the options
            removeChannel.Header = "Remove from Channel";
            renameFeed.Header = "Rename Channel";

            this.ContextMenu.Items.Add(addChannel);                      // Placing these items in the right click menu
            this.ContextMenu.Items.Add(removeChannel);
            this.ContextMenu.Items.Add(renameFeed);

        }
        public String Title { get; set; }
        public String Path { get; set; }

    }
}
