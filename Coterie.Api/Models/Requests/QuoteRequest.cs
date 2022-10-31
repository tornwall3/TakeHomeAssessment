namespace Coterie.Api.Models.Requests
{
    public class QuoteRequest
    {
        public string Business { get; set; }
        public float Revenue { get; set; }
        public string[] States { get; set; }
    }
}