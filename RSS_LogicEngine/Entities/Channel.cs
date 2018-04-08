using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSS_LogicEngine
{
    public class Channel : Component
    {
        private Dictionary<string, Component> children;
        public override void Add_Child(string name, Component c) => children.Add(name, c);
        public override void Remove_Child(string name) => children.Remove(name);
        public override bool Has_Children() => (children.Count > 0);
        public override List<string> Get_Children() => children.Keys.ToList<string>();
        public override Component Get_Child(string name)
        {
            if (name == "") return null;
            return children[name];
        }
        public override List<Article> Get_Articles()
        {
            List<Article> ret = new List<Article>();
            foreach (Component c in children.Values.ToList<Component>())
            {
                ret.AddRange(c.Get_Articles());
            }
            return ret;
        }
        public override bool Is_Leaf() => false;
        public Channel() => children = new Dictionary<string, Component>();
    }
}
