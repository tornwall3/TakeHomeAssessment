namespace Coterie.Api.Models.Responses
{
    public class QuoteResponse
    {
        public string Business { get; set; }
        public float Revenue { get; set; }
        public StatePremium[] Premiums { get; set; }


        public class StatePremium
        {
            public float Premium { get; set; }
            public string State { get; set; }
        }
    }
}