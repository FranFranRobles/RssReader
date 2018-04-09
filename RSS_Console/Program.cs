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
        List<string> expanded = new List<string>();
        static Component_View cv = Component_View.Get_Instance();
        static void Main(string[] args)
        {

            while (true)
            {
                Console.WriteLine("0. Exit Program");
                Console.WriteLine("1. Add Channel");
                Console.WriteLine("2. Add Feed");
                Console.WriteLine("3. Expand/Colapse Channel");
                Console.WriteLine("4. Print Tree");
                Console.Write(" > ");
                int input = Input_Checker.ReadInteger();
                switch (input)
                {
                    case 0: return;
                    case 1: Add_Channel(); break;
                    case 2: Add_Feed(); break;
                    case 3: Expand_Colapse_Channel(); break;
                    case 4: Print_Tree(); break;
                    default: Input_Checker.Error(); break;
                }
            }
        }
        static void Add_Channel()
        {
            Console.WriteLine("Enter channel path as: parentpath/channelname");
            Console.Write(" > ");
            string pathname = Console.ReadLine();
            cv.Add_Channel(pathname);
        }
        static void Add_Feed()
        {

        }
        static void Expand_Colapse_Channel()
        {
            
        }
        static void Print_Tree()
        {
            Print_Tree_Recursive("/", 0);
        }
        static void Print_Tree_Recursive(string path, int depth)
        {
            List<string> children = cv.Get_Children_Of(path);
            foreach (string child in children)
            {
                for (int i = 0; i < depth; i++)
                    Console.Write("\t");
                Console.WriteLine(child);
                Print_Tree_Recursive(path + child + "/", depth + 1);
            }
        }
    }
}
