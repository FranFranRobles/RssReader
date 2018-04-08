using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("Test_RSS_LogicEngine")]

namespace RSS_LogicEngine
{
    public class Article
    {
        #region Article Atributes
        private string title; // name of the article
        private string url; //location of full article
        private string summary; // summary of the article
        private string publication_date; // date published
        private bool read; // True if article has been read, false otherwise
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
    }
}
