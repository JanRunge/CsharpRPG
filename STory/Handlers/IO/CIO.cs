using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STory
{
    
    public static class CIO //console input output
    {
        static ConsoleColor userinputColor = ConsoleColor.Blue;
        static ConsoleColor defaultcolor = ConsoleColor.White;
        static List<Handlers.IO.Context> lastContexts= new List<Handlers.IO.Context>();

        public static string ReadLine()
        {
            Boolean TestCheat(string input)
            {
                if (GlobalCommands.existsCheat(input))
                {
                    GlobalCommands.executeCheat(input);
                    return true;

                }
                return false;
            }
            while (true)
            {
                Console.ForegroundColor = userinputColor;
                string input = Console.ReadLine().ToLower();
                if (!TestCheat(input))
                {
                    Console.ForegroundColor= defaultcolor;
                    return input;
                }
            }
        }

        public static void PrintHelp(string s)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(s);
            Console.ForegroundColor = defaultcolor;
        }
        public static void PrintStory(string s)
        {
            Console.WriteLine(s);
        }
        public static void Print(string s)
        {
            Console.WriteLine(s);
        }
        public static void Print(string s, string[] placeholders, ConsoleColor?[] colors)
        {
            //parse the string, and whenever aplaceholde ris found change the color accordingly
        }
        public static void Print(string s, ConsoleColor TextColor)
        {
            Console.ForegroundColor=TextColor;
            Console.WriteLine(s);
            Console.ForegroundColor=defaultcolor;
        }
        public static void PrintError(string s)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(s);
            Console.ForegroundColor = defaultcolor;
        }

        public static void initialize()
        {
            Console.ForegroundColor=defaultcolor;
            GlobalCommands.LoadAll();
        }
        public static void StartNewContext(Handlers.IO.Context c)
        {
            lastContexts.Add(c);
        }
        public static void EndContext()
        {
            EndContextWithoutReEnter();
            ReEnterCurrentContext();
        }
        public static void ReEnterCurrentContext()
        {
            if (lastContexts.Count > 0)
            {
                lastContexts[lastContexts.Count - 1].reEnter();
            }
        }

        public static void EndContextWithoutReEnter()
        {
            lastContexts.RemoveAt(lastContexts.Count - 1);

        }
        public static Handlers.IO.Context getCurrentContext()
        {
            return lastContexts[lastContexts.Count - 1];
        }
        //TODO: does the contextstack grow? DO i end them correctly??

    }
}
