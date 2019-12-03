using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using STory.GameContent;
using STory.GameContent.Items;
using STory.GameContent.Items.Armors;
using STory.GameContent.Minigames;
using STory.Handlers.Option;
using STory.Types;

namespace STory
{
    class Program
    {
        public static Room currentRoom;
        public static Player player;
        

        static void createStartRoom()
        {
            void add(Room r)
            {
                Room.AllRooms.Add(r.GetType(), r);
            }
            add(new GameContent.Rooms.Forest_start());
        }
        public static void gameOver()
        {
            Optionhandler d = new Optionhandler("and now?");
            GenericOption o = new GenericOption("Exit", true);
            o.AddExecutionAction(() => Environment.Exit(0));
            d.AddOption(o);

            o = new GenericOption("Restart", true);
            o.AddExecutionAction(() => restart());
            d.AddOption(o);
            d.selectOption();
        }
        static void restart()
        {
            currentRoom = Room.AllRooms[typeof(GameContent.Rooms.Forest_start)];
            throw new NotImplementedException();
        }

        static void Main(string[] args)
        {
            player = new Player();
            
            createStartRoom();
            CIO.Initialize();
            currentRoom = Room.AllRooms[typeof(GameContent.Rooms.Forest_start)];
            //player.giveItem(new Potion(10, "Healthpotion", PotionEffect.Heal, Potionsize.Small));
            GlobalCommands.GiveSword();
            //player.receiveDamage(30, DamageType.Blunt);

            while (true)
            {
                Console.WriteLine("#########################");
                if (currentRoom.OnEnter())
                {
                    Room nextroom=null;
                    while (nextroom == null)
                    {
                        Optionhandler h;
                        if (currentRoom.ActivitiesInRoom != null)//print activities if there are any
                        {
                            h = new Optionhandler(currentRoom.name + ". Activities:");
                            h.AddOptions(currentRoom.ActivitiesInRoom);
                            h.AddHeading("Next Rooms:");
                        }
                        else
                        {
                            h = new Optionhandler(currentRoom.name + ". next room?");
                        }
                        h.AddOptions(Optionhandler.RoomsToOption(currentRoom.nextRooms));
                        Option selectedOpt = h.selectOption();
                        if (selectedOpt.GetType() != typeof(GenericOption))
                        { 
                            nextroom = (Room)selectedOpt;
                        }
                    }
                    Console.Clear();
                    currentRoom.OnExit();
                    currentRoom = nextroom;
                }
                else
                {
                    currentRoom.OnExit();
                }
            }
        }
    }
}
