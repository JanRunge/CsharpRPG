using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using STory.GameContent;
using STory.GameContent.Items.Armors;
using STory.Types;

namespace STory
{
    class Program
    {
        public static Room currentRoom;
        public static Player player;
        public static Dictionary<Type, Room> Rooms ;

        static void createStartRoom()
        {
            void add(Room r)
            {
                Rooms.Add(r.GetType(), r);
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
            currentRoom = Rooms[typeof(GameContent.Rooms.Forest_start)];
            throw new Exception("");
        }

        static void Main(string[] args)
        {
            player = new Player();
            
            
            Rooms = new Dictionary<Type, Room>();
            createStartRoom();
            CIO.initialize();
            

            currentRoom = Rooms[typeof(GameContent.Rooms.Forest_start)];
            while (true)
            {
                Console.WriteLine("#########################");
                if (currentRoom.OnEnter())
                {
                    Optionhandler h = new Optionhandler(currentRoom.name+". next room?");
                    h.AddOptions(Optionhandler.RoomsToOption( currentRoom.nextRooms));
                    Room nextroom = (Room)h.selectOption();
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
