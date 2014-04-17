using System;
using NUnit.Framework;

namespace GSA.Search.Tests
{
    [TestFixture]
    public class SearchTests
    {
        private const string GsaHost = "http://google04.domain.se/search/";
        private const string SuggestHost = "http://google04.domain.se/suggest/";

        [Test]
        public void ConstructingQueryWithoutValidHostShouldThrowException()
        {
            var q = new Query();
            
            Assert.Throws<ArgumentException>(() => q.ConstructQuery());
        }

        [Test]
        public void ConstructingQueryWithoutSpecifyingAnyParametersShouldYieldCorrectQuery()
        {
            var q = new Query();
            q.GsaHostAddress = GsaHost;

            StringAssert.AreEqualIgnoringCase("http://google04.domain.se/search/?q=&ie=utf8&oe=utf8&output=xml_no_dtd", q.ConstructQuery());
        }

        [Test]
        public void ConstructingQueryWithSpecifiedQueryShouldYieldCorrectParameter()
        {
            var q = new Query();
            q.GsaHostAddress = GsaHost;
            q.SearchTerm = "apa";

            StringAssert.Contains("?q=apa", q.ConstructQuery());
        }

        [Test]
        public void ConstructingQueryWithSpecifiedQueryAndAccessShouldYieldCorrectParameter()
        {
            var q = new Query();
            q.GsaHostAddress = GsaHost;
            q.SearchTerm = "apa";
            q.Access = SearchAccess.Public;

            StringAssert.Contains("?q=apa", q.ConstructQuery());
            StringAssert.Contains("&access=p", q.ConstructQuery());

            q.Access = SearchAccess.Secure;
            StringAssert.Contains("&access=s", q.ConstructQuery());

            q.Access = SearchAccess.All;
            StringAssert.Contains("&access=a", q.ConstructQuery());

            q.Access = SearchAccess.Ignore;
            StringAssert.DoesNotContain("&access=", q.ConstructQuery());
        }

        [Test]
        public void ConstructingQueryWithSpecifiedQueryAndSiteSearchModificationShouldYieldCorrectParameter()
        {
            var q = new Query();
            q.GsaHostAddress = GsaHost;
            q.SearchTerm = "apa";
            q.SiteSearchModification = SiteSearchModification.Include;

            StringAssert.Contains("?q=apa", q.ConstructQuery());
            StringAssert.Contains("&ad_dt=i", q.ConstructQuery());

            q.SiteSearchModification = SiteSearchModification.Exclude;
            StringAssert.Contains("&ad_dt=e", q.ConstructQuery());

            q.SiteSearchModification = SiteSearchModification.Ignore;
            StringAssert.DoesNotContain("&ad_dt=", q.ConstructQuery());
        }

        [Test]
        public void ConstructingQueryWithSpecifiedQueryAndAdditionalQueryPhraseShouldYieldCorrectParameter()
        {
            var q = new Query();
            q.GsaHostAddress = GsaHost;
            q.SearchTerm = "apa";
            q.AdditionalQueryPhrase = "phrase";

            StringAssert.Contains("?q=apa", q.ConstructQuery());
            StringAssert.Contains("&as_epq=phrase", q.ConstructQuery());
        }

        [Test]
        public void ConstructingQueryWithSpecifiedQueryAndExcludedQueryTermsShouldYieldCorrectParameter()
        {
            var q = new Query();
            q.GsaHostAddress = GsaHost;
            q.SearchTerm = "apa";
            q.ExcludedQueryTerms = "phrase";

            StringAssert.Contains("?q=apa", q.ConstructQuery());
            StringAssert.Contains("&as_eq=phrase", q.ConstructQuery());
        }

        [Test]
        public void ConstructingQueryWithSpecifiedQueryAndLinkedUrlShouldYieldCorrectParameter()
        {
            var q = new Query();
            q.GsaHostAddress = GsaHost;
            q.SearchTerm = "apa";
            q.LinkedUrl = "phrase";

            StringAssert.Contains("?q=apa", q.ConstructQuery());
            StringAssert.Contains("&as_lq=phrase", q.ConstructQuery());
        }

