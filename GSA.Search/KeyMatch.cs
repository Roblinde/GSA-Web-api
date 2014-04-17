using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSA.Search
{
    /// <summary>
    /// Encapsulates one keymatch search hit
    /// </summary>
    public class KeyMatch
    {
        /// <summary>
        /// The url for the keymatch
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// The description of the keymatch
        /// </summary>
        public string Description { get; set; }
    }
}
