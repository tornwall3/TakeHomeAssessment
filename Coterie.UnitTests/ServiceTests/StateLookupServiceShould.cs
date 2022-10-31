using System;
using System.Linq;
using NUnit.Framework;

namespace Coterie.UnitTests
{
    public class StateLookupServiceTests : StateLookupServiceTestsBase
    {
        static object[] StateNameHappyCases =
        {
            new [] { "OH" },
            new [] { "FL" },
            new [] { "TX" },
            new [] { "OH", "FL"},
            new [] { "OH", "TX"},
            new [] { "FL", "TX"},
            new [] { "OH", "FL", "TX"},
            new [] { "OHio" },
            new [] { "Florida" },
            new [] { "TEXAS" },
            new [] { "OH", "FLorida"},
            new [] { "OHio", "TX"},
            new [] { "florida", "TeXAS"},
            new [] { "OHio", "FL", "teXas"},
        };

        static object[] StateNameBadCases =
        {
            new [] { "Not Ohio" },
            new [] { "Happy Halloween!" },
            new [] { "And Merry Texmas" },
        };

        static object[] ValidStateCodeCases =
        {
            "OH",
            "FL",
            "TX"
        };

        static object[] InvalidStateCodeCases =
        {
            "CO",
            "TE",
            "RI"
        };

        [TestCaseSource(nameof(StateNameHappyCases))]
        public void ReturnListOfMappedStates(string[] states)
        {
            // Act
            var actual = StateLookupService.LookupStateNames(states);
            
            // Assert
            Assert.IsNotNull(actual);
            Assert.That(actual.ToList().Count, Is.EqualTo(states.Count()));

            foreach (var result in actual)
            {
                // State Abbreviations are all 2 letters long
                Assert.That(result.Length, Is.EqualTo(2));
            }
        }

        [TestCaseSource(nameof(StateNameBadCases))]
        public void ThrowExceptionGivenInvalidStateNames(string[] states)
        {
            Assert.Throws<ArgumentException>(() => StateLookupService.LookupStateNames(states));
        }

        [TestCaseSource(nameof(ValidStateCodeCases))]
        public void ReturnPremiumFactorForStateCodes(string stateCode)
        {
            Assert.That(StateLookupService.LookupStatePremiumFactor(stateCode), Is.Positive);
        }

        [TestCaseSource(nameof(InvalidStateCodeCases))]
        public void ThrowExceptionGivenInvalidStateCodes(string stateCode)
        {
            Assert.Throws<ArgumentException>(() => StateLookupService.LookupStatePremiumFactor(stateCode));
        }

    }
}