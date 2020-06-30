using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECSFramework.Interfaces.Generalnterfaces
{
    public interface IEventListener
    {

        void HandleEvent(object pSource, EventArgs pArgs);
    }
}
