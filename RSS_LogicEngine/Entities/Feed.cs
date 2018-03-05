using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSS_LogicEngine
{
    class Feed : Component
    {
        List<Article> articles;
        public override void Add_Child(string name, Component c) { }
        public override void Remove_Child(string comonent_name) { }
        public override bool Has_Children() { return false; }
        public override List<string> Get_Children() { return new List<string>(); }
        public override Component Get_Child(string name) { return null; }
        public override List<Article> Get_Articles() { return new List<Article>(); }
        public override bool Is_Leaf() { return true; }
    }
}
