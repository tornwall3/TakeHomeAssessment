using System;
using System.Linq;
using Coterie.Api.Interfaces;
using Coterie.Api.Models.Requests;
using Coterie.Api.Models.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Coterie.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class QuoteController : ControllerBase
    {
        private readonly IQuoteService _quoteService;
        
        public QuoteController(IQuoteService quoteService)
        {
            _quoteService = quoteService;
        }

        [HttpPost]
        public ActionResult<ItemResponse<QuoteResponse>> Get(QuoteRequest request)
        {
            if (string.IsNullOrEmpty(request.Business))
            {
                throw new ArgumentException("Business name is empty or missing");
            }

            if (request.Revenue <= 0f)
            {
                throw new ArgumentException("Revenue is missing or not a positive value");
            }

            if (request.States == null)
            {
                throw new ArgumentException("States is missing");
            }

            if (request.States.Length == 0)
            {
                throw new ArgumentException("States is empty");
            }

            var result = _quoteService.CalculateQuote(request);
            return new ItemResponse<QuoteResponse>
            {
                Item = result
            };
        }
    }
}