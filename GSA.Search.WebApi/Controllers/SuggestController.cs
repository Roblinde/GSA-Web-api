using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Mvc;
using GSA.Search.WebApi.Helpers;

namespace GSA.Search.WebApi.Controllers
{
    /// <summary>
    /// API controller that handles all suggest base calls
    /// </summary>
    public class SuggestController : ApiController
    {
        /// <summary>
        ///  Provides suggestions that complete a user's search query.
        /// </summary>
        /// <param name="q">The partial query as inputted by the user</param>
        /// <param name="s">The system name that performs the search. Supported values are: "Klara", "SuntLiv", "Klaratest", "SuntLivTest"</param>
        /// <param name="collections">The Collections in which to search for hits. You can use . (AND) or | (OR) to search in several collections.</param>
        /// <param name="client">A string that indicates a valid front end</param>
        /// <param name="maxSuggestions">The maximum number of results that the suggest server should return. The minimum is 0, which indicates that the server should return an empty set, 
        /// however this result would not be meaningful. Maximum is not set. Default is 10. If fewer suggestions are configured, then they are returned.</param>
        /// <returns>A list of suggestions</returns>
        public List<string> Get(string q, string s, string collections = "", string client = "", int maxSuggestions = 10)
        {
            var server = new SearchServer();
            var query = new SuggestionQuery();
            query.SearchTerm = q;
            query.Collections = collections;
            query.Client = client;
            query.MaxSuggestions = maxSuggestions;

            query.GsaHostAddress = string.Format(this.GetHostFromSystemName(s), "/suggest");

            var result = server.Suggest(query);

            return result;
        }

    }
}
