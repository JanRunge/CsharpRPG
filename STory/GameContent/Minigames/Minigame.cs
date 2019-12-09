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
            //todo: implement a possibility to exit
            CIO.StartNewContext(new Handlers.IO.Context("TODO THIS PARAM IS NOT DOCUMENTED WELL"));
            while (this.stake == -1)
            {
                CIO.Print("How Much do you wish to bet?");
                int stake = CIO.ReadLineInt();
                if (!Player.getInstance().hasGold(stake))
                {
                    CIO.PrintError("you dont have enough gold!");
                }
                else
                {
                    this.stake = stake;
                }
            }
            
            CIO.EndContext();
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
