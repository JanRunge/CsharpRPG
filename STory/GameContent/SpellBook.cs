using STory.GameContent.Spells;
using STory.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STory.GameContent
{
    class SpellBook
    {
        Spell equippedSpell;
        Dictionary<SpellType,List<Spell>> LearnedSpells = new Dictionary<SpellType, List<Spell>>();

        public void LearnSpell(Spell s)
        {
            if (!LearnedSpells.ContainsKey(s.getType()))
            {
                LearnedSpells.Add(s.getType(), new List<Spell>());
            }
            if (!LearnedSpells[s.getType()].Contains(s))
            {
                LearnedSpells[s.getType()].Add(s);
            }
        }
        public void EquipSpell(Spell s)
        {
            equippedSpell = s;
            LearnSpell(s);
        }
        public bool IsSpellLearned(Spell s)
        {
            return LearnedSpells.ContainsKey(s.getType());
        }
        public List<Spell> getSpells(SpellType type)
        {
            if (LearnedSpells.ContainsKey(type))
            {
                return LearnedSpells[type];
            }
            else
            {
                return new List<Spell>();
            }
        }
        public List<Spell> getAllSpells()
        {
            List<Spell> l = new List<Spell>();
            foreach(KeyValuePair<SpellType, List<Spell>> kv in this.LearnedSpells){
                l.AddRange(kv.Value);
            }
            return l;
        }
        public Dictionary<SpellType,List<Spell>> getSpells()
        {
            return LearnedSpells;
        }

    }
}
