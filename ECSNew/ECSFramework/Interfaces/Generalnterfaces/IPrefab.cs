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
    /// INTERFACE PURPOSE: The purpose of this interface is for any and all pre-fabs to
    /// conform to this to allow a generic class to get necessary components
    /// </summary>
    public interface IPrefab
    {
        IList<IComponent> ReturnComponents(IComponentSystem componentSystem, ContentManager content, IList<IComponent> listToFill);
    }
}
