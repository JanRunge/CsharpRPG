using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using STory.GameContent;
using STory.Handlers.Fight;
using STory.Types;

namespace STory.GameContent.Rooms
{
    class Banditcamp : Room
    {
        NPC bandit = new NPC("bandit");

        public Banditcamp()
        {
            this.name = "Bandit camp";
        }


        void tryflee()
        {
            Random r = new Random();
            double d = r.NextDouble();
            if (true)//d > 0.1
            {
                CIO.Print("you have been brutally slaughtered.");
                Program.gameOver();
            }
        }


        public override Boolean OnEnter()
        {
            base.OnEnter();
            AddRoom(typeof(Forest_start));

            Optionhandler threat = new Optionhandler("A Bandit appears between the trees. He points a saber at you");
            GenericOption opt;
            opt = new GenericOption("flee");
            opt.AddExecutionAction(() => tryflee());
            threat.AddOption(opt);

            opt = new GenericOption("Attack");
            opt.AddExecutionAction(() => attack());
            threat.AddOption(opt);
            

            threat.selectOption();

            if (!bandit.isAlive())
            {
                Optionhandler oh = new Optionhandler("next move?",true);
                opt = new GenericOption("Loot");
                oh.AddOption(opt);
                opt.AddExecutionAction(() => this.bandit.loot());

                oh.selectOption().Select();
            }
            return true;

        }
        void attack()
        {
            Fight f = new Fight(this.bandit);
            f.startFight();
        }//Danach springt er wieder in den dalog. warum?
        public override void OnExit()
        {
            base.OnExit();
        }
    }
}
