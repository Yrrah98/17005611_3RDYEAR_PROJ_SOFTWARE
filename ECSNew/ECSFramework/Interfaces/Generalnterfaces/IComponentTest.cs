using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECSFramework.Interfaces.Generalnterfaces
{
    /// <summary>
    /// AUTHOER: Harry Jacob Jones
    /// VERSION: 0.2
    /// CLASS PURPOSE: Provides an interface for all components to implement 
    /// this is a test class which is intended to make use of generics to return any piece of data 
    /// without ever having to down cast 
    /// </summary>
    public interface IComponentTest<T> where T : Type
    {
        // METHOD which will return data based on a specified Type
        T Data();
    }
}
