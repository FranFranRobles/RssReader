using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("Test_RSS_LogicEngine")]

namespace RSS_LogicEngine
{
    class Component_Tree
    {
        Component root;
        public Component_Tree(Component root) => this.root = root;
        public void Insert_Component_At(string path, Component c)
        {
            Pathname pathname = new Pathname(path);
            Component parent = Get_Component_At(pathname.Parent);
            parent.Add_Child(pathname.Child, c);
            c.Parent = parent;
        }
        public Component Remove_Component_At(string path)
        {
            Pathname pathname = new Pathname(path);
            Component parent = Get_Component_At(pathname.Parent);
            Component c = parent.Get_Child(pathname.Child);
            parent.Remove_Child(pathname.Child);
            c.Parent = null;
            return c;
        }
        public Component Get_Component_At(string path)
        {
            Component curr = root;
            Component temp;
            string[] path_component = path.Split('/');
            foreach (string s in path_component)
            {
                temp = curr.Get_Child(s);
                if (temp == null)
                    return curr;
            }
            return curr;
        }
        private class Pathname
        {
            public string Parent;
            public string Child;
            public Pathname(string pathname)
            {
                int last_slash = pathname.LastIndexOf("/", System.StringComparison.Ordinal);
                Child = pathname.Substring(last_slash + 1);
                Parent = pathname.Remove(last_slash);
            }
        }
    }
}
