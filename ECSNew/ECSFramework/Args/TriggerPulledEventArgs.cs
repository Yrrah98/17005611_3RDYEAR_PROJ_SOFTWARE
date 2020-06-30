using ECSFramework.Interfaces.Generalnterfaces;
using ECSFramework.Prefabs.EntityPrefabs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECSFramework.Args
{
    public class TriggerPulledEventArgs : EventArgs
    {
        public String Entity;

        public float X;

        public float Y;
        public TriggerPulledEventArgs(String entity, float x, float y) 
        {

            Entity = entity;

            X = x;

            Y = y;
        }
    }
}
