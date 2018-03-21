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

    class Feed_Manager
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
            string rss_filename = "rss_temp.xml";
<<<<<<< HEAD
                (new WebClient()).DownloadFile(f.URI, rss_filename);
=======
            (new WebClient()).DownloadFile(f.URI, rss_filename);
>>>>>>> b8d0e55db8f2850e6109394585b2c243fa5399e8
            (new WebClient()).DownloadFile(f.URI, rss_filename);
            f.Add_Articles(ParseArticles(rss_filename));
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

<<<<<<< HEAD
            XElement xmlFile = XElement.Load(rss_filename); 
=======
            XElement xmlFile = XElement.Load(rss_filename);
>>>>>>> b8d0e55db8f2850e6109394585b2c243fa5399e8

            List<Article> articleList = new List<Article>();
            foreach (XElement article in xmlFile.Descendants(ARTICLE))
            {
                string pubDate = "";
                try
                {
                    pubDate = article.Element(PUB_DATE).Value;
                }
                catch (NullReferenceException)
                {
                    pubDate = "N/A";
<<<<<<< HEAD
=======
                    throw new XmlException();
>>>>>>> b8d0e55db8f2850e6109394585b2c243fa5399e8
                }
                articleList.Add(new Article(article.Element(TITLE).Value, article.Element(URL).Value, article.Element(DESCRIPTION).Value, pubDate));
            }
            return articleList;
        }
    }
<<<<<<< HEAD
}
=======
}
>>>>>>> b8d0e55db8f2850e6109394585b2c243fa5399e8
