using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Xml;

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
        public List<Article> Update_Feed(Feed f)
        {
            string rss_filename = "rss_temp.xml";
            (new WebClient()).DownloadFile(f.URI, rss_filename);
            XmlReaderSettings xml_reader_settings = new XmlReaderSettings();
            xml_reader_settings.DtdProcessing = DtdProcessing.Parse;
            XmlReader parser = XmlReader.Create(rss_filename, xml_reader_settings);

            return (List<Article>)null;
        }
        ///// <summary>
        ///// function parses the list of items to create article object from
        ///// </summary>
        ///// <param name="listOfItems">list of unparsed articles</param>
        ///// <returns>a list of article objects</returns>
        //private static List<Article> ParseItems(List<XElement> listOfItems)
        //{
        //    const string TITLE = "title";
        //    const string URL = "link";

        //    List<Article> articleList = new List<Article>();
        //    foreach (XElement item in listOfItems)
        //    {
        //        articleList.Add(new Article(GetData(item.Descendants(TITLE)), GetData(item.Descendants(URL))));
        //    }
        //    return articleList;
        //}
        ///// <summary>
        ///// function outputs the inner data of the node 
        ///// IEnumerable will always be size 1
        ///// </summary>
        ///// <param name="node">node to grab data from</param>
        ///// <returns>the inner data from the input node</returns>
        //private static string GetData(IEnumerable<XElement> node)
        //{
        //    return node.ToArray()[0].Value; // the value 0 is used since  the array size will always be of size 1
        //}
    }
}
