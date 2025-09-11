using CoreWebAPIs.Models;
using Microsoft.EntityFrameworkCore;

namespace CoreWebAPIs.Context
{
    public partial class PaxFlightsDbContext : DbContext
    {
        public PaxFlightsDbContext(DbContextOptions<PaxFlightsDbContext> options) : base(options) { }

        public virtual DbSet<PaxFlightDetail> PaxFlightDetails { get; set; }
        public virtual DbSet<VwotpFlight> VwotpFlights { get; set; }
        public virtual DbSet<VwotpQregularity> VwotpQregularities { get; set; }
        public virtual DbSet<VwotpQpax> VwotpQpaxes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PaxFlightDetail>().HasNoKey().ToView("V_PAX_ALL_DETAILS", "GFARPRTDATA");
            modelBuilder.Entity<VwotpFlight>().HasNoKey().ToView("VWOTP_FLIGHTS", "GFARPRTDATA");
            modelBuilder.Entity<VwotpQregularity>().HasNoKey().ToView("VWOTP_QREGULARITY", "GFARPRTDATA");
            modelBuilder.Entity<VwotpQpax>().HasNoKey().ToView("VWOTP_QPAX", "GFARPRTDATA");
        }
    }
}
