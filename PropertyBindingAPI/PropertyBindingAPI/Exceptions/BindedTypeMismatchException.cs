using System;
using System.Collections.Generic;
using System.Text;

namespace BindingAPI.Exceptions
{
    public class BindedTypeMismatchException : PropertyBindingExeption
    {

        public BindedTypeMismatchException()
        {

        }

        public BindedTypeMismatchException(string message) : base(message)
        {
            
            
        }

        public BindedTypeMismatchException(string message, Exception inner, Type type1, Type type2) : base(message, inner)
        {

        }
    }
}
