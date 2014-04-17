using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;

namespace GSA.Search
{
    /// <summary>
    /// Encapsulates a query to the google search appliance.
    /// </summary>
    public class Query : BaseQuery, IQuery
    {
        /// <summary>
        /// Initializes a new instance of the GSA.Search.Query class
        /// </summary>
        public Query()
        {
            RequiredFields = new List<MetaDataField>();
            PartialFields = new List<MetaDataField>();
        }

        /// <summary>
        /// Modifies the SiteToSearch (if specified) as include or exclude hits from the specified site.
        /// </summary>
        public SiteSearchModification SiteSearchModification { get; set; }
        //ad_dt

        /// <summary>
        /// Adds the specified phrase to the search as entered by the user as if he had entered "uservalue AdditionalQueryTerms"
        /// </summary>
        public string AdditionalQueryPhrase { get; set; }
        //as_epq

        /// <summary>
        /// Excludes the specified terms from the search results.
        /// </summary>
        public string ExcludedQueryTerms { get; set; }
        //as_eq

        /// <summary>
        /// A URL for which to show all results that link to that URL. No other query terms can be used when using this parameter.
        /// </summary>
        public string LinkedUrl { get; set; }
        //as_lq

        /// <summary>
        /// Specify where the search engine should look for query terms on the page. In the title, the url or anywhere.
        /// </summary>
        public SearchLocation SearchLocation { get; set; }
        //as_occt

        /// <summary>
        /// Combine the specified term with the search term entered by the user. Effectively the same as if the user had used OR in his search query.
        /// </summary>
        public string CombinedSearchTerm { get; set; }
        //as_oq

        /// <summary>
        /// Addes the specified term to the search term entered by the user. Effectively the same as if he had also entered the AddedSearchTerm in his query.
        /// </summary>
        public string AddedSearchTerm { get; set; }
        //as_q

        /// <summary>
        /// Specify whether only a specified site should be searched for content. Effectively the same as if the user had used "site:" in his query.
        /// The value of this property will be appended to the users query.
        /// </summary>
        public string AsSiteToSearch { get; set; }
        //as_sitesearch

        /// <summary>
        /// Specify the query expansion policy. If set to Ignore the setting of the selected front end will be used.
        /// </summary>
        public QueryExpansion QueryExpansion { get; set; }
        //entqr

        /// <summary>
        /// Specify whether advanced scoring should be used.  If set to Ignore the setting of the selected front end will be used.
        /// </summary>
        public RelevanceScoring RelevanceScoring { get; set; }
        //entsp

        /// <summary>
        /// Activates or deactivates google automatic resultsfiltering. See <a href="https://developers.google.com/search-appliance/documentation/50/xml_reference#request_filter_auto">https://developers.google.com/search-appliance/documentation/50/xml_reference#request_filter_auto</a> for more information.
        /// </summary>
        public QueryFilter Filter { get; set; }
        //filter

        /// <summary>
        /// Specify if you wish certain metafields to be returned with the searchhit. Useful for faceted search for example. 
        /// Be careful to urlencode these correctly, they need to be double url encoded.
        /// </summary>
        public string GetMetaFields { get; set; }
        //getfields

        /// <summary>
        /// Specify the character encoding that is used to interpret the query string.
        /// </summary>
        public string CharacterEncodingQuery { get; set; }
        //ie

        /// <summary>
        /// Restrict searches to to pages in the specified language. 
        /// See <a href="https://developers.google.com/search-appliance/documentation/50/xml_reference#request_subcollections">https://developers.google.com/search-appliance/documentation/50/xml_reference#request_subcollections</a> 
        /// for more information on how to specify language.
        /// </summary>
        public string RestrictToLanguage { get; set; }
        //lr

        /// <summary>
        /// The maximum number of hits to return. Default is 10. Maximum is 100. 0 will be treated as default.
        /// </summary>
        public int MaxSearchHits { get; set; }
        //num

        /// <summary>
        /// The maximum number of KeyMatch results to return. Default is 3. Maximum is 5.
        /// </summary>
        public int MaxKeyMatchResults { get; set; }
        //numgm

        /// <summary>
        /// Specify the character encoding that is used to encode the search result.
        /// </summary>
        public string CharacterEncodingResult { get; set; }
        //oe

