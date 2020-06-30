using ECSFramework.Interfaces.Generalnterfaces;
using ECSFramework.Interfaces.SystemInterfaces.DataSystems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECSFramework.Systems.DataSystems
{
    public class ECSDatabase : IECSDatabase
    {
        /// <summary>
        /// PROPERTY: A property which is a dictionary, the key is the entitiy
        /// </summary>
        public IDictionary<String, IInnerDictionary> Entities { get; private set; }
        /// <summary>
        /// PROPERTY: A propert which is a Dictionary, the key is the entity and the element is 
        /// an Int32 which stores the different components as 1s in a 32 bit set. Further version may require use of a long
        /// number of components equals number of bits. 5 max components = 00000, 6 max components = 000000
        /// Example: an entity with component x,y,z and nothing else may look like 111000
        /// this is to be used to allow systems to find which entitys fit there system, a system may look for a 
        /// 111000 or a 111111 
        /// </summary>
        public IDictionary<String, long> EntityKeys { get; private set; }

        public IDictionary<Type, long> CompKeys { get; private set; }

        public IList<Type> ActiveComponents { get; private set; }

        // DECLARE an IInnerDictionary called _innerDictionary
        private IInnerDictionary _innerDictionary;
        // DELCARE a Delegate of type GiveEntities to store a reference to a delegate
        public GiveEntities _giveEntities;
        // DECLARE a delegate of type AddNewCompKey which will store a delegate to create new component
        // keys if anything is wrong with the current list of keys
        public AddNewCompKey _newCompKey;

        /// <summary>
        /// CONSTRUCTOR for class ECSDatabase
        /// </summary>
        /// <param name="pGive"> Delegate which allows database to give entities to CentralSystem</param>
        /// <param name="pCompKeys"> A reference to a variable in the SystemRequirements class</param>
        public ECSDatabase(IDictionary<Type, long> pCompKeys, GiveEntities pGive2, AddNewCompKey pNewComp) 
        {
            _giveEntities = pGive2;

            CompKeys = pCompKeys;

            ActiveComponents = new List<Type>();

            EntityKeys = new Dictionary<String, long>();

            Entities = new Dictionary<String, IInnerDictionary>();

            _newCompKey = pNewComp;
            
        }


        #region IECSDatabase methods

        public void AddEntity(string pEntity, IList<IComponent> pComponents)
        {
            // INITIALISE InnerDictionary as new to clear it
            _innerDictionary = new InnerDictionary();

            // INITIALISE UInt32 called newBitKey, to store a base 2 value
            // use binary literal and prefix the amount of values
            long newBitKey = 0b_0000_0000_0000_0000_0000_0000_0000_0000;

            for (int i = 0; i < pComponents.Count; i++)
            {
                if (CompKeys.ContainsKey(pComponents[i].GetType()))
                    newBitKey += CompKeys[pComponents[i].GetType()];
                if (!CompKeys.ContainsKey(pComponents[i].GetType()))
                    _newCompKey(pComponents[i].GetType());
            }


            // FOREACH IComponent in the list components
            foreach (IComponent c in pComponents)
            {
                // CALL AddEntry method of the InnerDictionary
                // passing in the name of the component and the component
                _innerDictionary.AddEntry(c.GetType(), c);
                // IF the list of components does not contain the component name
                if (!ActiveComponents.Contains(c.GetType()))
                    // THEN ADD the component type, to the list
                    ActiveComponents.Add(c.GetType());
            }

            if (!EntityKeys.ContainsKey(pEntity) && !Entities.ContainsKey(pEntity))
            {
                EntityKeys.Add(pEntity, newBitKey);
                // ADD the string as a key and the InnerDictionary with all the components to the 
                // dictionary
                Entities.Add(pEntity, _innerDictionary);

                _giveEntities(pEntity, newBitKey);
            }
        }

        public void RemoveEntity(string pEntity)
        {
            Entities.Remove(pEntity);
        }
        #endregion
    }
}
