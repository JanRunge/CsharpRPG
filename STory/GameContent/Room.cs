using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using STory.Handlers.Fight;
using STory.Handlers.Option;

namespace STory
{
    public class Room :  Option
    {
        //todo: implement rooms as singletons?
        public string name;
        public Room LastRoom;
        public bool thrownToNewRoom = false;
        public List<Attackable> NPCs;
        public List<GenericOption> optionsInRoom = new List<GenericOption>();
        
        
        public List<Room> nextRooms = new List<Room>();
        public virtual Boolean OnEnter()
        {
            Console.WriteLine("you entered " + name);
           
            return true;
        }
        public virtual void OnExit()
        {
            Console.WriteLine("you left " + name);
        }
        public void PrintNextOptions()
        {
            Console.WriteLine("The next Areas:");
            int i = 0;
            Console.ForegroundColor= ConsoleColor.Yellow;
            foreach (Room r in this.nextRooms)
            {
                Console.WriteLine(r.name+"["+i+"]");
                i++;
            }
            Console.ForegroundColor= ConsoleColor.White;

        }
        public void AddRoom(Type t)
        {
            Room r;
            if (!Program.Rooms.ContainsKey(t))
            {
                r = (Room)Activator.CreateInstance(t);
                Program.Rooms.Add(t, r);
            }
            else
            {
                r = Program.Rooms[t];
            }

            AddRoom(r);
        }
        public void AddRoom(Room r)
        {
            if (!nextRooms.Contains(r))
            {
                this.nextRooms.Add(r);
            }
            r.LastRoom = this;
        }
        public Attackable getNPCWithName(string s){
            foreach(Attackable a in this.NPCs){
                if (a.getName()==s){
                    return a;
                }
            }
            return null;
        }


        public string getText(){
            return this.name;
        }
        public bool isAvailalbe(){
            return true;
        }
        public ConsoleColor? GetColor(){
            return null;
        }

        public bool isAvailable()
        {
            return true;
        }

        public string getPreferredCommand()
        {
            return null;
        }
        public void Select() { }
    }
}
    