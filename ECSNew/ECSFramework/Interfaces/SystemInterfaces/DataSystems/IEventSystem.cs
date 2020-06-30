using ECSFramework.Interfaces.Generalnterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECSFramework.Interfaces.SystemInterfaces.DataSystems
{
    public interface IEventSystem
    {

        void AddHandler(EventHandler<EventArgs> handler);

        void RemoveHandler(EventHandler<EventArgs> handler);

        void AddToQueue(EventArgs args);
    }
}
