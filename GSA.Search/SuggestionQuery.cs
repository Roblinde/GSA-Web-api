using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GSA.Search.Interfaces;

namespace GSA.Search
{
    /// <summary>
    /// Encapsulates a simple suggestionquery
    /// </summary>
    public class SuggestionQuery : BaseQuery, ISuggestionQuery
    {
        //We do not support Format as the returned format is always typed
        //We do not support callback as the returned format is always typed

        /// <summary>
        /// The maximum number of results that the suggest server should return. The minimum is 0, which indicates that the server should return an empty set, 
        /// however this result would not be meaningful. Maximum is not set. Default is 10. If fewer suggestions are configured, then they are returned.
        /// </summary>
        public int MaxSuggestions { get; set; }

        /// <summary>
        /// Constructs a valid suggestionquery to the google search appliance
        /// </summary>
        /// <returns>Returns the constructed query. This is a url that is directly useable to query the GSA.</returns>
        public override string ConstructQuery()
        {
            if (string.IsNullOrEmpty(GsaHostAddress))
            {
                throw new ArgumentException("You must specify a valid host address");
            }

            var sb = new StringBuilder();

            //get the root values from the base implementation
            sb.Append(base.ConstructQuery());

            //Append the rest of the parameters

          

            if (MaxSuggestions > 0)
            {
                QueryHelper.AppendQueryString(sb, "max", MaxSuggestions.ToString(CultureInfo.InvariantCulture));
            }


            //We always use the rich format so we know how to parse the response.
            QueryHelper.AppendQueryString(sb, "format", "rich");

            return sb.ToString();
        }
    }
}
