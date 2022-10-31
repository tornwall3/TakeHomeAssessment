using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Coterie.Api.Interfaces;

namespace Coterie.Api.Services
{
    public class StateLookupService : IStateLookupService
    {
        protected string[] ValidStates { get; set; }
        protected Dictionary<string, string> StateNameToAbbreviation { get; } = new Dictionary<string, string>()
        {
            {"TEXAS", "TX" },
            {"FLORIDA", "FL" },
            {"OHIO", "OH" }
        };

        public StateLookupService()
        {
            ValidStates = new string[StateNameToAbbreviation.Count];
            StateNameToAbbreviation.Values.CopyTo(ValidStates, 0);
        }

        public string[] LookupStateNames(string[] stateInputs)
        {
            var stateResults = stateInputs.Select(state => StateNameToAbbreviation.GetValueOrDefault(state.ToUpperInvariant(), state.ToUpperInvariant()));

            StringBuilder invalidStates = null;
            foreach(var state in stateResults)
            {
                if (!ValidStates.Contains(state))
                {
                    if (invalidStates == null)
                    {
                        invalidStates = new StringBuilder();
                        invalidStates.Append(state);
                    }
                    else
                    {
                        invalidStates.Append(", ");
                        invalidStates.Append(state);
                    }
                }
            }

            if (invalidStates != null)
            {
                throw new ArgumentException(string.Format("Invalid states in request: {0}", invalidStates.ToString()));
            }

            return stateResults.ToArray();
        }

        public float LookupStatePremiumFactor(string stateCode) => stateCode switch
        {
            "TX" => 0.943f,
            "FL" => 1.2f,
            "OH" => 1.0f,
            _ => throw new ArgumentException(string.Format("Invalid state in request: {0}", stateCode))
        };
        
    }
}