        //Only support xml_no_dtd at the moment.

        /// <summary>
        /// Metadata fields that are partially required, that means the metadatafield and value must contain the specified word or phrases
        /// </summary>
        public List<MetaDataField> PartialFields { get; set; }


        //Do not support proxycustom at the moment.

        //Do not support proxyreload at the moment.

        //Do not support proxystylesheet at the moment.

        


        /// <summary>
        /// The required metadata fields that the hits must contain in order to be returned
        /// </summary>
        public List<MetaDataField> RequiredFields { get; set; }

        

        /// <summary>
        /// Specify whether only a specified site should be searched for content. Effectively the same as if the user had used "site:" in his query.
        /// This property works slightly differently from the <see cref="AsSiteToSearch"/> property. SiteToSearch disregards the <see cref="SiteSearchModification"/>
        /// property. This propertys value is not appended to the search query in the results.
        /// </summary>
        public string SiteToSearch { get; set; }
        //sitesearch

        /// <summary>
        /// Specify how to sort the searchresults. For more information on how to format the Sort parameter see <a href="https://developers.google.com/search-appliance/documentation/50/xml_reference#request_sort">https://developers.google.com/search-appliance/documentation/50/xml_reference#request_sort</a>
        /// </summary>
        public string Sort { get; set; }
        //sort

        //do not support ud at the moment
        
      

        /// <summary>
        /// Specify which index to start showing results from
        /// </summary>
        public int Start { get; set; }

