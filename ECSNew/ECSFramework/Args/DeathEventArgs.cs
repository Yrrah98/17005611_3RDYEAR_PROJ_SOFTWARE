using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECSFramework.Args
{
    public class DeathEventArgs : EventArgs
    {
        public String DeadEntity;

        public DeathEventArgs(String entity) 
        {
            DeadEntity = entity;
        }
    }
}
