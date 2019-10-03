using System;
using System.Collections.Generic;
using System.Text;

namespace BindingAPI.Exceptions
{
    class BindedPropertyMutatorException : PropertyBindingExeption
    {

        public BindedPropertyMutatorException()
        {

        }

        public BindedPropertyMutatorException(string message) : base(message)
        {


        }

        public BindedPropertyMutatorException(string message, Exception inner) : base(message, inner)
        {

        }
    }
}