        [Test]
        public void ConstructingQueryWithSpecifiedQueryAndSearchLocationShouldYieldCorrectParameter()
        {
            var q = new Query();
            q.GsaHostAddress = GsaHost;
            q.SearchTerm = "apa";
            q.SearchLocation = SearchLocation.Anywhere;

            StringAssert.Contains("?q=apa", q.ConstructQuery());
            StringAssert.Contains("&as_occt=any", q.ConstructQuery());

            q.SearchLocation = SearchLocation.Title;
            StringAssert.Contains("&as_occt=title", q.ConstructQuery());

            q.SearchLocation = SearchLocation.Url;
            StringAssert.Contains("&as_occt=url", q.ConstructQuery());

            q.SearchLocation = SearchLocation.Ignore;
            StringAssert.DoesNotContain("&as_occt=", q.ConstructQuery());
        }

        [Test]
        public void ConstructingQueryWithSpecifiedQueryAndCombinedSearchTermShouldYieldCorrectParameter()
        {
            var q = new Query();
            q.GsaHostAddress = GsaHost;
            q.SearchTerm = "apa";
            q.CombinedSearchTerm = "combine";

            StringAssert.Contains("?q=apa", q.ConstructQuery());
            StringAssert.Contains("&as_oq=combine", q.ConstructQuery());
        }

        [Test]
        public void ConstructingQueryWithSpecifiedQueryAndAddedSearchTermShouldYieldCorrectParameter()
        {
            var q = new Query();
            q.GsaHostAddress = GsaHost;
            q.SearchTerm = "apa";
            q.AddedSearchTerm = "added";

            StringAssert.Contains("?q=apa", q.ConstructQuery());
            StringAssert.Contains("&as_q=added", q.ConstructQuery());
        }

        [Test]
        public void ConstructingQueryWithSpecifiedQueryAndAsSiteToSearchShouldYieldCorrectParameter()
        {
            var q = new Query();
            q.GsaHostAddress = GsaHost;
            q.SearchTerm = "apa";
            q.AsSiteToSearch = "site";

            StringAssert.Contains("?q=apa", q.ConstructQuery());
            StringAssert.Contains("&as_sitesearch=site", q.ConstructQuery());
        }

        [Test]
        public void ConstructingQueryWithSpecifiedQueryAndClientShouldYieldCorrectParameter()
        {
            var q = new Query();
            q.GsaHostAddress = GsaHost;
            q.SearchTerm = "apa";
            q.Client = "client";

            StringAssert.Contains("?q=apa", q.ConstructQuery());
            StringAssert.Contains("&client=client", q.ConstructQuery());
        }

        [Test]
        public void ConstructingQueryWithSpecifiedQueryAndQueryExpansionShouldYieldCorrectParameter()
        {
            var q = new Query();
            q.GsaHostAddress = GsaHost;
            q.SearchTerm = "apa";
            q.QueryExpansion = QueryExpansion.Full;

            StringAssert.Contains("?q=apa", q.ConstructQuery());
            StringAssert.Contains("&entqr=3", q.ConstructQuery());

            q.QueryExpansion = QueryExpansion.Local;
            StringAssert.Contains("&entqr=2", q.ConstructQuery());

            q.QueryExpansion = QueryExpansion.None;
            StringAssert.Contains("&entqr=0", q.ConstructQuery());

            q.QueryExpansion = QueryExpansion.Standard;
            StringAssert.Contains("&entqr=1", q.ConstructQuery());

            q.QueryExpansion = QueryExpansion.Ignore;
            StringAssert.DoesNotContain("&entqr=", q.ConstructQuery());
        }

        [Test]
        public void ConstructingQueryWithSpecifiedQueryAndRelevanceScoringShouldYieldCorrectParameter()
        {
            var q = new Query();
            q.GsaHostAddress = GsaHost;
            q.SearchTerm = "apa";
            q.RelevanceScoring = RelevanceScoring.Advanced;

            StringAssert.Contains("?q=apa", q.ConstructQuery());
            StringAssert.Contains("&entsp=a", q.ConstructQuery());

            q.RelevanceScoring = RelevanceScoring.Standard;
            StringAssert.Contains("&entsp=0", q.ConstructQuery());

            q.RelevanceScoring = RelevanceScoring.Ignore;
            StringAssert.DoesNotContain("&entsp=", q.ConstructQuery());
        }

