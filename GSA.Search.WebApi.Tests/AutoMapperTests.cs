using System;
using GSA.Search.WebApi.App_Start;
using AutoMapper;
using NUnit.Framework;

namespace GSA.Search.WebApi.Tests
{
    [TestFixture]
    public class AutoMapperTests
    {
        [Test]
        public void AutoMapperConfigurationShouldBeValid()
        {
            MappingConfig.ConfigureMappings();

            Assert.DoesNotThrow(Mapper.AssertConfigurationIsValid);
        }
    }
}
