using Microsoft.AspNetCore.Mvc;
using Emissions.Contracts;
using Emissions.Domain.Constants;

namespace Emissions.FakeBackgroundApi.Controllers
{
    [ApiController]
    [Route("emissions")]
    public class EmissionsController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get(
            [FromQuery(Name = "CustomerName")] List<string> customerNames,
            [FromQuery(Name = "CustomerId")] List<long> customerIds,
            [FromQuery(Name = "FacilityId")] List<long> facilityIds,
            [FromQuery(Name = "FacilityCode")] List<string> facilityCodes,
            [FromQuery(Name = "Commodity")] List<string> commodities,
            [FromQuery(Name = "PeriodStart")] string periodStart,
            [FromQuery(Name = "PeriodEnd")] string? periodEnd)
        {
         

            var months = GetMonthsBetween(periodStart, periodEnd);
            var safeCustomerIds = customerIds.Any() ? customerIds : new List<long> { 1 };
            var safeCustomerNames = customerNames.Any() ? customerNames : new List<string> { "Test1" };
            var safeCommodities = commodities.Any() ? commodities : new List<string> { null };
            var safeFacilityIds = facilityIds.Any() ? facilityIds : new List<long> { 100 };
            var safeFacilityCodes = facilityCodes.Any() ? facilityCodes : new List<string> { "FC-01" };

            var result = (from customerId in safeCustomerIds
                          from customerName in safeCustomerNames
                          from facilityId in safeFacilityIds
                          from facilityCode in safeFacilityCodes
                          from commodity in safeCommodities
                          from month in months
                          let trimmedCommodity = commodity?.Trim()
                          let scope = ScopeMapper.MapScope(trimmedCommodity)
                          let withEmissions = scope != null
                          select new BackgroundEmissionModel
                          {
                              Scope = scope,
                              Customer = customerName,
                              CustomerId = customerId,
                              FacilityId = facilityId,
                              FacilityCode = facilityCode,
                              Commodity = commodity,
                              Month = month,
                              LocationBasedProfile1 = withEmissions ? "LocProfile1" : null,
                              LocationBasedProfile2 = withEmissions ? "LocProfile2" : null,
                              LocationBasedProfile3 = withEmissions ? "LocProfile3" : null,
                              LocationBasedEmissions = withEmissions ? new EmissionValuesDto
                              {
                                  CO2 = 1.1111m,
                                  CO2e = 2.2222m,
                                  CH4 = 0.3333m,
                                  N2O = 0.4444m
                              } : null,
                              MarketBasedProfile1 = withEmissions ? "MarProfile1" : null,
                              MarketBasedProfile2 = withEmissions ? "MarProfile2" : null,
                              MarketBasedProfile3 = withEmissions ? "MarProfile3" : null,
                              MarketBasedEmissions = withEmissions ? new EmissionValuesDto
                              {
                                  CO2 = 3.1111m,
                                  CO2e = 4.2222m,
                                  CH4 = 1.3333m,
                                  N2O = 1.4444m
                              } : null
                          }).ToList();

            return Ok(await Task.FromResult(result));
        }

        private List<string> GetMonthsBetween(string start, string? end)
        {
            var result = new List<string>();
            var from = DateTime.Parse(start + "-01");
            var to = string.IsNullOrWhiteSpace(end) ? from : DateTime.Parse(end + "-01");

            for (var dt = from; dt <= to; dt = dt.AddMonths(1))
                result.Add(dt.ToString("yyyy-MM"));

            return result;
        }
    }
}
