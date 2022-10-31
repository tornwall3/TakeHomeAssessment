using System;
using System.Collections.Generic;
using System.Linq;
using Coterie.Api.Controllers;
using Coterie.Api.Models.Requests;
using Coterie.Api.Services;
using Moq;
using NUnit.Framework;

namespace Coterie.UnitTests
{
    public class QuoteControllerShould
    {
        protected QuoteController QuoteController;
        protected Mock<QuoteService> QuoteService;
        protected Mock<StateLookupService> StateLookupService;

        [OneTimeSetUp]
        public void BaseOneTimeSetup()
        {
            StateLookupService = new Mock<StateLookupService>();
            QuoteService = new Mock<QuoteService>(StateLookupService.Object);
            QuoteController = new QuoteController(QuoteService.Object);
        }


        static object[] RequestsWithMissingDetails =
            {
                new QuoteRequest()
                {
                    Revenue = 6000000f,
                    States = new [] { "State" }
                },
                new QuoteRequest()
                {
                    Business = "",
                    Revenue = 6000000f,
                    States = new [] { "State" }
                },
                new QuoteRequest()
                {
                    Business = "Plumber",
                    States = new [] { "State" }
                },
                new QuoteRequest()
                {
                    Business = "Plumber",
                    Revenue = 6000000f,
                },
                new QuoteRequest()
                {
                    Business = "Plumber",
                    Revenue = 6000000f,
                    States = new string[0]
                },
            };

        [TestCaseSource(nameof(RequestsWithMissingDetails))]
        public void ThrowExceptionForMissingDetails(QuoteRequest request)
        {
            Assert.Throws<ArgumentException>(() => QuoteController.Get(request));
        }
    }
}