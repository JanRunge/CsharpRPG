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

            while (true)
            {
                Console.WriteLine("#########################");
                Room r = currentRoom;
                if (r.OnEnter())
                {
                    Room nextroom=null;
                    while (nextroom == null)
                    {
                        Optionhandler h;
                        if (r.ActivitiesInRoom != null)//print activities if there are any
                        {
                            h = new Optionhandler(r.name + ". Activities:");
                            h.AddOptions(r.ActivitiesInRoom);
                            h.AddHeading("Next Rooms:");
                        }
                        else
                        {
                            h = new Optionhandler(r.name + ". next room?");
                        }
                        h.AddOptions(Optionhandler.RoomsToOption(r.nextRooms));
                        Option selectedOpt = h.selectOption();
                        if (selectedOpt.GetType().IsSubclassOf(typeof(Room)))
                        { 
                            nextroom = (Room)selectedOpt;
                        }
                    }
                    CIO.Clear();
                    currentRoom.OnExit();
                    currentRoom = nextroom;
                }
                else
                {
                    CIO.Clear();
                    r.OnExit();
                }
            }
        }
    }
}
