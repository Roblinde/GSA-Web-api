namespace GSA.Search
{
    /// <summary>
    /// Encapsulates a metadatafield that can be used to filter results from the GSA
    /// </summary>
    public class MetaDataField
    {
        /// <summary>
        /// The metadatafield name
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// The value of the metadatafield
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Specify how the following metafield will be handled. If set to And the next Metadatafield 
        /// in the collection will be required to also exist and have its specified value. If set to Or 
        /// the result will be considered a hit if on or the other metadatafield is specified with the correct values.
        /// </summary>
        public MetaDataSearchSpecification MetaDataSearchSpecification { get; set; }

        /// <summary>
        /// Set to true if you wish no hits with this metafield to be returned
        /// </summary>
        public bool Negate { get; set; }
    }
}
