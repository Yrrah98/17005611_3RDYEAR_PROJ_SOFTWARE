using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECSFramework.Interfaces.SystemInterfaces
{
    public interface IRqdComponents
    {
        /// <summary>
        /// PROPERTY: A property which will contain an individual list of elements which 
        /// any system that implements this will require
        /// </summary>
        IReadOnlyList<Type> RqdComponents { get; }
        /// <summary>
        /// PROPERTY: A property which contains a sequence of bit values 
        /// which pertain to a set of components that the class that implements this requires
        /// </summary>
        long RequiredKeys { get; set; }
    }
}
