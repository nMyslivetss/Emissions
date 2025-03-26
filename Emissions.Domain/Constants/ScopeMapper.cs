namespace Emissions.Domain.Constants
{
    public static class ScopeMapper
    {
        private static readonly Dictionary<string, int> commodityScopeMap = new(StringComparer.OrdinalIgnoreCase)
    {
        { "Natural Gas", 1 },
        { "Propane", 1 },
        { "Electric", 2 },
        { "Steam", 2 }
    };

        public static int? MapScope(string? commodity)
        {
            if (string.IsNullOrWhiteSpace(commodity))
                return null;

            return commodityScopeMap.TryGetValue(commodity, out var scope) ? scope : null;
        }

    }
}
