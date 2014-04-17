using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using GSA.Search.Feeds;
using GSA.Search.WebApi.Helpers;

namespace GSA.Search.WebApi.Controllers
{
    /// <summary>
    /// API controller that handles pushing feeds to the Google Search Appliance
    /// </summary>
    public class FeedController : ApiController
    {
        /// <summary>
        /// Sends the specified url to be updated by the Google Search Appliance
        /// </summary>
        /// <param name="u">The url to update. Must be properly url-encoded</param>
        /// <param name="s">The system name that should receive the update. Supported values are: "Klara", "SuntLiv", "Klaratest", "SuntLivTest"</param>
        /// <returns>The <see cref="HttpResponseMessage"/> for the call</returns>
        public HttpResponseMessage Post(string u, string s)
        {
            if (string.IsNullOrEmpty(u))
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest,
                    new HttpError(new ArgumentException("The u parameter must be a valid url"), true));
            }

            if (string.IsNullOrEmpty(s))
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest,
                    new HttpError(new ArgumentException("The s parameter must be specified"), true));
            }

            var feed = new Feed();

            var feedRecord = new FeedRecord();
            feedRecord.Url = HttpUtility.UrlDecode(u);
            feedRecord.CrawlImmediately = true;
            feed.Records.Add(feedRecord);

            var manager = new FeedManager();
            manager.GsaHostAddress = string.Format(this.GetHostFromSystemName(s), string.Empty);

            var response = manager.PushFeed(feed);

            return Request.CreateResponse(response);
        }

        /// <summary>
        /// Sends the specified url to be deleted from the Google Search Appliance index
        /// </summary>
        /// <param name="u">The url to delete. Must be properly url-encoded</param>
        /// <param name="s">The system name that should receive the delete. Supported values are: "Klara", "SuntLiv", "Klaratest", "SuntLivTest"</param>
        /// <returns>The <see cref="HttpResponseMessage"/> for the call</returns>
        public HttpResponseMessage Delete(string u, string s)
        {
            if (string.IsNullOrEmpty(u))
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest,
                    new HttpError(new ArgumentException("The u parameter must be a valid url"), true));
            }

            if (string.IsNullOrEmpty(s))
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest,
                    new HttpError(new ArgumentException("The s parameter must be specified"), true));
            }

            var feed = new Feed();

            var feedRecord = new FeedRecord();
            feedRecord.Url = HttpUtility.UrlDecode(u);
            feedRecord.Action = FeedAction.delete;
            feed.Records.Add(feedRecord);

            var manager = new FeedManager();
            manager.GsaHostAddress = string.Format(this.GetHostFromSystemName(s), string.Empty);

            var response = manager.PushFeed(feed);

            return Request.CreateResponse(response);
        }
    }
}
