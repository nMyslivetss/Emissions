using Microsoft.AspNetCore.Mvc;
using Emissions.Contracts;
using Emissions.Application.Interfaces;
using AutoMapper;

namespace Emissions.API.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/emissions")]
    public class EmissionsController : ControllerBase
    {
        private readonly IEmissionService emissionService;
        private readonly IMapper mapper;

        public EmissionsController(IEmissionService emissionService, IMapper mapper)
        {
            this.emissionService = emissionService;
            this.mapper = mapper;
        }


        [HttpGet]
        [MapToApiVersion("1.0")]
        [MapToApiVersion("2.0")]
        public async Task<IActionResult> Get([FromQuery] EmissionRequestDto request)
        {
            var emissions = await emissionService.GetEmissionsAsync(request);
            return Ok(emissions);
        }
    }


}
