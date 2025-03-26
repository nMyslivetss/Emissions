using AutoMapper;
using Emissions.Application.Interfaces;
using Emissions.Contracts;
using Emissions.Infrastructure.Clients;
using Emissions.Domain.Constants;

namespace Emissions.Application.Services
{
    public class EmissionService : IEmissionService
    {
        private readonly IBackgroundEmissionsClient backgroundEmissionsClient;
        private readonly IMapper mapper;

        public EmissionService(IBackgroundEmissionsClient backgroundEmissionsClient, IMapper mapper)
        {
            this.backgroundEmissionsClient = backgroundEmissionsClient;
            this.mapper = mapper;
        }

        public async Task<List<EmissionResponseDto>> GetEmissionsAsync(EmissionRequestDto request)
        {
            var backgroundData = await backgroundEmissionsClient.FetchEmissionsAsync(request);
            var response = mapper.Map<List<EmissionResponseDto>>(backgroundData);

            foreach (var item in response)
            {
                item.Scope = ScopeMapper.MapScope(item.Commodity);

                if (item.LocationBasedEmissions == null)
                {
                    item.LocationBasedProfile1 = null;
                    item.LocationBasedProfile2 = null;
                    item.LocationBasedProfile3 = null;
                }

                if (item.MarketBasedEmissions == null)
                {
                    item.MarketBasedProfile1 = null;
                    item.MarketBasedProfile2 = null;
                    item.MarketBasedProfile3 = null;
                }
            }

            return response;
        }
    }
}
