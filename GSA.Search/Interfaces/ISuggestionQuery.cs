namespace GSA.Search.Interfaces
{
    public interface ISuggestionQuery
    {
        /// <summary>
        /// The maximum number of results that the suggest server should return. The minimum is 0, which indicates that the server should return an empty set, 
        /// however this result would not be meaningful. Maximum is not set. Default is 10. If fewer suggestions are configured, then they are returned.
        /// </summary>
        int MaxSuggestions { get; set; }

        /// <summary>
        /// The host address to your GSA e.g. http://google04.domain.se/search please note that you should NOT end the url with a trailing slash.
        /// </summary>
        string GsaHostAddress { get; set; }

        /// <summary>
        /// Specify whether to search public, secure or all content. 
        /// </summary>
        SearchAccess Access { get; set; }

        /// <summary>
        /// A string that indicates a valid front end
        /// </summary>
        string Client { get; set; }

        /// <summary>
        /// The searchterm as entered by the user
        /// </summary>
        string SearchTerm { get; set; }

        /// <summary>
        /// The Collections in which to search for hits. You can use . (AND) or | (OR) to search in several collections.
        /// </summary>
        string Collections { get; set; }

        /// <summary>
        /// Constructs a valid suggestionquery to the google search appliance
        /// </summary>
        /// <returns>Returns the constructed query. This is a url that is directly useable to query the GSA.</returns>
        string ConstructQuery();
    }
}