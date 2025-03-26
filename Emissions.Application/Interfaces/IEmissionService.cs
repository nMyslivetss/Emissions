using Emissions.Contracts;

namespace Emissions.Application.Interfaces
{
    public interface IEmissionService
    {
        Task<List<EmissionResponseDto>> GetEmissionsAsync(EmissionRequestDto request);
    }
}
