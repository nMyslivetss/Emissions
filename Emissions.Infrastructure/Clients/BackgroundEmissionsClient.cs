using Emissions.Contracts;
using Microsoft.Extensions.Configuration;
using System.Text.Json;

namespace Emissions.Infrastructure.Clients
{
    public class BackgroundEmissionsClient : IBackgroundEmissionsClient
    {
        private readonly HttpClient httpClient;
        private readonly IConfiguration configuration;

        public BackgroundEmissionsClient(HttpClient httpClient, IConfiguration configuration)
        {
            this.httpClient = httpClient;
            this.configuration = configuration;
        }

        public async Task<List<BackgroundEmissionModel>> FetchEmissionsAsync(EmissionRequestDto request)
        {
            var query = QueryStringHelper.Build(request);
            var baseUrl = configuration["BackgroundApi:Url"];
            var fullUrl = $"{baseUrl}{query}";

            var response = await httpClient.GetAsync(fullUrl);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            return JsonSerializer.Deserialize<List<BackgroundEmissionModel>>(content, options) ?? new();
        }
    }
}
