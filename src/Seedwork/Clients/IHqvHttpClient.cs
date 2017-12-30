using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hqv.Seedwork.Clients
{
    /// <summary>
    /// HQV Http Client. 
    /// </summary>
    public interface IHqvHttpClient
    {
        /// <summary>
        /// Calls async GET with bearer token and parses the response body
        /// </summary>
        /// <typeparam name="TResult">Type to Return</typeparam>
        /// <param name="baseUri">Base Uri</param>
        /// <param name="relativeUri">Relative Uri</param>
        /// <param name="bearerToken">Bearer token</param>
        /// <param name="parser">Function that parses the response body to TResult</param>
        /// <param name="queryParameters">Query parameters</param>
        /// <returns></returns>
        Task<TResult> GetAsyncWithBearerToken<TResult>(
            string baseUri, string relativeUri,
            string bearerToken,
            Func<object, Task<TResult>> parser,
            ICollection<KeyValuePair<string, string>> queryParameters = null);

        /// <summary>
        /// Calls async POST with bearer token and json body. It'll parse the response body and returns a Result
        /// </summary>
        /// <typeparam name="TResult">Type to Return</typeparam>
        /// <param name="baseUri">Base Uri</param>
        /// <param name="relativeUri">Relative Uri</param>
        /// <param name="bearerToken">Bearer token</param>
        /// <param name="body">JSON body</param>
        /// <param name="parser">Function that parses the response body to TResult</param>
        /// <param name="queryParameters">Query parameters</param>
        /// <returns></returns>
        Task<TResult> PostAsyncJsonWithBearerToken<TResult>(
            string baseUri,
            string relativeUri,
            string bearerToken,
            string body,
            Func<object, Task<TResult>> parser,
            ICollection<KeyValuePair<string, string>> queryParameters = null);
    }
}
