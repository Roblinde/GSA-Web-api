using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GSA.Search.Feeds
{
    /// <summary>
    /// Represents a feed to be sent to the Google Search Appliance
    /// </summary>
    public class Feed
    {
        /// <summary>
        /// Initializes a new instance of <see cref="Feed"/>
        /// </summary>
        public Feed()
        {
            Records = new List<FeedRecord>();
        }

        //Only support web feed at the moment

        //only support incremental feed type at the moment



        /// <summary>
        /// The FeedRecords to send
        /// </summary>
        public List<FeedRecord> Records { get; set; }

        /// <summary>
        /// Constructs a correct feed xml given the current FeedRecords and GsaHostAddress
        /// </summary>
        /// <returns>Well formed feed xml</returns>
        public string ConstructXml()
        {
            var xmlstring = "<?xml version=\"1.0\" encoding=\"utf-8\"?><!DOCTYPE gsafeed PUBLIC \"-//Google//DTD GSA Feeds//EN\" \"\"><gsafeed><header><datasource>web</datasource><feedtype>incremental</feedtype></header><group></group></gsafeed>";

            var doc = XDocument.Parse(xmlstring);

            foreach (var feedRecord in Records)
            {
                doc.Root.Element("group").Add(XElement.Parse(feedRecord.ToXml()));
            }

            return doc.ToString();
        }
    }
}
