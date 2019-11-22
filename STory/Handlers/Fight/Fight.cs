using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using STory.GameContent;
using STory.GameContent.Items;
using STory.Handlers.Fight;

namespace STory.GameContent
{
    class Fight
    {
        public Attackable Enemy;
        public Fight(Attackable Enemy)
        {
            this.Enemy = Enemy;
        }
        public void Attack()
        {
            Program.player.attack(Enemy);
            if(Enemy.isAlive()){
                Enemy.attack(Program.player);
            }
            
        }
        public void startFight()
        {
            //attacks back and forth until someone dies
            while(Enemy.isAlive()){
                Program.player.Weapon = getPlayerWeapon();
                Attack();
            }

            CIO.Print("you defeated " + Enemy.getName());
            Program.player.GiveXP(Enemy.XPOnDeath());
            
        }
        public Weapon getPlayerWeapon(){
            Optionhandler oh = new Optionhandler("Choose your weapon");
            oh.setOptionGenerator(() => oh.AddOptions(Optionhandler.ItemToOption(Program.player.inventory.GetAllItems("Weapons"))));
            return (Weapon)oh.selectOption();
        }
    }
}
