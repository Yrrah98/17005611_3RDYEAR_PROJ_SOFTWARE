using ECSFramework.Interfaces.Generalnterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECSFramework.Interfaces.SystemInterfaces
{
    public interface IComponentReceiver
    {
        void ReceiveEntities(IDictionary<String, IInnerDictionary> pEntities);
    }
}
