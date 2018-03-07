 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Xml.Linq;

[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("Test_RSS_LogicEngine")]

namespace RSS_LogicEngine
{
    public class HasNoElements : Exception
    {
        public HasNoElements(string message = "", Exception inner = null)
            : base(message, inner)
        { }
    }
    public class RssParser
    {
        /// <summary>
        /// function downloads a file from specified location  & parses it  to create 
        /// article objects
        /// </summary>
        /// <param name="url">webpage to download file to parse from</param>
        /// <param name="fileName">place to temparyly store the download file</param>
        /// <returns>returns a list of article objects</returns>
        public static List<Article> Parse(string url,string fileName)
        {
            const string ITEM = "item";

            DownloadFile(url, fileName);
            XElement xmlFile = XElement.Load(fileName); // load data from xml file
            if (xmlFile.HasElements == false)
            {
                throw new HasNoElements(); // has no inner elements to parse
            }
            return ParseItems(xmlFile.Descendants(ITEM).ToList()); // create article objects
        }
        /// <summary>
        /// Function downloads a file from the associated url and save the file to the input path
        /// </summary>
        /// <param name="url">place to download file from</param>
        /// <param name="fileName">name to give to downloaded file</param>
        private static void DownloadFile(string url, string fileName)
        {
            WebClient myWebClient = new WebClient();
            myWebClient.DownloadFile(url, fileName); // downloads file and saves to specified path
        }
        /// <summary>
        /// function parses the list of items to create article object from
        /// </summary>
        /// <param name="listOfItems">list of unparsed articles</param>
        /// <returns>a list of article objects</returns>
        private static List<Article> ParseItems(List<XElement> listOfItems)
        {
            const string TITLE = "title";
            const string URL = "link";

            List<Article> articleList = new List<Article>();
            foreach (XElement item in listOfItems)
            {
                articleList.Add(new Article(GetData(item.Descendants(TITLE)), GetData(item.Descendants(URL))));
            }
            return articleList;
        }
        /// <summary>
        /// function outputs the inner data of the node 
        /// IEnumerable will always be size 1
        /// </summary>
        /// <param name="node">node to grab data from</param>
        /// <returns>the inner data from the input node</returns>
        private static string GetData(IEnumerable<XElement> node)
        {
            return node.ToArray()[0].Value; // the value 0 is used since  the array size will always be of size 1
        }
    }
}
