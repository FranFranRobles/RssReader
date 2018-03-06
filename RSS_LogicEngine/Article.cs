using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSS_LogicEngine
{
    public class Article
    {
        private string name;
        private string date;

        public Article(string name, string date) {
            this.Name = name;
            this.Date = date;
        }       

        // Standard getters/setters
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string Date
        {
            get { return date; }
            set { date = value; }
        }

    }
}
