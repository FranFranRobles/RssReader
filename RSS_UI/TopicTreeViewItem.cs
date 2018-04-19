using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace RSS_UI
{
    public class TopicTreeViewItem : TreeViewItem
    {
        public MenuItem deleteTopic;

        public TopicTreeViewItem(string topicName)
        {
            this.Header = topicName;

            deleteTopic = new MenuItem();
            deleteTopic.Header = "Delete Topic";
            this.ContextMenu = new ContextMenu();
            this.ContextMenu.Items.Add(deleteTopic);
        }

    }
}
