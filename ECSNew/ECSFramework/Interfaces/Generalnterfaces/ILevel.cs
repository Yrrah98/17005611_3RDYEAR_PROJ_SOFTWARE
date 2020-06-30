using ECSFramework.Interfaces.SystemInterfaces.DataSystems;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECSFramework.Interfaces.Generalnterfaces
{
    /// <summary>
    /// AUTHOR: Harry Jones
    /// VERSION: 0.2
    /// CLASS PURPOSE: The purpose of this class is for all levels to implement this interface
    /// to allow them to be interchangeable and load the neccessary entities for each level
    /// </summary>
    public interface ILevel
    {
        /// <summary>
        /// METHOD: LoadContent, the method where a level will be generated
        /// </summary>
        /// <param name="pES"> Entity system to create entities</param>
        /// <param name="pCS"> Component system to generate components</param>
        /// <param name="pDB"> Database for entities to be added to </param>
        /// <param name="pCM"> ContentManager so content like textures and sounds can be loaded</param>
        void LoadContent(IEntitySystem pES, IComponentSystem pCS, IECSDatabase pDB, ContentManager pCM);
    }
}
