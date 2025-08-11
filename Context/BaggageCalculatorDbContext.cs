
using CoreWebAPIs.Models;
using Microsoft.EntityFrameworkCore;

namespace CoreWebAPIs.Context
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("BaggageCalculator");

            modelBuilder.Entity<ZoneModel>()
                .HasMany(z => z.Countries)
                .WithOne(x => x.Zone);

            modelBuilder.Entity<ZoneModel>()
                .HasMany(z => z.Airports)
                .WithOne(x => x.Zone);

            modelBuilder.Entity<CountryModel>()
                .HasOne(x => x.Zone)
                .WithMany(x => x.Countries);

            modelBuilder.Entity<CountryModel>()
                .HasMany(x => x.Airports)
                .WithOne(x => x.Country);
            modelBuilder.Entity<AirportCodeModel>().HasOne(x => x.Country).WithMany(x => x.Airports);
            modelBuilder.Entity<AirportCodeModel>().HasOne(x => x.Zone).WithMany(x => x.Airports);
            modelBuilder.Entity<ClassOfServiceModel>().HasMany(x => x.FareTypes).WithOne(x => x.ClassOfService);
            modelBuilder.Entity<FareTypeModel>().HasOne(x => x.ClassOfService).WithMany(x => x.FareTypes);
            modelBuilder.Entity<FareTypeModel>().HasMany(x => x.Routes);
            modelBuilder.Entity<BaggageSensitiveDestinationsConfiguration>().HasNoKey();
            modelBuilder.Entity<ZoneToZoneFeeModel>().HasNoKey();
            modelBuilder.Entity<AdditionalInformationModel>().HasNoKey();

        }



        public DbSet<ZoneModel> Zones { get; set; }

        public DbSet<CountryModel> Countries { get; set; }

        public DbSet<ClassOfServiceModel> ClassOfServices { get; set; }

        public DbSet<FareTypeModel> FareTypes { get; set; }

        public DbSet<RouteModel> Routes{ get; set; }
        public DbSet<AirportCodeModel> Airports{ get; set; }
        public DbSet<ZoneToZoneFeeModel> ZoneToZoneFees { get; set; }

        public DbSet<BaggageSensitiveDestinationsConfiguration> BaggageSensitiveDestinationsConfigurations { get; set; }

        public DbSet<AdditionalInformationModel> AdditionalInformation{ get; set; }
    }
}
