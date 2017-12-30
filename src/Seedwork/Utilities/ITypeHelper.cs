namespace Hqv.Seedwork.Utilities
{
    /// <summary>
    /// Set of methods that works on a type
    /// </summary>
    public interface ITypeHelper
    {
        /// <summary>
        /// Does type T have properties in fields? 
        /// </summary>
        /// <typeparam name="T">A type</typeparam>
        /// <param name="fields">Comma-delimited set of property names</param>
        /// <returns>Whether T has all the properties in fields</returns>
        bool TypeHasProperties<T>(string fields);
    }
}