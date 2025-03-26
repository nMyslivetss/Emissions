using AutoMapper;
using Emissions.Contracts;

namespace Emissions.Application.Mapping
{
    public class EmissionMappingProfile : Profile
    {
        public EmissionMappingProfile()
        {
            CreateMap<BackgroundEmissionModel, EmissionResponseDto>().ReverseMap();
        }
    }
}
