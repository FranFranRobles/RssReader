using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("Test_RSS_LogicEngine")]
/*
 * parse the entire file completely & parse them into words... then place into a balanced tree
 * create a query off the subtree
 * search search the sub tree
 * verify results
 * update the article
 */
namespace RSS_LogicEngine
{
    public class Article
    {
        #region Article Atributes
        private string title;                // name of the article
        private string url;                  //location of full article
        private string summary;              // summary of the article
        private string publication_date;     // date published
        private bool read;                   // True if article has been read, false otherwise
        private string latitude;             // latidude cordinate of most probable location of origin
        private string longitude;            // longitude cordinate of the most probable location of origin
        #endregion
        /// <summary>
        /// Article object CTOR
        /// </summary>
        /// <param name="title">name of the article</param>
        /// <param name="url">source of the complete article</param>
        /// <param name="summary">short summary of the article</param>
        /// <param name="publication_date">date published</param>
        /// <param name="read">True if article has been read, false otherwise</param>
        public Article(string title, string url, string summary, string publication_date)
        {
            Title = title;
            URL = url;
            Summary = summary;
            Publication_Date = publication_date;
            Read = false;
            latitude = "";
            longitude = "";
        }
        /// <summary>
        /// Title of article
        /// </summary>
        public string Title
        {
            get => title;
            private set => title = value;
        }
        /// <summary>
        /// URL where the article can be found
        /// </summary>
        public string URL
        {
            get => url;
            private set => url = value;
        }
        /// <summary>
        /// Brief summary of article
        /// </summary>
        public string Summary
        {
            get => summary;
            private set => summary = value;
        }
        /// <summary>
        /// Publication date of Article
        /// </summary>
        public string Publication_Date
        {
            get => publication_date;
            private set => publication_date = value;
        }
        public bool Read
        {
            get => read;
            set => read = value;
        }
        public string Latitude
        {
            get => latitude;
            set => latitude = value;
        }
        public string Longitude
        {
            get => longitude;
            set => longitude = value;
        }
    }
}
