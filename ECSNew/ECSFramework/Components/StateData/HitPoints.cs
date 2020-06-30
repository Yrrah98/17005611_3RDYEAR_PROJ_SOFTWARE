using ECSFramework.Interfaces.Generalnterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECSFramework.Components.StateData
{
    public class HitPoints : IComponent
    {
        public int MaxHitPoints { get; set; }
        public int CurrentHitPoints { get; set; }
        public HitPoints() 
        {

        }
    }
}
