using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace RSS_LogicEngine.Controllers
{
    public class LocationsManager
    {
        private class Word
        {
            private string myName;
            private bool IsA_City;
            private string Latidude;
            private string Longitude;
            public Word(string name, bool isA_City, string latidude, string longitude)
            {
                myName = name;
                IsA_City = isA_City;
                Latidude = latidude;
                Longitude = longitude;
            }
            public bool IsACity() => IsA_City;
            /// <summary>
            /// retrieves the words latidude value
            /// </summary>
            /// <returns></returns>
            public string GetLatidude() => Latidude;
            /// <summary>
            /// retrieves the words longitude value 
            /// </summary>
            /// <returns></returns>
            public string GetLongitude() => Longitude;
        }
        static private LocationsManager instance = null;
        private Dictionary<string, Word> myDictionary;
        private string latidude = "";
        private string longitude = "";
        private LocationsManager()
        {
            LoadCities();
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
        /// retrives the the latitude cordinate from the last search
        /// </summary>
        /// <returns></returns>
        public string Latidude() => latidude;
        /// <summary>
        /// retrieves the longitude cordinate from the last search
        /// </summary>
        /// <returns></returns>
        public string Longitude() => longitude;
        /// <summary>
        /// function returns a bool indicating whether the input text has a posiible cordinates
        ///         If true is return the latitude & longitude properties will be set to the last
        ///         searched text
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public bool HasCordinates(string text)
        {
            string[] parsedWords = ParseFile(text);
            int currWord = 0;   // count on current index in parsedWords
            Word tempWord = null;               
            bool hasCordinates = false; // need to add code for it to work  //
            while (hasCordinates == false && currWord < parsedWords.Length)
            {
                hasCordinates = SearchWord(parsedWords[currWord], out tempWord);
                if (hasCordinates == false && currWord + 1 != parsedWords.Length) // check for a two word city
                {
                    hasCordinates = SearchWord(parsedWords[currWord] + " " + parsedWords[currWord + 1], out tempWord);
                }
                currWord++;
            }
            latidude = tempWord.GetLatidude();
            longitude = tempWord.GetLongitude();
            return hasCordinates;
        }

        private bool SearchWord(string currWord, out Word tempWord)
        {
             bool foundInDictionary = myDictionary.TryGetValue(currWord, out tempWord);
            // currently only search for one word needs to be able to chain words together
            if (foundInDictionary == false) // single word not found  in the dictionary
            {
                tempWord = SearchDataBase(currWord);
                myDictionary.Add(currWord, tempWord); // add word with it city information

            }

            return tempWord.IsACity();
        }

        private Word SearchDataBase(string word)
        {
            const string LOCATIONS_FILE = @"..\..\..\RSS_LogicEngine\USLocationsFile\uslocations.csv";
            const int HEADER = 1;
            IOrderedEnumerable<string[]> myQuery = CreateQuery(File.ReadLines(LOCATIONS_FILE).Skip(HEADER), word);
            Word searchedWord = VerifyResults(myQuery, word);
            return searchedWord;
        }

        private Word VerifyResults(IOrderedEnumerable<string[]> myQuery, string word)
        {
            bool verifiedWord = false;
            int curr = 0;
            Word myWord = null;
            while (verifiedWord == false && curr < myQuery.Count())
            {
                if ((word.Length < myQuery.ElementAt(curr)[0].Length))
                {
                    break;
                }
                if (word == myQuery.ElementAt(curr)[0])
                {
                    myWord = new Word(myQuery.ElementAt(curr)[0], true, myQuery.ElementAt(curr)[1], myQuery.ElementAt(curr)[2]);
                    verifiedWord = true;
                }
                curr++;
            }
            if (verifiedWord == false)
            {
                myWord = new Word(word, false, "", "");
            }
            return myWord;
        }

        private string[] ParseCity(string unparsedText)
        {
            const int CITY_NAME = 3;
            const int LATITUDE = 5;
            const int LONGITUDE = 6;
            List<string> myCity = new List<string>();
            const char separator = ',';
            string[] parsedText = unparsedText.Split(separator);
            myCity.Add(parsedText[CITY_NAME].ToLower());
            myCity.Add(parsedText[LATITUDE]);
            myCity.Add(parsedText[LONGITUDE]);
            return myCity.ToArray();
        }

        private IOrderedEnumerable<string[]> CreateQuery(IEnumerable<string> unparsedCities,string searchWord)
        {
            IEnumerable<string[]> cityList = ParseCities(unparsedCities);
            IOrderedEnumerable<string[]> query = from item in cityList
                        where item[0].Contains(searchWord)
                        orderby item[0].Length ascending
                        select item;
            return query;
        }

        private IEnumerable<string[]> ParseCities(IEnumerable<string> unparsedCities)
        {
            List<string[]> parsedCities = new List<string[]>();
            foreach (var city in unparsedCities)
            {
                parsedCities.Add(ParseCity(city));
            }
            return parsedCities.AsEnumerable();
        }

        private string[] ParseFile(string text)
        {
            char[] parserTokens = { ' ', ',', '.', '!', '?' };
            text = text.ToLower();
            return text.Split(parserTokens);
        }
        private void LoadCities()
        {
            myDictionary = new Dictionary<string, Word>();
            // will be reimplented later with acutal load save functionality
        }

    }
}