        [Test]
        public void ConstructingQueryWithSpecifiedQueryAndFilterShouldYieldCorrectParameter()
        {
            var q = new Query();
            q.GsaHostAddress = GsaHost;
            q.SearchTerm = "apa";
            q.Filter = QueryFilter.DirectoryOnly;

            StringAssert.Contains("?q=apa", q.ConstructQuery());
            StringAssert.Contains("&filter=s", q.ConstructQuery());

            q.Filter = QueryFilter.SnippetAndDirectory;
            StringAssert.Contains("&filter=1", q.ConstructQuery());

            q.Filter = QueryFilter.None;
            StringAssert.Contains("&filter=0", q.ConstructQuery());

            q.Filter = QueryFilter.SnippetOnly;
            StringAssert.Contains("&filter=p", q.ConstructQuery());

            q.Filter = QueryFilter.Ignore;
            StringAssert.DoesNotContain("&filter=", q.ConstructQuery());
        }

        [Test]
        public void ConstructingQueryWithSpecifiedQueryAndGetMetaFieldsShouldYieldCorrectParameter()
        {
            var q = new Query();
            q.GsaHostAddress = GsaHost;
            q.SearchTerm = "apa";
            q.GetMetaFields = "meta";

            StringAssert.Contains("?q=apa", q.ConstructQuery());
            StringAssert.Contains("&getfields=meta", q.ConstructQuery());
        }

        [Test]
        public void ConstructingQueryWithSpecifiedQueryAndCharacterEncodingQueryShouldYieldCorrectParameter()
        {
            var q = new Query();
            q.GsaHostAddress = GsaHost;
            q.SearchTerm = "apa";
            q.CharacterEncodingQuery = "enc";

            StringAssert.Contains("?q=apa", q.ConstructQuery());
            StringAssert.Contains("&ie=enc", q.ConstructQuery());

            //default value should kick in
            q.CharacterEncodingQuery = "";
            StringAssert.Contains("&ie=utf8", q.ConstructQuery());
        }

        [Test]
        public void ConstructingQueryWithSpecifiedQueryAndRestrictToLanguageShouldYieldCorrectParameter()
        {
            var q = new Query();
            q.GsaHostAddress = GsaHost;
            q.SearchTerm = "apa";
            q.RestrictToLanguage = "lang";

            StringAssert.Contains("?q=apa", q.ConstructQuery());
            StringAssert.Contains("&lr=lang", q.ConstructQuery());
        }

        [Test]
        public void ConstructingQueryWithSpecifiedQueryAndMaxSearchHitsShouldYieldCorrectParameter()
        {
            var q = new Query();
            q.GsaHostAddress = GsaHost;
            q.SearchTerm = "apa";
            q.MaxSearchHits = 8;

            StringAssert.Contains("?q=apa", q.ConstructQuery());
            StringAssert.Contains("&num=8", q.ConstructQuery());

            //Default value should kick in
            q.MaxSearchHits = 0;
            StringAssert.DoesNotContain("&num=", q.ConstructQuery());

            //Max value should kick in
            q.MaxSearchHits = 81909;
            StringAssert.Contains("&num=100", q.ConstructQuery());
        }

        [Test]
        public void ConstructingQueryWithSpecifiedQueryAndMaxKeyMatchResultsShouldYieldCorrectParameter()
        {
            var q = new Query();
            q.GsaHostAddress = GsaHost;
            q.SearchTerm = "apa";
            q.MaxKeyMatchResults = 3;

            StringAssert.Contains("?q=apa", q.ConstructQuery());
            StringAssert.Contains("&numgm=3", q.ConstructQuery());

            //Default value should kick in
            q.MaxKeyMatchResults = 0;
            StringAssert.DoesNotContain("&numgm=", q.ConstructQuery());

            //Max value should kick in
            q.MaxKeyMatchResults = 7;
            StringAssert.Contains("&numgm=5", q.ConstructQuery());
        }

        [Test]
        public void ConstructingQueryWithSpecifiedQueryAndCharacterEncodingResultShouldYieldCorrectParameter()
        {
            var q = new Query();
            q.GsaHostAddress = GsaHost;
            q.SearchTerm = "apa";
            q.CharacterEncodingResult = "enc";

            StringAssert.Contains("?q=apa", q.ConstructQuery());
            StringAssert.Contains("&oe=enc", q.ConstructQuery());

            //Default value should kick in
            q.CharacterEncodingResult = "";
            StringAssert.Contains("&oe=utf8", q.ConstructQuery());
        }

