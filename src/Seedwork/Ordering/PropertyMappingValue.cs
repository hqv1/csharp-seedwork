using System.Collections.Generic;

namespace Hqv.Seedwork.Ordering
{
    public class PropertyMappingValue
    {
        public PropertyMappingValue(IEnumerable<string> destinationProperties, bool revert = false)
        {
            DestinationProperties = destinationProperties;
            Revert = revert;
        }

        public IEnumerable<string> DestinationProperties { get; set; }
        public bool Revert { get; private set; }
    }
}