using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSS_LogicEngine.Data_Structures
{
    public class Trie
    {
        private const int TOTAL_CHILDREN = 26;

        private Node root;
        private class Node
        {
            public bool isCity;
            public Node parent;
            public int currIndex;
            public Node[] children;
            public string latitude;
            public string longitude;

            public Node(bool is_City, Node myParent)
            {
                isCity = is_City;
                parent = myParent;
                latitude = longitude = "";
                currIndex = 0;
                children = new Node[TOTAL_CHILDREN];
            }
		}

        public Trie()
        {
            root = new Node(false, null);
        }
        public void Insert(string city, string lat, string longi)
        {
            city = city.ToLower();
            int index = 0;
            char currLtr = ' ';
            Node temp = root;

            while (index < city.Count())
            {
                currLtr = city[index];
                if (temp.children[currLtr - 'a'] == null)
                {
                    temp.children[currLtr - 'a'] = new Node(false, temp);
                }
                if (index == city.Count() - 1)
                {
                    temp.isCity = true;
                    temp.latitude = lat;
                    temp.longitude = longi;
                }
                else
                {
                    temp = temp.children[currLtr - 'a'];
                }
                index++;
            }
        }
        public bool Search(string city, out string lat, out string longi)
        {
            city = city.ToLower();
            int index = 0;
            Node temp = root;
            char letter = ' ';
            string word = "";
            bool found = false;
            lat = "";
            longi = "";
            while (index < city.Count() && temp != null && found == false)
            {
                letter = city[index];
                word += city[index];
                if (temp.isCity == true && city.Count() - 1 == index)
                {
                    lat = temp.latitude;
                    longi = temp.longitude;
                    found = true;
                }

                temp = temp.children[letter - 'a'];
                index++;
            }
            return found;
        }
        public bool Contains(string city)
        {
            city = city.ToLower();
            bool found = true;
            int index = 0;
            char letter = ' ';
            Node temp = root;

            while (index < city.Count() && found == true)
            {
                letter = city[index];
                if (temp.children[letter - 'a'] == null)
                {
                    found = false;
                }
                else
                {
                    temp = temp.children[letter - 'a'];
                }
                index++;
            }
            return found;
        }
    }
}
