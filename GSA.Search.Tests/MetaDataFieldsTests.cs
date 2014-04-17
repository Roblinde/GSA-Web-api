using System.Collections.Generic;
using NUnit.Framework;

namespace GSA.Search.Tests
{
    [TestFixture]
    public class MetaDataFieldsTests
    {
        private const string GsaHost = "http://google04.domain.se/search/";


        [Test]
        public void SimplePartialMetaDataSearchShouldYieldCorrectUrl()
        {
            var q = new Query();
            q.GsaHostAddress = GsaHost;
            q.SearchTerm = "Meta";
            q.PartialFields = new List<MetaDataField>();

            StringAssert.Contains("?q=Meta", q.ConstructQuery());
            StringAssert.DoesNotContain("&partialfields=", q.ConstructQuery());

            q.PartialFields.Add(new MetaDataField {Key ="metakey", Value = "metavalue"});
            StringAssert.Contains("&partialfields=metakey:metavalue", q.ConstructQuery());

        }

        [Test]
        public void SimpleRequiredMetaDataSearchShouldYieldCorrectUrl()
        {
            var q = new Query();
            q.GsaHostAddress = GsaHost;
            q.SearchTerm = "Meta";
            q.RequiredFields = new List<MetaDataField>();

            StringAssert.Contains("?q=Meta", q.ConstructQuery());
            StringAssert.DoesNotContain("&requiredfields=", q.ConstructQuery());

            q.RequiredFields.Add(new MetaDataField { Key = "metakey", Value = "metavalue" });
            StringAssert.Contains("&requiredfields=metakey:metavalue", q.ConstructQuery());

        }

        [Test]
        public void PartialMetaDataSearchWithAndShouldYieldCorrectUrl()
        {
            var q = new Query();
            q.GsaHostAddress = GsaHost;
            q.SearchTerm = "Meta";
            q.PartialFields = new List<MetaDataField>();

            StringAssert.Contains("?q=Meta", q.ConstructQuery());
            StringAssert.DoesNotContain("&partialfields=", q.ConstructQuery());

            q.PartialFields.Add(new MetaDataField { Key = "metakey", Value = "metavalue", MetaDataSearchSpecification = MetaDataSearchSpecification.And});
            q.PartialFields.Add(new MetaDataField { Key = "metakey2", Value = "metavalue2" });
            StringAssert.Contains("&partialfields=metakey:metavalue.metakey2:metavalue2", q.ConstructQuery());

        }

        [Test]
        public void RequiredMetaDataSearchWithAndShouldYieldCorrectUrl()
        {
            var q = new Query();
            q.GsaHostAddress = GsaHost;
            q.SearchTerm = "Meta";
            q.RequiredFields = new List<MetaDataField>();

            StringAssert.Contains("?q=Meta", q.ConstructQuery());
            StringAssert.DoesNotContain("&requiredfields=", q.ConstructQuery());

            q.RequiredFields.Add(new MetaDataField { Key = "metakey", Value = "metavalue", MetaDataSearchSpecification = MetaDataSearchSpecification.And });
            q.RequiredFields.Add(new MetaDataField { Key = "metakey2", Value = "metavalue2" });
            StringAssert.Contains("&requiredfields=metakey:metavalue.metakey2:metavalue2", q.ConstructQuery());

        }

        [Test]
        public void PartialMetaDataSearchWithOrShouldYieldCorrectUrl()
        {
            var q = new Query();
            q.GsaHostAddress = GsaHost;
            q.SearchTerm = "Meta";
            q.PartialFields = new List<MetaDataField>();

            StringAssert.Contains("?q=Meta", q.ConstructQuery());
            StringAssert.DoesNotContain("&partialfields=", q.ConstructQuery());

            q.PartialFields.Add(new MetaDataField { Key = "metakey", Value = "metavalue", MetaDataSearchSpecification = MetaDataSearchSpecification.Or });
            q.PartialFields.Add(new MetaDataField { Key = "metakey2", Value = "metavalue2" });
            StringAssert.Contains("&partialfields=metakey:metavalue|metakey2:metavalue2", q.ConstructQuery());

        }

        [Test]
        public void RequiredMetaDataSearchWithOrShouldYieldCorrectUrl()
        {
            var q = new Query();
            q.GsaHostAddress = GsaHost;
            q.SearchTerm = "Meta";
            q.RequiredFields = new List<MetaDataField>();

            StringAssert.Contains("?q=Meta", q.ConstructQuery());
            StringAssert.DoesNotContain("&requiredfields=", q.ConstructQuery());

            q.RequiredFields.Add(new MetaDataField { Key = "metakey", Value = "metavalue", MetaDataSearchSpecification = MetaDataSearchSpecification.Or });
            q.RequiredFields.Add(new MetaDataField { Key = "metakey2", Value = "metavalue2" });
            StringAssert.Contains("&requiredfields=metakey:metavalue|metakey2:metavalue2", q.ConstructQuery());

        }

        [Test]
        public void PartialMetaDataSearchWithOrAndNotShouldYieldCorrectUrl()
        {
            var q = new Query();
            q.GsaHostAddress = GsaHost;
            q.SearchTerm = "Meta";
            q.PartialFields = new List<MetaDataField>();

            StringAssert.Contains("?q=Meta", q.ConstructQuery());
            StringAssert.DoesNotContain("&partialfields=", q.ConstructQuery());

            q.PartialFields.Add(new MetaDataField { Key = "metakey", Value = "metavalue", MetaDataSearchSpecification = MetaDataSearchSpecification.Or });
            q.PartialFields.Add(new MetaDataField { Key = "metakey2", Value = "metavalue2", Negate = true});
            StringAssert.Contains("&partialfields=metakey:metavalue|-metakey2:metavalue2", q.ConstructQuery());

        }

        [Test]
        public void RequiredMetaDataSearchWithOrAndNotShouldYieldCorrectUrl()
        {
            var q = new Query();
            q.GsaHostAddress = GsaHost;
            q.SearchTerm = "Meta";
            q.RequiredFields = new List<MetaDataField>();

            StringAssert.Contains("?q=Meta", q.ConstructQuery());
            StringAssert.DoesNotContain("&requiredfields=", q.ConstructQuery());

            q.RequiredFields.Add(new MetaDataField { Key = "metakey", Value = "metavalue", MetaDataSearchSpecification = MetaDataSearchSpecification.Or });
            q.RequiredFields.Add(new MetaDataField { Key = "metakey2", Value = "metavalue2", Negate = true});
            StringAssert.Contains("&requiredfields=metakey:metavalue|-metakey2:metavalue2", q.ConstructQuery());

        }

        [Test]
        public void MetaDataFieldsShouldBeCorrectlyEncoded()
        {
            var q = new Query();
            q.GsaHostAddress = GsaHost;
            q.SearchTerm = "Meta";
            q.RequiredFields = new List<MetaDataField>();

            StringAssert.Contains("?q=Meta", q.ConstructQuery());
            StringAssert.DoesNotContain("&requiredfields=", q.ConstructQuery());

            q.RequiredFields.Add(new MetaDataField { Key = "department", Value = "Human Resources", MetaDataSearchSpecification = MetaDataSearchSpecification.And });
            q.RequiredFields.Add(new MetaDataField { Key = "metakey2", Value = "metavalue2" });
            StringAssert.Contains("&requiredfields=department:Human%2520Resources.metakey2:metavalue2", q.ConstructQuery());

        }

    }
}
