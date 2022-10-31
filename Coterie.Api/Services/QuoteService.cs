using System;
using System.Collections.Generic;
using System.Linq;
using Coterie.Api.Interfaces;
using Coterie.Api.Models.Requests;
using Coterie.Api.Models.Responses;

namespace Coterie.Api.Services
{
    public class QuoteService : IQuoteService
    {
        private readonly IStateLookupService stateLookupService;
        protected int HazardFactor { get; } = 4;
        protected Dictionary<string, float> BusinessFactors { get; } = new Dictionary<string, float>()
        {
            {"ARCHITECT", 1.0f },
            {"PLUMBER", 0.5f },
            {"PROGRAMMER", 1.25f }
        };


        public QuoteService(IStateLookupService stateLookupService)
        {
            this.stateLookupService = stateLookupService;
        }

        public QuoteResponse CalculateQuote(QuoteRequest quoteRequest)
        {
            QuoteResponse quoteResponse = new QuoteResponse();
            quoteResponse.Business = quoteRequest.Business;
            quoteResponse.Revenue = quoteRequest.Revenue;

            quoteResponse.Premiums = stateLookupService
                .LookupStateNames(quoteRequest.States)
                .Select(state => new QuoteResponse.StatePremium
                {
                    State = state,
                    Premium = CalculateQuoteForState(state, quoteRequest.Business, quoteRequest.Revenue)
                })
                .ToArray();

            return quoteResponse;
        }

        protected float CalculateQuoteForState(string state, string business, float revenue)
        {
            if (!BusinessFactors.TryGetValue(business.ToUpperInvariant(), out var businessFactor))
            {
                throw new ArgumentException(string.Format("Invalid business name in request: {0}", business));
            }

            return (float)Math.Round(CalculateBasePremium(revenue) * stateLookupService.LookupStatePremiumFactor(state) * businessFactor * HazardFactor, 2);
        }

        protected float CalculateBasePremium(float revenue)
        {
            return (float)Math.Ceiling(revenue / 1000f);
        }
    }
}