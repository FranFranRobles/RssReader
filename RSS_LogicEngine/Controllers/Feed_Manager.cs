using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Xml;
using System.Xml.Linq;

[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("Test_RSS_LogicEngine")]

namespace RSS_LogicEngine
{
    public class Feed_Manager
    {
        private static Feed_Manager instance;
        private Feed_Manager()
        {
            feeds = new HashSet<Feed>();
            Update_Manager.Get_Instance().Update_Tasks += Update_Feeds;
        }
        public static Feed_Manager Get_Instance()
        {
            if (instance == null)
                instance = new Feed_Manager();
            return instance;
        }
        private HashSet<Feed> feeds;
        public void Update_Feeds(object sender, EventArgs e)
        {
            foreach (Feed f in feeds)
                Update_Feed(f);
        }
        public void Add_Feed(Feed f) => feeds.Add(f);
        public bool Remove_Feed(Feed f) => feeds.Remove(f);
        public void Update_Feed(Feed f)
        {
            lock (this)
            {
                string rss_filename = "rss_temp.xml";
                (new WebClient()).DownloadFile(f.URI, rss_filename);
                f.Clear_Articles();
                List<Article> currFeed = ParseArticles(rss_filename);
                var a = Task.Run(() => FindCordinates(currFeed));
                f.Add_Articles(currFeed);
            }
        }
        /// <summary>
        /// attempts to find and set a possible location origin to the article
        /// </summary>
        /// <param name="articles"></param>
        /// <returns></returns>
        private List<Article> FindCordinates(List<Article> articles)
        {
            string latitude = "";
            string longitude = "";
            Controllers.LocationsManager myManager = Controllers.LocationsManager.GetInstance();
            foreach (Article article in articles)
            {
                if (article.Latitude == "" || article.Longitude == "")
                {
                    latitude = myManager.SearchCordinates(article.Title, out longitude);
                    if (latitude == "" && longitude == "")
                    {
                        latitude = myManager.SearchCordinates(article.Summary, out longitude);
                    }
                    article.Latitude = latitude;
                    article.Longitude = longitude;
                }
            }
            return articles;
        }
        /// <summary>
        /// Parses the unparsed articles to create article objects
        /// </summary>
        /// <remarks>
        /// this function should be private but it is only public for testing purposes
        /// </remarks>
        /// <param name="listOfUnparsedArticles"> list of articles to create objects out of</param>
        /// <returns>a list of article objects</returns>
        public List<Article> ParseArticles(string rss_filename)
        {
            const string ARTICLE = "item"; // search tag for Article Content
            const string TITLE = "title";
            const string URL = "link";
            const string DESCRIPTION = "description";
            const string PUB_DATE = "pubDate";

            List<Article> articleList = new List<Article>();
            using (XmlReader temp = XmlReader.Create(rss_filename))
            {
                var file = XDocument.Load(temp);
                foreach (XElement article in file.Descendants(ARTICLE))
                {
                    string pubDate = "";
                    try
                    {
                        pubDate = article.Element(PUB_DATE).Value;
                    }
                    catch (NullReferenceException)
                    {
                        pubDate = "N/A";
                    }
                    articleList.Add(new Article(article.Element(TITLE).Value, article.Element(URL).Value, article.Element(DESCRIPTION).Value, pubDate));
                }
               

            }
           
            return articleList;
        }
        /// <summary>
        /// function retreves all article titles cordinates from the feed manager
        /// elements are return in the following fashing index: title = 0, latitude = 1, longitude = 2 
        /// <remarks>this is not the final product of the function.....
        ///          we need to have this placed in the correct locations & then  needs the 
        ///          method of return to maybe be mor efficient...
        ///          this was only done this way just to get it done....</remarks>
        /// </summary>
        /// <returns></returns>
        public List<string[]> GetAllTitlesAndCordinates()
        {
            const int ARTICLE_TITLE = 0;
            const int LATITUDE = 1;
            const int LONGITUDE = 2;
            List<string[]> holder = new List<string[]>();
            List<string> tempArticle = new List<string>();
            foreach (Feed f in feeds)
            {
                foreach (Article a in f.Get_Articles())
                {
                    tempArticle.Add(a.Title);
                    tempArticle.Add(a.Latitude);
                    tempArticle.Add(a.Longitude);
                    holder.Add(tempArticle.ToArray());
                }
            }
            return holder;
        }

    }
}
