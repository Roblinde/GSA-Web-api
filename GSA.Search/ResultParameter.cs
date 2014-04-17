using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSA.Search
{
    /// <summary>
    /// Encapsulates a parameter used to present a certain result from GSA
    /// </summary>
    public class ResultParameter
    {
        /// <summary>
        /// The name of the parameter as interpreted by GSA
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The value of the parameter
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// The original value of the paramter before GSA interpretation
        /// </summary>
        public string OriginalValue { get; set; }
    }
}
