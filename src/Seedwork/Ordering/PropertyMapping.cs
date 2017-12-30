using System.Collections.Generic;

namespace Hqv.Seedwork.Ordering
{
    /// <inheritdoc />
    /// <summary>
    /// Describes how to map source properties to destination properties
    /// For example, a string Name can be mapped to a property FirstName descending and a property LastName
    /// </summary>
    public class PropertyMapping : IPropertyMapping
    {
        public Dictionary<string, PropertyMappingValue> MappingDictionary { get; private set; }

        public PropertyMapping(Dictionary<string, PropertyMappingValue> mappingDictionary)
        {
            MappingDictionary = mappingDictionary;
        }
    }
}