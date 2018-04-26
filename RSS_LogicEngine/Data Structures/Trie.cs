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
        public void Clear()
        { }
        public void insert(string city, string lat, string longi)
        { }
        public bool Search(string city, out string lat, out string longi)
        {
            throw new NotImplementedException();
        }
        public bool Contains(string city)
        {
            throw new NotImplementedException();
        }
    }
}
