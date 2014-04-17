using System.Collections.Generic;

namespace GSA.Search
{
    /// <summary>
    /// 
    /// </summary>
    public interface IQuery
    {
        /// <summary>
        /// The host address to your GSA e.g. http://google04.domain.se/search please note that you should NOT end the url with a trailing slash.
        /// </summary>
        string GsaHostAddress { get; set; }

        /// <summary>
        /// Specify whether to search public, secure or all content. 
        /// </summary>
        SearchAccess Access { get; set; }

        /// <summary>
        /// Modifies the SiteToSearch (if specified) as include or exclude hits from the specified site.
        /// </summary>
        SiteSearchModification SiteSearchModification { get; set; }

        /// <summary>
        /// Adds the specified phrase to the search as entered by the user as if he had entered "uservalue AdditionalQueryTerms"
        /// </summary>
        string AdditionalQueryPhrase { get; set; }

        /// <summary>
        /// Excludes the specified terms from the search results.
        /// </summary>
        string ExcludedQueryTerms { get; set; }

        /// <summary>
        /// A URL for which to show all results that link to that URL. No other query terms can be used when using this parameter.
        /// </summary>
        string LinkedUrl { get; set; }

        /// <summary>
        /// Specify where the search engine should look for query terms on the page. In the title, the url or anywhere.
        /// </summary>
        SearchLocation SearchLocation { get; set; }

        /// <summary>
        /// Combine the specified term with the search term entered by the user. Effectively the same as if the user had used OR in his search query.
        /// </summary>
        string CombinedSearchTerm { get; set; }

        /// <summary>
        /// Addes the specified term to the search term entered by the user. Effectively the same as if he had also entered the AddedSearchTerm in his query.
        /// </summary>
        string AddedSearchTerm { get; set; }

        /// <summary>
        /// Specify whether only a specified site should be searched for content. Effectively the same as if the user had used "site:" in his query.
        /// The value of this property will be appended to the users query.
        /// </summary>
        string AsSiteToSearch { get; set; }

        /// <summary>
        /// A string that indicates a valid front end
        /// </summary>
        string Client { get; set; }

        /// <summary>
        /// Specify the query expansion policy. If set to Ignore the setting of the selected front end will be used.
        /// </summary>
        QueryExpansion QueryExpansion { get; set; }

        /// <summary>
        /// Specify whether advanced scoring should be used.  If set to Ignore the setting of the selected front end will be used.
        /// </summary>
        RelevanceScoring RelevanceScoring { get; set; }

        /// <summary>
        /// Activates or deactivates google automatic resultsfiltering. See <a href="https://developers.google.com/search-appliance/documentation/50/xml_reference#request_filter_auto">https://developers.google.com/search-appliance/documentation/50/xml_reference#request_filter_auto</a> for more information.
        /// </summary>
        QueryFilter Filter { get; set; }

        /// <summary>
        /// Specify if you wish certain metafields to be returned with the searchhit. Useful for faceted search for example. 
        /// Be careful to urlencode these correctly, they need to be double url encoded.
        /// </summary>
        string GetMetaFields { get; set; }

        /// <summary>
        /// Specify the character encoding that is used to interpret the query string.
        /// </summary>
        string CharacterEncodingQuery { get; set; }

        /// <summary>
        /// Restrict searches to to pages in the specified language. 
        /// See <a href="https://developers.google.com/search-appliance/documentation/50/xml_reference#request_subcollections">https://developers.google.com/search-appliance/documentation/50/xml_reference#request_subcollections</a> 
        /// for more information on how to specify language.
        /// </summary>
        string RestrictToLanguage { get; set; }

        /// <summary>
        /// The maximum number of hits to return. Default is 10. Maximum is 100. 0 will be treated as default.
        /// </summary>
        int MaxSearchHits { get; set; }

        /// <summary>
        /// The maximum number of KeyMatch results to return. Default is 3. Maximum is 5.
        /// </summary>
        int MaxKeyMatchResults { get; set; }

        /// <summary>
        /// Specify the character encoding that is used to encode the search result.
        /// </summary>
        string CharacterEncodingResult { get; set; }

        /// <summary>
        /// Metadata fields that are partially required, that means the metadatafield and value must contain the specified word or phrases
        /// </summary>
        List<MetaDataField> PartialFields { get; set; }

        /// <summary>
        /// The searchterm as entered by the user
        /// </summary>
        string SearchTerm { get; set; }

        /// <summary>
        /// The required metadata fields that the hits must contain in order to be returned
        /// </summary>
        List<MetaDataField> RequiredFields { get; set; }

        /// <summary>
        /// The Collections in which to search for hits. You can use . (AND) or | (OR) to search in several collections.
        /// </summary>
        string Collections { get; set; }

        /// <summary>
        /// Specify whether only a specified site should be searched for content. Effectively the same as if the user had used "site:" in his query.
        /// This property works slightly differently from the <see cref="AsSiteToSearch"/> property. SiteToSearch disregards the <see cref="SiteSearchModification"/>
        /// property. This propertys value is not appended to the search query in the results.
        /// </summary>
        string SiteToSearch { get; set; }

        /// <summary>
        /// Specify how to sort the searchresults. For more information on how to format the Sort parameter see <a href="https://developers.google.com/search-appliance/documentation/50/xml_reference#request_sort">https://developers.google.com/search-appliance/documentation/50/xml_reference#request_sort</a>
        /// </summary>
        string Sort { get; set; }

        /// <summary>
        /// Specify which index to start showing results from
        /// </summary>
        int Start { get; set; }

        /// <summary>
        /// Constructs a valid query to the google search appliance
        /// </summary>
        /// <returns>Returns the constructed query. This is a url that is directly useable to query the GSA.</returns>
        string ConstructQuery();

        /// <summary>
        /// Gets the domain of the current query. 
        /// </summary>
        /// <returns>The domain as a string</returns>
        string GetDomain();
    }
}