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
        const string NOT_APPLICABLE = "N/A";
        #region Article Atributes
        
        private string title; // name of the article
        private string articleUrl; //location of full article
        private string discription; // summary of the article
        private string pubDate; // date published
        #endregion
        /// <summary>
       /// Article object CTOR
        /// </summary>
        /// <param name="_title">name of the article</param>
        /// <param name="_articleUrl">source of the complete article</param>
        /// <param name="summary">short summary of the article</param>
        /// <param name="_pubDate">date published</param>
        /// <param name="_catagories">list of search keywords</param>
        public Article(string _title, string _articleUrl, string summary = NOT_APPLICABLE,
            string _pubDate = NOT_APPLICABLE, List<string> _catagories = null)
        {
            title = _title;
            articleUrl = _articleUrl;
            discription = summary;
            pubDate = _pubDate;
        }
        /// <summary>
        /// function grabs the title of the article
        /// </summary>
        public string Title
        {
            get { return title; }
        }
        /// <summary>
        /// function grabs the url of the full article
        /// </summary>
        public string ArticleUrl
        {
            get { return articleUrl; }
        }
        /// <summary>
       /// function grabs the articles summary
        /// </summary>
        public string ArticleSummary
        {
           get { return discription; }
        }
        /// <summary>
        /// function grabs the publication date fo the article
        /// </summary>
        public string PublicationDate
        {
            get { return pubDate; }
        }
    }
}
