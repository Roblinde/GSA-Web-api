using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSA.Search
{
    /// <summary>
    /// Encapsulates a facet of one MetaTag
    /// </summary>
    public class Facet
    {
        /// <summary>
        /// The name of the meta-tag
        /// </summary>
        public string MetaName { get; set; }

        /// <summary>
        /// The displayname of the meta-tag
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Specifies whether the meta-tag contains a range value
        /// </summary>
        public bool IsRange { get; set; }

        /// <summary>
        /// The type of facet. String, Integer, Date, Float or Currency
        /// </summary>
        public FacetType FacetType { get; set; }

        /// <summary>
        /// A list of <see cref="FacetItem"/> available in this Facet. All FacetItems are contained under the current Meta-tag.
        /// </summary>
        public List<FacetItem> FacetItems { get; set; }
    }

    /// <summary>
    /// Encapsulates a facetitem of one facet
    /// </summary>
    public class FacetItem
    {
        /// <summary>
        /// The meta-data value of this facetitem
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// The low range of this facetitem, only applies if the containing <see cref="Facet.IsRange"/> is true
        /// </summary>
        public string LowRange { get; set; }

        /// <summary>
        /// The high range of this facetitem, only applies if the containing <see cref="Facet.IsRange"/> is true
        /// </summary>
        public string HighRange { get; set; }

        /// <summary>
        /// The number of search hits in this facet
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// The metaname of this facetitem. Will always be the same as the containing <see cref="Facet"/>
        /// </summary>
        public string MetaName { get; set; }
    }

}
