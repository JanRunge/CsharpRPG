using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using STory.GameContent.Items;

namespace STory
{
    public class Optionhandler
    {
        public static GenericOption Exit = new GenericOption("Exit", "E");
        Dictionary<string, Option> options = new Dictionary<string, Option>();
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
            foreach (KeyValuePair<string, Option> kv in options) {
                if (kv.Value.GetColor() != null) {
                    Console.ForegroundColor = (ConsoleColor) kv.Value.GetColor();
                }
                if (!kv.Value.isAvailable()) {
                    Console.ForegroundColor = ConsoleColor.Red;
                }
                CIO.Print(getTextWithCommand(kv.Value.getText(), kv.Key));
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        public void AddOption(Option option)
        {
            AddOptionWithoutKey(option, "");
        }
        public void AddOptionWithoutKey(Option option, string forbiddenKey) {
            //wenn der preferred command der option bereits vergeben ist, wird der blockierende reKeyed
            
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
                if (options.ContainsKey(preferredCommand)) {
                    currentBlocker = options[preferredCommand];
                    options.Remove(preferredCommand);
                    AddOptionWithoutKey(currentBlocker, preferredCommand);
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
            if (options.ContainsKey(command.ToLower()) || GlobalCommands.existsCheat(command.ToLower())) {
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
            bool commandAllowed(String scommand)
            {
                return scommand.ToLower() != forbiddenCommand.ToLower() && !options.ContainsKey(scommand.ToLower()) && !GlobalCommands.existsCheat(scommand.ToLower());
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

        public virtual Option selectOption() {
            if(canExit && !options.ContainsKey("E"))
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
            while (!options.ContainsKey(userInput) || !options[userInput].isAvailable()) {

                if (options.ContainsKey(userInput) && !options[userInput].isAvailable()) {
                    printNotAvailable(options[userInput]);
                } else {
                    CIO.Print("invalid option");
                }
                userInput = CIO.ReadLine();
            }
            CIO.EndContext();
            Option selectedOption = options[userInput];
            selectedOption.Select();
            

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
        
    }
    
}