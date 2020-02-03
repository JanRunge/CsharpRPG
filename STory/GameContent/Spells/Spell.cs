using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using STory.Handlers.Fight;
using STory.Handlers.Option;
using STory.Types;

namespace STory.GameContent.Spells
{
    public abstract class Spell : FightAction
    {
        string name;
        int Cost;
        SpellType type;
        public static Spell Flames = new Damagespell(DamageType.Fire, 20,20,"Flamespell");
        public Spell(string name, int Cost, SpellType st)
        {
            this.name = name;
            this.Cost = Cost;
            this.type = st;
        }

        public virtual void Cast(Attackable from, Attackable onto)
        {
            from.RemoveMana(Cost);
        }
        public SpellType getType()
        {
            return type;
        }
        public int GetCost()
        {
            return Cost;
        }
        public virtual void Cast(Character caster, Attackable target)
        {
            caster.RemoveMana(caster.calculateCost(this));
        }

        public string getText()
        {
            return this.name + "("+this.Cost+" mana)";
        }

        public ConsoleColor GetColor()
        {
            return CIO.defaultcolor;
            
        }

        public virtual bool isAvailable()
        {//todo: certain spells should only be available in combat
            //todo check for items that reduce mana
            Player inst = Player.getInstance();
            return inst.HasMana(inst.calculateCost(this));
        }

        public string getPreferredCommand()
        {
            return null;
        }

        public void Select()//todo it makes no sense that items define waht should happen when they are selected
        {
            Player.getInstance().EquipSpell(this);
        }

        public void Use(Character user, Character target)
        {
            Cast(user, target);
        }
    }
}
