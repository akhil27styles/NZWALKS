using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZwalksApi.Repositories;
using NZwalksApi.Data;
using NZwalksApi.Models.Domain;
using NZwalksApi.Models.DTO;
using NZwalksApi.CustomActionFilters;
using Microsoft.AspNetCore.Authorization;
using System.Text.Json;

namespace NZwalksApi.Controllers
{

    // http://localhost:1234/api/region
    [Route("api/[controller]")]
    [ApiController]
  
    public class RegionController : Controller
    {
        private readonly NZWalksDbContext dbContext;
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;
        private readonly ILogger<RegionController> logger;

        public RegionController(NZWalksDbContext dbContext, 
            IRegionRepository regionRepository, 
            IMapper mapper,
            ILogger<RegionController> logger)
        {
            this.dbContext = dbContext;
            this.regionRepository = regionRepository;
            this.mapper = mapper;
            this.logger = logger;
        }

        [HttpGet]
       // [Authorize(Roles = "Reader,Writer")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                logger.LogInformation("Getting all regions from the database");
                var regionsDomain = await regionRepository.GetAllAsync();

                //Return to DTO
                logger.LogInformation($"Finished GetAllRegions request with data: {JsonSerializer.Serialize(regionsDomain)}");
                var regionsDto = mapper.Map<List<RegionDto>>(regionsDomain);
                return Ok(regionsDto);
            }
           
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while getting all regions");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet]
        [Route("{id:guid}")]
        [Authorize]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var regionDomain = await regionRepository.GetByIdAsync(id);
            if (regionDomain == null) 
            {
                return NotFound();
            }
            return Ok(mapper.Map<RegionDto>(regionDomain)); 
        }

        [HttpPost]
        [ValidateModel]
      //  [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Create([FromBody] AddRegionRequest addRegionDto)
        {
            
            var regionDomainModel = mapper.Map<Region>(addRegionDto);
            regionDomainModel = await regionRepository.CreateAsync(regionDomainModel);
            var regionDto = mapper.Map<RegionDto>(regionDomainModel); 
            return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);
         
        }

        [HttpPut]
        [Route("{id:guid}")]
      //  [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequest updateRegionRequest)
        {

                var regionDomainModel = mapper.Map<Region>(updateRegionRequest);
                regionDomainModel = await regionRepository.UpdateAsync(id, regionDomainModel);

                if (regionDomainModel == null)
                {
                    return NotFound();
                }

                var regionDto = mapper.Map<RegionDto>(regionDomainModel);
                return Ok(regionDto);
        }

        [HttpDelete]
        [Route("{id:guid}")]
       // [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var regionDomainModel = await regionRepository.DeleteAsync(id);

            if (regionDomainModel is null)
            {
                return NotFound();
            }

            var regionDto = mapper.Map<RegionDto>(regionDomainModel);
            return Ok(regionDto);
        }
    }
}
