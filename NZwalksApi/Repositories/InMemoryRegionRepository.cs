using NZWalks.API.Models.Domain;
using System.Collections.Generic;
namespace NZWalks.API.Repositories
{
    public class InMemoryRegionRepository : IRegionRepository
    {
        public Task<List<Region>> GetAllAsync()
        {
            return List<Region>
             {
                new Region()
                {
                    Id = Guid.NewGuid(),
                    Code = "Sam",
                    Name = "Samoa",
                }
            }
        }
    } 
 
}