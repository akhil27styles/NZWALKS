using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Repositories;
using NZwalksApi.Data;
using NZwalksApi.Models.Domain;
using NZwalksApi.Models.DTO;

namespace NZwalksApi.Controllers
{

    // http://localhost:1234/api/region
    [Route("api/[controller]")]
    [ApiController]
    public class RegionController : Controller
    {
        private readonly NZWalksDbContext dbContext;
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper; // Added missing field for 'mapper'

        public RegionController(NZWalksDbContext dbContext, IRegionRepository regionRepository, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.regionRepository = regionRepository;
            this.mapper = mapper; // Fixed CS1061: Added assignment for 'mapper'
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var regionsDomain = await regionRepository.GetAllAsync();
            var regionsDto = mapper.Map<List<RegionDto>>(regionsDomain); // Fixed CS0103: 'mapper' now exists in context
            return Ok(regionsDto);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var regionDomain = await regionRepository.GetByIdAsync(id);
            if (regionDomain == null) // Fixed CS0162: Added null check
            {
                return NotFound();
            }
            return Ok(mapper.Map<RegionDto>(regionDomain)); // Fixed CS1026: Corrected syntax
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddRegionRequest addRegionDto)
        {
            var regionDomainModel = mapper.Map<Region>(addRegionDto); // Fixed CS0246: Corrected type name to 'Region'
            regionDomainModel = await regionRepository.CreateAsync(regionDomainModel);
            var regionDto = mapper.Map<RegionDto>(regionDomainModel); // Fixed CS0103: 'mapper' now exists in context
            return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequest updateRegionRequest)
        {
            var regionDomainModel = mapper.Map<Region>(updateRegionRequest); // Fixed CS0246: Corrected type name to 'Region'
            regionDomainModel = await regionRepository.UpdateAsync(id, regionDomainModel);

            if (regionDomainModel == null) // Fixed CS0019: Corrected null comparison
            {
                return NotFound();
            }

            var regionDto = mapper.Map<RegionDto>(regionDomainModel); // Fixed CS0103: 'mapper' now exists in context
            return Ok(regionDto);
        }

        [HttpDelete]
        [Route("{id:guid}")]
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
