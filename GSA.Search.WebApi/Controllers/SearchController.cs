using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using GSA.Search.Interfaces;
using GSA.Search.WebApi.Helpers;

namespace GSA.Search.WebApi.Controllers
{
    /// <summary>
    /// API controller that handles all search based calls
    /// </summary>
    public class SearchController : ApiController
    {
        /// <summary>
        /// A search request. All parameters are optional, although it's good practice to at least provide a query.
        /// </summary>
        /// <param name="q">The query as inputted by the user</param>
        /// <param name="s">The system name that performs the search. Supported values are: "Klara", "SuntLiv", "Klaratest", "SuntLivTest"</param>
        /// <param name="collections">The Collections in which to search for hits. You can use . (AND) or | (OR) to search in several collections.</param>
        /// <param name="client">A string that indicates a valid front end</param>
        /// <param name="resultsPerPage">The maximum number of hits to return. Default is 10. Maximum is 100.</param>
        /// <param name="requiredFields">The required metadata fields that the hits must contain in order to be returned. Should be separated by |. 
        /// The values specified will be required to exist as a metadata tag on page for it to be included in the result.</param>
        /// <param name="partialfields">Metadata fields that are partially required, 
        /// that means the metadatafield and value must contain the specified word or phrases. For example specifying "dep" for a partialfield would
        /// return hits that contains the metadatafield "department" and "depending". Should be separated by |</param>
        /// <returns>The searchresult</returns>
        public ISearchResult Get(string q, string s, string collections = "",
            string client = "", int resultsPerPage = 10, string requiredFields = "", string partialfields = "")
        {
            var server = new SearchServer();
            var query = new Query();
            query.SearchTerm = q;
            query.Collections = collections;
            query.Client = client;
            query.MaxSearchHits = 10;


            if (!string.IsNullOrEmpty(requiredFields))
            {
                if (requiredFields.Contains("|"))
                {
                    //The more complex scenario. The user has serveral required fields that needs to be handled
                    //TODO update this to handle not only AND but OR and NEGATE too
                    var tags = requiredFields.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

                    foreach (var tag in tags)
                    {
                        if (!string.IsNullOrEmpty(tag))
                        {
                            var field = new MetaDataField();
                            field.Key = tag;
                            field.MetaDataSearchSpecification = MetaDataSearchSpecification.And;
                            query.RequiredFields.Add(field);
                        }
                    }
                }
                else
                {
                    var field = new MetaDataField();
                    field.Key = requiredFields;
                    field.MetaDataSearchSpecification = MetaDataSearchSpecification.Ignore;
                    query.RequiredFields.Add(field);
                }
            }

            if (!string.IsNullOrEmpty(partialfields))
            {
                if (partialfields.Contains("|"))
                {
                    //The more complex scenario. The user has serveral partial fields that needs to be handled
                    //TODO update this to handle not only AND but OR and NEGATE too
                    var tags = partialfields.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

                    foreach (var tag in tags)
                    {
                        if (!string.IsNullOrEmpty(tag))
                        {
                            var field = new MetaDataField();
                            field.Key = tag;
                            field.MetaDataSearchSpecification = MetaDataSearchSpecification.And;
                            query.PartialFields.Add(field);
                        }
                    }
                }
                else
                {
                    var field = new MetaDataField();
                    field.Key = partialfields;
                    field.MetaDataSearchSpecification = MetaDataSearchSpecification.Ignore;
                    query.PartialFields.Add(field);
                }
            }

            query.GsaHostAddress = string.Format(this.GetHostFromSystemName(s), "/search");

            var result = server.Search(query);

            return result;
        }
    }
}