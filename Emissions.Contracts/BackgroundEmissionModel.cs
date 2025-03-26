namespace Emissions.Contracts
{
    public class BackgroundEmissionModel
    {
        public string Customer { get; set; }
        public long CustomerId { get; set; }
        public long FacilityId { get; set; }
        public string FacilityCode { get; set; }
        public string Commodity { get; set; }
        public string Month { get; set; }
        public int? Scope { get; set; }

        public string LocationBasedProfile1 { get; set; }
        public string LocationBasedProfile2 { get; set; }
        public string LocationBasedProfile3 { get; set; }
        public EmissionValuesDto LocationBasedEmissions { get; set; }

        public string MarketBasedProfile1 { get; set; }
        public string MarketBasedProfile2 { get; set; }
        public string MarketBasedProfile3 { get; set; }
        public EmissionValuesDto MarketBasedEmissions { get; set; }
    }
}