        /// <summary>
        /// Constructs a valid query to the google search appliance
        /// </summary>
        /// <returns>Returns the constructed query. This is a url that is directly useable to query the GSA.</returns>
        public override string ConstructQuery()
        {

           
            var sb = new StringBuilder();
           
            //get the root values from the base implementation
            sb.Append(base.ConstructQuery());

            //append every other possible query-parameter.
            

            switch (SiteSearchModification)
            {
                case SiteSearchModification.Include:
                    QueryHelper.AppendQueryString(sb, "ad_dt", "i");
                    break;
                case SiteSearchModification.Exclude:
                    QueryHelper.AppendQueryString(sb, "ad_dt", "e");
                    break;
                case SiteSearchModification.Ignore:
                    break;
                default:
                    throw new InvalidEnumArgumentException("You have not specified a valid enum value for SiteSearchModification");
            }

            if (!string.IsNullOrEmpty(AdditionalQueryPhrase))
            {
                QueryHelper.AppendQueryString(sb, "as_epq", AdditionalQueryPhrase);
            }

            if (!string.IsNullOrEmpty(ExcludedQueryTerms))
            {
                QueryHelper.AppendQueryString(sb, "as_eq", ExcludedQueryTerms);
            }

            if (!string.IsNullOrEmpty(LinkedUrl))
            {
                QueryHelper.AppendQueryString(sb, "as_lq", LinkedUrl);
            }

            switch (SearchLocation)
            {
                case SearchLocation.Title:
                    QueryHelper.AppendQueryString(sb, "as_occt", "title");
                    break;
                case SearchLocation.Url:
                    QueryHelper.AppendQueryString(sb, "as_occt", "url");
                    break;
                case SearchLocation.Anywhere:
                    QueryHelper.AppendQueryString(sb, "as_occt", "any");
                    break;
                case SearchLocation.Ignore:
                    break;
                default:
                    throw new InvalidEnumArgumentException("You have not specified a valid enum value for SearchLocation");
            }

            if (!string.IsNullOrEmpty(CombinedSearchTerm))
            {
                QueryHelper.AppendQueryString(sb, "as_oq", CombinedSearchTerm);
            }

            if (!string.IsNullOrEmpty(AddedSearchTerm))
            {
                QueryHelper.AppendQueryString(sb, "as_q", AddedSearchTerm);
            }

            if (!string.IsNullOrEmpty(AsSiteToSearch))
            {
                QueryHelper.AppendQueryString(sb, "as_sitesearch", AsSiteToSearch);
            }

            switch (QueryExpansion)
            {
                case QueryExpansion.None:
                    QueryHelper.AppendQueryString(sb, "entqr", "0");
                    break;
                case QueryExpansion.Standard:
                    QueryHelper.AppendQueryString(sb, "entqr", "1");
                    break;
                case QueryExpansion.Local:
                    QueryHelper.AppendQueryString(sb, "entqr", "2");
                    break;
                case QueryExpansion.Full:
                    QueryHelper.AppendQueryString(sb, "entqr", "3");
                    break;
                case QueryExpansion.Ignore:
                    break;
                default:
                    throw new InvalidEnumArgumentException("You have not specified a valid enum value for QueryExpansion");
            }
            
            switch (RelevanceScoring)
            {
                case RelevanceScoring.Standard:
                    QueryHelper.AppendQueryString(sb, "entsp", "0");
                    break;
                case RelevanceScoring.Advanced:
                    QueryHelper.AppendQueryString(sb, "entsp", "a");
                    break;
                case RelevanceScoring.Ignore:
                    break;
                default:
                    throw new InvalidEnumArgumentException("You have not specified a valid enum value for RelevanceScoring");
            }

            switch (Filter)
            {
                case QueryFilter.SnippetAndDirectory:
                    QueryHelper.AppendQueryString(sb, "filter", "1");
                    break;
                case QueryFilter.None:
                    QueryHelper.AppendQueryString(sb, "filter", "0");
                    break;
                case QueryFilter.DirectoryOnly:
                    QueryHelper.AppendQueryString(sb, "filter", "s");
                    break;
                case QueryFilter.SnippetOnly:
                    QueryHelper.AppendQueryString(sb, "filter", "p");
                    break;
                case QueryFilter.Ignore:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            if (!string.IsNullOrEmpty(GetMetaFields))
            {
                QueryHelper.AppendQueryString(sb, "getfields", GetMetaFields);
            }

            if (!string.IsNullOrEmpty(CharacterEncodingQuery))
            {
                QueryHelper.AppendQueryString(sb, "ie", CharacterEncodingQuery);
            }
            else
            {
                //Default to utf-8
                QueryHelper.AppendQueryString(sb, "ie", "utf8");

            }

            if (!string.IsNullOrEmpty(RestrictToLanguage))
            {
                QueryHelper.AppendQueryString(sb, "lr", RestrictToLanguage);
            }

            if (MaxSearchHits > 0)
            {
                if (MaxSearchHits > 100)
                    MaxSearchHits = 100;
                QueryHelper.AppendQueryString(sb, "num", MaxSearchHits.ToString(CultureInfo.InvariantCulture));
            }

            if (MaxKeyMatchResults > 0)
            {
                if (MaxKeyMatchResults > 5)
                    MaxKeyMatchResults = 5;
                QueryHelper.AppendQueryString(sb, "numgm", MaxKeyMatchResults.ToString(CultureInfo.InvariantCulture));
            }

            if (!string.IsNullOrEmpty(CharacterEncodingResult))
            {
                QueryHelper.AppendQueryString(sb, "oe", CharacterEncodingResult);
            }
            else
            {
                //default to utf8
                QueryHelper.AppendQueryString(sb, "oe", "utf8");
            }

            QueryHelper.AppendQueryString(sb, "output", "xml_no_dtd");

            if (PartialFields != null && PartialFields.Count > 0)
            {
                QueryHelper.ParseMetaDataFields(sb, "partialfields", PartialFields);
            }

            if (RequiredFields != null && RequiredFields.Count > 0)
            {
                QueryHelper.ParseMetaDataFields(sb, "requiredfields", RequiredFields);
            }

            if (!string.IsNullOrEmpty(SiteToSearch))
            {
                QueryHelper.AppendQueryString(sb, "sitesearch", SiteToSearch);
            }

            if (!string.IsNullOrEmpty(Sort))
            {
                QueryHelper.AppendQueryString(sb, "sort", Sort);
            }

            if (Start > 0)
            {
               QueryHelper.AppendQueryString(sb, "start", Start.ToString(CultureInfo.InvariantCulture));
            }

            return sb.ToString();
        }

        /// <summary>
        /// Gets the domain of the current query. 
        /// </summary>
        /// <returns>The domain as a string</returns>
        public string GetDomain()
        {
            var uri = new Uri(GsaHostAddress);

            return uri.DnsSafeHost;
        }
    }
}
