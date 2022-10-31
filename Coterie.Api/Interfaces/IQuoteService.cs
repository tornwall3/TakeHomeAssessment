using Coterie.Api.Models.Requests;
using Coterie.Api.Models.Responses;

namespace Coterie.Api.Interfaces
{
    public interface IQuoteService
    {
        QuoteResponse CalculateQuote(QuoteRequest quoteRequest);
    }
}