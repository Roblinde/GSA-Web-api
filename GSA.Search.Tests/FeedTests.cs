using System.Net;
using GSA.Search.Feeds;
using GSA.Search.Interfaces;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSA.Search.Tests
{
    [TestFixture]
    public class FeedTests
    {
        [Test]
        public void FeedRecordShouldReturnCorrectXml()
        {
            var record = new FeedRecord();
            record.Url = "http://www.sf.se/";
            var xml = record.ToXml();

            Assert.AreEqual("<record url=\"http://www.sf.se/\" lock=\"false\" mimetype=\"requiredbutignored\" authmethod=\"none\" crawl-immediately=\"false\" crawl-once=\"false\" />", xml);
        }

        [Test]
        public void FeedRecordWithoutUrlShouldThrowException()
        {
            var record = new FeedRecord();

            Assert.Throws<ArgumentException>(() => record.ToXml());
        }

        [Test]
        public void FeedRecordWithAllValuesSetShouldReturnCorrectXml()
        {
            var record = new FeedRecord();
            record.Url = "http://www.sf.se/";
            record.DisplayUrl = "http://www.somethingelse.se/";
            record.Action = FeedAction.delete;
            record.AuthMethod = FeedAuthorization.httpsso;
            record.CrawlImmediately = true;
            record.CrawlOnce = true;
            record.LastModified = "Mon, 15 Nov 2004 04:58:08 GMT";
            record.Lock = true;

            var xml = record.ToXml();

            Assert.AreEqual("<record url=\"http://www.sf.se/\" displayurl=\"http://www.somethingelse.se/\" action=\"delete\" lock=\"true\" mimetype=\"requiredbutignored\" last-modified=\"Mon, 15 Nov 2004 04:58:08 GMT\" authmethod=\"httpsso\" crawl-immediately=\"true\" crawl-once=\"true\" />", xml);

        }

        [Test]
        public void FeedShouldReturnCorrectXml()
        {
            var feed = new Feed();
            var record = new FeedRecord();
            record.Url = "http://www.sf.se/";

            feed.Records.Add(record);

            var xml = feed.ConstructXml();

            Assert.AreEqual("<!DOCTYPE gsafeed PUBLIC \"-//Google//DTD GSA Feeds//EN\" \"\"[]>\r\n<gsafeed>\r\n  <header>\r\n    <datasource>web</datasource>\r\n    <feedtype>incremental</feedtype>\r\n  </header>\r\n  <group>\r\n    <record url=\"http://www.sf.se/\" lock=\"false\" mimetype=\"requiredbutignored\" authmethod=\"none\" crawl-immediately=\"false\" crawl-once=\"false\" />\r\n  </group>\r\n</gsafeed>", xml);
        }

        [Test]
        public void FeedManagerShouldReturnCorrectResult()
        {
            var feed = new Feed();
            var record = new FeedRecord();
            record.Url = "http://klaratest.domain.se/Info/Nyheter/Nyhet-Test-2-Reload/";

            feed.Records.Add(record);

            var manager = new FeedManager();
            manager.GsaHostAddress = "http://google03.domain.se";

            var succedingFeedClient = new Mock<IFeedClient>();
            succedingFeedClient.Setup(m => m.PushFeed(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>())).Returns(HttpStatusCode.OK);

            manager.FeedClient = succedingFeedClient.Object;

            var result = manager.PushFeed(feed);

            Assert.AreEqual(HttpStatusCode.OK, result);
        }


        [Test]
        public void FeedManagerShouldReturn404IfFeedClientFails()
        {
            var feed = new Feed();
            var record = new FeedRecord();
            record.Url = "http://klaratest.domain.se/Info/Nyheter/Nyhet-Test-2-Reload/";

            feed.Records.Add(record);

            var manager = new FeedManager();
            manager.GsaHostAddress = "http://google03.domain.se";

            var failingFeedClient = new Mock<IFeedClient>();
            failingFeedClient.Setup(m => m.PushFeed(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>())).Returns(HttpStatusCode.NotFound);

            manager.FeedClient = failingFeedClient.Object;

            var result = manager.PushFeed(feed);

            Assert.AreEqual(HttpStatusCode.NotFound, result);
        }


        [Test]
        public void FeedManagerShouldReturnThrowIfGsaHostArgumentsAreFaulty()
        {
            var feed = new Feed();
            var record = new FeedRecord();
            record.Url = "http://www.claremont.se/test/";

            feed.Records.Add(record);

            var manager = new FeedManager();

            Assert.Throws<ArgumentException>(() => manager.PushFeed(feed));
        }

        [Test]
        public void FeedManagerShouldReturnThrowIfFeedArgumentsAreFaulty()
        {
            var manager = new FeedManager();
            manager.GsaHostAddress = "http://google03.domain.se";

            Assert.Throws<ArgumentException>(() => manager.PushFeed(null));
        }
    }
}
