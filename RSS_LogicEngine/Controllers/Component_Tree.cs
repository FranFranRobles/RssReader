using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSS_LogicEngine.Controllers
{
    public class Component_Tree
    {
        private Component root;
        public List<string> Get_Children_Of(string path)
            => Get_Component_At(path).Get_Children();
        public bool Is_Leaf(string path)
            => Get_Component_At(path).Is_Leaf();
        private Component Get_Component_At(string path)
        {
            Component curr = root;
            string[] path_component = path.Split('/');
            foreach (string s in path_component)
            {
                curr = curr.Get_Child(s);
                if (curr == null)
                    break;
            }
            return curr;
        }
    }
}
