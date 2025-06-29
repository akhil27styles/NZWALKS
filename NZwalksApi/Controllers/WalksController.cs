using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZwalksApi.Models.Domain;
using NZwalksApi.Models.DTO;
using NZwalksApi.Repositories;

namespace NZwalksApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : Controller
    {
        private readonly IMapper mapper;
        private readonly IWalkRepository walkRepository;

        public WalksController(IMapper mapper, IWalkRepository walkRepository)
        {
            this.mapper = mapper;
            this.walkRepository = walkRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddWalkRequest addWalkRequest)
        {

                //Map DTO to Domain Model
            var walkDomainModel = mapper.Map<Walk>(addWalkRequest);
            await walkRepository.CreateAsync(walkDomainModel);
            return Ok(mapper.Map<WalkDto>(walkDomainModel));
        }

        // Get : /api/walks?filterOn=Name&filterQuery=Mountain&sortBy=LengthInKm&isAscending=true&pageNumber=1&pageSize=10
        [HttpGet]

        public async Task<IActionResult> GetAll([FromQuery] string? filterOn, 
            [FromQuery] string? filterQuery , [FromQuery] string? sortBy, [FromQuery] bool? isAscending,
            [FromQuery] int pageNumber =1, [FromQuery] int pageSize = 1000)
        {
            var walksDomain = await walkRepository.GetAllAsync(filterOn,filterQuery, sortBy, isAscending ?? true ,pageNumber , pageSize);
            var walksDto = mapper.Map<List<WalkDto>>(walksDomain);
            return Ok(walksDto);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var walkDomain = await walkRepository.GetByIdAsync(id);
            if(walkDomain == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<WalkDto>(walkDomain));
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateWalkRequest updateWalkRequest)
        {

                // Map DTO to Domain Model
                var walkDomainModel = mapper.Map<Walk>(updateWalkRequest);
                walkDomainModel.Id = id; // Ensure the ID is set for the update operation
                var updatedWalk = await walkRepository.UpdateAsync(id, walkDomainModel);
                if (updatedWalk == null)
                {
                    return NotFound();
                }
                return Ok(mapper.Map<WalkDto>(updatedWalk));
    
        }
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var deletedWalk = await walkRepository.DeleteAsync(id);
            if (deletedWalk == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<WalkDto>(deletedWalk));
        }
    }
}
