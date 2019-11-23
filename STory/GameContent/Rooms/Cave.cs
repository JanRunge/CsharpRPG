using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STory.GameContent.Rooms
{
    
    class Cave :Room
    {
        public Cave()
        {
            this.name = "Gruselhöhle";
            this.nextRooms.Clear();
        }
        private void flee()
        {
            Program.currentRoom = this.LastRoom;
            thrownToNewRoom=true;
        }
        private void friend()
        {
            
            Optionhandler d;
            d = new Optionhandler("Creature is barking: Hey hooman. Just need someone to talk. Nice to meet you. Byebye.");
            d.AddOption(new GenericOption("Petting the Dog-Ghost"));
            GenericOption opt = new GenericOption("Flee");
            d.AddOption(opt);

            opt.AddExecutionAction(() => flee());
            d.selectOption();
        }
        public override bool OnEnter()
        {
            base.OnEnter();
            //CIO.PrintStory("The cave is as dark as night and an unsettling static lies in the air. Your Lucifer creates but a dim light that bareley reaches your feet." );
            Optionhandler d = new Optionhandler("The cave is it dark and scary, to have some you grab your lucifer. You hear a creepy noise just several meters infront of you. A white, dusty creation apperas in front of you, moving her mouth without sound Seconds later echoing from the walls 'Greetings traveler' ");
            d.AddOption(new GenericOption("Attack"));
            GenericOption opt = new GenericOption("Hello spooky creature? How can I help you?");
            d.AddOption(opt);
            opt.AddExecutionAction(() => friend());
            d.selectOption();

            if (thrownToNewRoom)
            {
                return false;
            }
            AddRoom(typeof(Forest_start));
            

            



            return true;


        }
    }
}
