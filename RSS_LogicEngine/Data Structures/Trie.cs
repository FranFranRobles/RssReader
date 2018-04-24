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

            public Node(bool is_City, Node myParent)
            {
                isCity = is_City;
                parent = myParent;
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
        public void insert(string myData)
        { }
        public bool Search(string data)
        {
            throw new NotImplementedException();
        }
        bool Contains(string Data)
        {
            throw new NotImplementedException();
        }

    }
}
