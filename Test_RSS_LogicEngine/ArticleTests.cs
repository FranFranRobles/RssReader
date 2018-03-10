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
        const string NOT_APPLICABLE = "N/A";
        const string TEST_ARTCLE_URL = "www.wsu.edu";
        const string TEST_ARTCLE_TITLE = "Test title";
        const string TEST_CATEGORY = "Article Unit Tests";
        const string TEST_ARTCLE_SUMMARY = "This is a test article";
        const string TEST_PUB_DATE = "Sun, 01 Jan 2018 19:05:23 -0500";
        const string INCORRECT_TITLE_MSG = "Incorrect article title returned";
        const string INCORRECT_URL_MSG = "Incorrect article URl returned";
        const string INCORRECT_SUMMARY_MSG = "Incorrect article Summary found";
        const string INCORRECT_PUB_DATE_MSG = "Incorrect publication date found";
        #endregion

        //[TestMethod()]
        //[TestCategory(TEST_CATEGORY)]
        //public void TestDefaultCTOR()
        //{
        //    Article myArticle = new Article(TEST_ARTCLE_TITLE,TEST_ARTCLE_URL);
        //    Assert.AreEqual(TEST_ARTCLE_TITLE, myArticle.Title, INCORRECT_TITLE_MSG);
        //    Assert.AreEqual(TEST_ARTCLE_URL, myArticle.ArticleUrl, INCORRECT_TITLE_MSG);
        //    Assert.AreEqual(NOT_APPLICABLE, myArticle.ArticleSummary, INCORRECT_SUMMARY_MSG);
        //    Assert.AreEqual(NOT_APPLICABLE, myArticle.PublicationDate, INCORRECT_PUB_DATE_MSG);
        //}
        //[TestMethod()]
        //[TestCategory(TEST_CATEGORY)]
        //public void TestCTORWithArticleSummary()
        //{
        //    Article myArticle = new Article(TEST_ARTCLE_TITLE, TEST_ARTCLE_URL,TEST_ARTCLE_SUMMARY);
        //    Assert.AreEqual(TEST_ARTCLE_TITLE, myArticle.Title, INCORRECT_TITLE_MSG);
        //    Assert.AreEqual(TEST_ARTCLE_URL, myArticle.ArticleUrl, INCORRECT_TITLE_MSG);
        //    Assert.AreEqual(TEST_ARTCLE_SUMMARY, myArticle.ArticleSummary, INCORRECT_SUMMARY_MSG);
        //    Assert.AreEqual(NOT_APPLICABLE, myArticle.PublicationDate, INCORRECT_PUB_DATE_MSG);
        //}
        //[TestMethod()]
        //[TestCategory(TEST_CATEGORY)]
        //public void TestCTORWithPublicationDate()
        //{
        //    Article myArticle = new Article(TEST_ARTCLE_TITLE, TEST_ARTCLE_URL, TEST_ARTCLE_SUMMARY, TEST_PUB_DATE);
        //    Assert.AreEqual(TEST_ARTCLE_TITLE, myArticle.Title, INCORRECT_TITLE_MSG);
        //    Assert.AreEqual(TEST_ARTCLE_URL, myArticle.ArticleUrl, INCORRECT_TITLE_MSG);
        //    Assert.AreEqual(TEST_ARTCLE_SUMMARY, myArticle.ArticleSummary, INCORRECT_SUMMARY_MSG);
        //    Assert.AreEqual(TEST_PUB_DATE, myArticle.PublicationDate, INCORRECT_PUB_DATE_MSG);
        //}
    }
}