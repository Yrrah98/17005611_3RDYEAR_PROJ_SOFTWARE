using ECSFramework.Interfaces.Generalnterfaces;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECSFramework.Interfaces.SystemInterfaces
{
    public interface ICentralSystem
    {
        void ComponentReceiver(String pEntity, long componentKey);

        void Initialise();

        void LoadContent(ILevel pLvl);
    }
}
