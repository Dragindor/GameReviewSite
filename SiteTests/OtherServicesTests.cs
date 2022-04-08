using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System;

namespace SiteTests
{
    public class OtherServicesTests//rename it after one of your services for later tests
    {
        private ServiceProvider serviceProvider;
        private InMemoryDbContext dbContext;
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
    }
}

