using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RSS_LogicEngine;

namespace Test_RSS_LogicEngine
{
    [TestClass]
    public class Test_Component_Tree
    {
        static Component_Factory component_factory = Component_Factory.Get_Instance();
        [TestMethod]
        [TestCategory("Componet Tree Tests")]
        public void Test_EmptyOnConstruction()
        {
            Component_Tree t = new Component_Tree(component_factory.Create_Channel());
            Assert.AreNotEqual(null, t.Get_Component_At("/"));
            Assert.AreEqual(0, t.Get_Component_At("/").Get_Children().Count);
        }
        [TestMethod]
        [TestCategory("Componet Tree Tests")]
        public void Test_InsertSingleChild()
        {
            Component_Tree t = new Component_Tree(component_factory.Create_Channel());
            t.Insert_Component_At("/name", component_factory.Create_Channel());
            Assert.AreEqual(1, t.Get_Component_At("/").Get_Children().Count);
            Assert.AreNotEqual(null, t.Get_Component_At("/name"));
            
        }
    }
}
