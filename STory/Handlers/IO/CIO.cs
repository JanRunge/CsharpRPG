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
        public static ConsoleColor defaultcolor = ConsoleColor.White;
        static List<Handlers.IO.Context> lastContexts= new List<Handlers.IO.Context>();
        static Dictionary<string, ConsoleColor> ColorVars = 
            new Dictionary<string, ConsoleColor> { 
            {"{Black}",ConsoleColor.Black               },
            {"{Blue}",ConsoleColor.Blue                 },
            {"{Cyan}",ConsoleColor.Cyan                 },
            {"{DarkBlue}",ConsoleColor.DarkBlue         },
            {"{DarkCyan}",ConsoleColor.DarkCyan         },
            {"{DarkGray}",ConsoleColor.DarkGray         },
            {"{DarkGreen}",ConsoleColor.DarkGreen       },
            {"{DarkMagenta}",ConsoleColor.DarkMagenta   },
            {"{DarkRed}",ConsoleColor.DarkRed           },
            {"{DarkYellow}",ConsoleColor.DarkYellow     },
            {"{Gray}",ConsoleColor.Gray                 },
            {"{Green}",ConsoleColor.Green               },
            {"{Magenta}",ConsoleColor.Magenta           },
            {"{Red}",ConsoleColor.Red                   },
            {"{White}",ConsoleColor.White               },
            {"{Yellow}",ConsoleColor.Yellow             },
            {"{Default}",defaultcolor}
            };
        public static Dictionary<ConsoleColor, string> ColorVarsReversed = new Dictionary<ConsoleColor, string>();

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
        private static void print(string s)
        {
            Console.WriteLine(s);
        }
        private static bool stringContainsColorVar(string str)
        {
            if (str.Contains("{"))//pre check the string, so we dont have to check for every keyword
            {
                foreach (KeyValuePair<string, ConsoleColor> kvPair in ColorVars)
                {
                    if (str.Contains(kvPair.Key))
                    {
                        return true;//end as soon as possible
                    }
                }
            }
            return false;
        }
        private static int[] findNextOccuranceOfColorVar(string str)
        {
            int index;
            int lowestIndex = 999999999;
            int varLength = -1;
            foreach (KeyValuePair<string, ConsoleColor> kvPair in ColorVars)
            {
                index = str.IndexOf(kvPair.Key);
                if (index != -1)
                {
                    if (index < lowestIndex)
                    {
                        lowestIndex = index;
                        varLength = kvPair.Key.Length;
                    }
                }
            }
            return new int[2] { lowestIndex, varLength };
        }
        public static void Print(string s)
        {
            
            if (!stringContainsColorVar(s))
            {
                print(s);
            }
            else
            {
                string stringToPrint;
                string remainingString=s;
                string colorvar;
                //parse the string, and whenever a placeholder is found change the color accordingly
                int[] nextColorVar;
                ConsoleColor color = defaultcolor;

                while (remainingString.Length > 0)
                {

                    nextColorVar = findNextOccuranceOfColorVar(remainingString);
                    if (nextColorVar[1] != -1)
                    {
                        colorvar = remainingString.Substring(nextColorVar[0], nextColorVar[1]);
                        stringToPrint = remainingString.Substring(0, nextColorVar[0]);
                        color = ColorVars[colorvar];
                        remainingString = remainingString.Substring(stringToPrint.Length+ nextColorVar[1]);
                    }
                    else
                    {
                        stringToPrint = remainingString;
                        remainingString = remainingString.Substring(stringToPrint.Length);

                    }
                    Console.Write(stringToPrint);
                    Console.ForegroundColor = color;

                }
                Console.ForegroundColor = defaultcolor;
            }
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
            foreach(KeyValuePair<string,ConsoleColor> kvPair in ColorVars)
            {
                if(kvPair.Value == defaultcolor)
                {
                    continue;
                }
                ColorVarsReversed.Add(kvPair.Value, kvPair.Key);//twist key as value and value as key
            }
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
