using System.Collections.Generic;
using GSA.Search.Interfaces;

namespace GSA.Search
{
    /// <summary>
    /// Encapsulates a search result returned from the GSA
    /// </summary>
    public class SearchResult : ISearchResult
    {
        /// <summary>
        /// Initializes a new instance of <see cref="SearchResult"/>
        /// </summary>
        public SearchResult()
        {
            Parameters = new List<ResultParameter>();
            SearchHits = new List<SearchHit>();
            KeyMatches = new List<KeyMatch>();
            Synonyms = new List<string>();
            SpellingSuggestions = new List<string>();
            Facets = new List<Facet>();
        }

        /// <summary>
        /// A list of all parameters (<see cref="ResultParameter"/>) that were used in the call to GSA to receive the current result
        /// </summary>
        public List<ResultParameter> Parameters { get; set; }

        /// <summary>
        /// The execution time of the current call to GSA
        /// </summary>
        public string ExecutionTime { get; set; }

        /// <summary>
        /// The query that resulted in the current result
        /// </summary>
        public string Query { get; set; }

        /// <summary>
        /// The xml version parsed for this result
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// Estimated total number of hits for the current query
        /// </summary>
        public int NumberOfHits { get; set; }

        /// <summary>
        /// The filtering used (if any) for the current result
        /// </summary>
        public string Filtering { get; set; }

        /// <summary>
        /// The url to the next page of the search result
        /// </summary>
        public string NextUrl { get; set; }

        /// <summary>
        /// The url to the previous page of the search result
        /// </summary>
        public string PreviousUrl { get; set; }

        /// <summary>
        /// List of <see cref="SearchHit"/> presented in this page of the result.
        /// </summary>
        public List<SearchHit> SearchHits { get; set; }

        /// <summary>
        /// A list of <see cref="KeyMatch"/> for this query.
        /// </summary>
        public List<KeyMatch> KeyMatches { get; set; } 

        /// <summary>
        /// Encapsulates all errors during the call to GSA
        /// </summary>
        public string Error { get; set; }

        /// <summary>
        /// A list of spellingsuggestions for the query as inputted by the user
        /// </summary>
        public List<string> SpellingSuggestions { get; set; }

        /// <summary>
        /// A list of synonyms for the query as inputted by the user
        /// </summary>
        public List<string> Synonyms { get; set; }

        /// <summary>
        /// A list of <see cref="Facet"/> available for the current query
        /// </summary>
        public List<Facet> Facets { get; set; }

    }
}
