using System.Collections.Generic;

namespace GSA.Search
{
    /// <summary>
    /// Encapsulates one hit in a search result returned from GSA
    /// </summary>
    public class SearchHit
    {
        /// <summary>
        /// The current index of this item in the total result. 1-based.
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// The url to this hit
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// The encoded url for this hit (Encoded by GSA)
        /// </summary>
        public string EncodedUrl { get; set; }

        /// <summary>
        /// The title of this hit
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// The search rating for this hit as valued by GSA
        /// </summary>
        public int Rating { get; set; }

        /// <summary>
        /// The short snippet of the hits content
        /// </summary>
        public string Snippet { get; set; }

        /// <summary>
        /// The mime type of the hit
        /// </summary>
        public string MimeType { get; set; }

        /// <summary>
        /// The size (if available) of this item in the GSA cache
        /// </summary>
        public string Size { get; set; }

        /// <summary>
        /// Returned MetaTags (if any) from the searchresult for this hit
        /// </summary>
        public Dictionary<string,string> MetaTags { get; set; } 
    }
}
