using Coterie.Api.Services;
using NUnit.Framework;

namespace Coterie.UnitTests
{
    public class StateLookupServiceTestsBase
    {
        protected StateLookupService StateLookupService;

        [OneTimeSetUp]
        public void BaseOneTimeSetup()
        {
            StateLookupService = new StateLookupService();
        }
    }
}