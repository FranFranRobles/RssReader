using Microsoft.VisualStudio.TestTools.UnitTesting;
using RSS_LogicEngine;
using System;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Xml;
using System.Xml.Linq;

namespace RSS_LogicEngine.Tests
{
    [TestClass()]
    public class RssParserTests
    {
        const string PROVIDER_TYPE = "Microsoft.VisualStudio.TestTools.DataSource.XML";
        const string FILE = @"~\..\..\..\TestingDataSources\RssSourceLocations.xml";
        const string TEST_RSS_SOURCE = "RssSource";
        const string TEST_NON_RSS_SOURCE = "nonRss";
        const string PARSER_TESTS = "Parser Tests";
        const string URL = "url";
        const string BAD_FEED_INSTANTIATION = "Feed should not Contain any Articles";
        const string BAD_FEED_UPDATE = "Update_Feed did not add articles";

        #region Test intializer
        private TestContext testContextInstance;
        public TestContext TestContext
        {
            get { return testContextInstance; }
            set { testContextInstance = value; }
        }
        Feed_Manager MyFeedManager = null;
        [TestInitialize]
        public void TestIntializer() => MyFeedManager = Feed_Manager.Get_Instance();
        #endregion

        [TestMethod()]
        [TestCategory(PARSER_TESTS)]
        [DataSource(PROVIDER_TYPE, FILE, TEST_RSS_SOURCE, DataAccessMethod.Sequential)]
        public void ParseFromRssSource()
        {
            string url = TestContext.DataRow[URL].ToString();
            Feed myFeed = new Feed(url);
            Assert.AreEqual(url, myFeed.URI);
            Assert.AreEqual(0, myFeed.Get_Articles().Count, BAD_FEED_INSTANTIATION);
            MyFeedManager.Update_Feed(myFeed);
            Assert.AreNotEqual(0, myFeed.Get_Articles().Count, BAD_FEED_UPDATE);
            List<Article> listOfArticles = myFeed.Get_Articles();
            for (int index = 1; index < listOfArticles.Count; index++)
            {
                CompareArticles(listOfArticles[index], listOfArticles[index - 1]);
            }
        }
        //[TestMethod()]
        //[TestCategory(PARSER_TESTS)]
        //[DataSource(PROVIDER_TYPE, FILE, TEST_NON_RSS_SOURCE, DataAccessMethod.Sequential)]
        //public void ParseFromNonRssSource()
        //{
            //string url = TestContext.DataRow[URL].ToString();
            //bool caughtException = false;
            //Feed myFeed = new Feed(url);
            //Assert.AreEqual(url, myFeed.URI);
            //Assert.AreEqual(0, myFeed.Get_Articles().Count, BAD_FEED_INSTANTIATION);
            //try
            //{
            //    MyFeedManager.Update_Feed(myFeed);
            //}
            //catch (System.Net.WebException)
            //{
            //    caughtException = true;
            //}
            //catch (System.Xml.XmlException) // have some weird error here
            //{
            //    caughtException = true;
            //}
            //Assert.IsTrue(caughtException, "Failed toThow an exception");
        //}
        [TestMethod()]
        [TestCategory(PARSER_TESTS)]
        public void ParseFromSampleRssFile()
        {
            const string SAMPLE_RSS_FILE = @"~\..\..\..\TestingDataSources\sampleRssFile.xml";
            const int AMOUNT_OF_ARTICLES = 25;
            List<Article> listOfArticles = MyFeedManager.ParseArticles(SAMPLE_RSS_FILE);
            Assert.AreEqual(AMOUNT_OF_ARTICLES, listOfArticles.Count, BAD_FEED_UPDATE);
            for (int index = 2; index < listOfArticles.Count; index++)
            {
                CompareArticles(listOfArticles[index], listOfArticles[index - 1]);
                CompareArticles(listOfArticles[index], listOfArticles[index - 2]);
            }
        }
        /// <summary>
        /// function campares two artice to check if they are identical objects
        /// </summary>
        /// <param name="firstArticle">first article to compare </param>
        /// <param name="CompareArticle">second article to compare </param>
        public void CompareArticles(Article firstArticle, Article CompareArticle)
        {
            const string ERROR_MSG = "Articles have identical data";
            Assert.AreNotEqual(firstArticle.Title, CompareArticle.Title, ERROR_MSG);
            Assert.AreNotEqual(firstArticle.URL, CompareArticle.URL, ERROR_MSG);
            Assert.AreNotEqual(firstArticle.Summary, CompareArticle.Summary, ERROR_MSG);
        }
    }
}