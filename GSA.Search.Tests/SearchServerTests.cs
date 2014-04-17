using System.Linq;
using GSA.Search.Interfaces;
using Moq;
using NUnit.Framework;

namespace GSA.Search.Tests
{
    [TestFixture]
    public class SearchServerTests
    {
        string gsaHost = "http://google04.domain.se/search?q=ledighet&client=Klara2013&ie=utf8&num=10&oe=utf8&output=xml_no_dtd&site=gamla_och_nya";

        [Test]
        public void SearchShouldReturnResults()
        {
            var q = new Mock<IQuery>();
            q.Setup(m => m.ConstructQuery()).Returns(@"samplegsaxml.xml");

            var server = new SearchServer();

            var result = server.Search(q.Object);

            Assert.IsNotNull(result);
            Assert.IsNullOrEmpty(result.Error);
        }

        [Test]
        public void MalformedSearchHostShouldReturnAnError()
        {
            var q = new Mock<IQuery>();
            q.Setup(m => m.ConstructQuery()).Returns("apa.xml");

            var server = new SearchServer();

            var result = server.Search(q.Object);

            Assert.IsNotNull(result);
            Assert.IsNotNullOrEmpty(result.Error);
        }

        [Test]
        public void CorrectXmlShouldReturnSynonyms()
        {
            var q = new Mock<IQuery>();
            q.Setup(m => m.ConstructQuery()).Returns(@"samplegsaxml.xml");

            var server = new SearchServer();

            var result = server.Search(q.Object);

            Assert.IsNotNull(result);
            Assert.IsNullOrEmpty(result.Error);
            CollectionAssert.IsNotEmpty(result.Synonyms);
            Assert.IsTrue(result.Synonyms.Count == 3);
        }

         [Test]
        public void CorrectXmlShouldReturnSpellingSuggestions()
        {
            var q = new Mock<IQuery>();
            q.Setup(m => m.ConstructQuery()).Returns(@"samplegsaxml.xml");

            var server = new SearchServer();

            var result = server.Search(q.Object);

            Assert.IsNotNull(result);
            Assert.IsNullOrEmpty(result.Error);
            CollectionAssert.IsNotEmpty(result.SpellingSuggestions);
            Assert.IsTrue(result.SpellingSuggestions.Count == 1);
        }

         [Test]
         public void CorrectXmlShouldReturnCorrectTimings()
         {
             var q = new Mock<IQuery>();
             q.Setup(m => m.ConstructQuery()).Returns(@"samplegsaxml.xml");

             var server = new SearchServer();

             var result = server.Search(q.Object);

             Assert.IsNotNull(result);
             Assert.IsNullOrEmpty(result.Error);
             StringAssert.AreEqualIgnoringCase(result.ExecutionTime, "0.111599");
         }

         [Test]
         public void CorrectXmlShouldReturnQuery()
         {
             var q = new Mock<IQuery>();
             q.Setup(m => m.ConstructQuery()).Returns(@"samplegsaxml.xml");

             var server = new SearchServer();

             var result = server.Search(q.Object);

             Assert.IsNotNull(result);
             Assert.IsNullOrEmpty(result.Error);
             StringAssert.AreEqualIgnoringCase(result.Query, "Försäkring");
         }

         [Test]
         public void CorrectXmlShouldReturnParameters()
         {
             var q = new Mock<IQuery>();
             q.Setup(m => m.ConstructQuery()).Returns(@"samplegsaxml.xml");

             var server = new SearchServer();

             var result = server.Search(q.Object);

             Assert.IsNotNull(result);
             Assert.IsNullOrEmpty(result.Error);
             CollectionAssert.IsNotEmpty(result.Parameters);
             Assert.IsTrue(result.Parameters.Count == 14);
         }

         [Test]
         public void CorrectXmlShouldReturnCorrectResult()
         {
             var q = new Mock<IQuery>();
             q.Setup(m => m.ConstructQuery()).Returns(@"samplegsaxml.xml");

             var server = new SearchServer();

             var result = server.Search(q.Object);

             Assert.IsNotNull(result);
             Assert.IsNullOrEmpty(result.Error);
             CollectionAssert.IsNotEmpty(result.SearchHits);
             Assert.IsTrue(result.SearchHits.Count == 10);
             CollectionAssert.AllItemsAreNotNull(result.SearchHits);
             CollectionAssert.AllItemsAreUnique(result.SearchHits);
         }

         [Test]
         public void CorrectXmlShouldReturnCorrectNumberOfHits()
         {
             var q = new Mock<IQuery>();
             q.Setup(m => m.ConstructQuery()).Returns(@"samplegsaxml.xml");

             var server = new SearchServer();

             var result = server.Search(q.Object);

             Assert.IsNotNull(result);
             Assert.IsNullOrEmpty(result.Error);
             Assert.AreEqual(3480, result.NumberOfHits);

         }

         [Test]
         public void CorrectXmlShouldReturnCorrectKeyMatches()
         {
             var q = new Mock<IQuery>();
             q.Setup(m => m.ConstructQuery()).Returns(@"samplegsaxml.xml");

             var server = new SearchServer();

             var result = server.Search(q.Object);

             Assert.IsNotNull(result);
             Assert.IsNullOrEmpty(result.Error);
             Assert.AreEqual(1, result.KeyMatches.Count);

         }

         [Test]
         public void CorrectXmlShouldReturnCorrectFacets()
         {
             var q = new Mock<IQuery>();
             q.Setup(m => m.ConstructQuery()).Returns(@"samplegsaxml.xml");

             var server = new SearchServer();

             var result = server.Search(q.Object);

             Assert.IsNotNull(result);
             Assert.IsNullOrEmpty(result.Error);
             CollectionAssert.IsNotEmpty(result.Facets);
             Assert.AreEqual(6, result.Facets.Count);
             CollectionAssert.AllItemsAreNotNull(result.Facets);
             CollectionAssert.AllItemsAreUnique(result.Facets);

             Assert.AreEqual(1, result.Facets.First().FacetItems.Count);
             Assert.AreEqual(628, result.Facets.First().FacetItems.First().Count);

         }

         [Test]
         public void CorrectJsonShouldReturnResult()
         {
             var q = new Mock<ISuggestionQuery>();
             q.Setup(m => m.ConstructQuery()).Returns(@"C:\Repos\WebApi\GSA.Search.Tests\samplejson.txt");

             var server = new SearchServer();

             var result = server.Suggest(q.Object);

             Assert.IsNotNull(result);
             CollectionAssert.IsNotEmpty(result);
             CollectionAssert.AllItemsAreNotNull(result);
             Assert.AreEqual(10, result.Count);
             CollectionAssert.Contains(result, "lediga dagar 2014");
             CollectionAssert.Contains(result, "ledighetsansökan");
             CollectionAssert.DoesNotContain(result, "fakeandbake");
             CollectionAssert.DoesNotContain(result, "suggest");
         }

    }
}
