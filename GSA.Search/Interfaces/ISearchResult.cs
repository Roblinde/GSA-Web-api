using System.Collections.Generic;

namespace GSA.Search.Interfaces
{
    public interface ISearchResult
    {
        /// <summary>
        /// A list of all parameters (<see cref="ResultParameter"/>) that were used in the call to GSA to receive the current result
        /// </summary>
        List<ResultParameter> Parameters { get; set; }

        /// <summary>
        /// The execution time of the current call to GSA
        /// </summary>
        string ExecutionTime { get; set; }

        /// <summary>
        /// The query that resulted in the current result
        /// </summary>
        string Query { get; set; }

        /// <summary>
        /// The xml version parsed for this result
        /// </summary>
        string Version { get; set; }

        /// <summary>
        /// Estimated total number of hits for the current query
        /// </summary>
        int NumberOfHits { get; set; }

        /// <summary>
        /// The filtering used (if any) for the current result
        /// </summary>
        string Filtering { get; set; }

        /// <summary>
        /// The url to the next page of the search result
        /// </summary>
        string NextUrl { get; set; }

        /// <summary>
        /// The url to the previous page of the search result
        /// </summary>
        string PreviousUrl { get; set; }

        /// <summary>
        /// List of <see cref="SearchHit"/> presented in this page of the result.
        /// </summary>
        List<SearchHit> SearchHits { get; set; }

        /// <summary>
        /// A list of <see cref="KeyMatch"/> for this query.
        /// </summary>
        List<KeyMatch> KeyMatches { get; set; }

        /// <summary>
        /// Encapsulates all errors during the call to GSA
        /// </summary>
        string Error { get; set; }

        /// <summary>
        /// A list of spellingsuggestions for the query as inputted by the user
        /// </summary>
        List<string> SpellingSuggestions { get; set; }

        /// <summary>
        /// A list of synonyms for the query as inputted by the user
        /// </summary>
        List<string> Synonyms { get; set; }

        /// <summary>
        /// A list of <see cref="Facet"/> available for the current query
        /// </summary>
        List<Facet> Facets { get; set; }
    }
}