using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSA.Search
{
    public abstract class BaseQuery
    {
        /// <summary>
        /// The host address to your GSA e.g. http://google04.domain.se/search please note that you should NOT end the url with a trailing slash.
        /// </summary>
        public string GsaHostAddress { get; set; }

        /// <summary>
        /// Specify whether to search public, secure or all content. 
        /// </summary>
        public SearchAccess Access { get; set; }
        //access

        /// <summary>
        /// A string that indicates a valid front end
        /// </summary>
        public string Client { get; set; }
        //client

        /// <summary>
        /// The searchterm as entered by the user
        /// </summary>
        public string SearchTerm { get; set; }
        //q

        /// <summary>
        /// The Collections in which to search for hits. You can use . (AND) or | (OR) to search in several collections.
        /// </summary>
        public string Collections { get; set; }
        //site

        /// <summary>
        /// Constructs a valid query to the google search appliance
        /// </summary>
        /// <returns>Returns the constructed query. This is a url that is directly useable to query the GSA.</returns>
        public virtual string ConstructQuery()
        {
            if (string.IsNullOrEmpty(GsaHostAddress))
            {
                throw new ArgumentException("You must specify a valid host address");
            }

            var sb = new StringBuilder();

            sb.Append(GsaHostAddress);
            //append the searchterm
            sb.Append("?q=").Append(SearchTerm);

            switch (Access)
            {
                case SearchAccess.Public:
                    QueryHelper.AppendQueryString(sb, "access", "p");
                    break;
                case SearchAccess.Secure:
                    QueryHelper.AppendQueryString(sb, "access", "s");
                    break;
                case SearchAccess.All:
                    QueryHelper.AppendQueryString(sb, "access", "a");
                    break;
                case SearchAccess.Ignore:
                    break;
                default:
                    throw new InvalidEnumArgumentException("You have not specified a valid enum value for SearchAccess");
            }

            if (!string.IsNullOrEmpty(Client))
            {
                QueryHelper.AppendQueryString(sb, "client", Client);
            }

            if (!string.IsNullOrEmpty(Collections))
            {
                QueryHelper.AppendQueryString(sb, "site", Collections);
            }


            return sb.ToString();
        }
    }
}
