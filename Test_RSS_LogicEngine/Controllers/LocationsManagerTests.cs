using Microsoft.VisualStudio.TestTools.UnitTesting;
using RSS_LogicEngine.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSS_LogicEngine.Controllers.Tests
{
    [TestClass()]
    public class LocationsManagerTests
    {
        const string LOCATIONS_MGR_TESTS = "Locations Manager Tests";
        const string INVALID_CORDINATE = "";
        const int TEST_TIMEOUT = 10000; // 10 seconds in milliseconds


        [TestMethod()]
        [TestCategory(LOCATIONS_MGR_TESTS)]
        public void GetInstanceTest()
        {
            Assert.IsNotNull(LocationsManager.GetInstance(), "locations Manager Object is set to a null reference");
        }
        [TestMethod()]
        [TestCategory(LOCATIONS_MGR_TESTS)]
        public void GetSecondInstanceTest()
        {
            LocationsManager myLocations = LocationsManager.GetInstance();
            Assert.IsNotNull(myLocations, "locations Manager Object is set to a null reference");
            Assert.AreEqual(myLocations, LocationsManager.GetInstance(), "the objects be identical");
        }
        [TestMethod()]
        [Timeout(TEST_TIMEOUT)]
        [TestCategory(LOCATIONS_MGR_TESTS)]
        public void HasCordinatesTest()
        {
            string[] validCities = { "everett", "freeday seattle", "in homes in miami", "lost in MiAmI" };
            LocationsManager myLocations = LocationsManager.GetInstance();
            foreach (string city in validCities)
            {
                Assert.IsTrue(myLocations.HasCordinates(city));
                Assert.AreNotEqual(INVALID_CORDINATE, myLocations.Latidude(), "search should return a latidude cordinate");
                Assert.AreNotEqual(INVALID_CORDINATE, myLocations.Longitude(), "search should return a longitude cordinate");
            }
        }
        [TestMethod()]
        [Timeout(TEST_TIMEOUT)]
        [TestCategory(LOCATIONS_MGR_TESTS)]
        public void NoCordinatesTest()
        {
            string[] notACity = {"a", "blah", "sjsj blah in", "the in F a"};
            LocationsManager myLocations = LocationsManager.GetInstance();
            foreach (string text in notACity)
            {
                Assert.IsFalse(myLocations.HasCordinates(text));
                Assert.AreEqual(INVALID_CORDINATE, myLocations.Latidude(), "search should not return a latidude cordinate");
                Assert.AreEqual(INVALID_CORDINATE, myLocations.Longitude(), "search should not return a longitude cordinate");
            }
        }
    }
}