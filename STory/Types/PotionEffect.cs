using STory.GameContent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STory.Types
{
    public class PotionEffect
    {
        public Action<float> action;
        public Dictionary<Potionsize, float> EffectAmplifier;//list of all Sizes and how much they emplify the effect
        string name;
        public PotionEffect(Action<float> action, Dictionary<Potionsize, float>  sizeMultiplicator, string name)
        {
            foreach(Potionsize p in Potionsize.AllSizes())
            {
                if (!sizeMultiplicator.ContainsKey(p))
                {
                    sizeMultiplicator.Add(p, p.defaultMultiplicator);
                }
            }
            this.EffectAmplifier = sizeMultiplicator;
            this.action = action;
            this.name = name;
        }
        public PotionEffect(Action<float> action, string name):this(action,new Dictionary<Potionsize, float>(), name)
        {

        }
        public void startEffect(Potionsize size)
        {
            action(EffectAmplifier[size]);
        }
        public static PotionEffect Heal = new PotionEffect((multiplicator) => Player.getInstance().RestoreHealth(multiplicator * 20), "Heal");

    }
}
