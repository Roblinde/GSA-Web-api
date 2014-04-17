using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace GSA.Search
{
    internal static class QueryHelper
    {

        /// <summary>
        /// Helper method to append a parameter and value to a querystring
        /// </summary>
        /// <param name="sb">The stringbuilder to append the parameter to</param>
        /// <param name="parameterName">The parameter name as used in the querystring</param>
        /// <param name="value">The value of the parameter as used in the querystring</param>
        public static void AppendQueryString(StringBuilder sb, string parameterName, string value)
        {
            sb.Append(string.Format("&{0}={1}", parameterName, value));
        }

        /// <summary>
        /// Helper method to parse meta data fields
        /// </summary>
        /// <param name="sb">The stringbuilder to append the metadatafields to</param>
        /// <param name="parametername">The parametername as used in the constructed querystring</param>
        /// <param name="fields">The metadatafields to parse</param>
        public static void ParseMetaDataFields(StringBuilder sb, string parametername, List<MetaDataField> fields)
        {
            var value = string.Empty;
            foreach (var metadata in fields)
            {
                //needs to be double encoded
                var encodedKey = HttpUtility.UrlEncode(Uri.EscapeUriString(metadata.Key));
                var encodedValue = HttpUtility.UrlEncode(Uri.EscapeUriString(metadata.Value));

                var negateValue = metadata.Negate ? "-" : "";

                var operatorValue = "";

                //We do not add an operator to the last entry
                if (fields.Last() != metadata)
                {
                    switch (metadata.MetaDataSearchSpecification)
                    {
                        case MetaDataSearchSpecification.And:
                            operatorValue = ".";
                            break;
                        case MetaDataSearchSpecification.Or:
                            operatorValue = "|";
                            break;
                        case MetaDataSearchSpecification.Ignore:
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }

                value += string.Format("{0}{1}:{2}{3}", negateValue, encodedKey, encodedValue, operatorValue);
            }
            AppendQueryString(sb, parametername, value);
        }
    }
}
