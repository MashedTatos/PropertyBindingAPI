using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using BindingAPI.Exceptions;
namespace BindingAPI
{
    public class BindingRepository
    {

        private object sourceObject;

        private Dictionary<string, PropertyInfo> bindings = new Dictionary<string, PropertyInfo>();

        private Dictionary<string, PropertyInfo> deactivatedBindings = new Dictionary<string, PropertyInfo>();

        public bool initialized;
        /// <summary>
        /// Constructor for BindingRespository
        /// </summary>
        /// <param name="sourceObject">Object which has source properties</param>
        public BindingRepository(object sourceObject)
        {
            initialized = true;
            SourceObject = sourceObject;
        }

        public BindingRepository()
        {
            initialized = false;
        }

        public PropertyInfo this[string s]
        {
            get => bindings[s];
            set => bindings[s] = value;
        }

        public object SourceObject { get => sourceObject; set => sourceObject = value; }
        public Dictionary<string, PropertyInfo> Bindings {set => bindings = value; }

        /// <summary>
        /// Checks to see if binded property is in repository
        /// </summary>
        /// <param name="n">Name of property</param>
        /// <returns>true if it does; false if not</returns>
        public bool HasBinding(string n)
        {
            Console.WriteLine(n);
            bool result = false;
            PropertyInfo p;
            if (bindings.TryGetValue(n, out p))
            {
                result = true;
            }

            return result;
        }

        /// <summary>
        /// Trys to get value of which property is binded
        /// </summary>
        /// <typeparam name="T">Data type of binding</typeparam>
        /// <param name="name">Name of property</param>
        /// <param name="value">Result of getting value</param>
        /// <returns>true if it does; false if not</returns>
        public bool TryGetBinding<T>(string name, out T value)
        {
            
            PropertyInfo info;
            value = default(T);
            bool result = false;
            if (bindings.TryGetValue(name, out info))
            {
                value = (T)info.GetValue(sourceObject);

                result = true;
            }

            
            return result;
        }

        public T TrySetter<T>(string name, T val)
        {
            if(HasBinding(name))
            {
                throw new BindedPropertyMutatorException("Access of mutator restricted on binded property: " + name);
            }

            else
            {
                return val;
            }
        }

        public bool DeactivateBinding(string key)
        {
            SwapBindings(deactivatedBindings, bindings, key);
            return bindings.ContainsKey(key);

        }

        private void SwapBindings(Dictionary<string, PropertyInfo> destination, Dictionary<string, PropertyInfo> source, string key)
        {
            source.MoveKeyValuePair<string, PropertyInfo>(destination, key);
        }

        public bool ReactivateBinding(string key)
        {
            SwapBindings(bindings, deactivatedBindings, key);
            return bindings.ContainsKey(key);
        }

        public void ClearDeactivatedBindings()
        {
            deactivatedBindings.Clear();
        }

        
    }

    public static class DictionaryExtensions
    {
        public static void MoveKeyValuePair<T, V>(this Dictionary<T, V> thisDict, Dictionary<T, V> targetDict, T key)
        {
            if (thisDict.ContainsKey(key))
            {
                targetDict.Add(key, thisDict[key]);
                thisDict.Remove(key);
            }
        }
    }
}
