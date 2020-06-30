using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECSFramework.Args
{
    public class CollisionEventArgs : EventArgs
    {
        public IList<String> entities;
        public CollisionEventArgs(IList<String> pEntities) 
        {
            entities = pEntities;

        }
    }
}
