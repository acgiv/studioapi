using API.Model.Domain;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class ApiDbContext : DbContext
    {
        public ApiDbContext(DbContextOptions dbContextOptions): base(dbContextOptions)
        {
            
        }

        // scriviamo la collezione delle tabelle 
        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Region> Regions { get; set; }

        public DbSet<Walks> Walks { get; set; }

    }
}
