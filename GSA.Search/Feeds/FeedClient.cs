using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using GSA.Search.Interfaces;

namespace GSA.Search.Feeds
{
    /// <summary>
    /// Simple wrapper for the WebClient class for unit testing purposes
    /// </summary>
    internal class FeedClient: IFeedClient
    {
        public HttpStatusCode PushFeed(string xml, string gsaHost, bool async)
        {
            using (var client = new WebClient())
            {
                var values = new NameValueCollection();
                values["feedtype"] = "incremental";
                values["datasource"] = "web";
                values["data"] = xml;

                try
                {
                    if (async)
                    {
                        client.UploadValuesAsync(new Uri(string.Format("{0}:19900/xmlfeed", gsaHost)), values);
                    }
                    else
                    {
                        client.UploadValues(new Uri(string.Format("{0}:19900/xmlfeed", gsaHost)), values);
                    }
                }
                catch (WebException wex)
                {
                    if (wex.Status == WebExceptionStatus.ProtocolError)
                    {
                        var response = wex.Response as HttpWebResponse;
                        if (response != null)
                        {
                            return response.StatusCode;
                        }
                    }
                    return HttpStatusCode.InternalServerError;
                }
                catch (Exception ex)
                {
                    return HttpStatusCode.InternalServerError;
                }

                return HttpStatusCode.OK;
            }
        }
    }
}
