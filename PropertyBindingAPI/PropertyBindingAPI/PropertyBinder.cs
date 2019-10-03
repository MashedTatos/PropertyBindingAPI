using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Linq;
using BindingAPI.Exceptions;

namespace BindingAPI
{
    public static class PropertyBinder
    {

        /// <summary>
        /// Binds properties of two classes
        /// </summary>
        /// <typeparam name="T">Class which contains Binding defintions</typeparam>
        /// <typeparam name="S">Class which contains properties to bind to</typeparam>
        /// <param name="bindSource">Object which has the properties that will be binded to the target</param>
        /// <returns>A BindingRespository that contains all the bindings</returns>
        public static BindingRepository Bind<T, S>(S bindSource)
        {
            Console.WriteLine("Binding");
            BindingRepository bindingRepository = new BindingRepository(bindSource);

            //Get all properties in source object that have a binding specified
            List<PropertyInfo> sourceProperties = typeof(S).GetProperties()
                .Where(p => p.IsBinding()).ToList();
            
            List<PropertyInfo> targetProperties = typeof(T).GetProperties().ToList();

            foreach(PropertyInfo sourceProperty in sourceProperties)
            {
                foreach(PropertyInfo targetProperty in targetProperties)
                {

                    //Gets PropertyBindingAttribute. This is needed to specify which properties in the target to bind
                    PropertyBindingAttribute bindingAttribute = sourceProperty.GetCustomAttribute<PropertyBindingAttribute>();
                    if(bindingAttribute.name == targetProperty.Name)
                    {
                        try
                        {
                            //Verifies the two properties are the same type
                            VerifyTypes(sourceProperty, targetProperty);
                        }
                        catch(Exception e)
                        {
                            Console.WriteLine(e.ToString());
                            continue;
                        }
                        Console.WriteLine($"{targetProperty.Name} is bound to {sourceProperty.Name}");

                        //Add binding to repository
                        bindingRepository[targetProperty.Name] = sourceProperty;
                    }
                    
                }
            }

            return bindingRepository;

                
        }

        /// <summary>
        /// Verifies two property types
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        public static void VerifyTypes(PropertyInfo p1 , PropertyInfo p2)
        {
            Type t1 = p1.PropertyType;
            Type t2 = p2.PropertyType;
            if(t1 != t2)
            {

                throw new BindedTypeMismatchException($"Types do not match for properties: {p1.Name} {p2.Name}");
            }
        }

        

    }

    public static class PropertyInfoExtensions
    {
        /// <summary>
        /// Extension method to see if Property has specified attribute
        /// </summary>
        /// <typeparam name="T">Attribute to check</typeparam>
        /// <param name="info"></param>
        /// <returns>true if yes; false if no</returns>
        public static bool HasAttribute<T>(this PropertyInfo info)
            where T : Attribute
        {
            if (info.GetCustomAttribute(typeof(T)) != null)
            {
                return true;
            }

            else
            {
                return false;
            }
        }

        /// <summary>
        /// Extension method to check if property has a binding
        /// </summary>
        /// <param name="info"></param>
        /// <returns>true if yes; false if no</returns>
        public static bool IsBinding(this PropertyInfo info)
        {
            return info.HasAttribute<PropertyBindingAttribute>();
        }
    }
}
