namespace Coterie.Api.Interfaces
{
    public interface IStateLookupService
    {
        string[] LookupStateNames(string[] stateInputs);
        float LookupStatePremiumFactor(string state);
    }
}