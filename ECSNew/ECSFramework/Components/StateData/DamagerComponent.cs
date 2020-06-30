using ECSFramework.Interfaces.Generalnterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECSFramework.Components.StateData
{
    public class DamagerComponent : IComponent
    {
        public int Damage { get { if (_damage < 0) { _damage = 0; return _damage; }
                else return _damage; }
            set { if (value < 0) { _damage = 0; }
                else _damage = value;
            }
        }

        private int _damage;
        public DamagerComponent() 
        {

        }
    }
}
