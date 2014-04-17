using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using GSA.Search.Interfaces;

namespace GSA.Search.Feeds
{
    /// <summary>
    /// Represents a way to manage feeds to be sent to the Google Search Appliance
    /// </summary>
    public class FeedManager : IFeedManager
    {
        /// <summary>
        /// The host address to your GSA e.g. http://google04.domain.se please note that you should NOT end the url with a trailing slash.
        /// </summary>
        public string GsaHostAddress { get; set; }

        /// <summary>
        /// The <see cref="IFeedClient"/> used to handle the feed. If not set the internal implementation is used.
        /// </summary>
        public IFeedClient FeedClient { get; set; }

        /// <summary>
        /// Push the specified feed to the Google Search Appliance
        /// </summary>
        /// <param name="feed">The feed to push</param>
        /// <param name="async">Should the request to GSA be sent asynchronously</param>
        /// <returns>a <see cref="HttpStatusCode"/> that indicates whether the request succeded or failed</returns>
        /// <exception cref="ArgumentException">Thrown if you have not specified a correct host adress or feed</exception>
        public HttpStatusCode PushFeed(Feed feed, bool async = false)
        {
            if (string.IsNullOrEmpty(GsaHostAddress))
            {
                throw new ArgumentException("You have not specified a correct host address");
            }

            if (feed == null)
            {
                throw new ArgumentException("You must specify a feed", "feed");
            }

            var xml = feed.ConstructXml();

            if (FeedClient == null)
            {
                FeedClient = new FeedClient();
            }

            return FeedClient.PushFeed(xml, GsaHostAddress, async);
        }
    }
}
