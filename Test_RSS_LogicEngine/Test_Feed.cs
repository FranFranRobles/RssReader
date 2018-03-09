using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RSS_LogicEngine;

namespace Test_RSS_LogicEngine
{
    [TestClass]
    public class Test_Feed
    {
        [TestMethod]
        [TestCategory("Feed Tests")]
        public void Test_FeedEmptyOnConstruction()
        {
            Feed f = new Feed("url");
            Assert.AreEqual(0, f.Get_Articles().Count, "Feed should be empty on construction");
        }
        [TestMethod]
        [TestCategory("Feed Tests")]
        public void Test_FeedIsLeaf()
        {
            Feed f = new Feed("url");
            Assert.AreEqual(true, f.Is_Leaf(), "Feed should be a leaf");
        }
        [TestMethod]
        [TestCategory("Feed Tests")]
        public void Test_FeedHasNoChildren()
        {
            Feed f = new Feed("url");
            Random r = new Random();
            Assert.AreEqual(false, f.Has_Children(), "Feed should not have children");
            Assert.AreEqual(0, f.Get_Children().Count, "Feed has nonzero number of children");
            for (int i = 0; i < r.Next(0, 100); i++)
                Assert.AreEqual(null, f.Get_Child(Random_String(r, r.Next(0, 100))));
        }
        private string Random_String(Random r, int length)
        {
            const string chars = @"ABCDEFGHIJKLMNOPQRSTUVWXYZ
                                   abcdefghijklmnopqrstuvwxyz
                                   0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[r.Next(s.Length)]).ToArray());
        }
    }
}
