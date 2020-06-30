using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECSFramework.Interfaces.Generalnterfaces
{
    public interface IECSUpdateable
    {
        /// <summary>
        /// METHOD: Update, a method which is called on each frame of the
        /// simulation
        /// </summary>
        /// <param name="gameTime"></param>
        void Update(GameTime gameTime);
    }
}
