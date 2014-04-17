using System;
using System.Net;
using GSA.Search.Feeds;
using GSA.Search.Interfaces;

namespace GSA.Search.Interfaces
{
    public interface IFeedManager
    {
        /// <summary>
        /// The host address to your GSA e.g. http://google04.domain.se please note that you should NOT end the url with a trailing slash.
        /// </summary>
        string GsaHostAddress { get; set; }

        /// <summary>
        /// The <see cref="IFeedClient"/> used to handle the feed. If not set the internal implementation is used.
        /// </summary>
        IFeedClient FeedClient { get; set; }

        /// <summary>
        /// Push the specified feed to the Google Search Appliance
        /// </summary>
        /// <param name="feed">The feed to push</param>
        /// <param name="async">Should the request to GSA be sent asynchronously</param>
        /// <returns>a <see cref="HttpStatusCode"/> that indicates whether the request succeded or failed</returns>
        /// <exception cref="ArgumentException">Thrown if you have not specified a correct host adress or feed</exception>
        HttpStatusCode PushFeed(Feed feed, bool async = false);
    }
}