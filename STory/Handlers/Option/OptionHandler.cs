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


        Action generateOptions;
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
        public void setOptionGenerator(Action fun)
        {
            this.generateOptions = fun;
        }
        public void printOptions()
        {
            if (generateOptions != null)
            {
                this.options = new Dictionary<string, Option>();
                generateOptions();
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
                    {
                        processedMultioptions.Add(opt);
                        string prefix = opt.Prefix;
                        output = prefix;
                        foreach (KeyValuePair<string, Option> pair in opt.options)
                        {
                            Option subOpt = pair.Value;
                            
                            if (subOpt.isAvailable())
                            {
                                color = subOpt.GetColor();
                                if (color != CIO.defaultcolor)
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
                   color = kv.Value.GetColor();
                    output = getTextWithCommand(kv.Value.getText(), kv.Key);
                }
                if (output != "")
                {
                    CIO.Print(output, color);
                    if (headings.ContainsKey(kv.Value))
                    {
                        CIO.Print(headings[kv.Value]);
                    }
                }
                

                
            }
        }
        public bool isCommandTaken(string s)
        {
            return options.ContainsKey(s);
        }
        public void AddOption(Option option)
        {
            AddOptionWithoutKey(option, "");
        }
        public void AddOptionWithoutKey(Option option, string forbiddenKey) {
            //wenn der preferred command der option bereits vergeben ist, wird der blockierende reKeyed
            if (option.GetType() == typeof(GenericItemOption))
            {
                GenericItemOption opt = (GenericItemOption)option;
                Multioption mo = new Multioption();
                mo.Prefix = opt.NameOfItem();
                foreach(Option o in opt.getOptions())
                {
                    string comm = generateCommand(opt.NameOfItem()+ o.getText());
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
            Option currentBlocker;
            if (preferredCommand != null) {
                if (GlobalCommands.existsCheat(preferredCommand)) {
                    throw new Exception("This command is already taken! (Cheat)");
                }
                if (isCommandTaken(preferredCommand)) {
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
        public virtual void AddOption(Option option, string command) {
            if (isCommandTaken(command.ToLower()) || GlobalCommands.existsCheat(command.ToLower())) {
                throw new Exception("This command is already taken!");
            }
            options.Add(command.ToLower(), option);

        }

        private string getTextWithCommand(string text, string command)
        {

            return text + "[" + command.ToUpper() + "]";
        }
        private string generateCommand(string text) {
            return generateCommand(text, "");
        }
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
                    int index=-1;
                    while (c < n)
                    {
                        index = s.IndexOf(" ");
                        s = s.Substring(index+1);
                        c++;
                    }
                    return index;
                    
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
        public virtual Option selectOption() {
            
            if(canExit && !options.ContainsKey("e"))
            {
                AddOption(Exit);
            }
            if (this.name != "")
            {
                CIO.StartNewContext(new Handlers.IO.Context(this, this.name));
            }
            else
            {
                CIO.StartNewContext(new Handlers.IO.Context(this));
            }
            printOptions();
            string userInput = CIO.ReadLine();
            while (!isCommandAvailable(userInput)) {
                if (options.ContainsKey(userInput) && !options[userInput].isAvailable()) {
                    printNotAvailable(options[userInput]);
                } else {
                    CIO.Print("invalid option");
                }
                userInput = CIO.ReadLine();
            }
            CIO.EndContext();
            Option selectedOption = options[userInput];
            if(selectedOption.GetType() == typeof(Multioption))
            {
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
        public void clearOptions()
        {
            this.options.Clear();
        }
        public void AddHeading(string heading)
        {
            //the heading will be displayed after the newest option
            headings.Add(options.ElementAt(options.Count - 1).Value, heading);
        }
        
    }
    
}