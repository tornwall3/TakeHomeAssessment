using System;
using System.Collections.Generic;
using System.Linq;
using Coterie.Api.Models.Requests;
using NUnit.Framework;

namespace Coterie.UnitTests
{
    public class Tests : QuoteServiceTestsBase
    {
        [Test]
        public void ReturnQuoteForValidRequest()
        {
            QuoteRequest request = new QuoteRequest()
            {
                Business = "Plumber",
                Revenue = 6000000f,
                States = new [] { "OH", "FL", "TX" }
            };

            SetupStateLookup(DefaultStateLookups);

            var response = QuoteService.CalculateQuote(request);

            Assert.That(response.Business, Is.EqualTo(request.Business));
            Assert.That(response.Revenue, Is.EqualTo(request.Revenue));
            Assert.That(response.Premiums.Length, Is.EqualTo(request.States.Length));

            Assert.That(response.Premiums.Where(s => s.State.Equals("OH")).First().Premium, Is.EqualTo(12000f));
            Assert.That(response.Premiums.Where(s => s.State.Equals("FL")).First().Premium, Is.EqualTo(14400f));
            Assert.That(response.Premiums.Where(s => s.State.Equals("TX")).First().Premium, Is.EqualTo(11316f));
        }

        [Test]
        public void ThrowExceptionForInvalidBusinessName()
        {
            QuoteRequest request = new QuoteRequest()
            {
                Business = "Crime",
                Revenue = 6000000f,
                States = new[] { "State" }
            };

            SetupStateLookup(new Dictionary<string, float>() { { "State", 1.0f } });

            var response = QuoteService.CalculateQuote(request);

            Assert.Throws<ArgumentException>(() => QuoteService.CalculateQuote(request));
        }
    }
}