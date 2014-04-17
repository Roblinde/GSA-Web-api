using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Xml.Linq;
using GSA.Search.Interfaces;
using Newtonsoft.Json.Linq;

namespace GSA.Search
{
    

    /// <summary>
    /// Responsible for creating the call to the GSA and handling the XML result.
    /// </summary>
    public class SearchServer : ISearchServer
    {

        /// <summary>
        /// Will call the GSA with the specified host and query. No other properties will be set on the call.
        /// </summary>
        /// <param name="query">The query as inputted by the user</param>
        /// <param name="host">The qualified host name of the GSA</param>
        /// <returns>The <see cref="ISearchResult"/></returns>
        public ISearchResult Search(string query, string host)
        {
            return Search(new Query { SearchTerm = query, GsaHostAddress = host });
        }

        private string CallUrl(string url, CookieContainer cookieContainer = null, CookieCollection cookies = null)
        {
            var req = WebRequest.Create(url) as HttpWebRequest;

            if (req == null)
                throw new ApplicationException("Could not create a web request with the specified url:" + url);


            req.AllowAutoRedirect = false;
            req.CookieContainer = new CookieContainer();
            
            if (cookieContainer != null)
            {
                req.CookieContainer = cookieContainer;
            }

            if (cookies != null)
            {
                req.CookieContainer.Add(cookies);
            }
            
            var response = req.GetResponse() as HttpWebResponse;

            if(response == null)
                return null;

            if (response.Headers["Location"] != null)
            {
                //remove /path from the cookies. ASP.Net adds it for some unknown reason.
                foreach (var cookie in response.Cookies)
                {
                    if (cookie is Cookie)
                    {
                        (cookie as Cookie).Path = string.Empty;
                    }
                }


                return CallUrl(response.Headers["Location"], req.CookieContainer, response.Cookies);
            }
            var result = "";
            using (var sr = new StreamReader(response.GetResponseStream()))
            {
                result = sr.ReadToEnd();
            }

            return result;
        }

