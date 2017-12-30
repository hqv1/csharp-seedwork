namespace Hqv.Seedwork.Web.Api
{
    /// <summary>
    /// PagedList helper interface
    /// </summary>
    public interface IPagedListHelper
    {
        /// <summary>
        /// Add pagination metadata to the response headers
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items">Collection of resources</param>
        /// <param name="resourceParameters">Resource parameters. Contains paging, searching, etc. information</param>
        /// <param name="filterParameters">Filter parameters</param>
        /// <param name="routeName">Name of the route</param>
        /// <returns></returns>
        string AddPaginationMetadataToResponseHeader<T>(PagedList<T> items,
            ResourceParameters resourceParameters,
            object filterParameters,
            string routeName);
    }
}