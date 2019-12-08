using STory.GameContent.Spells;
using STory.Handlers.Option;
using STory.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STory.GameContent
{
    public class SpellBook
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
        public List<SpellType> getSpellTypes()
        {
            List<SpellType> l = new List<SpellType>();
            foreach (KeyValuePair<SpellType, List<Spell>> kv in this.LearnedSpells)
            {
                l.Add(kv.Key);
            }
            return l;
        }
        public Dictionary<SpellType,List<Spell>> getSpells()
        {
            return LearnedSpells;
        }
        public void Open()
        {
            while (true)
            {
                Option g = PickCategory();
                if (g == Optionhandler.Exit)
                {
                    return;
                }
                NavigateItems((SpellType)g);//hier kann nur eine Exit-option rauskommen

            }
        }
        protected Option PickCategory()
        {
            printHeader();
            Optionhandler OH = new Optionhandler("pick a category", true);
            OH.setName("Spellbook.Types");

            foreach (SpellType i in this.getSpellTypes())
            {
                OH.AddOption(i);
            }
            return OH.selectOption(false);
        }
        void NavigateItems(SpellType type)
        {
            Option selected = null;
            while (selected != Optionhandler.Exit)
            {
                List<Spell> Spells = this.getSpells(type);
                if (Spells.Count == 0)
                {
                    return;
                }
                printHeader();
                Optionhandler OH = new Optionhandler(true);
                OH.setName("Spellbook.Spell");
                foreach (Spell i in Spells)
                {
                   OH.AddOption(i);
                }
                selected = OH.selectOption(false);
            }
        }
        void printHeader()
        {
            CIO.Clear();
            CIO.Print("TODO: No header");
        }

    }
}
