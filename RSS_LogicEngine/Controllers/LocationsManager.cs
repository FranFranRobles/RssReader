using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSS_LogicEngine.Controllers
{
    public class LocationsManager
    {
        private LocationsManager instance = null;
        private Dictionary<string, Tuple<bool, string, string>> locationsDictionary;
        private string latidude = "";
        private string longitude = "";
        private LocationsManager() { }
        public static LocationsManager GetInstance()
        {
            throw new NotImplementedException();
        }
        public string Latidude() => latidude;
        public string Longitude() => longitude;
        public bool HasCordinates(string text)
        {
            string[] words = Parse(text);
            int index = 0;
            bool found = false;
            Tuple<bool, string, string> temp = null;
            bool hasCordinates = false; // need to add code for it to work
            while (found == false && index < words.Length)
            {
                temp = SearchDictionary(words[index]);
                found = IsValid(temp);
                if (found == false)
                {
                    temp = SearchDictionary(words[index]);
                    found = temp.Item1;
                    AddToDictionary(words[index], temp);
                }
                index++;
            }
            return hasCordinates;

            throw new NotImplementedException();
        }
        private string[] Parse(string Text)
        {
            throw new NotImplementedException();
        }
        private Tuple<bool, string, string> SearchDictionary(string city)
        {
            throw new NotImplementedException();
        }
        private bool IsValid(Tuple<bool, string, string> city)
        {
            throw new NotImplementedException();
        }
        private void AddToDictionary(string city, Tuple<bool, string, string> details)
        {
            throw new NotImplementedException();
        }
        private Tuple<bool, string, string> SearchForCity(string word)
        {
            throw new NotImplementedException();
        }

    }
}
