using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodePad_C__15
{
    public class FoundTextEventArgs : EventArgs
    {
        public int FoundIndex { get; }
        public FoundTextEventArgs()
        {
        }
        public FoundTextEventArgs(int foundIndex)
        {
            FoundIndex = foundIndex;
        }
    }
}
