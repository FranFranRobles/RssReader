using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSS_UI
{
    public class ArticleListItem
    {
        // Property definitions so that the articleList can bind to an ArticleListItem and organize properly
        public String Date { get; set; }        // Allows the user to see the date the article was published in the UI, binding property
        public String Title { get; set; }       // Allows the user to see the title of the article in the UI, binding property
        public String URL { get; set; }         // Allows the UI to navigate to the appropriate page in the browser 
        public String Description { get; set; } // Allows the user to see the summary of the article in the UI
        public String Read { get; set; }          // Allows the user to see if articles has been read or not
    }
}
