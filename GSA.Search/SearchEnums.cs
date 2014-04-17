namespace GSA.Search
{
    /// <summary>
    /// Enumeration to specify the search access. Corresponds to the access xml-tag.
    /// </summary>
    public enum SearchAccess
    {
        Ignore,
        Public,
        Secure,
        All
    }

    /// <summary>
    /// Enumeration to specify the searchlocation. Corresponds to the as_occt xml-tag.
    /// </summary>
    public enum SearchLocation
    {
        Ignore,
        Title,
        Url,
        Anywhere
        
    }

    /// <summary>
    /// Enumeration to specify how the sitesearch is handled. Corresponds to the as_dt xml-tag.
    /// </summary>
    public enum SiteSearchModification
    {
        Ignore,
        Include,
        Exclude
    }

    /// <summary>
    /// Enumeration to specify how queries are filtered. Corresponds to the filter xml-tag.
    /// </summary>
    public enum QueryFilter
    {
        Ignore,
        SnippetAndDirectory,
        None,
        DirectoryOnly,
        SnippetOnly
    }

    /// <summary>
    /// Enumeration to specify how queries are expanded. Corresponds to the entqr xml-tag.
    /// </summary>
    public enum QueryExpansion
    {
        Ignore,
        None,
        Standard,
        Local,
        Full
    }

    /// <summary>
    /// Enumeration to specify how queries are ranked. Corresponds to the entsp xml-tag.
    /// </summary>
    public enum RelevanceScoring
    {
        Ignore,
        Standard,
        Advanced
    }

    /// <summary>
    /// Enumeration to specify how the following metatag will be handled, AND or OR. Ignore will simply append the next metadata field.
    /// </summary>
    public enum MetaDataSearchSpecification
    {
        Ignore,
        And,
        Or
    }

    /// <summary>
    /// Enumeration to specify what value type of items a Facet contains
    /// </summary>
    public enum FacetType
    {
        Integer,
        String,
        Float,
        Currency,
        Date
    }
}
