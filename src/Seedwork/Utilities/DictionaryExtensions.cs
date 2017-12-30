using System;
using System.Collections.Generic;

namespace Hqv.Seedwork.Utilities
{
    /// <summary>
    /// Extensions for Dictionary
    /// </summary>
    public static class DictionaryExtensions
    {
        /// <summary>
        /// Convert a dictionary to an object. It'll set the value of the properties to the
        /// property name in the object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static T ToObject<T>(this IDictionary<string, object> source)
            where T : class, new()
        {
            var someObject = new T();
            var someObjectType = someObject.GetType();

            foreach (var item in source)
            {
                var prop = someObjectType.GetProperty(item.Key);
                if (prop == null)
                {
                    throw new Exception($"Type does not contain dictionary key {item.Key}");
                }

                try
                {
                    prop.SetValue(someObject, item.Value, null);
                }
                catch (Exception ex)
                {
                    ex.Data["DictionaryKey"] = item.Key;
                    throw;
                }
            }
            return someObject;
        }
    }
}