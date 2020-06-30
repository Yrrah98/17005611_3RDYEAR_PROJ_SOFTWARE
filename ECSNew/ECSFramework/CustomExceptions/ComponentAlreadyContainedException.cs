using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECSFramework.CustomExceptions
{
    public class ComponentAlreadyContainedException : Exception
    {

        public ComponentAlreadyContainedException(String exception) : base(exception) 
        {

        }

    }
}
