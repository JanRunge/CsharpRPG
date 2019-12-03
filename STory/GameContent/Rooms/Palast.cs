using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using STory.Handlers.Fight;
namespace STory.GameContent.Rooms
{
    class Palast : Room
    {
        Boolean unlocked = false;
        public Palast()
        {
            this.name = "Palace";
            this.nextRooms.Clear();
        }
        
        public override Boolean OnEnter()
        {
            if (!unlocked)
            {
                CIO.Print("You try to enter "+this.name);
                CIO.PrintStory("A guard approaches you");
                Optionhandler d = new Optionhandler("'No Place for you here peasant'");
                d.AddOption(new Bribe(200, () => this.unlocked=true, 0.2f));

                d.AddOption(new GenericOption("Leave", true));
                d.selectOption();


                if (!unlocked)
                {
                    Program.currentRoom = this.LastRoom;
                    return false;
                }
                
            }
            base.OnEnter();
            this.AddRoom(LastRoom);
            return true;
        }
        public override void OnExit()
        {
            base.OnExit();
        }

    }
}
