using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;

namespace RSS_LogicEngine
{
    public class Component_View
    {
        private static Component_View instance;
        private Component_View()
        {
            component_factory = Component_Factory.Get_Instance();
            component_tree = new Component_Tree(component_factory.Create_Channel());
        }
        public static Component_View Get_Instance()
        {
            if (instance == null)
                instance = new Component_View();
            return instance;
        }
        private Component_Factory component_factory;
        private Component_Tree component_tree;
        public void Add_Feed(string path, string uri)
        {
            Component feed = component_factory.Create_Feed(uri);
            component_tree.Insert_Component_At(path, feed);
        }
        public void Add_Channel(string path)
        {
            Component channel = component_factory.Create_Channel();
            component_tree.Insert_Component_At(path, channel);
        }
        public void Move_Component(string old_path, string new_path)
        {
            Component c = component_tree.Remove_Component_At(old_path);
            component_tree.Insert_Component_At(new_path, c);
        }
        public List<string> Get_Children_Of(string path)
            => component_tree.Get_Component_At(path).Get_Children();

        public List<Article> Get_Articles_Under(string path)
            => component_tree.Get_Component_At(path).Get_Articles();
        public void Save_Components(Stream stream)
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            XmlWriter writer = XmlWriter.Create(stream, settings);
            component_tree.Save_Tree(writer);
        }
        public bool Is_Channel(string path)
            => !component_tree.Get_Component_At(path).Is_Leaf();
        public void Load_Components(Stream stream)
        {
            XmlReader reader = XmlReader.Create(stream);
            component_tree.Load_Tree(reader);
        }
    }
}
