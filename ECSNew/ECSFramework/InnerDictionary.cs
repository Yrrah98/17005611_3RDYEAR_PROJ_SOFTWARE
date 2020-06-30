using ECSFramework.Interfaces.Generalnterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECSFramework
{
    public class InnerDictionary : IInnerDictionary
    {
        /// <summary>
        /// PROPERTY: Components, an InnerDictionary which contains a dictionary of IComponents where 
        /// the key is the type for that component
        /// </summary>
        public IDictionary<Type, IComponent> Components { get; private set; }

        /// <summary>
        /// CONSTRUCTOR for InnerDictionary
        /// </summary>
        public InnerDictionary() 
        {
            Components = new Dictionary<Type, IComponent>();
        }

        /// <summary>
        /// METHOD: AddEntry a method which will add an entry to the dictionary providing it
        /// doesnt already contain the key
        /// </summary>
        /// <param name="pKey"> The key to be added to the dictionary </param>
        /// <param name="pComponent"> The component to be added along side the key</param>
        public void AddEntry(Type pKey, IComponent pComponent)
        {
            // IF the Components dictionary does not contain the key already
            if (!Components.ContainsKey(pKey))
                Components.Add(pKey, pComponent);
            else
                Console.WriteLine("Key already contained");
        }
    }
}
