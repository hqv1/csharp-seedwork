using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Hqv.Seedwork.Utilities
{
    /// <summary>
    /// Extensions for objects
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        /// Convert the source's properties into a dictionary of name-value pairs
        /// </summary>
        /// <param name="source">An object</param>
        /// <param name="bindingAttr">Binding flags. It'll use these binding flags to grab the
        ///  properties to convert to a dictionary</param>
        /// <returns></returns>
        public static IDictionary<string, object> AsDictionary(this object source,
            BindingFlags bindingAttr = BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance)
        {
            return source.GetType().GetProperties(bindingAttr).ToDictionary
            (
                propInfo => propInfo.Name,
                propInfo => propInfo.GetValue(source, null)
            );
        }
    }
}