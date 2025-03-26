namespace Emissions.Contracts
{
    public class EmissionRequestDto
    {
        public required List<string> CustomerName { get; set; }
        public required List<long> CustomerId { get; set; }
        public List<long> FacilityId { get; set; } = new();
        public List<string> FacilityCode { get; set; } = new();
        public List<string> Commodity { get; set; } = new();
        public required string PeriodStart { get; set; }
        public string? PeriodEnd { get; set; }
    }
}
