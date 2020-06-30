using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECSFramework.Args
{
    public class RemoveEntityArgs : EventArgs
    {
        public String entity;
        public RemoveEntityArgs(String e) 
        {
            entity = e;
        }
    }
}
