using STory.GameContent;
using STory.GameContent.Items.Weapons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using STory.Handlers.Fight;
using STory.GameContent.Items.Armors;

namespace STory
{
    /// <summary>
    /// GlobalCommands are Actions which can be executed in any situation. Most of them can be Executed by the user through a command.
    /// Some of them qualify as Cheats, while others are basic game mechanics
    /// </summary>
    public static class GlobalCommands
    {
        private static Dictionary<string, List<Action>> commands = new Dictionary<string, List<Action>>();

        public static void GiveGold()
        {
            CIO.StartNewContext(new Handlers.IO.Context("How much?"));
            int amount = -1;
            while (amount == -1)
            {
                CIO.Print("How much?");
                string input = CIO.ReadLine();
                if (wannaExit(input))
                {
                    return;
                }
                Int32.TryParse(input, out amount);
            }
            Program.player.AddGold(amount);
            CIO.EndContextWithoutReEnter();

        }
        public static void GiveXP()
        {
            int amount = -1;
            while (amount == -1)
            {
                CIO.Print("How much?");
                string input = CIO.ReadLine();
                if (wannaExit(input))
                {
                    return;
                }
                Int32.TryParse(input, out amount);
            }
            Program.player.GiveXP(amount);
        }
        public static void GiveSword()
        {
            Program.player.giveItem(new Sword(10, 10, 30, "kauldruns knife"));
            Helmet he = new Helmet(0.2f, 0.2f, 0.2f, 10, 15, "Helgen helmet");
            Program.player.giveItem(he);

        }
        public static void Attack() {
            CIO.Print("Which NPC?");
            string input = CIO.ReadLine();
            if (wannaExit(input))
            {
                return;
            }
            Attackable enemy = Program.currentRoom.getNPCByName(input);
            Fight f = new Fight(enemy);
            f.startFight();
        }
        public static void Help() {
            if(CIO.GetCurrentContext().name== "Inventory.Category")
            {
                CIO.PrintHelp("help for Inventory");
            }
            else
            {
                CIO.PrintHelp("type i for inventory");
                CIO.PrintHelp("type Attack to attack an NPC");

            }


        }
        private static bool wannaExit(string input){
            return input.ToLower() == "e";
        }
        public static Boolean existsCheat(string s){
            return commands.ContainsKey(s);
        }
        public static void executeCheat(string s){
            if(!commands.ContainsKey(s)){
                return;
            }
            Console.ForegroundColor= ConsoleColor.White;
            
            foreach (Action a in commands[s])
            {
                a();
            }
            CIO.ReEnterCurrentContext();
            Console.ForegroundColor= ConsoleColor.Blue;
        }


        public static void LoadAll()
        {
            void AddCheat(string s, Action a)
            {
                s = s.ToLower();
                if (commands.ContainsKey(s))
                {
                    commands[s].Add(a);
                }
                else
                {
                    commands.Add(s, new List<Action>());
                    commands[s].Add(a);
                }
            }
            AddCheat("give gold", () => GlobalCommands.GiveGold());
            AddCheat("i", () => Program.player.OpenInventory());
            AddCheat("Attack", () => Attack());
            AddCheat("h", () => Help());
            AddCheat("help", () => Help());
            AddCheat("give sword", () => GiveSword());
            AddCheat("give xp", () => GiveXP());
            AddCheat("debugging", () => CIO.ToggleDebugging());
        }
    }
}
