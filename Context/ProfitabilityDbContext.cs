using CoreWebAPIs.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace CoreWebAPIs.Context
{
    public partial class ProfitabilityDbContext : DbContext
    {
        public ProfitabilityDbContext()
        {
        }

        public ProfitabilityDbContext(DbContextOptions<ProfitabilityDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<RvChatbotM> RvChatbotMs { get; set; }

        //        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        //            => optionsBuilder.UseOracle("Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=SGOLDBKVY1.GULFAIRSTG.LOC)(PORT=1521))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=GFBCDD)));User ID=GFCPM;Password=RouteGF#787;");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasDefaultSchema("GFCPM")
                .UseCollation("USING_NLS_COMP");

            modelBuilder.Entity<RvChatbotM>(entity =>
            {
                entity
                    .HasNoKey()
                    .ToView("RV_CHATBOT_M");

                entity.Property(e => e.Acreg)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("ACREG");
                entity.Property(e => e.Arr)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("ARR");
                entity.Property(e => e.Asks)
                    .HasColumnType("NUMBER")
                    .HasColumnName("ASKS");
                entity.Property(e => e.Cargonc)
                    .HasColumnType("NUMBER")
                    .HasColumnName("CARGONC");
                entity.Property(e => e.Cargorev)
                    .HasColumnType("NUMBER")
                    .HasColumnName("CARGOREV");
                entity.Property(e => e.Cargowt)
                    .HasColumnType("NUMBER")
                    .HasColumnName("CARGOWT");
                entity.Property(e => e.Dep)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("DEP");
                entity.Property(e => e.Dircost)
                    .HasColumnType("NUMBER")
                    .HasColumnName("DIRCOST");
                entity.Property(e => e.Eqpcode)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("EQPCODE");
                entity.Property(e => e.Fltno)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("FLTNO");
                entity.Property(e => e.Fltseqnr)
                    .HasColumnType("NUMBER")
                    .HasColumnName("FLTSEQNR");
                entity.Property(e => e.Fuelburn)
                    .HasColumnType("NUMBER")
                    .HasColumnName("FUELBURN");
                entity.Property(e => e.Indcost)
                    .HasColumnType("NUMBER")
                    .HasColumnName("INDCOST");
                entity.Property(e => e.Lclfltdt)
                    .HasColumnType("DATE")
                    .HasColumnName("LCLFLTDT");
                entity.Property(e => e.Localfltdate)
                    .HasColumnType("DATE")
                    .HasColumnName("LOCALFLTDATE");
                entity.Property(e => e.MnFuelCost)
                    .HasColumnType("NUMBER")
                    .HasColumnName("MN_FUEL_COST");
                entity.Property(e => e.Nonrevpaxj)
                    .HasColumnType("NUMBER")
                    .HasColumnName("NONREVPAXJ");
                entity.Property(e => e.Nonrevpaxy)
                    .HasColumnType("NUMBER")
                    .HasColumnName("NONREVPAXY");
                entity.Property(e => e.Otherrev)
                    .HasColumnType("NUMBER")
                    .HasColumnName("OTHERREV");
                entity.Property(e => e.Paxj)
                    .HasColumnType("NUMBER")
                    .HasColumnName("PAXJ");
                entity.Property(e => e.Paxnc)
                    .HasColumnType("NUMBER")
                    .HasColumnName("PAXNC");
                entity.Property(e => e.Paxrev)
                    .HasColumnType("NUMBER")
                    .HasColumnName("PAXREV");
                entity.Property(e => e.Paxrevj)
                    .HasColumnType("NUMBER")
                    .HasColumnName("PAXREVJ");
                entity.Property(e => e.Paxrevnoexb)
                    .HasColumnType("NUMBER")
                    .HasColumnName("PAXREVNOEXB");
                entity.Property(e => e.Paxrevy)
                    .HasColumnType("NUMBER")
                    .HasColumnName("PAXREVY");
                entity.Property(e => e.Paxy)
                    .HasColumnType("NUMBER")
                    .HasColumnName("PAXY");
                entity.Property(e => e.Paxyqj)
                    .HasColumnType("NUMBER")
                    .HasColumnName("PAXYQJ");
                entity.Property(e => e.Paxyqy)
                    .HasColumnType("NUMBER")
                    .HasColumnName("PAXYQY");
                entity.Property(e => e.Revflag)
                    .HasColumnType("NUMBER")
                    .HasColumnName("REVFLAG");
                entity.Property(e => e.RouteName)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("ROUTE_NAME");
                entity.Property(e => e.Rpks)
                    .HasColumnType("NUMBER")
                    .HasColumnName("RPKS");
                entity.Property(e => e.Schdest)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("SCHDEST");
                entity.Property(e => e.Schorg)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("SCHORG");
                entity.Property(e => e.Tktpaxj)
                    .HasColumnType("NUMBER")
                    .HasColumnName("TKTPAXJ");
                entity.Property(e => e.Tktpaxy)
                    .HasColumnType("NUMBER")
                    .HasColumnName("TKTPAXY");
                entity.Property(e => e.Totcost)
                    .HasColumnType("NUMBER")
                    .HasColumnName("TOTCOST");
                entity.Property(e => e.Totrev)
                    .HasColumnType("NUMBER")
                    .HasColumnName("TOTREV");
                entity.Property(e => e.Varcost)
                    .HasColumnType("NUMBER")
                    .HasColumnName("VARCOST");
                entity.Property(e => e.WBlockTime)
                    .HasPrecision(4)
                    .HasColumnName("W_BLOCK_TIME");
                entity.Property(e => e.WFlightTime)
                    .HasPrecision(4)
                    .HasColumnName("W_FLIGHT_TIME");
                entity.Property(e => e.WFltSector)
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .HasColumnName("W_FLT_SECTOR");
            });
            modelBuilder.HasSequence("SEQ_DAILY");
            modelBuilder.HasSequence("SEQ_TMPLS");

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
