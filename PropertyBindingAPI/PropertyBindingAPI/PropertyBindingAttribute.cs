using System;
using System.Collections.Generic;
using System.Text;

namespace BindingAPI
{
    public class PropertyBindingAttribute : Attribute
    {
        public string name;

        public PropertyBindingAttribute(string name)
        {
            this.name = name;
        }

    }
}
