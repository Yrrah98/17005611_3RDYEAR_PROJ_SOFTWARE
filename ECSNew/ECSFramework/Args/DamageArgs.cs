using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECSFramework.Args
{
    public class DamageArgs : EventArgs
    {

        public int Damage;

        public String Entity;

        public DamageArgs(int dmg, String entityToDamage) 
        {
            Damage = dmg;

            Entity = entityToDamage;
        }
    }
}
