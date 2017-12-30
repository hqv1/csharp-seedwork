using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http.Extensions;

namespace Hqv.Seedwork.Web.Client
{
    /// <summary>
    /// URI Helper class
    /// </summary>
    public static class UriHelper
    {
        /// <summary>
        /// Create a URI 
        /// </summary>
        /// <param name="baseUri">Base URI</param>
        /// <param name="relativeUri">Relative URI</param>
        /// <param name="queryParameters">Collection of query parameters</param>
        /// <returns></returns>
        public static string Create(string baseUri, string relativeUri, ICollection<KeyValuePair<string, string>> queryParameters)
        {
            var queryBuilder = new QueryBuilder(queryParameters);
            return Create(baseUri, relativeUri, queryBuilder.ToQueryString().Value);
        }

        /// <summary>
        /// Create a URI 
        /// </summary>
        /// <param name="baseUri">Base URI</param>
        /// <param name="relativeUri">Relative URI</param>
        /// <param name="queryString">Query string. It needs to be properly formatted</param>
        /// <returns></returns>
        public static string Create(string baseUri, string relativeUri, string queryString = null)
        {
            var uriBuilder = new UriBuilder(baseUri);
            uriBuilder.Path += relativeUri;
            uriBuilder.Query = queryString;
            return uriBuilder.ToString();
        }
    }
}