using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STory
{
    /// <summary>
    /// static class that handles ConsoleOutputs and user-input
    /// </summary>
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
        /// <summary>
        /// Reads user input and returns the first string which is NOT a global Command
        /// </summary>
        public static string ReadLine()
        {
            /// <summary>
            /// Checks if the String is a Global COmmand and Executes it, if true
            /// </summary>
            Boolean TestGC(string input)
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
                if (!TestGC(input))
                {
                    Console.ForegroundColor= defaultcolor;
                    return input;
                }
            }
        }
        /// <summary>
        /// Prints the given string in the Color defined for help-texts
        /// </summary>
        public static void PrintHelp(string s)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(s);
            Console.ForegroundColor = defaultcolor;
        }
        /// <summary>
        /// Prints the given string in the Color defined for story-texts
        /// </summary>
        public static void PrintStory(string s)
        {
            Console.WriteLine(s);
        }
        /// <summary>
        /// Private print method which wraps Console.writeLine
        /// </summary>
        private static void print(string s)
        {
            Console.WriteLine(s);
        }

        /// <summary>
        /// Checks if a string contains a placeholder which is associated with a color
        /// </summary>
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
        /// <summary>
        /// returns the index of the first occurance of a placeholder which is associated with a color inside the given string
        /// </summary>
        private static int[] findFirstOccuranceOfColorVar(string str)
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
        /// <summary>
        /// Prints a String to the Console.
        /// The String may contain a placeholder E.g. {Red}, which will result in all chars after that begin printed in red. 
        /// <para> the conversion from Color to Playceholder can be made through CIO.ColorVarsReversed </para>
        /// </summary>
        public static void Print(string s)
        {
            
            if (!stringContainsColorVar(s))
            {
                print(s);
            }
            else
            {
                string stringUntilNextPlaceholder;
                string remainingString=s;
                string colorPlaceholder;
                //parse the string, and whenever a placeholder is found change the color accordingly
                int[] start_lengthNextPlaceholder;
                ConsoleColor color = defaultcolor;
                while (remainingString.Length > 0)
                {

                    start_lengthNextPlaceholder = findFirstOccuranceOfColorVar(remainingString);
                    if (start_lengthNextPlaceholder[1] != -1)
                    {
                        colorPlaceholder = remainingString.Substring(start_lengthNextPlaceholder[0], start_lengthNextPlaceholder[1]);
                        stringUntilNextPlaceholder = remainingString.Substring(0, start_lengthNextPlaceholder[0]);
                        color = ColorVars[colorPlaceholder];
                        remainingString = remainingString.Substring(stringUntilNextPlaceholder.Length+ start_lengthNextPlaceholder[1]);
                    }
                    else
                    {
                        stringUntilNextPlaceholder = remainingString;
                        remainingString = remainingString.Substring(stringUntilNextPlaceholder.Length);

                    }
                    Console.Write(stringUntilNextPlaceholder);
                    Console.ForegroundColor = color;
                }
                Console.WriteLine();
                Console.ForegroundColor = defaultcolor;
            }
        }
        /// <summary>
        /// Prints a String to the Console in the given Color.
        /// The String may contain a placeholder E.g. {Red}, which will result in all chars after that begin printed in red. 
        /// <para> the conversion from Color to Playceholder can be made through CIO.ColorVarsReversed </para>
        /// </summary>
        public static void Print(string s, ConsoleColor TextColor)
        {
            Console.ForegroundColor=TextColor;
            Print(s);
            Console.ForegroundColor=defaultcolor;
        }
        /// <summary>
        /// Prints the given string in the Color defined for Error-texts
        /// </summary>
        public static void PrintError(string s)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(s);
            Console.ForegroundColor = defaultcolor;
        }
        /// <summary>
        /// Initializes the class CIO to make it ready for use
        /// </summary>
        public static void Initialize()
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
        /// <summary>
        /// Adds a new Context to the Context-Stack
        /// </summary>
        public static void StartNewContext(Handlers.IO.Context c)
        {
            lastContexts.Add(c);
        }
        /// <summary>
        /// Pops the top-Context of the stack and then reprints the Context below
        /// </summary>
        public static void EndContext()
        {
            EndContextWithoutReEnter();
            ReEnterCurrentContext();
        }
        /// <summary>
        /// reprints the current top-Context below
        /// </summary>
        public static void ReEnterCurrentContext()
        {
            if (lastContexts.Count > 0)
            {
                lastContexts[lastContexts.Count - 1].Enter();
            }
        }
        /// <summary>
        /// Pops the current top-Context without reprinting the one below
        /// </summary>
        public static void EndContextWithoutReEnter()
        {
            lastContexts.RemoveAt(lastContexts.Count - 1);
        }
        /// <summary>
        /// Returns the current top-Context
        /// </summary>
        public static Handlers.IO.Context GetCurrentContext()
        {
            return lastContexts[lastContexts.Count - 1];
        }
        //TODO: does the contextstack grow? DO i end them correctly??

    }
}
