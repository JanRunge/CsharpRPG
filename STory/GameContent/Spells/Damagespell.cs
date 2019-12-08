using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using STory.Handlers.Fight;
using STory.Types;

namespace STory.GameContent.Spells
{
    class Damagespell : Spell
    {
        

        DamageType damageType;
        int damage;
        public Damagespell(DamageType dmgt, int baseDamage, int Cost, string name):base(name,Cost,SpellType.DamageSpell)
        {
            this.damageType = dmgt;
            this.damage = baseDamage;
        }
        override public void Cast(Character caster, Attackable target)
        {
            base.Cast(caster, target);
            int dmg = getDamage(caster.GetIntelligence(), target.GetStrength());
            target.receiveDamage(dmg, damageType);

        }
        public int getDamage(int Intelligence, int Strength)
        {
            return (int)Math.Round(damage*( 1 + (0.015f * (Intelligence - 20))));
        }
    }
}
