using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STory.Handlers.IO
{
    /// <summary>
    /// A COntext contains a text an/or an Optionhandler, which both are can be reprinted (the optionhandler also gets re-evaluated)
    /// </summary>
    public class Context
    {
        Optionhandler myOptionhandler;
        string text;
        public string name;
        public Context(Optionhandler currentOptionhandler)
        {
            this.myOptionhandler = currentOptionhandler;
        }
        public Context(Optionhandler currentOptionhandler,string name)
        {
            this.myOptionhandler = currentOptionhandler;
            this.name = name;
        }
        public Context(string text)
        {
            this.text = text;
        }
        /// <summary>
        /// reprint the text and Optionhandler (the optionhandler  gets re-evaluated)
        /// </summary>
        public void Enter()
        {
            if (text != null)
            {
                CIO.Print(text);
            }
            if (myOptionhandler != null)
            {
                myOptionhandler.printOptions();
            }

        }
    }
}
