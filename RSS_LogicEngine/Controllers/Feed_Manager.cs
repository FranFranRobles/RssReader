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
            const string ARTICLE = "item"; // search tag for Article Content
            string rss_filename = "rss_temp.xml";
            (new WebClient()).DownloadFile(f.URI, rss_filename);

            //XmlReaderSettings xml_reader_settings = new XmlReaderSettings();
            //xml_reader_settings.DtdProcessing = DtdProcessing.Parse;
            //XmlReader parser = XmlReader.Create(rss_filename, xml_reader_settings);

            XElement xmlFile = XElement.Load(rss_filename);   //load file
            f.Add_Articles(ParseArticles(xmlFile.Descendants(ARTICLE)));
        }
        /// <summary>
        /// Parses the unparsed articles to create article objects
        /// </summary>
        /// <param name="listOfUnparsedArticles"> list of articles to create objects out of</param>
        /// <returns>a list of article objects</returns>
        private static List<Article> ParseArticles(IEnumerable<XElement> listOfUnparsedArticles)
        {
            const string TITLE = "title";
            const string URL = "link";
            const string DESCRIPTION = "description";
            const string PUB_DATE = "pubDate";
            List<Article> articleList = new List<Article>();
            foreach (XElement item in listOfUnparsedArticles)
            {
                string pubDate = null;
                if ((pubDate = item.Element(PUB_DATE).Value) == null)
                    pubDate = "N/A";
                articleList.Add(new Article(item.Element(TITLE).Value, item.Element(URL).Value, item.Element(DESCRIPTION).Value, pubDate));
            }
            return articleList;
        }
    }
}
