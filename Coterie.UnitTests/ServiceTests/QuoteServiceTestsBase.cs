using Coterie.Api.Interfaces;
using Coterie.Api.Services;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;

namespace Coterie.UnitTests
{
    public class QuoteServiceTestsBase
    {
        protected Dictionary<string, float> DefaultStateLookups { get; } = new Dictionary<string, float>()
        {
            { "OH", 1.0f },
            { "FL", 1.2f },
            { "TX", 0.943f }
        };

        protected QuoteService QuoteService;
        protected Mock<IStateLookupService> MockStateLookupService;

        [OneTimeSetUp]
        public void BaseOneTimeSetup()
        {
            MockStateLookupService = new Mock<IStateLookupService>();
            QuoteService = new QuoteService(MockStateLookupService.Object);
        }

        [TearDown]
        public void Cleanup()
        {
            MockStateLookupService.Reset();
        }

        protected void SetupStateLookup(Dictionary<string, float> statePremiums)
        {
            MockStateLookupService
                .Setup(p => p.LookupStateNames(It.IsAny<string[]>()))
                .Returns<string[]>(s => s);

            foreach (var entry in statePremiums)
            {
                MockStateLookupService
                    .Setup(p => p.LookupStatePremiumFactor(It.Is<string>(s => s.Equals(entry.Key))))
                    .Returns(entry.Value);
            }
        }
    }
}