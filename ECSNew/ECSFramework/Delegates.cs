using ECSFramework.Interfaces.Generalnterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECSFramework
{
    public delegate void GiveEntities(String entity, long componentKey);

    public delegate void AddToDatabase(String entity, IList<IComponent> componentList);

    public delegate void CheckCollisions(String e1, String e2);

    public delegate void EnqueueEvent(EventArgs args);

    public delegate void VoidEntity(String entity);

    public delegate void AddNewCompKey(Type pKey);
}