        [Test]
        public void ConstructingQueryWithSpecifiedQueryAndCollectionsShouldYieldCorrectParameter()
        {
            var q = new Query();
            q.GsaHostAddress = GsaHost;
            q.SearchTerm = "apa";
            q.Collections = "samplecollection";

            StringAssert.Contains("?q=apa", q.ConstructQuery());
            StringAssert.Contains("&site=samplecollection", q.ConstructQuery());
        }

        [Test]
        public void ConstructingQueryWithSpecifiedQueryAndSiteToSearchShouldYieldCorrectParameter()
        {
            var q = new Query();
            q.GsaHostAddress = GsaHost;
            q.SearchTerm = "apa";
            q.SiteToSearch = "somesite";

            StringAssert.Contains("?q=apa", q.ConstructQuery());
            StringAssert.Contains("&sitesearch=somesite", q.ConstructQuery());
        }

        [Test]
        public void ConstructingQueryWithSpecifiedQueryAndSortShouldYieldCorrectParameter()
        {
            var q = new Query();
            q.GsaHostAddress = GsaHost;
            q.SearchTerm = "apa";
            q.Sort = "sortme";

            StringAssert.Contains("?q=apa", q.ConstructQuery());
            StringAssert.Contains("&sort=sortme", q.ConstructQuery());
        }


        [Test]
        public void ConstructingQueryWithSpecifiedQueryAndStartShouldYieldCorrectParameter()
        {
            var q = new Query();
            q.GsaHostAddress = GsaHost;
            q.SearchTerm = "apa";
            q.Start = 10;

            StringAssert.Contains("?q=apa", q.ConstructQuery());
            StringAssert.Contains("&start=10", q.ConstructQuery());
        }

        [Test]
        public void ConstructingSuggestQueryWithoutValidHostShouldThrowException()
        {
            var q = new SuggestionQuery();

            Assert.Throws<ArgumentException>(() => q.ConstructQuery());
        }

        [Test]
        public void ConstructingSuggestQueryWithoutSpecifyingAnyParametersShouldYieldCorrectQuery()
        {
            var q = new SuggestionQuery();
            q.GsaHostAddress = SuggestHost;

            StringAssert.AreEqualIgnoringCase("http://google04.domain.se/suggest/?q=&format=rich", q.ConstructQuery());
        }

        [Test]
        public void ConstructingSuggestQueryWithMaxSuggestionsShouldYieldCorrectQuery()
        {
            var q = new SuggestionQuery();
            q.GsaHostAddress = SuggestHost;
            q.MaxSuggestions = 7;

            StringAssert.Contains("&max=7", q.ConstructQuery());
        }

        [Test]
        public void ConstructingSuggestQueryWithCollectionsShouldYieldCorrectQuery()
        {
            var q = new SuggestionQuery();
            q.GsaHostAddress = SuggestHost;
            q.Collections = "col";

            StringAssert.Contains("&site=col", q.ConstructQuery());
        }

        [Test]
        public void ConstructingSuggestQueryWithClientShouldYieldCorrectQuery()
        {
            var q = new SuggestionQuery();
            q.GsaHostAddress = SuggestHost;
            q.Client = "client";

            StringAssert.Contains("&client=client", q.ConstructQuery());
        }

        [Test]
        public void ConstructingSuggestQueryWithAccessShouldYieldCorrectQuery()
        {
            var q = new SuggestionQuery();
            q.GsaHostAddress = SuggestHost;
            q.Access = SearchAccess.Public;
            q.SearchTerm = "apa";

            StringAssert.Contains("?q=apa", q.ConstructQuery());
            StringAssert.Contains("&access=p", q.ConstructQuery());

            q.Access = SearchAccess.Secure;
            StringAssert.Contains("&access=s", q.ConstructQuery());

            q.Access = SearchAccess.All;
            StringAssert.Contains("&access=a", q.ConstructQuery());

            q.Access = SearchAccess.Ignore;
            StringAssert.DoesNotContain("&access=", q.ConstructQuery());
        }
    }
}
