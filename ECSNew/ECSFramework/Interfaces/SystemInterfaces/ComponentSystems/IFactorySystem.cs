using ECSFramework.Interfaces.SystemInterfaces.DataSystems;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECSFramework.Interfaces.SystemInterfaces.ComponentSystems
{
    public interface IFactorySystem
    {
        /// <summary>
        /// METHOD: A method in the FactorySystem where the necessary systems/delegates will be
        /// given so that the necessary operations can be performed
        /// </summary>
        /// <param name="entitySystem"></param>
        /// <param name="componentSystem"></param>
        void InjectSystems(IEntitySystem entitySystem, IComponentSystem componentSystem, ContentManager content, AddToDatabase add);
    }
}
