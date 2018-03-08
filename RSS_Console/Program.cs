using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RSS_LogicEngine;

namespace RSS_Console
{
    class Program
    {
        static int event_num = 0;
        static void Main(string[] args)
        {
            Update_Manager um = Update_Manager.Get_Instance();
            um.Update_Tasks += Print;
            um.Set_Update_Period(1);
            while (true) ;
        }
        static void Print(object sender, EventArgs e)
        {
            event_num++;
            Console.WriteLine("This is an event #" + event_num.ToString());
        }
    }
}
