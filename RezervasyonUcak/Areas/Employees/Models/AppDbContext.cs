using Microsoft.EntityFrameworkCore;
using RezervasyonUcak.Models;

namespace RezervasyonUcak.Areas.Employees.Models


{
    public class AppDbContext : DbContext
    {


        public AppDbContext()
        {
        }

        protected readonly IConfiguration Configuration;
        public AppDbContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(Configuration.GetConnectionString("MyWebApiConnection"));
        }

        public Task CreateAsync(Musteri user)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Musteri user)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Musteri user)
        {
            throw new NotImplementedException();
        }

        public Task<Musteri> FindByIdAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<Musteri> FindByNameAsync(string userName)
        {
            throw new NotImplementedException();
        }

        public DbSet<Ucak> Ucak { get; set; }
        public DbSet<Bilet> Bilets { get; set; }
		public DbSet<UcusTarih> Tarih { get; set; }

        public DbSet<Koltuk> Koltuk { get; set; }

        public DbSet<Firma> Firma { get; set; }
        public DbSet<Musteri> Musteri { get; set; }
        public DbSet<UcusSefer> UcusSefers { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UcusKonum> UcusKonum { get; set; }

    }
}
