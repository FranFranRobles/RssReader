using Microsoft.VisualStudio.TestTools.UnitTesting;
using RSS_LogicEngine.Data_Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSS_LogicEngine.Data_Structures.Tests
{
    [TestClass()]
    public class TrieTests
    {
        const string TRIE_TESTS = "Trie Tests";
        readonly string[] SAMPLE_NAMES = { "Everett", "seattle", "PorTland", "San FrancIsco" };
        const string NOT_FOUND = "blah";

        [TestCategory(TRIE_TESTS)]
        [TestMethod()]
        public void TrieCTOR_Test()
        {
            Trie mytrie = new Trie();
            TestContainment(mytrie, SAMPLE_NAMES, false);
        }
        [TestCategory(TRIE_TESTS)]
        [TestMethod()]
        public void insertTest()
        {
            Trie mytrie = new Trie();
            TestContainment(mytrie, SAMPLE_NAMES, false);
            InsertAll(mytrie, SAMPLE_NAMES);
            TestContainment(mytrie, SAMPLE_NAMES, true);
        }
        [TestCategory(TRIE_TESTS)]
        [TestMethod()]
        public void SearchTest()
        {
            int compareVal = 0;
            string tempValOne = "";
            string tempValTwo = "";
            Trie mytrie = new Trie();
            TestContainment(mytrie, SAMPLE_NAMES, false);
            InsertAll(mytrie, SAMPLE_NAMES);
            foreach (string city in SAMPLE_NAMES)
            {
                Assert.IsTrue(mytrie.Search(city, out tempValOne, out tempValTwo), "Value should be found within the Trie");
                Assert.AreEqual((compareVal++).ToString(), tempValOne, "Values should be equal");
                Assert.AreEqual((compareVal++).ToString(), tempValTwo, "Values should be equal");
            }
            Assert.IsFalse(mytrie.Search(NOT_FOUND, out tempValOne, out tempValTwo), "Value should be found within the Trie");
            Assert.AreEqual("", tempValOne, "Values should be equal");
            Assert.AreEqual("", tempValTwo, "Values should be equal");
        }
        [TestCategory(TRIE_TESTS)]
        [TestMethod()]
        public void ContainsTest()
        {
            string tempValOne = "";
            string tempValTwo = "";
            Trie mytrie = new Trie();
            TestContainment(mytrie, SAMPLE_NAMES, false);
            InsertAll(mytrie, SAMPLE_NAMES);
            foreach (string city in SAMPLE_NAMES)
            {
                Assert.AreEqual(mytrie.Search(city, out tempValOne, out tempValTwo), mytrie.Contains(city), "both values should be found");
            }
            Assert.AreEqual(mytrie.Search(NOT_FOUND, out tempValOne, out tempValTwo), mytrie.Contains(NOT_FOUND), "both values should be found");
        }
        public void TestContainment(Trie myTrie, string[] sample, bool expected)
        {
            foreach (string city in sample)
            {
                Assert.AreEqual(expected, myTrie.Contains(city), expected == true ? "Trie should not be empty" : "Trie should be empty");
            }
        }
        public void InsertAll(Trie myTrie, string[] data)
        {
            int count = 0;
            foreach (string elem in data)
            {
                myTrie.Insert(elem, (count++).ToString(), (count++).ToString());
            }
        }
    }
}