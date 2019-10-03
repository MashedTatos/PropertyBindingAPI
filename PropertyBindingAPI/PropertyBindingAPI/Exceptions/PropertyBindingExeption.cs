using System;
using System.Collections.Generic;
using System.Text;

namespace BindingAPI.Exceptions
{
    public class PropertyBindingExeption : Exception
    {

        public PropertyBindingExeption()
        {

        }

        public PropertyBindingExeption(string message) : base(message)
        {

        }

        public PropertyBindingExeption(string message, Exception inner) : base(message,inner)
        {

        }
    }
}
