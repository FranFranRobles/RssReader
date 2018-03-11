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
    public class RssParserTests
    {
        [TestMethod()]
        public void ParseTest()
        {
            Feed_Manager temp = Feed_Manager.Get_Instance();
            temp.Update_Feed(new Feed("http://abcnews.go.com/abcnews/topstories"));
        }
    }
}