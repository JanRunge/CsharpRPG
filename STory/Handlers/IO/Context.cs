using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STory.Handlers.IO
{
    public class Context
    {
        Optionhandler currentOptionhandler;
        string text;
        public string name;
        public Context(Optionhandler currentOptionhandler)
        {
            this.currentOptionhandler = currentOptionhandler;
        }
        public Context(Optionhandler currentOptionhandler,string name)
        {
            this.currentOptionhandler = currentOptionhandler;
            this.name = name;
        }
        public Context(string text)
        {
            this.text = text;
        }
        public void reEnter()
        {
            if (text != null)
            {
                CIO.Print(text);
            }
            if (currentOptionhandler != null)
            {
                currentOptionhandler.printOptions();
            }

        }
    }
}
