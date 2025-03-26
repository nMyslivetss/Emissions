using Emissions.Contracts;

namespace Emissions.Infrastructure.Clients
{
    public interface IBackgroundEmissionsClient
    {
        Task<List<BackgroundEmissionModel>> FetchEmissionsAsync(EmissionRequestDto request);
    }
}
