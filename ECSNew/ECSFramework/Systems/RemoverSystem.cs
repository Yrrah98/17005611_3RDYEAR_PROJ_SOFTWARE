using ECSFramework.Args;
using ECSFramework.Interfaces.Generalnterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECSFramework.Systems
{
    public class RemoverSystem : IRemoverSystem, IEventListener
    {

        public IList<VoidEntity> _removerList;

        public VoidEntity _removers;

        public RemoverSystem() 
        {
            _removerList = new List<VoidEntity>();
        }

        #region IEventListener
        public void HandleEvent(object source, EventArgs args) 
        {
            if (args is RemoveEntityArgs)
            {
                _removers((args as RemoveEntityArgs).entity);
            }
        }

        #endregion

        #region IRemoverSystem
        public void InjectRemovers(VoidEntity remover) 
        {
            _removers += remover;
        }

        #endregion
    }
}
