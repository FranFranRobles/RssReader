using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSS_Console
{
    static class Input_Checker
    {
        private static string[] confirmation = { "yes", "Yes", "y", "Y" };
        private static string[] deconfirmation = { "no", "No", "n", "N" };
        public static int ReadInteger()
        {
            string input;
            while ((input = Console.ReadLine()).All(Char.IsDigit) == false || input.Length == 0)
                Error();
            return Convert.ToInt32(input);
        }
        public static bool YesOrNo()
        {
            while (true)
            {
                string input = Console.ReadLine();
                foreach (string s in confirmation)
                    if (input.Equals(s))
                        return true;
                foreach (string s in deconfirmation)
                    if (input.Equals(s))
                        return false;
                Error();
            }
        }
        public static void WaitUntilPress(ConsoleKey key)
        {
            while (Console.ReadKey().Key != key) ;
            Console.WriteLine();
        }
        public static void Error() => Console.WriteLine("Not a valid option");
    }
}
