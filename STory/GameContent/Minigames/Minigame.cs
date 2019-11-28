using STory.Handlers.Option;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STory.GameContent.Minigames
{
    public abstract class Minigame : Option
    {
        protected int stake=-1;
        protected string name; 
        public void SetStake(int amnt)
        {
            this.stake = amnt;
        }
        public void AskPlayerForStake()
        {
            Optionhandler oh = new Optionhandler("choose the stakes", true);
            GenericOption opt = new GenericOption("5g");
            opt.setPreferredCommand("a");//todo preferredcommand is not used!
            opt.AddExecutionAction(() => this.SetStake(10));
            oh.AddOption(opt);

            opt = new GenericOption("10g");
            opt.setPreferredCommand("b");
            opt.AddExecutionAction(() => this.SetStake(10));
            oh.AddOption(opt);

            opt = new GenericOption("50g");
            opt.setPreferredCommand("c");
            opt.AddExecutionAction(() => this.SetStake(10));
            oh.AddOption(opt);

            opt = new GenericOption("100g");
            opt.setPreferredCommand("d");
            opt.AddExecutionAction(() => this.SetStake(10));
            oh.AddOption(opt);
            oh.selectOption();
        }
        
        public abstract void Play();
        public abstract void PrintRules();
        public ConsoleColor GetColor()
        {
            return CIO.defaultcolor;
        }

        public string getPreferredCommand()
        {
            return null;
        }

        public string getText()
        {
            return this.name;
        }

        public bool isAvailable()
        {
            return true;
        }

        public void Select()
        {
            this.PrintRules();
            this.AskPlayerForStake();
            if (this.stake == -1)
            {
                //player wants to exit
            }
            else
            {
                this.Play();
            }
            
        }
    }
}
