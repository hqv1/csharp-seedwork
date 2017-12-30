using System.Linq;
using System.Reflection;

namespace Hqv.Seedwork.Utilities
{
    public class TypeHelper : ITypeHelper
    {
        /// <summary>
        /// Does type T have properties in fields? 
        /// </summary>
        /// <typeparam name="T">A type</typeparam>
        /// <param name="fields">Comma-delimited set of property names</param>
        /// <returns>Whether T has all the properties in fields</returns>
        public bool TypeHasProperties<T>(string fields)
        {
            if (string.IsNullOrWhiteSpace(fields))
            {
                return true;
            }

            // the field are separated by ",", so we split it.
            var fieldsAfterSplit = fields.Split(',');

            // check if the requested fields exist on source. If all exist, return true
            return fieldsAfterSplit.Select(field => field.Trim())
                .Select(propertyName => typeof(T).GetProperty(propertyName,
                    BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance))
                .All(propertyInfo => propertyInfo != null);
        }
    }
}