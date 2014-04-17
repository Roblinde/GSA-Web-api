using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using GSA.Search;
using GSA.Search.Interfaces;

namespace GSA.Search.Interfaces
{
    public interface ISearchServer
    {
        /// <summary>
        /// Will call the GSA with the specified host and query. No other properties will be set on the call.
        /// </summary>
        /// <param name="query">The query as inputted by the user</param>
        /// <param name="host">The qualified host name of the GSA</param>
        /// <returns>The <see cref="SearchResult"/></returns>
        ISearchResult Search(string query, string host);

        /// <summary>
        /// Will call the GSA using the specified Query object. All parameters of the query object will be used to construct the call.
        /// </summary>
        /// <param name="query">The <see cref="Query"/> object that encapsulates the query parameters </param>
        /// <param name="cookie">The optional authentication cookie to send with the request</param>
        /// <returns>The <see cref="SearchResult"/></returns>
        ISearchResult Search(IQuery query, HttpCookie cookie = null);

        /// <summary>
        /// Provides search suggestion for an inputted term
        /// </summary>
        /// <param name="query">The partial query as inputted by the user</param>
        /// <returns>A list of search suggestions</returns>
        List<string> Suggest(ISuggestionQuery query);
    }
}
