using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        private Dictionary<string, Word> locationsDictionary;
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
        public bool HasCordinates(string text)
        {
            string[] parsedWords = Parse(text);
            int currWord = 0;   // count on current index in parsedWords
            bool foundInDictionary = false;     //indicates whether the parsed word is found within the dictionary
            Word tempWord = null;               
            bool hasCordinates = false; // need to add code for it to work  //
            while (hasCordinates == false && currWord < parsedWords.Length)
            {
                foundInDictionary = locationsDictionary.TryGetValue(parsedWords[currWord], out tempWord);
                // currently only search for one word needs to be able to chain words together
                if (foundInDictionary == false) // single word not found  in the dictionary
                {
                    tempWord = SearchDataBase(parsedWords[currWord]);
                    AddToDictionary(tempWord);
                    hasCordinates = tempWord.IsACity();
                }
                currWord++;
            }
            latidude = tempWord.GetLatidude();
            longitude = tempWord.GetLongitude();
            return hasCordinates;

            throw new NotImplementedException();
        }
        private Word SearchDataBase(string word)
        {
            //var atemp = SearchDictionary(parsedWords[currWord]);
            //var foundInDictionary = temp.Item1;
            //AddToDictionary(parsedWords[currWord], temp);
            throw new NotImplementedException();
        }
        private string[] Parse(string Text)
        {
            throw new NotImplementedException();
        }
        private void AddToDictionary(Word details)
        {
            throw new NotImplementedException();
        }
        private Word SearchForCity(string word)
        {
            throw new NotImplementedException();
        }
        private void LoadCities()
        {
            locationsDictionary = new Dictionary<string, Word>();
            // will be reimplented later with acutal load save functionality
        }

    }
}
