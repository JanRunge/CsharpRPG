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
                FA.Use(Player.getInstance(), Enemy);
                if (Enemy.isAlive())
                {
                    Enemy.attack(Player.getInstance());
                }
            }
            CIO.Print("you defeated " + Enemy.getName());
            Player.getInstance().GiveXP(Enemy.XPOnDeath());
            

        }
        public FightAction getPlayerWeapon()
        {
            while (true)
            {
                Optionhandler ohCategory = new Optionhandler("Choose your next Action");
                ohCategory.AddOption(new GenericOption("Weapons"));
                ohCategory.AddOption(new GenericOption("Potion"));
                ohCategory.AddOption(new GenericOption("Spell"));//todo: check if items are in the option beforehand
                GenericOption selectedCategory = (GenericOption)ohCategory.selectOption();
                List<Option> optionsInTheCategory;
                if (selectedCategory.name == "Spell")
                {
                    optionsInTheCategory = Player.getInstance().getAllSpells().Cast<Option>().ToList();
                }
                else
                {
                    optionsInTheCategory = Player.getInstance().inventory.GetAllItems(selectedCategory.name).Cast<Option>().ToList();
                }
                Optionhandler ohItem = new Optionhandler("choose your " + selectedCategory.name, true);
                ohItem.AddOptions(optionsInTheCategory);
                Option selected = ohItem.selectOption();
                if (selected != Optionhandler.Exit)
                { 
                   return (FightAction)selected;
                }
            }
            


        }

    }
}
