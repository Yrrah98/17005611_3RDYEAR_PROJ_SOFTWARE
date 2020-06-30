using ECSFramework.Interfaces.SystemInterfaces.DataSystems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECSFramework.Systems.DataSystems
{
    public class EntitySystem : IEntitySystem
    {

        public EntitySystem() 
        {

        }

        public String CreateNewEntity() 
        {
            return Guid.NewGuid().ToString();
        }
    }
}
