using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSS_LogicEngine
{
    public class Feed : Component
    {
        private Queue<Article> articles;
        public override void Add_Child(string name, Component c)
            => throw new Exception("a Feed cannot have a child component");
        public override void Remove_Child(string comonent_name)
            => throw new Exception("cannot remove child from Feed");
        public override bool Has_Children() => false;
        public override List<string> Get_Children() => new List<string>();
        public override Component Get_Child(string name) => null;
        public override List<Article> Get_Articles() => articles.ToList<Article>();
        public override bool Is_Leaf() { return true; }
        public void Add_Article(Article a) => articles.Enqueue(a);
        public List<Article> Remove_Articles(int num_to_remove)
        {
            // The only reason this function returns a value is for testing
            // See Test_RSS_LogicEngine project, Test_Feed.cs for details
            List<Article> removed_articles = new List<Article>();
            for (int n = 0; n < num_to_remove; n++)
                removed_articles.Add(articles.Dequeue());
            return removed_articles;
        }
        public Feed() => articles = new Queue<Article>();
    }
}
