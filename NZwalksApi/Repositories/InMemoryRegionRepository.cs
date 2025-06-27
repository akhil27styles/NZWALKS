using NZwalksApi.Models.Domain;
using NZwalksApi.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NZWalks.API.Repositories
{
    public class InMemoryRegionRepository 
    {
        public Task<List<Region>> GetAllAsync()
        {
            var regions = new List<Region>
            {
                new Region
                {
                    Id = Guid.NewGuid(),
                    Code = "SAM",
                    Name = "Samoa"
                }
            };

            return Task.FromResult(regions);
        }
    }
}
