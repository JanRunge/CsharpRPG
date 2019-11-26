using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STory.Handlers.Option
{
    class Multioption 
    {
        List<Option> options= new List<Option>();

        public Multioption()
        {

        }
        public Multioption(List<Option> l)
        {
            options = l;
        }
        public void AddOption(Option o)
        {
            options.Add(o);
        }
        public void getText()
        {
            
        }

        public static bool isMultioption(object o)
        {
            return o.GetType() == typeof(Multioption);
        }
    }
}
