using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace GSA.Search.Interfaces
{
    /// <summary>
    /// A feed client used by the FeedManager to push a result to the Google Search Appliance
    /// </summary>
    public interface IFeedClient
    {
        /// <summary>
        /// Pushes a specified feed to the Google Search Appliance
        /// </summary>
        /// <param name="xml">The xml feed to push</param>
        /// <param name="gsaHost">The google search appliance host to push to</param>
        /// <param name="async">Should the request to GSA be sent asynchronously</param>
        /// <returns>a <see cref="HttpStatusCode"/> that indicates whether the request succeded or failed</returns>
        HttpStatusCode PushFeed(string xml, string gsaHost, bool async);
    }
}
