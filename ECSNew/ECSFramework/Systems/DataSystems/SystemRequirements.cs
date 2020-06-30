using ECSFramework.Interfaces.Generalnterfaces;
using ECSFramework.Interfaces.SystemInterfaces;
using ECSFramework.Interfaces.SystemInterfaces.DataSystems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECSFramework.Systems.DataSystems
{
    /// <summary>
    /// AUTHOR: Harry Jones
    /// VERSION: 0.2
    /// CLASS PURPOSE: The purpose of this class is to setup the requirements for any number of systems 
    /// REFERENCE to MichaelHouse: 
    /// https://gamedev.stackexchange.com/questions/31473/what-is-the-role-of-systems-in-a-component-based-entity-architecture/31491#31491
    /// MichaelHouse provided the inspiration to use a bitkey system for components and systems. The implementation is one
    /// which has been interpreted from MichaelHouse's answer. 
    /// </summary>
    public class SystemRequirements : ISystemRequirements
    {
        #region int requirements
        private const long _systemBaseKey = 0b_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000;

        private int _bitCount;
        public IDictionary<Type, long> CompWithKey { get; private set; }
        #endregion

        public SystemRequirements() 
        {
            CompWithKey = new Dictionary<Type, long>();
        }

        #region ISystemRequirements
        public long GenerateSystemRequirements(IRqdComponents pCompReceiver)
        {
            // INITIALISE a new long and set its value to the _systemBaseKey value
            long newKey = _systemBaseKey;

            // FOR LOOP through the RqdComponents of the IRqdComponents variables passed in
            for (int i = 0; i < pCompReceiver.RqdComponents.Count; i++)
            {
                // IF the required component is already contained in the list
                if (CompWithKey.ContainsKey(pCompReceiver.RqdComponents[i]))
                    // THEN ADD the long value of that component to the value of the local long variable
                    newKey += CompWithKey[pCompReceiver.RqdComponents[i]];
                else
                {
                    // IF IT IS NOT CONTAINED
                    // THEN 
                    // INITIALISE a local long inside this statement and set its value
                    // to the returned value of the 2 to the power of the _bitCount
                    long newCompKey = ((int)Math.Pow(2, _bitCount));
                    // ADD the component type to the dictionary as the key and the long local to this 
                    // statement as the element
                    CompWithKey.Add(pCompReceiver.RqdComponents[i], newCompKey);
                    // ADD the value of that new component to the value of the long local to this method
                    newKey += CompWithKey[pCompReceiver.RqdComponents[i]];

                    // INCREMENT the _bitCount variable
                    _bitCount++;
                }
            }
            // RETURN the new key which will be the systems "lock"
            // which determines whether it requires an entity
            return newKey;
        }

        public void GenerateNewComponentKey(Type pKey) 
        {

            // INITIALISE a local long inside this statement and set its value
            // to the returned value of the 2 to the power of the _bitCount
            long newCompKey = ((int)Math.Pow(2, _bitCount));

            // ADD the component type to the dictionary as the key and the long local to this 
            // statement as the element
            CompWithKey.Add(pKey, newCompKey);

            _bitCount++;

        }

        #endregion
    }
}
