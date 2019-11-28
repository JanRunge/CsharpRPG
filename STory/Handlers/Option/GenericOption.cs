using STory.Handlers.Option;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STory
{
    public class GenericOption : Option
    {
        public string name;
        protected string preferredCommand;
        protected ConsoleColor color = CIO.defaultcolor;
        protected Func<string> notavailableMessageAction;

        protected Func<bool> available;
        
        protected List<Action> OnExecution;
        protected List<Action> OnTry;
        protected List<Action> OnFailure;

        public GenericOption(string name, Action OnExecution):this(name)
        {
            this.OnExecution = new List<Action>();
            this.OnExecution.Add(OnExecution);
        }
        public GenericOption(string name) : this(name, CIO.defaultcolor)
        {

        }
        public GenericOption(string name, ConsoleColor color) : this(name,true, color)
        {
            
        }
        public GenericOption(string name, Boolean available) : this(name, available, CIO.defaultcolor)
        {

        }
        public GenericOption(string name, Boolean available, ConsoleColor color)
        {
            this.name = name;
            SetAvailable(available);
            this.color = color;
        }
        public GenericOption(string name, string preferredcommand) : this(name)
        {
            this.setPreferredCommand(preferredcommand);
        }


        public void SetAvailable(bool available)
        {
            SetAvailable(() => available);
        }
        public void SetAvailable(Func<bool> available)
        {
            this.available = available;
        }


        public void AddExecutionAction(Action a)
        {
            if (OnExecution == null)
            {
                OnExecution = new List<Action>();
            }
            this.OnExecution.Add(a);
        }

        public void AddFailureAction(Action a)
        {
            if (OnFailure == null)
            {
                OnFailure = new List<Action>();
            }
            this.OnFailure.Add(a);
        }

        public void AddTryAction(Action a)
        {
            if (OnTry == null)
            {
                OnTry = new List<Action>();
            }
            this.OnTry.Add(a);
        }


        public void setPreferredCommand(string s)
        {
            preferredCommand = s;
        }

        public string getText()
        {
            return name;
        }

        public bool isAvailable()
        {
            return available();
        }

        public ConsoleColor GetColor()
        {
            return color;
        }

        public string getPreferredCommand()
        {
            return preferredCommand;
        }

        public void Execute()
        {
            if (OnExecution != null)
            {
                foreach (Action a in OnExecution)
                {
                    a();
                }
            }
        }
        public void Select()
        {
            if (OnTry != null)
            {
                foreach (Action a in OnTry)
                {
                    a();//if one fails, it sets available==false
                    if (available() == false)
                    {
                        onFailure();
                        return;
                    }
                }
            }
            Execute();
        }
        public void setNotAvailable(Func<string> a)
        {
            notavailableMessageAction = a;
        }
        public virtual void onNotAvailable()
        {
            if (notavailableMessageAction == null)
            {
                CIO.Print("not avaulable");
            }
            else
            {
                CIO.Print(notavailableMessageAction());
            }
        }
        public virtual void onFailure()
        {
            if (OnFailure == null)
            {
                Console.WriteLine("Failed (generic)");
            }
            else
            {
                foreach (Action a in OnFailure)
                {
                    a();
                }
            }
        }
        public static bool isGenericOption(Option option)
        {
            return (option.GetType() == typeof(GenericOption) || option.GetType().IsSubclassOf(typeof(GenericOption)));
        }
    }
}