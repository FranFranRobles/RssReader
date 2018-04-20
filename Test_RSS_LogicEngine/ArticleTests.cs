using Microsoft.VisualStudio.TestTools.UnitTesting;
using RSS_LogicEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSS_LogicEngine.Tests
{
    [TestClass()]
    public class ArticleTests
    {
        #region Test Constants
        const string SAMPLE_URL = "www.wsu.edu";
        const string SAMPLE_TITLE = "Test title";
        const string TEST_CATEGORY = "Article Unit Tests";
        const string SAMPLE_SUMMARY = "This is a test article";
        const string SAMPLE_PUB_DATE = "Sun, 01 Jan 2018 19:05:23 -0500";
        const string INCORRECT_TITLE_MSG = "Incorrect article title returned";
        const string INCORRECT_URL_MSG = "Incorrect article URl returned";
        const string INCORRECT_SUMMARY_MSG = "Incorrect article Summary found";
        const string INCORRECT_PUB_DATE_MSG = "Incorrect publication date found";
        #endregion

        [TestMethod()]
        [TestCategory(TEST_CATEGORY)]
        public void TestDefaultCTOR()
        {
            Article myArticle = new Article(SAMPLE_TITLE, SAMPLE_URL, SAMPLE_SUMMARY, SAMPLE_PUB_DATE);
            TestConstructor(myArticle);
        }
        [TestMethod()]
        [TestCategory(TEST_CATEGORY)]
        public void TestArticleCordinates()
        {
            const string LATITUDE = "41.0877";
            const string LONGITUDE = "-73.7768";
            Article myArticle = new Article(SAMPLE_TITLE, SAMPLE_URL, SAMPLE_SUMMARY, SAMPLE_PUB_DATE);
            TestConstructor(myArticle);
            myArticle.Latitude = LATITUDE;
            myArticle.Longitude = LONGITUDE;
            Assert.AreEqual(LATITUDE, myArticle.Latitude, "Article does not have the correct cordinate");
            Assert.AreEqual(LONGITUDE, myArticle.Longitude, "Article does not have the correct cordinate");

        }
        [TestMethod()]
        [TestCategory(TEST_CATEGORY)]
        public void TestArticleReadRead()
        {
            Article myArticle = new Article(SAMPLE_TITLE, SAMPLE_URL, SAMPLE_SUMMARY, SAMPLE_PUB_DATE);
            TestConstructor(myArticle);
            myArticle.Read = true;
            Assert.IsTrue(myArticle.Read, "Article should have been read");

        }
        public void TestConstructor(Article myArticle)
        {
            Assert.AreEqual(SAMPLE_TITLE, myArticle.Title, INCORRECT_TITLE_MSG);
            Assert.AreEqual(SAMPLE_SUMMARY, myArticle.Summary, INCORRECT_SUMMARY_MSG);
            Assert.AreEqual(SAMPLE_URL, myArticle.URL, INCORRECT_URL_MSG);
            Assert.AreEqual(SAMPLE_PUB_DATE, myArticle.Publication_Date, INCORRECT_PUB_DATE_MSG);
            Assert.AreEqual("", myArticle.Latitude, "Article should not contain a latitude cordinate");
            Assert.AreEqual("", myArticle.Longitude, "Article should not contain a longitude cordinate");
            Assert.IsFalse(myArticle.Read, "article should not be read");
        }
    }
}