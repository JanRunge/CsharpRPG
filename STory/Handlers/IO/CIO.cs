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
        class output
        {
            public string text;
            public ConsoleColor color;

            public output(string text, ConsoleColor color)
            {
                this.text = text;
                this.color = color;
            }
        }

        static ConsoleColor userinputColor = ConsoleColor.Blue;
        public static ConsoleColor defaultcolor = ConsoleColor.White;
        static bool debugging = false;
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
        static List<output> printsSinceLastRead = new List<output>();
        static Dictionary<ConsoleColor, string> ColorVarsReversed = new Dictionary<ConsoleColor, string>();

        public static string getVarForConsoleColor(ConsoleColor c)
        {
            return ColorVarsReversed[c];
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
                printsSinceLastRead.Clear();
                Console.ForegroundColor = userinputColor;
                string input = Console.ReadLine().ToLower();
                if (!TestGC(input))
                {
                    Console.ForegroundColor= defaultcolor;
                    return input;
                }
            }
        }
        public static int ReadLineInt()
        {
            string input; 
            int parsedInt=-1234321;
            bool parsedSuccessfully=false;
            while (!parsedSuccessfully)
            {
                input = ReadLine();
                parsedSuccessfully = Int32.TryParse(input, out parsedInt);
                if (!parsedSuccessfully)
                {
                    PrintError("Please enter a Number");
                }
            }
            return parsedInt;

        }
        public static void PrintDebug(string s)
        {
            if (debugging)
            {
                Print("<<" + s + ">>", ConsoleColor.Magenta);
            }
        }
        /// <summary>
        /// Prints the given string in the Color defined for Error-texts
        /// </summary>
        public static void PrintError(string s)
        {
            Print(s, ConsoleColor.Red);
        }
        /// <summary>
        /// Prints the given string in the Color defined for help-texts
        /// </summary>
        public static void PrintHelp(string s)
        {
            Print(s, ConsoleColor.Cyan);
        }
        /// <summary>
        /// Prints the given string in the Color defined for story-texts
        /// </summary>
        public static void PrintStory(string s)
        {
            Print(s, defaultcolor);
        }

        /// <summary>
        /// Prints a String to the Console.
        /// The String may contain a placeholder E.g. {Red}, which will result in all chars after that begin printed in red. 
        /// <para> the conversion from Color to Playceholder can be made through CIO.ColorVarsReversed </para>
        /// </summary>
        public static void Print(string s, ConsoleColor? TextColor=null, bool addToPrints = true)
        {
            if (TextColor != null)
            {
                Console.ForegroundColor = (ConsoleColor)TextColor;
            }
            if (addToPrints)
            {
                printsSinceLastRead.Add(new output(s, Console.ForegroundColor));
            }
            if (!stringContainsColorVar(s))
            {
                Console.WriteLine(s);
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
            }
            Console.ForegroundColor = defaultcolor;
        }
        public static void Clear()
        {
            if (!debugging)
            {
                Console.Clear();
            }
            else
            {
                PrintDebug("CLEAR");
            }
            foreach (output o in printsSinceLastRead)
            {
                Print(o.text, o.color, false);
            }

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
                if(kvPair.Key == "{Default}")
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
            PrintDebug("Contextcount after starting: " + lastContexts.Count);
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
            CIO.Clear();
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
            PrintDebug("Contextcount after ending: " + lastContexts.Count);
        }
        /// <summary>
        /// Returns the current top-Context
        /// </summary>
        public static Handlers.IO.Context GetCurrentContext()
        {
            return lastContexts[lastContexts.Count - 1];
        }
        //TODO: does the contextstack grow? DO i end them correctly??
        

        public static void EnableDebugging()
        {
            debugging = true;
        }
        public static void DisableDebugging()
        {
            debugging = false;
        }
        public static void ToggleDebugging()
        {
            if (debugging)
            {
                DisableDebugging();
            }
            else
            {
                EnableDebugging();
            }
        }

    }
}
