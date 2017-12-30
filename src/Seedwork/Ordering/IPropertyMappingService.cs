using System.Collections.Generic;

namespace Hqv.Seedwork.Ordering
{
    /// <summary>
    /// Interface for a service that maps properties from the source to the destination
    /// </summary>
    public interface IPropertyMappingService
    {
        /// <summary>
        /// Return the property mapping value for a destination type.
        /// </summary>
        IDictionary<string, PropertyMappingValue> GetPropertyMapping<TSource, TDestination>();

        /// <summary>
        /// Determine if there's a valid mapping for a collection of fields
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TDestination"></typeparam>
        /// <param name="fields">Comma-delimited collection of fields</param>
        /// <returns>Whether there's a valid mapping.</returns>
        bool ValidMappingExistsFor<TSource, TDestination>(string fields);
    }
}