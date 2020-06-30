using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECSFramework.Interfaces.Generalnterfaces
{
    public interface IRender
    {
        SpriteBatch Draw(SpriteBatch spriteBatch);
    }
}
