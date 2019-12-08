using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using STory.GameContent;
using STory.GameContent.Items;
using STory.Handlers.Fight;
using STory.Handlers.IO;
using STory.Handlers.Option;

namespace STory.GameContent
{
    /// <summary>
    /// An Object to Handle a fight between the player and an Attackable
    /// </summary>
    class Fight
    {
        public Character Enemy;
        public Fight(Character Enemy)
        {
            this.Enemy = Enemy;
        }
        /// <summary>
        /// User and Enemy attack each other until one dies.
        /// </summary>
        public void startFight()
        {
            //attacks back and forth until someone dies
            
            while (Enemy.isAlive()){
                FightAction FA = getPlayerWeapon();
                FA.Use(Program.player, Enemy);
                if (Enemy.isAlive())
                {
                    Enemy.attack(Program.player);
                }
            }
            CIO.Print("you defeated " + Enemy.getName());
            Program.player.GiveXP(Enemy.XPOnDeath());
            

        }
        /// <summary>
        /// Let the Player choose his weapon for the next turn
        /// </summary>
        public FightAction getPlayerWeapon(){
            Optionhandler oh = new Optionhandler("Choose your weapon");
            oh.setOptionGenerator(()=>getAllOptions());
            return (FightAction)oh.selectOption();
        }
        public List<Option> getAllOptions()
        {

            List<Option> Weapons = Program.player.inventory.GetAllItems("Weapons").Cast<Option>().ToList();
            List<Option> Potions = Program.player.inventory.GetAllItems("Potion").Cast<Option>().ToList();
            List<Option> Spells = Program.player.getAllSpells().Cast<Option>().ToList();//todo make this a submenu
            List<Option> all = new List<Option>();
            all.AddRange(Weapons);
            all.AddRange(Potions);
            all.AddRange(Spells);
            return all;

        }
    }
}
