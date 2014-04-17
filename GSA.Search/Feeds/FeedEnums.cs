using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSA.Search.Feeds
{
    /// <summary>
    /// Enumeration to specify the feed action.
    /// </summary>
    public enum FeedAction
    {
        /// <summary>
        /// Ignoring the feed action defaults to add
        /// </summary>
        Ignore,
        /// <summary>
        /// The feedrecord should be added/updated in the index
        /// </summary>
        add,
        /// <summary>
        /// The feedrecord should be deleted from the index
        /// </summary>
        delete,
    }

    /// <summary>
    /// Enumeration to specify the authorization method for a specific feedrecord.
    /// </summary>
    public enum FeedAuthorization
    {
        /// <summary>
        /// No authorization required
        /// </summary>
        none,
        /// <summary>
        /// Basic HTTP authorization required
        /// </summary>
        httpbasic,
        /// <summary>
        /// Ntlm authorization required
        /// </summary>
        ntlm,
        /// <summary>
        /// Single Sign On HTTP authorization required
        /// </summary>
        httpsso
    }
}
