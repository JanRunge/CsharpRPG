using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using STory.GameContent.Items;
using STory.Handlers.Option;

namespace STory
{
    public class Optionhandler
    {
        public static GenericOption Exit = new GenericOption("Exit", "E");

        Dictionary<string, Option> options = new Dictionary<string, Option>();
        Dictionary<Option, string> headings = new Dictionary<Option, string>();//The heading to be displayed AFTER the option
        Func<List<Option>> generateOptions;
        List<string> preferredCommands = new List<string>(); 
        string text;
        string name="";
        Boolean canExit;


        public Optionhandler(string text):this(text,false)
        {
           
        }
        public Optionhandler(string text, Boolean canExit)
        {
            this.text = text;
            this.canExit = canExit;
        }
        public Optionhandler(Boolean canExit):this("", canExit)
        {
            
        }
        public Optionhandler(): this(false)
        {

        }

        public void setName(String t)
        {
            name = t;
        }
        /// <summary>
        /// add an Function which will be used to calculate all Options everytime the Handler is printed.
        /// </summary>
        public void setOptionGenerator(Func<List<Option>> fun)
        {
            this.generateOptions = fun;
        }
        /// <summary>
        /// print a styled list with all Options and their commands
        /// </summary>
        public void printOptions()
        {
            if (generateOptions != null)
            {
                this.options = new Dictionary<string, Option>();
                AddOptions(generateOptions());
                
            }
            if (this.text != "")
            {
                CIO.Print(this.text);
            }
            List<Multioption> processedMultioptions = new List<Multioption>();
            foreach (KeyValuePair<string, Option> kv in options) {
                ConsoleColor color = default;
                string output="";
                if (kv.Value.GetType() == typeof(Multioption))
                {
                    Multioption opt = (Multioption)kv.Value;
                    if (!processedMultioptions.Contains(opt))

                    {//todo: make commands better. should look like this:
                        //Helgen Helmet Drop[HD] Equip [HE] etc
                        processedMultioptions.Add(opt);
                        string prefix = opt.Prefix;
                        output = CIO.getVarForConsoleColor(opt.GetColor())+prefix;
                        foreach (KeyValuePair<string, Option> pair in opt.options)
                        {
                            Option subOpt = pair.Value;
                            
                            if (subOpt.isAvailable())
                            {
                                color = subOpt.GetColor();
                                if (color != opt.GetColor())
                                {//only write in a color if it is not default, to save memory & cpu time
                                    output = output + " " + CIO.ColorVarsReversed[color] + subOpt.getText() + "[" + pair.Key + "]";
                                }
                                else
                                {
                                    output = output + " " + subOpt.getText() + "[" + pair.Key + "]";
                                }
                            }
                            else
                            {
                                output = output + " {Red}" + subOpt.getText() + "[" + pair.Key + "]{Default}";
                            }
                        }
                    }
                }
                else
                {
                    if (kv.Value.isAvailable())
                    {
                        color = kv.Value.GetColor();
                        
                    }
                    else
                    {
                        color = ConsoleColor.Red;
                    }
                    output = getTextWithCommand(kv.Value.getText(), kv.Key);
                }
                if (output != "")
                {
                    CIO.Print(output, color);
                    if (headings.ContainsKey(kv.Value))//print the heading only when
                    {
                        CIO.Print(headings[kv.Value]);
                    }
                }
            }
        }
        /// <summary>
        /// Checks wether an command is already taken in this handler
        /// </summary>
        private bool isCommandTaken(string s)
        {
            return options.ContainsKey(s);
        }
        /// <summary>
        /// Add an option. If multiple Options prefer the same command, the last one wins
        /// </summary>
        public void AddOption(Option option)
        {
            AddOptionWithoutKey(option, "");
        }
        /// <summary>
        /// Add an option without using the key. If multiple Options prefer the same command, the last one wins
        /// </summary>
        public void AddOptionWithoutKey(Option option, string forbiddenKey) {
            //wenn der preferred command der option bereits vergeben ist, wird der blockierende reKeyed
            if (option.GetType() == typeof(GenericItemOption))
            {
                GenericItemOption opt = (GenericItemOption)option;
                Multioption mo = new Multioption();
                mo.Prefix = opt.NameOfItem();
                mo.PrefixColor = () => opt.getItem().GetColor();
                foreach(Option o in opt.getOptions())
                {
                    string comm = generateCommand(opt.NameOfItem()+" "+o.getText());
                    mo.options.Add(comm, o);
                    options.Add(comm, mo);
                    
                }
                return;
            }
            string command = null;
            string preferredCommand = option.getPreferredCommand();
            if (preferredCommands.Contains(preferredCommand))
            {
                throw new Exception("Multiple Options desire the same command!");
            }
            if (preferredCommand != null) {
                if (GlobalCommands.existsCheat(preferredCommand)) {
                    throw new Exception("This command is already taken! (Cheat)");
                }
                if (isCommandTaken(preferredCommand)) {
                    Option currentBlocker;//the Option which currently holds the preferred command
                    //if the command is taken, re-key the old command with a new one
                    currentBlocker = options[preferredCommand];
                    options.Remove(preferredCommand);
                    
                    if (currentBlocker.GetType() == typeof(Multioption))
                    {
                        AddOptionWithoutKey(((Multioption)currentBlocker).options[preferredCommand], preferredCommand);
                        ((Multioption)currentBlocker).options.Remove(preferredCommand);
                    }
                    else
                    {
                        AddOptionWithoutKey(currentBlocker, preferredCommand);
                    }
                }
                command = preferredCommand;
            }
            if (command == null) {
                command = generateCommand(option.getText(), forbiddenKey);
            }
            AddOption(option, command);
        }
        public void AddOptions(List<Option> options) {
            foreach (Option o in options) {
                string command = generateCommand(o.getText());
                AddOption(o, command);
            }
        }
        /// <summary>
        /// Add the option and give it a command
        /// </summary>
        public virtual void AddOption(Option option, string command) {
            if (isCommandTaken(command.ToLower()) || GlobalCommands.existsCheat(command.ToLower())) {
                throw new Exception("This command is already taken!");
            }
            options.Add(command.ToLower(), option);
        }
        /// <summary>
        /// get the Text with its command print-ready
        /// </summary>
        private string getTextWithCommand(string text, string command)
        {

            return text + "[" + command.ToUpper() + "]";
        }
        /// <summary>
        /// generate a command for the given Text
        /// </summary>
        private string generateCommand(string text) {
            return generateCommand(text, "");
        }
        /// <summary>
        /// generate a command for the given Text which is not the forbiddenComamnd
        /// </summary>
        private string generateCommand(string text, string forbiddenCommand)
        {
            text = text.ToLower();
            bool commandAllowed(String scommand)
            {
                return scommand.ToLower() != forbiddenCommand.ToLower() && !isCommandTaken(scommand.ToLower()) && !GlobalCommands.existsCheat(scommand.ToLower());
            }
            string command;
            command = text.ToLower().Substring(0, 1);
            if (commandAllowed(command))
            {
                return command;
            }
            int countSpace = text.Split(' ').Length - 1;

            if (countSpace > 0)
            {
                int indexOfNthSpace(int n, String s)
                {
                    int c = 0;
                    int indexInSubstring=0;
                    string remainingString = s;
                    while (c < n)
                    {
                        indexInSubstring = remainingString.IndexOf(" ");
                        remainingString = remainingString.Substring(indexInSubstring + 1);//cut out everything before the Space and the space itself
                        c++;
                    }
                    return s.Length-remainingString.Length-1; //we  cut the subctring after each space, including the nth one. So the difference in length is its index.
                    
                }
                int counter = 0;
                command = text.Substring(0, 1);
                while (counter < countSpace)
                {
                    command+= text.Substring( indexOfNthSpace(counter + 1, text) +1,1);

                    if (commandAllowed(command))
                    {
                        return command;
                    }
                    counter++;
                }
            }
            int i = 2;
            while (true)
            {
                command = text.ToLower().Substring(0, i);
                if (commandAllowed(command))
                {
                    return command;
                }
                i++;
            }
        }
        protected virtual void printNotAvailable(Option o)
        {
            if (GenericOption.isGenericOption(o))
            {
                ((GenericOption)o).onNotAvailable();
            }
            else
            {
                CIO.Print("not available");
            }
            
        }
        public bool isCommandAvailable(string command)
        {
            if (options.ContainsKey(command))
            {
                if (options[command].GetType() == typeof(Multioption))
                {
                    return ((Multioption)options[command]).isAvailable(command);
                }
                else
                {
                    return options[command].isAvailable();
                }
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// print all Options and prompt the user to enter a command until a valid one is entered.
        /// <para>
        /// also calls the select Method of the selected option.
        /// </para>
        /// <para>
        /// returns the selected option
        /// </para>
        /// </summary>
        public virtual Option selectOption()
        {
            return selectOption(true);
        }
        public virtual Option selectOption(bool reEnterContext) {
            
            if(canExit && !options.ContainsKey("e"))// add the exit command if needed
            {
                AddOption(Exit);
            }
            if (this.name != "")// start a new Context
            {
                CIO.StartNewContext(new Handlers.IO.Context(this, this.name));
            }
            else
            {
                CIO.StartNewContext(new Handlers.IO.Context(this));
            }
            printOptions();
            string userInput = CIO.ReadLine();
            while (!isCommandAvailable(userInput))// prompt the user for input until he enters a valid command
            { 
                if (options.ContainsKey(userInput) && !options[userInput].isAvailable())// if the command exists but is not available
                { 
                    printNotAvailable(options[userInput]);//print the specific message of that option
                } else {
                    CIO.Print("invalid option");
                }
                userInput = CIO.ReadLine();
            }
            if (reEnterContext)
            {
                CIO.EndContext();
            }
            else
            {
                CIO.EndContextWithoutReEnter();
            }
            //CIO.EndContext();//todo: Check if you should reEnter (In the inventory the Previous room gets reprinted here)
            Option selectedOption = options[userInput];
            if(selectedOption.GetType() == typeof(Multioption))
            {//if the command is linked to a multioption, the Real Option is inside that multioption
                selectedOption = ((Multioption)selectedOption).Select(userInput);
            }
            else
            {
                selectedOption.Select();
            }
            
            return selectedOption;
        }
        public static List<Option> RoomsToOption(List<Room> Rooms)
        {
            List<Option> l = new List<Option>();
            foreach (Room r in Rooms)
            {
                l.Add(r);
            }
            return l;
        }
        public static List<Option> ItemToOption(List<Item> Weapons)
        {
            List<Option> l = new List<Option>();
            foreach (Item r in Weapons)
            {
                l.Add(r);
            }
            return l;
        }
        /// <summary>
        /// Remove all options from this Handler
        /// </summary>
        public void ClearOptions()
        {
            this.options.Clear();
            headings.Clear();
            this.preferredCommands.Clear();
        }
        /// <summary>
        /// Adds a heading which will be printed after the most recently added option
        /// </summary>
        public void AddHeading(string heading)
        {
            //the heading will be displayed after the newest option
            headings.Add(options.ElementAt(options.Count - 1).Value, heading);
        }
        
    }
    
}