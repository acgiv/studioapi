using API.Data;
using API.Model.Domain;
using API.Model.DTO;
using Microsoft.EntityFrameworkCore;

namespace API.Model.Repositories
{
    public class RegionRepositoryImp : RegionRepository
    {
        private readonly ApiDbContext dbContext;

        public RegionRepositoryImp(ApiDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Region?> delateRegionAsync(Guid Id)
        {
            var region = await dbContext.Regions.FirstOrDefaultAsync(region => region.Id == Id);
            if (region != null)
            {
                dbContext.Regions.Remove(region);
                await dbContext.SaveChangesAsync();
            }

            return region;
        }

        public async Task<List<Region>?> GetAllAsync()
        {
            return await dbContext.Regions.ToListAsync();
        }

        public async Task<Region?> GetbyIdAsync(Guid Id)
        {
            // altro controllo 
            // dbContext.Regions.Find(id);
            //dbContext.Regions.Where(elem =>  elem.Id == id).ToList();

            return await dbContext.Regions.FirstOrDefaultAsync(elem => elem.Id == Id);

        }

        public async Task<Region> insertRegionAsync(Region region)
        {
            await dbContext.Regions.AddAsync(region);
            await dbContext.SaveChangesAsync();
            return region;
        }

        public async Task<Region?> putRegionAsync(Guid Id, Region region)
        {
            var regiondb = await dbContext.Regions.FirstOrDefaultAsync(elem => elem.Id == Id);
            if (regiondb != null) {

                regiondb.Name = region.Name;
                regiondb.Code = region.Code;
                regiondb.RegionImageUrl = region.RegionImageUrl;

                await dbContext.SaveChangesAsync();

                

            }
            return regiondb;

        }
    }
}
