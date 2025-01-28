using API.Model.Domain;


namespace API.Model.Repositories
{
    public interface RegionRepository
    {
        Task<List<Region>?> GetAllAsync();
        Task<Region?> GetbyIdAsync(Guid Id);

        Task<Region> insertRegionAsync(Region region);

        Task<Region?> putRegionAsync(Guid Id, Region region);

        Task<Region?> delateRegionAsync(Guid Id);


    }
}
