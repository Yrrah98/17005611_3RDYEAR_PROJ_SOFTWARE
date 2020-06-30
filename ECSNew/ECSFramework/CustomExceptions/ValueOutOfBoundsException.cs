using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECSFramework.CustomExceptions
{
    public class ValueOutOfBoundsException : Exception
    {

        public ValueOutOfBoundsException() 
        {
        }

        public ValueOutOfBoundsException(String msg) : base(msg)
        {

        }

        public ValueOutOfBoundsException(String msg, Exception inner) : base(msg, inner)
        {

        }
    }
}
