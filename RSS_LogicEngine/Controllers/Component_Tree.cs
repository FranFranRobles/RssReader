using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

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
            StringSplitOptions options = StringSplitOptions.RemoveEmptyEntries;
            char[] separator = new char[] { '/' };
            string[] path_component = path.Split(separator, options);
            foreach (string s in path_component)
                curr = curr.Get_Child(s);
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
        /********** Begin Tree Saving Functions **********/
        public void Save_Tree(XmlWriter writer)
        {
            writer.WriteStartDocument();
            foreach (string s in root.Get_Children())
                Save_Component(writer, root.Get_Child(s), s);
            writer.WriteEndDocument();
            writer.Flush();
        }
        private void Save_Component(XmlWriter writer, Component c, string name)
        {
            string component_type = c.Is_Leaf() ? "feed" : "channel";
            writer.WriteStartElement(component_type);
            writer.WriteAttributeString("name", name);
            if (c.Is_Leaf())
            {
                Feed f = (Feed)c;
                writer.WriteValue(f.URI);
            }
            foreach (string s in c.Get_Children())
                Save_Component(writer, c.Get_Child(s), s);
            writer.WriteEndElement();
        }
        public void Load_Tree(XmlReader reader)
        {
            Component_Factory cf = Component_Factory.Get_Instance();
            Component c;
            while (reader.Read())
            {
                if (reader.Name == "feed")
                {
                    string name = reader.GetAttribute("name");
                    string URI = reader.ReadElementContentAsString();
                    c = cf.Create_Feed(URI);
                    root.Add_Child(name, c);
                }
            }
        }
        /********** End Tree Saving Functions **********/
    }
}
