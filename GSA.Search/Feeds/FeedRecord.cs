using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GSA.Search.Feeds
{
    /// <summary>
    /// Encapsulates a single record in a feed to be sent to the Google Search Appliance.
    /// </summary>
    public class FeedRecord
    {
        /// <summary>
        /// The url of the feed record. Required.
        /// </summary>
        public string Url { get; set; }
        
        /// <summary>
        /// The url to be displayed in the searchresult for the record. If not set URL will be used.
        /// </summary>
        public string DisplayUrl { get; set; }

        /// <summary>
        /// The action to perform on this feedrecord.
        /// </summary>
        public FeedAction Action { get; set; }

        /// <summary>
        /// When the search appliance reaches its license limit, unlocked documents are deleted to make room for more documents. After all other remedies are tried and if the license is still at its limit, then locked documents are deleted.
        /// </summary>
        public bool Lock { get; set; }

        //MimeType is not needed for web-feeds. If in future releases we are to support content feeds it needs to be supported.

        /// <summary>
        ///  Date time format specified in RFC822 (Mon, 15 Nov 2004 04:58:08 GMT). If you do not specify a last-modified date, then the implied value is blank.
        /// </summary>
        public string LastModified { get; set; }

        /// <summary>
        /// The Authorization method needed to access this feedrecord
        /// </summary>
        public FeedAuthorization AuthMethod { get; set; }

        //PageRank is not supported for web-feeds. If in future releases we are to support content feeds this needs to be implemented.

        /// <summary>
        ///  If set to true the search appliance crawls the URL immediately. If a large number of URLs with crawl-immediately="true" are fed, then other URLs to be crawled are deprioritized or halted until these URLs are crawled.
        /// </summary>
        public bool CrawlImmediately { get; set; }

        /// <summary>
        /// If set to true, then the search appliance crawls the URL once, but does not recrawl it after the initial crawl. crawl-once urls can get crawled again if explicitly instructed by a subsequent feed using crawl-immediately.
        /// </summary>
        public bool CrawlOnce { get; set; }

        /// <summary>
        /// Transforms this entity to an XML representation
        /// </summary>
        /// <returns>The xml string</returns>
        public string ToXml()
        {
            if (string.IsNullOrEmpty(Url))
            {
                throw new ArgumentException("You have not specified a correct URL for the feedrecord");
            }

            var element = new XElement("record");

            element.SetAttributeValue("url", Url);
            element.SetAttributeValue("displayurl", DisplayUrl);

            if (Action != FeedAction.Ignore)
            {
                element.SetAttributeValue("action", Action);
            }

            element.SetAttributeValue("lock", Lock);
            element.SetAttributeValue("mimetype", "requiredbutignored");

            if (!string.IsNullOrEmpty(LastModified))
            {
                element.SetAttributeValue("last-modified", LastModified);
            }

            element.SetAttributeValue("authmethod", AuthMethod);
            element.SetAttributeValue("crawl-immediately", CrawlImmediately);
            element.SetAttributeValue("crawl-once", CrawlOnce);

            return element.ToString();
        }
    }
}
