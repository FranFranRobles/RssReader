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
                string latitude = "";
                string longitude = "";
                latitude = myLocations.SearchCordinates(city, out longitude);
                Assert.AreNotEqual("", latitude, "Search should have found a latidude cordinate");
                Assert.AreNotEqual("", longitude, "Search should have found a longitude cordinate");
            }
        }
        [TestMethod()]
        //[Timeout(TEST_TIMEOUT)]
        [TestCategory(LOCATIONS_MGR_TESTS)]
        public void NoCordinatesTest()
        {
            string[] notACity = {"a", "blah", "sjsj blah"};
            LocationsManager myLocations = LocationsManager.GetInstance();
            foreach (string text in notACity)
            {
                string latitude = "";
                string longitude = "";
                latitude = myLocations.SearchCordinates(text, out longitude);
                Assert.AreEqual("", latitude, "Search should have found a latidude cordinate on  {0}", text);
                Assert.AreEqual("", longitude, "Search should have found a longitude cordinate");
            }
        }
    }
}