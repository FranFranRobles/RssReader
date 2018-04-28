using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using RSS_LogicEngine.Data_Structures;

namespace RSS_LogicEngine.Controllers
{
    public class LocationsManager
    {
        private const string DATABASE_DATA_SOURCE = @"..\..\..\RSS_LogicEngine\USLocationsFile\uslocations.csv";
        private enum City { Name = 0, Latitude, Longitude }
        private class Word
        {
            public string myName;
            public bool Is_A_City;
            public string Latidude;
            public string Longitude;
            public Word(string name, bool isA_City, string latidude, string longitude)
            {
                myName = name;
                Is_A_City = isA_City;
                Latidude = latidude;
                Longitude = longitude;
            }
        }
        static private LocationsManager instance = null;
        private Dictionary<string, Word> myCache;
        private Trie locationsTrie;
        private LocationsManager()
        {
            myCache = new Dictionary<string, Word>();
            locationsTrie = new Trie();
            LoadCities();
        }
        /// <summary>
        /// Intializes the locations manager cache
        /// </summary>
        private void LoadCities()
        {
            const int FILE_HEADER = 1;
            List<string[]> CitiesWithCordinates = ParseCities(File.ReadLines(DATABASE_DATA_SOURCE).Skip(FILE_HEADER));  // parse each row
            foreach (string[] city in CitiesWithCordinates)
            {
                if (!locationsTrie.Contains(city[(int)City.Name]))       // all duplicate names will not be inserted
                {
                    locationsTrie.Insert(city[(int)City.Name], city[(int)City.Latitude], city[(int)City.Longitude]);
                }
            }
        }
        /// <summary>
        /// Creates the locations manager if it has not already been instanciated
        /// </summary>
        /// <returns> a new instances of the locationManger if its currently not 
        ///             instanciated. else returns current instance
        /// </returns>
        public static LocationsManager GetInstance()
        {
            if (instance == null)
                instance = new LocationsManager();
            return instance;
        }
        /// <summary>
        /// function returns a string containing a latidude cordinate
        /// & returns longitude in the out param
        /// Note the strings will be empty if the search came out empty
        /// </summary>
        /// <param name="text">text to do search on</param>
        /// <param name="longitude"> longitude return value</param>
        /// <returns>latitude cordinate</returns>
        public string SearchCordinates(string text, out string longitude)
        {
            string[] parsedWords = ParseSearchWord(text);
            int currWord = 0;   // count on current index in parsedWords
            Word tempWord = null;               
            bool hasCordinates = false;
            while (hasCordinates == false && currWord < parsedWords.Length)
            {
                hasCordinates = SearchWord(parsedWords[currWord], out tempWord);
                if (hasCordinates == false && currWord + 1 != parsedWords.Length) // check for a two word city
                {
                    hasCordinates = SearchWord(parsedWords[currWord] + " " + parsedWords[currWord + 1], out tempWord);
                }
                currWord++;
            }
            longitude = tempWord.Longitude;
            return tempWord.Latidude;
        }
        /// <summary>
        /// performs a search on the inputed searchValue to check & see if it is a city
        /// </summary>
        /// <param name="searchValue">text to commit a search on</param>
        /// <param name="cityInfo">search results</param>
        /// <returns>true if input text is a city</returns>
        private bool SearchWord(string searchValue, out Word cityInfo)
        {
            lock (myCache)
            {
                bool InCache = myCache.TryGetValue(searchValue, out cityInfo); // search cache first
                if (InCache == false)  //search word in the database if not found in the cache
                {
                    cityInfo = SearchDataBase(searchValue);    // perform search
                    myCache.Add(searchValue, cityInfo);   // Add search results into cache
                }
            }

            return cityInfo.Is_A_City;
        }
        /// <summary>
        /// Performs a search in the database for the inputed word
        /// </summary>
        /// <param name="searchWord">word to be searched</param>
        /// <returns>searched word with avaible details on the word</returns>
        private Word SearchDataBase(string searchWord)
        {
            string lat = "";
            string longi = "";
            bool found = locationsTrie.Search(searchWord, out lat, out longi);
            return new Word(searchWord, found, lat, longi);
        }
        /// <summary>
        /// parses a data row to extract the city name, latitude cordinate, & the longitude cordinate
        /// </summary>
        /// <param name="dataRow"> data row from the file</param>
        /// <returns>string array the where index 0 = city name, 1 = latitude, 2 = longitude</returns>
        private string[] ParseCity(string dataRow)
        {
            const int CITY_NAME = 3;
            const int LATITUDE = 5;
            const int LONGITUDE = 6;
            const char separator = ',';

            List<string> myCity = new List<string>();
            string[] parsedText = dataRow.Split(separator);
            myCity.Add(parsedText[CITY_NAME].ToLower());
            myCity.Add(parsedText[LATITUDE]);
            myCity.Add(parsedText[LONGITUDE]);
            return myCity.ToArray();
        }
        /// <summary>
        /// Parses each data row from the parsed file to create a list of "city" arrays where index 0 = city name,
        /// 1 = latitude cordinate, 2 = longitude cordinate
        /// </summary>
        /// <param name="unparsedCities">list of string rows from the data</param>
        /// <returns> a list of "city" arrays where index 0 = city name, 1 = latitude cordinate, 2 = longitude cordinate</returns>
        private List<string[]> ParseCities(IEnumerable<string> unparsedCities)
        {
            List<string[]> parsedCities = new List<string[]>();
            foreach (var city in unparsedCities)
            {
                parsedCities.Add(ParseCity(city));
            }
            return parsedCities;
        }
        /// <summary>
        /// cleans the search word to remove any unnessary charactors from the text and then split the text 
        /// into individual words
        /// </summary>
        /// <param name="text">text to be parsed</param>
        /// <returns>an array of words parsed from the text</returns>
        private string[] ParseSearchWord(string text)
        {
            char[] parserTokens = { ' ', ',', '.', ';', '!', '(', ')', '?', '&', '@', '$', '[', ']',
                '{', '}', '\'', '\\', '/','\"','-' };
            text = text.ToLower();
            return text.Split(parserTokens);
        }

    }
}
