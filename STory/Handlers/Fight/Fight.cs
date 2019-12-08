using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using STory.GameContent;
using STory.GameContent.Items;
using STory.Handlers.Fight;
using STory.Handlers.IO;

namespace STory.GameContent
{
    /// <summary>
    /// An Object to Handle a fight between the player and an Attackable
    /// </summary>
    class Fight
    {
        public Attackable Enemy;
        public Fight(Attackable Enemy)
        {
            this.Enemy = Enemy;
        }
        /// <summary>
        /// Deal Damage to the Attackable and Receive Damage from the Attackable
        /// </summary>
        public void Attack()
        {
            Program.player.attack(Enemy);
            if(Enemy.isAlive()){
                Enemy.attack(Program.player);
            }
            
        }
        /// <summary>
        /// User and Enemy attack each other until one dies.
        /// </summary>
        public void startFight()
        {
            //attacks back and forth until someone dies
            while (Enemy.isAlive()){
                Program.player.Equip(getPlayerWeapon());
                Attack();
            }
            CIO.Print("you defeated " + Enemy.getName());
            Program.player.GiveXP(Enemy.XPOnDeath());
            

        }
        /// <summary>
        /// Let the Player choose his weapon for the next turn
        /// </summary>
        public Weapon getPlayerWeapon(){
            Optionhandler oh = new Optionhandler("Choose your weapon");
            oh.setOptionGenerator(() => Optionhandler.ItemToOption(Program.player.inventory.GetAllItems("Weapons")));
            return (Weapon)oh.selectOption();
        }
    }
}
