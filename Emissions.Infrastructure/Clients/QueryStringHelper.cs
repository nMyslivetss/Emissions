using Emissions.Contracts;
using System.Web;

namespace Emissions.Infrastructure.Clients
{
    public static class QueryStringHelper
    {
        public static string Build(EmissionRequestDto request)
        {
            var query = HttpUtility.ParseQueryString(string.Empty);

            foreach (var val in request.CustomerName)
                query.Add("CustomerName", val);

            foreach (var val in request.CustomerId)
                query.Add("CustomerId", val.ToString());

            foreach (var val in request.FacilityId)
                query.Add("FacilityId", val.ToString());

            foreach (var val in request.FacilityCode)
                query.Add("FacilityCode", val);

            foreach (var val in request.Commodity)
                query.Add("Commodity", val);

            query.Add("PeriodStart", request.PeriodStart);

            if (!string.IsNullOrWhiteSpace(request.PeriodEnd))
                query.Add("PeriodEnd", request.PeriodEnd);

            return "?" + query.ToString();
        }
    }
}
