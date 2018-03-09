using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("Test_RSS_LogicEngine")]

namespace RSS_LogicEngine
{
    class Component
    {
        protected Component parent;
        public Component Parent
        {
            get => parent;
            set => parent = value;
        }
        public virtual void Add_Child(string name, Component c) { }
        public virtual void Remove_Child(string comonent_name) { }
        public virtual bool Has_Children() { return false; }
        public virtual List<string> Get_Children() { return new List<string>(); }
        public virtual Component Get_Child(string name) { return null; }
        public virtual List<Article> Get_Articles() { return new List<Article>(); }
        public virtual bool Is_Leaf() { return false; }
    }
}
