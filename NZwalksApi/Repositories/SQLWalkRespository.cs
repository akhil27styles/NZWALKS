﻿using Microsoft.EntityFrameworkCore;
using NZwalksApi.Data;
using NZwalksApi.Models.Domain;

namespace NZwalksApi.Repositories
{
    public class SQLWalkRespository : IWalkRepository
    {
        private readonly NZWalksDbContext dbContext;
        public SQLWalkRespository(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Walk> CreateAsync(Walk walk)
        {
            await dbContext.Walks.AddAsync(walk);
            await dbContext.SaveChangesAsync();
            return walk;
        }
        public async Task<List<Walk>> GetAllAsync(string? filterOn = null, string? filterQuery = null,
            string? sortBy = null, bool isAscending = true ,
            int pageNumber = 1, int pageSize = 1000)
        {


            var walks = dbContext.Walks.Include("Difficulty")
                                         .Include("Region")
                                         .AsQueryable();
            //Filtering logic

            if (string.IsNullOrWhiteSpace(filterOn) == false &&
                string.IsNullOrWhiteSpace(filterQuery) == false)
            {
                if (filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = walks.Where(x => x.Name.Contains(filterQuery, StringComparison.OrdinalIgnoreCase));
                }
            }

            //sorting logic

            if (string.IsNullOrWhiteSpace(sortBy) == false)
            {
                if (sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = isAscending ?
                        walks.OrderBy(x => x.Name) :
                        walks.OrderByDescending(x => x.Name);
                }
                else if (sortBy.Equals("LengthInKm", StringComparison.OrdinalIgnoreCase))
                {
                    walks = isAscending ?
                        walks.OrderBy(x => x.LengthInKm) :
                        walks.OrderByDescending(x => x.LengthInKm);
                }

            }

            // Pagination logic
            var skipResults = (pageNumber - 1) * pageSize;

            return await walks.Skip(skipResults)
                                       .Take(pageSize)
                                       .ToListAsync();

           // return await walks.ToListAsync();
        }

        public async Task<Walk?> GetByIdAsync(Guid id)
        {
            return await dbContext.Walks.Include("Difficulty")
                                           .Include("Region")
                                           .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Walk?> UpdateAsync(Guid id, Walk walk)
        {
            var existingWalk = await dbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);
            if (existingWalk == null)
            {
                return null;
            }
            existingWalk.Name = walk.Name;
            existingWalk.LengthInKm = walk.LengthInKm;
            existingWalk.RegionId = walk.RegionId;
            existingWalk.DifficultyId = walk.DifficultyId;
            existingWalk.Description = walk.Description;
            existingWalk.WalkImageUrl = walk.WalkImageUrl;

            await dbContext.SaveChangesAsync();
            return existingWalk;
        }
        public async Task<Walk?> DeleteAsync(Guid id)
        {
            var existingWalk = await dbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);
            if (existingWalk == null)
            {
                return null;
            }
            dbContext.Walks.Remove(existingWalk);
            await dbContext.SaveChangesAsync();
            return existingWalk;
        }
    }
}
