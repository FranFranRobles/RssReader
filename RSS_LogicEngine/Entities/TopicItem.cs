using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSS_LogicEngine
{
    public class TopicItem
    {
        public string name { get; private set; }
        public List<Article> articleList { get; private set; }

        public TopicItem(string topicName, List<Article> articles)
        {
            name = topicName;
            articleList = articles;
        }
    }
}
