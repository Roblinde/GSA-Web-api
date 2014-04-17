using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;

namespace GSA.Search.WebApi.Handlers
{
    /// <summary>
    /// Baseclass for WebApi usage requests and responses
    /// </summary>
    public abstract class WebApiUsage
    {
        /// <summary>
        /// The type that was used during the response or request
        /// </summary>
        public string UsageType { get; set; }

        /// <summary>
        /// The content of the response or request
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// The html headers of the response or request
        /// </summary>
        public Dictionary<string, string> Headers { get; set; }

        /// <summary>
        /// Helper method to turn <see cref="HttpHeaders"/> into a regular dictionary
        /// </summary>
        /// <param name="h">The headers to extract</param>
        protected void ExtractHeaders(HttpHeaders h)
        {
            var dict = new Dictionary<string, string>();
            foreach (var i in h.ToList())
            {
                if (i.Value != null)
                {
                    string header = i.Value.Aggregate(string.Empty, (current, j) => current + (j + " "));
                    dict.Add(i.Key, header);
                }
            }
            Headers = dict;
        }
    }

    /// <summary>
    /// Encapsulates a web api usage request
    /// </summary>
    public class WebApiUsageRequest : WebApiUsage
    {
        /// <summary>
        /// The URI that was requested
        /// </summary>
        public string Uri { get; set; }

        /// <summary>
        /// The method of the request
        /// </summary>
        public string RequestMethod { get; set; }

        /// <summary>
        /// The IP the request originated from
        /// </summary>
        public string IP { get; set; }

        /// <summary>
        /// Initializes a new instance of <see cref="WebApiUsageRequest"/>
        /// </summary>
        /// <param name="request"></param>
        public WebApiUsageRequest(HttpRequestMessage request)
        {
            if (request != null)
            {
                UsageType = request.GetType().Name;
                RequestMethod = request.Method.Method;
                Uri = request.RequestUri.ToString();
                IP = ((HttpContextBase)request.Properties["MS_HttpContext"]).Request.UserHostAddress;
                base.ExtractHeaders(request.Headers);
            }
            else
            {
                throw new ArgumentNullException("Request cannot be null");
            }
        }

        public override string ToString()
        {
            var headers = new StringBuilder();

            foreach (var header in Headers)
            {
                headers.AppendFormat("\t{0} : {1}\r\n", header.Key, header.Value);
            }

            return string.Format("WebApiRequest:\r\n {0}\r\n {1}\r\n {2} Originated from IP: {3}\r\n UsageType: {4}", Uri, RequestMethod, headers, IP, UsageType);
        }
    }

    /// <summary>
    /// Encapsulates a web api usage response
    /// </summary>
    public class WebApiUsageResponse : WebApiUsage
    {
        /// <summary>
        /// The <see cref="HttpStatusCode"/> returned from the server
        /// </summary>
        public int StatusCode { get; set; }

        /// <summary>
        /// Initializes a new instance of <see cref="WebApiUsageResponse"/>
        /// </summary>
        /// <param name="response"></param>
        public WebApiUsageResponse(HttpResponseMessage response)
        {
            if (response != null)
            {
                UsageType = response.GetType().Name;
                StatusCode = Convert.ToInt32(response.StatusCode);
                base.ExtractHeaders(response.Headers);
            }
            else
            {
                throw new ArgumentNullException("response cannot be null");
            }
        }

        public override string ToString()
        {
            var headers = new StringBuilder();

            foreach (var header in Headers)
            {
                headers.AppendFormat("\t{0} : {1}\r\n", header.Key, header.Value);
            }

            return string.Format("WebApiResponse:\r\n {0}\r\n {1} UsageType: {2}", StatusCode, headers, UsageType);
        }
    }
}