        /// <summary>
        /// Will call the GSA using the specified Query object. All parameters of the query object will be used to construct the call.
        /// </summary>
        /// <param name="query">The <see cref="Query"/> object that encapsulates the query parameters </param>
        /// <param name="cookie">The optional authentication cookie to send with the request</param>
        /// <returns>The <see cref="ISearchResult"/></returns>
        public ISearchResult Search(IQuery query, HttpCookie cookie = null)
        {
            XDocument document;

            try
            {
                var url = query.ConstructQuery();

                if (query.Access == SearchAccess.Secure || query.Access == SearchAccess.All)
                {
                    //Secure search, we need to jump through some hoops


                    var cookieCollection = new CookieCollection();

                    //Do we have an authentication cookie to send with the request?
                    if (cookie != null)
                    {

                        var requestCookie = new Cookie(cookie.Name, cookie.Value, "/", query.GetDomain());
                        requestCookie.Expires = cookie.Expires;
                        requestCookie.HttpOnly = cookie.HttpOnly;
                        requestCookie.Secure = cookie.Secure;

                        cookieCollection.Add(requestCookie);
                    }

                    var xml = CallUrl(url, null, cookieCollection);


                    document = XDocument.Parse(xml);
                }
                else
                {
                    document = XDocument.Load(url);
                }
            }
            catch (Exception ex)
            {
                return new SearchResult { Error = "Server error occured while loading XML" };
            }

            if (document.Root == null)
            {
                return new SearchResult { Error = "The XML appears Malformed. No Root element found." };

            }

            var result = new SearchResult();

            var tm = document.Root.Element("TM");
            if (tm != null)
            {
                result.ExecutionTime = tm.Value;
            }
            var rq = document.Root.Element("Q");
            if (rq != null)
            {
                result.Query = rq.Value;
            }

            result.Version = document.Root.Attribute("VER").Value;

            result.Parameters = new List<ResultParameter>();

            foreach (var param in document.Root.Elements("PARAM"))
            {
                var resultParameter = new ResultParameter();
                resultParameter.Name = param.Attribute("name").Value;
                resultParameter.Value = param.Attribute("value").Value;
                resultParameter.OriginalValue = param.Attribute("original_value").Value;
                result.Parameters.Add(resultParameter);
            }

            result.SearchHits = new List<SearchHit>();

            var res = document.Root.Element("RES");
            if (res != null)
            {
                //The search gave results
                var matches = res.Element("M");
                if (matches != null)
                {
                    result.NumberOfHits = int.Parse(matches.Value);
                }

                var filter = res.Element("FI");
                if (filter != null)
                {
                    result.Filtering = filter.Value;
                }

                var nb = res.Element("NB");
                if (nb != null)
                {
                    var nexturl = nb.Element("NU");
                    if (nexturl != null)
                    {
                        result.NextUrl = nexturl.Value;
                    }

                    var prevurl = nb.Element("NP");
                    if (prevurl != null)
                    {
                        result.PreviousUrl = prevurl.Value;
                    }
                }

                foreach (var hit in res.Elements("R"))
                {
                    var searchHit = new SearchHit();
                    searchHit.Index = int.Parse(hit.Attribute("N").Value);

                    var url = hit.Element("U");
                    if (url != null)
                    {
                        searchHit.Url = url.Value;
                    }

                    var encodedUrl = hit.Element("UE");
                    if (encodedUrl != null)
                    {
                        searchHit.EncodedUrl = encodedUrl.Value;
                    }

                    var title = hit.Element("T");
                    if (title != null)
                    {
                        searchHit.Title = title.Value;
                    }

                    var rating = hit.Element("RK");
                    if (rating != null)
                    {
                        searchHit.Rating = int.Parse(rating.Value);
                    }

                    var snippet = hit.Element("S");
                    if (snippet != null)
                    {
                        searchHit.Snippet = snippet.Value;
                    }

                    var mimeType = hit.Attribute("MIME");
                    if (mimeType != null)
                    {
                        searchHit.MimeType = mimeType.Value;
                    }

                    var has = hit.Element("HAS");
                    if (has != null)
                    {
                        var c = has.Element("C");
                        if (c != null)
                        {
                            var sz = c.Attribute("SZ");
                            if (sz != null)
                            {
                                searchHit.Size = sz.Value;
                            }
                        }
                    }
                    searchHit.MetaTags = new Dictionary<string, string>();

                    foreach (var mt in hit.Elements("MT"))
                    {
                        var n = mt.Attribute("N");
                        if (n != null)
                        {
                            var v = mt.Attribute("V");
                            if (v != null)
                            {
                                searchHit.MetaTags.Add(n.Value, v.Value);
                            }
                        }
                    }

                    result.SearchHits.Add(searchHit);
                }

                result.Facets = new List<Facet>();

                var facets = res.Element("PARM");
                if (facets != null)
                {
                    //We have facets or "dynamic navigation" as it's called in google reference
                    foreach (var pmt in facets.Elements("PMT"))
                    {
                        var facet = new Facet();

                        var nm = pmt.Attribute("NM");
                        if (nm != null)
                        {
                            facet.MetaName = nm.Value;
                        }

                        var dn = pmt.Attribute("DN");
                        if (dn != null)
                        {
                            facet.DisplayName = dn.Value;
                        }

                        var ir = pmt.Attribute("IR");
                        if (ir != null)
                        {
                            facet.IsRange = int.Parse(ir.Value) > 0;
                        }

                        var t = pmt.Attribute("T");
                        if (t != null)
                        {
                            facet.FacetType = (FacetType)int.Parse(t.Value);
                        }

                        facet.FacetItems = new List<FacetItem>();

                        foreach (var pv in pmt.Elements("PV"))
                        {
                            var facetItem = new FacetItem();

                            var v = pv.Attribute("V");
                            if (v != null)
                            {
                                facetItem.Value = v.Value;
                            }

                            var l = pv.Attribute("L");
                            if (l != null)
                            {
                                facetItem.LowRange = l.Value;
                            }

                            var h = pv.Attribute("H");
                            if (h != null)
                            {
                                facetItem.HighRange = h.Value;
                            }

                            var c = pv.Attribute("C");
                            if (c != null)
                            {
                                facetItem.Count = int.Parse(c.Value);
                            }
                            facetItem.MetaName = facet.MetaName;

                            facet.FacetItems.Add(facetItem);
                        }

                        result.Facets.Add(facet);
                    }
                }

            }

            result.KeyMatches = new List<KeyMatch>();

            //We have keyMatches
            foreach (var gm in document.Root.Elements("GM"))
            {
                var keyMatch = new KeyMatch();

                var gl = gm.Element("GL");
                if (gl != null)
                {
                    keyMatch.Url = gl.Value;
                }

                var gd = gm.Element("GD");
                if (gd != null)
                {
                    keyMatch.Description = gd.Value;
                }
                result.KeyMatches.Add(keyMatch);
            }

            result.SpellingSuggestions = new List<string>();

            var spellings = document.Root.Element("Spelling");
            if (spellings != null)
            {
                //We have spelling suggestions
                foreach (var suggestion in spellings.Elements("Suggestion"))
                {
                    var q = suggestion.Attribute("q");
                    if (q != null)
                    {
                        result.SpellingSuggestions.Add(q.Value);
                    }
                }
            }

            result.Synonyms = new List<string>();

            var synonyms = document.Root.Element("Synonyms");
            if (synonyms != null)
            {
                //We have synonyms to the submitted query
                foreach (var synonym in synonyms.Elements("OneSynonym"))
                {
                    var q = synonym.Attribute("q");
                    if (q != null)
                    {
                        result.Synonyms.Add(q.Value);
                    }
                }
            }

            

            return result;
        }

        /// <summary>
        /// Provides search suggestion for an inputted term
        /// </summary>
        /// <param name="query">The partial query as inputted by the user</param>
        /// <returns>A list of search suggestions</returns>
        public List<string> Suggest(ISuggestionQuery query)
        {
            var url = query.ConstructQuery();
            var searchString = "";
            try
            {
                var request = WebRequest.Create(url);
                using (var response = request.GetResponse())
                {
                    var reader = new StreamReader(response.GetResponseStream());
                    searchString = reader.ReadToEnd();
                }
            }
            catch (Exception)
            {
                //TODO add logging here
                return new List<string>();
            }

            var suggestions = new List<string>();


            var json = JObject.Parse(searchString);


            
            var results = json.Last.First;

            foreach (var token in results)
            {
                    var suggestion = token["name"].ToString();
                    suggestions.Add(suggestion);
            }

            return suggestions;
        }

    }
}
