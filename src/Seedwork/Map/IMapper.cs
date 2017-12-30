namespace Hqv.Seedwork.Map
{
    /// <summary>
    /// Mapper interface. Mapper a source object to a destination object. Should make a deep copy.
    /// </summary>
    public interface IMapper
    {
        /// <summary>
        /// Map the source to a destination. Creates a new destination object.
        /// </summary>
        TDestination Map<TDestination>(object source);

        /// <summary>
        /// Map the source to the destination. Uses the destination object passed in.
        /// </summary>
        void Map<TSource, TDestination>(TSource source, TDestination destination);

        /// <summary>
        /// Assert that the source to destination mapping is valid.
        /// </summary>
        void AssertConfigurationIsValid();
    }
}
