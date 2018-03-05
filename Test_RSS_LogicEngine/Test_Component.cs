using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RSS_LogicEngine;

namespace Test_RSS_LogicEngine
{
    [TestClass]
    public class Test_Component
    {
        [TestMethod]
        public void Test_NoChildrenOnConstruction()
        {
            Component c = new Component("name");
            Assert.AreEqual(false, c.Has_Children(), "Componenet asserts that it has children after construction w/o children");
            Assert.AreEqual(0, c.Get_Children().Count, "Component returns children after construction w/o children");
        }
        [TestMethod]
        public void Test_ValidNameOnConstruction()
        {
            Component c = new Component("name");
            Assert.AreEqual("name", c.Name, "initialization of Component name failed");
        }
        [TestMethod]
        public void Test_ChangeName()
        {
            Component c = new Component("name");
            c.Name = "new name";
            Assert.AreEqual("new name", c.Name, "changing Component name failed");
        }
        [TestMethod]
        public void Test_ConstructWithChildren()
        {
            Component c;
            List<Component> components = new List<Component>();
            const int children_to_insert = 100;
            for (int i = 1; i <= children_to_insert; i++)
            {
                components.Add(new Component("child#" + i.ToString()));
                c = new Component("parent", components);
                Assert.AreEqual(true, c.Has_Children(), "Componenet initialized with children asserts no children");
                Assert.AreEqual(i, c.Get_Children().Count, "Component doesn't have same amount of children as it was initialized with");
                c.
            }
        }
    }
}
