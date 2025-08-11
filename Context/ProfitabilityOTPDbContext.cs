using CoreWebAPIs.Models;
using Microsoft.EntityFrameworkCore;

namespace CoreWebAPIs.Context
{
    public partial class ProfitabilityOTPDbContext : DbContext
    {
        public ProfitabilityOTPDbContext() { }
        public ProfitabilityOTPDbContext(DbContextOptions<ProfitabilityOTPDbContext> options)
      : base(options)
        {
        }

        public virtual DbSet<VwotpFlight> VwotpFlights { get; set; }

        public virtual DbSet<VwotpQpax> VwotpQpaxes { get; set; }

        public virtual DbSet<VwotpQregularity> VwotpQregularities { get; set; }

      
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasDefaultSchema("GFOTP")
                .UseCollation("USING_NLS_COMP");

            modelBuilder.Entity<VwotpQpax>(entity =>
            {
                entity
                    .HasNoKey()
                    .ToView("VWOTP_QPAX");

                entity.Property(e => e.Adult)
                    .HasPrecision(3)
                    .HasColumnName("ADULT");
                entity.Property(e => e.Asks)
                    .HasColumnType("NUMBER")
                    .HasColumnName("ASKS");
                entity.Property(e => e.Bookedj)
                    .HasColumnType("NUMBER")
                    .HasColumnName("BOOKEDJ");
                entity.Property(e => e.Bookedy)
                    .HasColumnType("NUMBER")
                    .HasColumnName("BOOKEDY");
                entity.Property(e => e.Business)
                    .HasPrecision(3)
                    .HasColumnName("BUSINESS");
                entity.Property(e => e.Child)
                    .HasPrecision(3)
                    .HasColumnName("CHILD");
                entity.Property(e => e.Coach)
                    .HasPrecision(3)
                    .HasColumnName("COACH");
                entity.Property(e => e.First)
                    .HasPrecision(3)
                    .HasColumnName("FIRST");
                entity.Property(e => e.FltDt)
                    .HasColumnType("DATE")
                    .HasColumnName("FLT_DT");
                entity.Property(e => e.FltSeqNr)
                    .HasPrecision(10)
                    .HasColumnName("FLT_SEQ_NR");
                entity.Property(e => e.Infant)
                    .HasPrecision(3)
                    .HasColumnName("INFANT");
                entity.Property(e => e.Jseatconfig)
                    .HasColumnType("NUMBER")
                    .HasColumnName("JSEATCONFIG");
                entity.Property(e => e.Rpks)
                    .HasColumnType("NUMBER")
                    .HasColumnName("RPKS");
                entity.Property(e => e.Seatconfig)
                    .HasColumnType("NUMBER")
                    .HasColumnName("SEATCONFIG");
                entity.Property(e => e.Total)
                    .HasPrecision(3)
                    .HasColumnName("TOTAL");
                entity.Property(e => e.Yseatconfig)
                    .HasColumnType("NUMBER")
                    .HasColumnName("YSEATCONFIG");
            });

            modelBuilder.Entity<VwotpQregularity>(entity =>
            {
                entity
                    .HasNoKey()
                    .ToView("VWOTP_QREGULARITY");

                entity.Property(e => e.ActualPax)
                    .HasColumnType("NUMBER")
                    .HasColumnName("ACTUAL_PAX");
                entity.Property(e => e.BookPax)
                    .HasColumnType("NUMBER")
                    .HasColumnName("BOOK_PAX");
                entity.Property(e => e.CnclDate)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("CNCL_DATE");
                entity.Property(e => e.Comments)
                    .HasMaxLength(29)
                    .IsUnicode(false)
                    .HasColumnName("COMMENTS");
                entity.Property(e => e.FPax)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("F_PAX");
                entity.Property(e => e.FltNr)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("FLT_NR");
                entity.Property(e => e.FltSeqNr)
                    .HasColumnType("NUMBER")
                    .HasColumnName("FLT_SEQ_NR");
                entity.Property(e => e.JPax)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("J_PAX");
                entity.Property(e => e.LatestEqpCd)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("LATEST_EQP_CD");
                entity.Property(e => e.LatestTailNr)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("LATEST_TAIL_NR");
                entity.Property(e => e.Loaddate)
                    .HasColumnType("DATE")
                    .HasColumnName("LOADDATE");
                entity.Property(e => e.PubDepDt)
                    .HasColumnType("DATE")
                    .HasColumnName("PUB_DEP_DT");
                entity.Property(e => e.RegDesc)
                    .HasMaxLength(17)
                    .IsUnicode(false)
                    .HasColumnName("REG_DESC");
                entity.Property(e => e.RegFltsComments)
                    .IsUnicode(false)
                    .HasColumnName("REG_FLTS_COMMENTS");
                entity.Property(e => e.RegFltsRegCd)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("REG_FLTS_REG_CD");
                entity.Property(e => e.Route)
                    .HasMaxLength(7)
                    .IsUnicode(false)
                    .HasColumnName("ROUTE");
                entity.Property(e => e.YPax)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("Y_PAX");
            });

