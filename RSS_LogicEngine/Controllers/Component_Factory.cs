using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSS_LogicEngine
{
    class Component_Factory
    {
        private static Component_Factory instance;
        private Component_Factory() { }
        public static Component_Factory Get_Instance()
        {
            if (instance == null)
                instance = new Component_Factory();
            return instance;
        }
        public Component Create_Channel()
            => new Channel();
        public Component Create_Feed(string uri)
            => new Feed(uri);
    }
}