            modelBuilder.Entity<VwotpFlight>(entity =>
            {
                entity
                    .HasNoKey()
                    .ToView("VWOTP_FLIGHTS");

                entity.Property(e => e.ActualAirborne)
                    .HasColumnType("DATE")
                    .HasColumnName("ACTUAL_AIRBORNE");
                entity.Property(e => e.ActualArvArpCd)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("ACTUAL_ARV_ARP_CD");
                entity.Property(e => e.ActualArvDt)
                    .HasColumnType("DATE")
                    .HasColumnName("ACTUAL_ARV_DT");
                entity.Property(e => e.ActualDepArpCd)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("ACTUAL_DEP_ARP_CD");
                entity.Property(e => e.ActualDepDt)
                    .HasColumnType("DATE")
                    .HasColumnName("ACTUAL_DEP_DT");
                entity.Property(e => e.ActualDivArpCd)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("ACTUAL_DIV_ARP_CD");
                entity.Property(e => e.ActualLanding)
                    .HasColumnType("DATE")
                    .HasColumnName("ACTUAL_LANDING");
                entity.Property(e => e.ActualOffblocks)
                    .HasColumnType("DATE")
                    .HasColumnName("ACTUAL_OFFBLOCKS");
                entity.Property(e => e.ActualOnblocks)
                    .HasColumnType("DATE")
                    .HasColumnName("ACTUAL_ONBLOCKS");
                entity.Property(e => e.AlnCd)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("ALN_CD");
                entity.Property(e => e.Arv15delay)
                    .HasColumnType("NUMBER")
                    .HasColumnName("ARV15DELAY");
                entity.Property(e => e.Arv15delaycount)
                    .HasColumnType("NUMBER")
                    .HasColumnName("ARV15DELAYCOUNT");
                entity.Property(e => e.ArvArpCd)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("ARV_ARP_CD");
                entity.Property(e => e.Arvdelay)
                    .HasColumnType("NUMBER")
                    .HasColumnName("ARVDELAY");
                entity.Property(e => e.Arvregion)
                    .IsUnicode(false)
                    .HasColumnName("ARVREGION");
                entity.Property(e => e.CnclCd)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("CNCL_CD");
                entity.Property(e => e.CrewDepDt)
                    .HasColumnType("DATE")
                    .HasColumnName("CREW_DEP_DT");
                entity.Property(e => e.Dep15delay)
                    .HasColumnType("NUMBER")
                    .HasColumnName("DEP15DELAY");
                entity.Property(e => e.Dep15delaycount)
                    .HasColumnType("NUMBER")
                    .HasColumnName("DEP15DELAYCOUNT");
                entity.Property(e => e.DepArpCd)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("DEP_ARP_CD");
                entity.Property(e => e.Depdelay)
                    .HasColumnType("NUMBER")
                    .HasColumnName("DEPDELAY");
                entity.Property(e => e.Depregion)
                    .IsUnicode(false)
                    .HasColumnName("DEPREGION");
                entity.Property(e => e.DivSuffix)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("DIV_SUFFIX");
                entity.Property(e => e.DlyCd1)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasColumnName("DLY_CD1");
                entity.Property(e => e.DlyCd10)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasColumnName("DLY_CD10");
                entity.Property(e => e.DlyCd2)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasColumnName("DLY_CD2");
                entity.Property(e => e.DlyCd3)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasColumnName("DLY_CD3");
                entity.Property(e => e.DlyCd4)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasColumnName("DLY_CD4");
                entity.Property(e => e.DlyCd5)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasColumnName("DLY_CD5");
                entity.Property(e => e.DlyCd6)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasColumnName("DLY_CD6");
                entity.Property(e => e.DlyCd7)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasColumnName("DLY_CD7");
                entity.Property(e => e.DlyCd8)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasColumnName("DLY_CD8");
                entity.Property(e => e.DlyCd9)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasColumnName("DLY_CD9");
                entity.Property(e => e.DlyRemark1)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("DLY_REMARK1");
                entity.Property(e => e.DlyRemark10)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("DLY_REMARK10");
                entity.Property(e => e.DlyRemark2)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("DLY_REMARK2");
                entity.Property(e => e.DlyRemark3)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("DLY_REMARK3");
                entity.Property(e => e.DlyRemark4)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("DLY_REMARK4");
                entity.Property(e => e.DlyRemark5)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("DLY_REMARK5");
                entity.Property(e => e.DlyRemark6)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("DLY_REMARK6");
                entity.Property(e => e.DlyRemark7)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("DLY_REMARK7");
                entity.Property(e => e.DlyRemark8)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("DLY_REMARK8");
                entity.Property(e => e.DlyRemark9)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("DLY_REMARK9");
                entity.Property(e => e.DlyTm1)
                    .HasColumnType("NUMBER")
                    .HasColumnName("DLY_TM1");
                entity.Property(e => e.DlyTm10)
                    .HasColumnType("NUMBER")
                    .HasColumnName("DLY_TM10");
                entity.Property(e => e.DlyTm2)
                    .HasColumnType("NUMBER")
                    .HasColumnName("DLY_TM2");
                entity.Property(e => e.DlyTm3)
                    .HasColumnType("NUMBER")
                    .HasColumnName("DLY_TM3");
                entity.Property(e => e.DlyTm4)
                    .HasColumnType("NUMBER")
                    .HasColumnName("DLY_TM4");
                entity.Property(e => e.DlyTm5)
                    .HasColumnType("NUMBER")
                    .HasColumnName("DLY_TM5");
                entity.Property(e => e.DlyTm6)
                    .HasColumnType("NUMBER")
                    .HasColumnName("DLY_TM6");
                entity.Property(e => e.DlyTm7)
                    .HasColumnType("NUMBER")
                    .HasColumnName("DLY_TM7");
                entity.Property(e => e.DlyTm8)
                    .HasColumnType("NUMBER")
                    .HasColumnName("DLY_TM8");
                entity.Property(e => e.DlyTm9)
                    .HasColumnType("NUMBER")
                    .HasColumnName("DLY_TM9");
                entity.Property(e => e.FltDt)
                    .HasColumnType("DATE")
                    .HasColumnName("FLT_DT");
                entity.Property(e => e.FltManipCode)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("FLT_MANIP_CODE");
                entity.Property(e => e.FltNr)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("FLT_NR");
                //entity.Property(e => e.FltSeqNr)
                //    .HasPrecision(10)
                //    .HasColumnName("FLT_SEQ_NR");
                entity.Property(e => e.Fltcount)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasColumnName("FLTCOUNT");
                entity.Property(e => e.Fltdtlocal)
                    .HasColumnType("DATE")
                    .HasColumnName("FLTDTLOCAL");
                entity.Property(e => e.FromSkdSys)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("FROM_SKD_SYS");
                entity.Property(e => e.LatestEqpCd)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("LATEST_EQP_CD");
                entity.Property(e => e.LatestTailNr)
                    .HasMaxLength(12)
                    .IsUnicode(false)
                    .HasColumnName("LATEST_TAIL_NR");
                entity.Property(e => e.LegSeqNr)
                    .HasPrecision(10)
                    .HasColumnName("LEG_SEQ_NR");
                entity.Property(e => e.MvaActualAirborne)
                    .HasColumnType("DATE")
                    .HasColumnName("MVA_ACTUAL_AIRBORNE");
                entity.Property(e => e.MvaActualLanding)
                    .HasColumnType("DATE")
                    .HasColumnName("MVA_ACTUAL_LANDING");
                entity.Property(e => e.MvaActualOffblocks)
                    .HasColumnType("DATE")
                    .HasColumnName("MVA_ACTUAL_OFFBLOCKS");
                entity.Property(e => e.MvaActualOnblocks)
                    .HasColumnType("DATE")
                    .HasColumnName("MVA_ACTUAL_ONBLOCKS");
                entity.Property(e => e.OpFrtg)
                    .IsUnicode(false)
                    .HasColumnName("OP_FRTG");
                entity.Property(e => e.OpSuffix)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("OP_SUFFIX");
                entity.Property(e => e.PubArvDt)
                    .HasColumnType("DATE")
                    .HasColumnName("PUB_ARV_DT");
                entity.Property(e => e.PubDepDt)
                    .HasColumnType("DATE")
                    .HasColumnName("PUB_DEP_DT");
                entity.Property(e => e.SchArvDt)
                    .HasColumnType("DATE")
                    .HasColumnName("SCH_ARV_DT");
                entity.Property(e => e.SchDepDt)
                    .HasColumnType("DATE")
                    .HasColumnName("SCH_DEP_DT");
                entity.Property(e => e.ScheduledEqpCd)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("SCHEDULED_EQP_CD");
                entity.Property(e => e.TaxiInTm)
                    .HasPrecision(4)
                    .HasColumnName("TAXI_IN_TM");
                entity.Property(e => e.TaxiOutTm)
                    .HasPrecision(4)
                    .HasColumnName("TAXI_OUT_TM");
                entity.Property(e => e.TotFltTm)
                    .HasPrecision(8)
                    .HasColumnName("TOT_FLT_TM");
            });
            modelBuilder.HasSequence("ASMSEQ");
            modelBuilder.HasSequence("BACSEQ");
            modelBuilder.HasSequence("LINKFLTS");
            modelBuilder.HasSequence("TDMSOTPSEQ");
            modelBuilder.HasSequence("TEST");
            modelBuilder.HasSequence("XMLSEQ");

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
    
}
