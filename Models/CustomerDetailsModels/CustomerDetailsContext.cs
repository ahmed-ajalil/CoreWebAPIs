using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CoreWebAPIs.Models.CustomerDetailsModels;

public partial class CustomerDetailsContext : DbContext
{
    public CustomerDetailsContext()
    {
    }

    public CustomerDetailsContext(DbContextOptions<CustomerDetailsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AcsiDxbFlight> AcsiDxbFlights { get; set; }

    public virtual DbSet<AcsiDxbManifest> AcsiDxbManifests { get; set; }

    public virtual DbSet<AcsiGcd> AcsiGcds { get; set; }

    public virtual DbSet<AcsiGcdDetail> AcsiGcdDetails { get; set; }

    public virtual DbSet<AcsiGf> AcsiGfs { get; set; }

    public virtual DbSet<AcsiGwl> AcsiGwls { get; set; }

    public virtual DbSet<AcsiGwlsCgo> AcsiGwlsCgos { get; set; }

    public virtual DbSet<AcsiGwlsLdm> AcsiGwlsLdms { get; set; }

    public virtual DbSet<AcsiInvolUpgrade> AcsiInvolUpgrades { get; set; }

    public virtual DbSet<AcsiPaxCount> AcsiPaxCounts { get; set; }

    public virtual DbSet<AcsiPaxManifest> AcsiPaxManifests { get; set; }

    public virtual DbSet<AcsiPaxaddnlManifest> AcsiPaxaddnlManifests { get; set; }

    public virtual DbSet<AcsiPending> AcsiPendings { get; set; }

    public virtual DbSet<AcsiTkt> AcsiTkts { get; set; }

    public virtual DbSet<AdmNoshow> AdmNoshows { get; set; }

    public virtual DbSet<BookingFlightTest> BookingFlightTests { get; set; }

    public virtual DbSet<CsmSsr> CsmSsrs { get; set; }

    public virtual DbSet<CsmSsr1> CsmSsr1s { get; set; }

    public virtual DbSet<CuCdgSncf> CuCdgSncfs { get; set; }

    public virtual DbSet<DxbIb> DxbIbs { get; set; }

    public virtual DbSet<EprCreate> EprCreates { get; set; }

    public virtual DbSet<EprGroup> EprGroups { get; set; }

    public virtual DbSet<EprReset> EprResets { get; set; }

    public virtual DbSet<EprUpdate> EprUpdates { get; set; }

    public virtual DbSet<EprcOption> EprcOptions { get; set; }

    public virtual DbSet<EprcStaff> EprcStaffs { get; set; }

    public virtual DbSet<ErrorList> ErrorLists { get; set; }

    public virtual DbSet<ExportTable> ExportTables { get; set; }

    public virtual DbSet<FfpFt> FfpFts { get; set; }

    public virtual DbSet<Fltno> Fltnos { get; set; }

    public virtual DbSet<GfSector> GfSectors { get; set; }

    public virtual DbSet<InEprCpy2> InEprCpy2s { get; set; }

    public virtual DbSet<Inepr> Ineprs { get; set; }

    public virtual DbSet<MfcFlight> MfcFlights { get; set; }

    public virtual DbSet<MyBidt> MyBidts { get; set; }

    public virtual DbSet<Mybidt1> Mybidts { get; set; }

    public virtual DbSet<PnrFinder> PnrFinders { get; set; }

    public virtual DbSet<PrinterAv> PrinterAvs { get; set; }

    public virtual DbSet<PrinterTree> PrinterTrees { get; set; }

    public virtual DbSet<ResCatBooked> ResCatBookeds { get; set; }

    public virtual DbSet<ResCatBooked1> ResCatBooked1s { get; set; }

    public virtual DbSet<ResCatBookedD3> ResCatBookedD3s { get; set; }

    public virtual DbSet<ResCatBookedD31> ResCatBookedD31s { get; set; }

    public virtual DbSet<ResCatError> ResCatErrors { get; set; }

    public virtual DbSet<ResCatSpml> ResCatSpmls { get; set; }

    public virtual DbSet<ResCatSpml1> ResCatSpml1s { get; set; }

    public virtual DbSet<ResCatSpml2> ResCatSpml2s { get; set; }

    public virtual DbSet<SalesInd> SalesInds { get; set; }

    public virtual DbSet<SixFsrFlight> SixFsrFlights { get; set; }

    public virtual DbSet<SixFsrPax> SixFsrPaxes { get; set; }

    public virtual DbSet<SixFsrPaxSeg> SixFsrPaxSegs { get; set; }

    public virtual DbSet<SixFsrPaxSer> SixFsrPaxSers { get; set; }

    public virtual DbSet<SixFsrfbFlight> SixFsrfbFlights { get; set; }

    public virtual DbSet<SixFsrfbFlightcount> SixFsrfbFlightcounts { get; set; }

    public virtual DbSet<SixFsrfbLeg> SixFsrfbLegs { get; set; }

    public virtual DbSet<SixFsrfbSegment> SixFsrfbSegments { get; set; }

    public virtual DbSet<SixPtc> SixPtcs { get; set; }

    public virtual DbSet<SsciDacPax> SsciDacPaxes { get; set; }

    public virtual DbSet<SsciDeporteeOld> SsciDeporteeOlds { get; set; }

    public virtual DbSet<SsciPax> SsciPaxes { get; set; }

    public virtual DbSet<SsciPending> SsciPendings { get; set; }

    public virtual DbSet<SsciPf> SsciPfs { get; set; }

    public virtual DbSet<SsciSumm> SsciSumms { get; set; }

    public virtual DbSet<SsciWc> SsciWcs { get; set; }

    public virtual DbSet<Table1> Table1s { get; set; }

    public virtual DbSet<Table2> Table2s { get; set; }

    public virtual DbSet<TmpAdm> TmpAdms { get; set; }

    public virtual DbSet<TmpAdmTicket> TmpAdmTickets { get; set; }

    public virtual DbSet<TmpDatum> TmpData { get; set; }

    public virtual DbSet<TmpDocMissing> TmpDocMissings { get; set; }

    public virtual DbSet<TmpFlownDay> TmpFlownDays { get; set; }

    public virtual DbSet<TmpFltSeq> TmpFltSeqs { get; set; }

    public virtual DbSet<TmpFltseqSeat> TmpFltseqSeats { get; set; }

    public virtual DbSet<TmpIraqdetail> TmpIraqdetails { get; set; }

    public virtual DbSet<TmpJPaxPak> TmpJPaxPaks { get; set; }

    public virtual DbSet<TmpSeqno> TmpSeqnos { get; set; }

    public virtual DbSet<TmpSeqnr> TmpSeqnrs { get; set; }

    public virtual DbSet<TmpTktnr1day> TmpTktnr1days { get; set; }

    public virtual DbSet<VAcsiPaxManifest> VAcsiPaxManifests { get; set; }

    public virtual DbSet<VBookedInfo> VBookedInfos { get; set; }

    public virtual DbSet<VCheckinReport> VCheckinReports { get; set; }

    public virtual DbSet<VFltDatum> VFltData { get; set; }

    public virtual DbSet<VPaxAllDetail> VPaxAllDetails { get; set; }

    public virtual DbSet<VmslFlightList> VmslFlightLists { get; set; }

    public virtual DbSet<VwPaxManufest> VwPaxManufests { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseOracle("Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=SGOLDBBVYS.GULFAIRSTG.LOC)(PORT=1521))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=GFCOMD)));User ID=GFARPRTDATA;Password=Airport#12;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasDefaultSchema("GFARPRTDATA")
            .UseCollation("USING_NLS_COMP");

        modelBuilder.Entity<AcsiDxbFlight>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("ACSI_DXB_FLIGHTS");

            entity.Property(e => e.Adddate)
                .HasDefaultValueSql("SYSDATE")
                .HasColumnType("DATE")
                .HasColumnName("ADDDATE");
            entity.Property(e => e.Dest)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("DEST");
            entity.Property(e => e.Female)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("FEMALE");
            entity.Property(e => e.Fltdate)
                .HasColumnType("DATE")
                .HasColumnName("FLTDATE");
            entity.Property(e => e.Fltno)
                .HasMaxLength(4)
                .IsUnicode(false)
                .HasColumnName("FLTNO");
            entity.Property(e => e.Inf)
                .HasColumnType("NUMBER")
                .HasColumnName("INF");
            entity.Property(e => e.Male)
                .HasColumnType("NUMBER")
                .HasColumnName("MALE");
            entity.Property(e => e.Org)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("ORG");
            entity.Property(e => e.Totalpax)
                .HasColumnType("NUMBER")
                .HasColumnName("TOTALPAX");
        });

        modelBuilder.Entity<AcsiDxbManifest>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("ACSI_DXB_MANIFEST");

            entity.HasIndex(e => new { e.SeatNo, e.FltSeqNr }, "INDEXDXB1");

            entity.Property(e => e.Adddate)
                .HasDefaultValueSql("SYSDATE")
                .HasColumnType("DATE")
                .HasColumnName("ADDDATE");
            entity.Property(e => e.CCls)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("C_CLS");
            entity.Property(e => e.Destination)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("DESTINATION");
            entity.Property(e => e.Dob)
                .HasColumnType("DATE")
                .HasColumnName("DOB");
            entity.Property(e => e.DocNumber)
                .IsUnicode(false)
                .HasColumnName("DOC_NUMBER");
            entity.Property(e => e.FltSeqNr)
                .HasPrecision(10)
                .HasColumnName("FLT_SEQ_NR");
            entity.Property(e => e.IssueDate)
                .HasColumnType("DATE")
                .HasColumnName("ISSUE_DATE");
            entity.Property(e => e.MCls)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("M_CLS");
            entity.Property(e => e.Name)
                .IsUnicode(false)
                .HasColumnName("NAME");
            entity.Property(e => e.PaxType)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasDefaultValueSql("'ADT'")
                .IsFixedLength()
                .HasColumnName("PAX_TYPE");
            entity.Property(e => e.SeatNo)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("SEAT_NO");
            entity.Property(e => e.Sex)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("SEX");
            entity.Property(e => e.Status)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("STATUS");
            entity.Property(e => e.TktNr)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("TKT_NR");
        });

        modelBuilder.Entity<AcsiGcd>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("ACSI_GCD");

            entity.HasIndex(e => e.FltSeqNr, "ACSI_GCD_UK1").IsUnique();

            entity.Property(e => e.Capcj)
                .HasPrecision(3)
                .HasColumnName("CAPCJ");
            entity.Property(e => e.Capcy)
                .HasPrecision(3)
                .HasColumnName("CAPCY");
            entity.Property(e => e.Chgclj)
                .HasPrecision(3)
                .HasColumnName("CHGCLJ");
            entity.Property(e => e.Chgcly)
                .HasPrecision(3)
                .HasColumnName("CHGCLY");
            entity.Property(e => e.Chgflj)
                .HasPrecision(3)
                .HasColumnName("CHGFLJ");
            entity.Property(e => e.Chgfly)
                .HasPrecision(3)
                .HasColumnName("CHGFLY");
            entity.Property(e => e.Chgsgj)
                .HasPrecision(3)
                .HasColumnName("CHGSGJ");
            entity.Property(e => e.Chgsgy)
                .HasPrecision(3)
                .HasColumnName("CHGSGY");
            entity.Property(e => e.Destination)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("DESTINATION");
            entity.Property(e => e.FltDate)
                .HasColumnType("DATE")
                .HasColumnName("FLT_DATE");
            entity.Property(e => e.FltNr)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("FLT_NR");
            entity.Property(e => e.FltSeqNr)
                .HasPrecision(10)
                .HasColumnName("FLT_SEQ_NR");
            entity.Property(e => e.Goshoj)
                .HasPrecision(3)
                .HasColumnName("GOSHOJ");
            entity.Property(e => e.Goshoy)
                .HasPrecision(3)
                .HasColumnName("GOSHOY");
            entity.Property(e => e.Icclsj)
                .HasPrecision(3)
                .HasColumnName("ICCLSJ");
            entity.Property(e => e.Icclsy)
                .HasPrecision(3)
                .HasColumnName("ICCLSY");
            entity.Property(e => e.Idpadj)
                .HasPrecision(3)
                .HasColumnName("IDPADJ");
            entity.Property(e => e.Idpady)
                .HasPrecision(3)
                .HasColumnName("IDPADY");
            entity.Property(e => e.Ivldnj)
                .HasPrecision(3)
                .HasColumnName("IVLDNJ");
            entity.Property(e => e.Ivldny)
                .HasPrecision(3)
                .HasColumnName("IVLDNY");
            entity.Property(e => e.Ivlupj)
                .HasPrecision(3)
                .HasColumnName("IVLUPJ");
            entity.Property(e => e.Ivlupy)
                .HasPrecision(3)
                .HasColumnName("IVLUPY");
            entity.Property(e => e.Misctj)
                .HasPrecision(3)
                .HasColumnName("MISCTJ");
            entity.Property(e => e.Miscty)
                .HasPrecision(3)
                .HasColumnName("MISCTY");
            entity.Property(e => e.Norecj)
                .HasPrecision(3)
                .HasColumnName("NORECJ");
            entity.Property(e => e.Norecy)
                .HasPrecision(3)
                .HasColumnName("NORECY");
            entity.Property(e => e.Noshoj)
                .HasPrecision(3)
                .HasColumnName("NOSHOJ");
            entity.Property(e => e.Noshoy)
                .HasPrecision(3)
                .HasColumnName("NOSHOY");
            entity.Property(e => e.Nrecsj)
                .HasPrecision(3)
                .HasColumnName("NRECSJ");
            entity.Property(e => e.Nrecsy)
                .HasPrecision(3)
                .HasColumnName("NRECSY");
            entity.Property(e => e.Obrdj)
                .HasPrecision(3)
                .HasColumnName("OBRDJ");
            entity.Property(e => e.Obrdy)
                .HasPrecision(3)
                .HasColumnName("OBRDY");
            entity.Property(e => e.Offlkj)
                .HasPrecision(3)
                .HasColumnName("OFFLKJ");
            entity.Property(e => e.Offlky)
                .HasPrecision(3)
                .HasColumnName("OFFLKY");
            entity.Property(e => e.Offlnj)
                .HasPrecision(3)
                .HasColumnName("OFFLNJ");
            entity.Property(e => e.Offlny)
                .HasPrecision(3)
                .HasColumnName("OFFLNY");
            entity.Property(e => e.Origin)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("ORIGIN");
            entity.Property(e => e.Posspj)
                .HasPrecision(3)
                .HasColumnName("POSSPJ");
            entity.Property(e => e.Posspy)
                .HasPrecision(3)
                .HasColumnName("POSSPY");
            entity.Property(e => e.Tsfrdj)
                .HasPrecision(3)
                .HasColumnName("TSFRDJ");
            entity.Property(e => e.Tsfrdy)
                .HasPrecision(3)
                .HasColumnName("TSFRDY");
            entity.Property(e => e.Voldnj)
                .HasPrecision(3)
                .HasColumnName("VOLDNJ");
            entity.Property(e => e.Voldny)
                .HasPrecision(3)
                .HasColumnName("VOLDNY");
            entity.Property(e => e.Volupj)
                .HasPrecision(3)
                .HasColumnName("VOLUPJ");
            entity.Property(e => e.Volupy)
                .HasPrecision(3)
                .HasColumnName("VOLUPY");
            entity.Property(e => e.Waitlj)
                .HasPrecision(3)
                .HasColumnName("WAITLJ");
            entity.Property(e => e.Waitly)
                .HasPrecision(3)
                .HasColumnName("WAITLY");
        });

        modelBuilder.Entity<AcsiGcdDetail>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("ACSI_GCD_DETAILS");

            entity.Property(e => e.Category)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("CATEGORY");
            entity.Property(e => e.Class)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("CLASS");
            entity.Property(e => e.FltSeqNr)
                .HasPrecision(10)
                .HasColumnName("FLT_SEQ_NR");
            entity.Property(e => e.PaxName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("PAX_NAME");
            entity.Property(e => e.Port)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("PORT");
            entity.Property(e => e.RLoc)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("R_LOC");
        });

        modelBuilder.Entity<AcsiGf>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("ACSI_GFS");

            entity.HasIndex(e => e.FltSeqNr, "ACSI_GFS_UK1").IsUnique();

            entity.Property(e => e.Availj)
                .HasPrecision(3)
                .HasColumnName("AVAILJ");
            entity.Property(e => e.Availy)
                .HasPrecision(3)
                .HasColumnName("AVAILY");
            entity.Property(e => e.BagNos)
                .HasPrecision(5)
                .HasColumnName("BAG_NOS");
            entity.Property(e => e.BagWeight)
                .HasPrecision(8)
                .HasColumnName("BAG_WEIGHT");
            entity.Property(e => e.Bkedj)
                .HasPrecision(3)
                .HasColumnName("BKEDJ");
            entity.Property(e => e.Bkedy)
                .HasPrecision(3)
                .HasColumnName("BKEDY");
            entity.Property(e => e.Blokj)
                .HasPrecision(3)
                .HasColumnName("BLOKJ");
            entity.Property(e => e.Bloky)
                .HasPrecision(3)
                .HasColumnName("BLOKY");
            entity.Property(e => e.Capcj)
                .HasPrecision(3)
                .HasColumnName("CAPCJ");
            entity.Property(e => e.Capcy)
                .HasPrecision(3)
                .HasColumnName("CAPCY");
            entity.Property(e => e.Ckdcsj)
                .HasPrecision(3)
                .HasColumnName("CKDCSJ");
            entity.Property(e => e.Ckdcsy)
                .HasPrecision(3)
                .HasColumnName("CKDCSY");
            entity.Property(e => e.Ckedj)
                .HasPrecision(3)
                .HasColumnName("CKEDJ");
            entity.Property(e => e.Ckedy)
                .HasPrecision(3)
                .HasColumnName("CKEDY");
            entity.Property(e => e.Ckpt)
                .HasPrecision(3)
                .HasColumnName("CKPT");
            entity.Property(e => e.Destination)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("DESTINATION");
            entity.Property(e => e.FltDate)
                .HasColumnType("DATE")
                .HasColumnName("FLT_DATE");
            entity.Property(e => e.FltNr)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("FLT_NR");
            entity.Property(e => e.FltSeqNr)
                .HasPrecision(10)
                .HasColumnName("FLT_SEQ_NR");
            entity.Property(e => e.Heldj)
                .HasPrecision(3)
                .HasColumnName("HELDJ");
            entity.Property(e => e.Heldy)
                .HasPrecision(3)
                .HasColumnName("HELDY");
            entity.Property(e => e.Inftj)
                .HasPrecision(3)
                .HasColumnName("INFTJ");
            entity.Property(e => e.Infty)
                .HasPrecision(3)
                .HasColumnName("INFTY");
            entity.Property(e => e.Jcapj)
                .HasPrecision(3)
                .HasColumnName("JCAPJ");
            entity.Property(e => e.Jcapy)
                .HasPrecision(3)
                .HasColumnName("JCAPY");
            entity.Property(e => e.Jumpj)
                .HasPrecision(3)
                .HasColumnName("JUMPJ");
            entity.Property(e => e.Jumpy)
                .HasPrecision(3)
                .HasColumnName("JUMPY");
            entity.Property(e => e.NrevPj)
                .HasPrecision(3)
                .HasColumnName("NREV_PJ");
            entity.Property(e => e.NrevPy)
                .HasPrecision(3)
                .HasColumnName("NREV_PY");
            entity.Property(e => e.NrevSj)
                .HasPrecision(3)
                .HasColumnName("NREV_SJ");
            entity.Property(e => e.NrevSy)
                .HasPrecision(3)
                .HasColumnName("NREV_SY");
            entity.Property(e => e.Origin)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("ORIGIN");
            entity.Property(e => e.Otldj)
                .HasPrecision(3)
                .HasColumnName("OTLDJ");
            entity.Property(e => e.Otldy)
                .HasPrecision(3)
                .HasColumnName("OTLDY");
            entity.Property(e => e.Presj)
                .HasPrecision(3)
                .HasColumnName("PRESJ");
            entity.Property(e => e.Presy)
                .HasPrecision(3)
                .HasColumnName("PRESY");
            entity.Property(e => e.Rstrj)
                .HasPrecision(3)
                .HasColumnName("RSTRJ");
            entity.Property(e => e.Rstry)
                .HasPrecision(3)
                .HasColumnName("RSTRY");
            entity.Property(e => e.Savuj)
                .HasPrecision(3)
                .HasColumnName("SAVUJ");
            entity.Property(e => e.Savuy)
                .HasPrecision(3)
                .HasColumnName("SAVUY");
            entity.Property(e => e.StbyNj)
                .HasPrecision(3)
                .HasColumnName("STBY_NJ");
            entity.Property(e => e.StbyNy)
                .HasPrecision(3)
                .HasColumnName("STBY_NY");
            entity.Property(e => e.StbyRj)
                .HasPrecision(3)
                .HasColumnName("STBY_RJ");
            entity.Property(e => e.StbyRy)
                .HasPrecision(3)
                .HasColumnName("STBY_RY");
            entity.Property(e => e.Thnvj)
                .HasPrecision(3)
                .HasColumnName("THNVJ");
            entity.Property(e => e.Thnvy)
                .HasPrecision(3)
                .HasColumnName("THNVY");
            entity.Property(e => e.Thrvj)
                .HasPrecision(3)
                .HasColumnName("THRVJ");
            entity.Property(e => e.Thrvy)
                .HasPrecision(3)
                .HasColumnName("THRVY");
            entity.Property(e => e.Tlobj)
                .HasPrecision(3)
                .HasColumnName("TLOBJ");
            entity.Property(e => e.Tloby)
                .HasPrecision(3)
                .HasColumnName("TLOBY");
            entity.Property(e => e.Trffj)
                .HasPrecision(3)
                .HasColumnName("TRFFJ");
            entity.Property(e => e.Trffy)
                .HasPrecision(3)
                .HasColumnName("TRFFY");
            entity.Property(e => e.Trftj)
                .HasPrecision(3)
                .HasColumnName("TRFTJ");
            entity.Property(e => e.Trfty)
                .HasPrecision(3)
                .HasColumnName("TRFTY");
            entity.Property(e => e.Tsfrj)
                .HasPrecision(3)
                .HasColumnName("TSFRJ");
            entity.Property(e => e.Tsfry)
                .HasPrecision(3)
                .HasColumnName("TSFRY");
            entity.Property(e => e.Unocj)
                .HasPrecision(3)
                .HasColumnName("UNOCJ");
            entity.Property(e => e.Unocy)
                .HasPrecision(3)
                .HasColumnName("UNOCY");
        });

        modelBuilder.Entity<AcsiGwl>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("ACSI_GWLS");

            entity.HasIndex(e => e.FltSeqNr, "ACSI_GWLS_UK1").IsUnique();

            entity.Property(e => e.Destination)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("DESTINATION");
            entity.Property(e => e.Doi)
                .HasColumnType("NUMBER(8,3)")
                .HasColumnName("DOI");
            entity.Property(e => e.DryOprWeight)
                .HasPrecision(8)
                .HasColumnName("DRY_OPR_WEIGHT");
            entity.Property(e => e.FltDate)
                .HasColumnType("DATE")
                .HasColumnName("FLT_DATE");
            entity.Property(e => e.FltNr)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("FLT_NR");
            entity.Property(e => e.FltSeqNr)
                .HasPrecision(10)
                .HasColumnName("FLT_SEQ_NR");
            entity.Property(e => e.Litow)
                .HasColumnType("NUMBER(8,3)")
                .HasColumnName("LITOW");
            entity.Property(e => e.Lizfw)
                .HasColumnType("NUMBER(8,3)")
                .HasColumnName("LIZFW");
            entity.Property(e => e.LndgWtAct)
                .HasPrecision(8)
                .HasColumnName("LNDG_WT_ACT");
            entity.Property(e => e.Mactow)
                .HasColumnType("NUMBER(8,3)")
                .HasColumnName("MACTOW");
            entity.Property(e => e.Maczfw)
                .HasColumnType("NUMBER(8,3)")
                .HasColumnName("MACZFW");
            entity.Property(e => e.Origin)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("ORIGIN");
            entity.Property(e => e.Soc)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("SOC");
            entity.Property(e => e.TakOffFuel)
                .HasPrecision(8)
                .HasColumnName("TAK_OFF_FUEL");
            entity.Property(e => e.TakOffWtAct)
                .HasPrecision(8)
                .HasColumnName("TAK_OFF_WT_ACT");
            entity.Property(e => e.TotTfcLoad)
                .HasPrecision(8)
                .HasColumnName("TOT_TFC_LOAD");
            entity.Property(e => e.TripFul)
                .HasPrecision(8)
                .HasColumnName("TRIP_FUL");
            entity.Property(e => e.TxiOutFul)
                .HasPrecision(8)
                .HasColumnName("TXI_OUT_FUL");
            entity.Property(e => e.ZerFulWtAct)
                .HasPrecision(8)
                .HasColumnName("ZER_FUL_WT_ACT");
        });

        modelBuilder.Entity<AcsiGwlsCgo>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("ACSI_GWLS_CGO");

            entity.Property(e => e.BagNos)
                .HasPrecision(5)
                .HasColumnName("BAG_NOS");
            entity.Property(e => e.BagWeight)
                .HasPrecision(8)
                .HasColumnName("BAG_WEIGHT");
            entity.Property(e => e.Cargo)
                .HasColumnType("NUMBER(8,3)")
                .HasColumnName("CARGO");
            entity.Property(e => e.Destination)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("DESTINATION");
            entity.Property(e => e.Filler)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("FILLER");
            entity.Property(e => e.FltSeqNr)
                .HasPrecision(10)
                .HasColumnName("FLT_SEQ_NR");
            entity.Property(e => e.Mail)
                .HasColumnType("NUMBER(8,3)")
                .HasColumnName("MAIL");
        });

        modelBuilder.Entity<AcsiGwlsLdm>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("ACSI_GWLS_LDM");

            entity.Property(e => e.Child)
                .HasPrecision(3)
                .HasColumnName("CHILD");
            entity.Property(e => e.Destination)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("DESTINATION");
            entity.Property(e => e.Female)
                .HasPrecision(3)
                .HasColumnName("FEMALE");
            entity.Property(e => e.Filler)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("FILLER");
            entity.Property(e => e.FltSeqNr)
                .HasPrecision(10)
                .HasColumnName("FLT_SEQ_NR");
            entity.Property(e => e.Infant)
                .HasPrecision(3)
                .HasColumnName("INFANT");
            entity.Property(e => e.Male)
                .HasPrecision(3)
                .HasColumnName("MALE");
        });

        modelBuilder.Entity<AcsiInvolUpgrade>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("ACSI_INVOL_UPGRADE");

            entity.Property(e => e.AddDate)
                .HasDefaultValueSql("SYSDATE")
                .HasColumnType("DATE")
                .HasColumnName("ADD_DATE");
            entity.Property(e => e.Class)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("CLASS");
            entity.Property(e => e.EdCode)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("ED_CODE");
            entity.Property(e => e.FltSeqNr)
                .HasPrecision(10)
                .HasColumnName("FLT_SEQ_NR");
            entity.Property(e => e.PaxName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("PAX_NAME");
            entity.Property(e => e.Port)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("PORT");
            entity.Property(e => e.RLoc)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("R_LOC");
            entity.Property(e => e.Seat)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("SEAT");
            entity.Property(e => e.Type)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("TYPE");
            entity.Property(e => e.Vcr)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("VCR");
        });

        modelBuilder.Entity<AcsiPaxCount>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("ACSI_PAX_COUNT");

            entity.Property(e => e.BagPcs)
                .HasPrecision(5)
                .HasColumnName("BAG_PCS");
            entity.Property(e => e.BagWt)
                .HasPrecision(6)
                .HasColumnName("BAG_WT");
            entity.Property(e => e.Child)
                .HasPrecision(3)
                .HasColumnName("CHILD");
            entity.Property(e => e.Class)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("CLASS");
            entity.Property(e => e.Female)
                .HasPrecision(3)
                .HasColumnName("FEMALE");
            entity.Property(e => e.FltSeqNr)
                .HasPrecision(10)
                .HasColumnName("FLT_SEQ_NR");
            entity.Property(e => e.Infant)
                .HasPrecision(3)
                .HasColumnName("INFANT");
            entity.Property(e => e.Male)
                .HasPrecision(3)
                .HasColumnName("MALE");
        });

        modelBuilder.Entity<AcsiPaxManifest>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("ACSI_PAX_MANIFEST");

            entity.HasIndex(e => new { e.SeatNo, e.FltSeqNr }, "INDEX1");

            entity.Property(e => e.Adddate)
                .HasDefaultValueSql("SYSDATE")
                .HasColumnType("DATE")
                .HasColumnName("ADDDATE");
            entity.Property(e => e.Bagcount)
                .HasColumnType("NUMBER")
                .HasColumnName("BAGCOUNT");
            entity.Property(e => e.Bagwt)
                .HasColumnType("NUMBER")
                .HasColumnName("BAGWT");
            entity.Property(e => e.CCls)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("C_CLS");
            entity.Property(e => e.Destination)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("DESTINATION");
            entity.Property(e => e.Dob)
                .HasColumnType("DATE")
                .HasColumnName("DOB");
            entity.Property(e => e.DocNumber)
                .IsUnicode(false)
                .HasColumnName("DOC_NUMBER");
            entity.Property(e => e.Docissuedt)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("DOCISSUEDT");
            entity.Property(e => e.Doctype)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("DOCTYPE");
            entity.Property(e => e.Expdt)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("EXPDT");
            entity.Property(e => e.Filename)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("FILENAME");
            entity.Property(e => e.FltSeqNr)
                .HasPrecision(10)
                .HasColumnName("FLT_SEQ_NR");
            entity.Property(e => e.Isscntry)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ISSCNTRY");
            entity.Property(e => e.MCls)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("M_CLS");
            entity.Property(e => e.Name)
                .IsUnicode(false)
                .HasColumnName("NAME");
            entity.Property(e => e.Opbkgcls)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("OPBKGCLS");
            entity.Property(e => e.PaxType)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasDefaultValueSql("'ADT'")
                .IsFixedLength()
                .HasColumnName("PAX_TYPE");
            entity.Property(e => e.Paxboardpoint)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("PAXBOARDPOINT");
            entity.Property(e => e.Paxdoc)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("PAXDOC");
            entity.Property(e => e.Paxnat)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("PAXNAT");
            entity.Property(e => e.Placissue)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("PLACISSUE");
            entity.Property(e => e.Pnr)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("PNR");
            entity.Property(e => e.SeatNo)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("SEAT_NO");
            entity.Property(e => e.Sex)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("SEX");
            entity.Property(e => e.Splcode)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("SPLCODE");
            entity.Property(e => e.Splsrvc)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("SPLSRVC");
            entity.Property(e => e.Status)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("STATUS");
            entity.Property(e => e.TktNr)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("TKT_NR");
        });

        modelBuilder.Entity<AcsiPaxaddnlManifest>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("ACSI_PAXADDNL_MANIFEST");

            entity.Property(e => e.Adddate)
                .HasColumnType("DATE")
                .HasColumnName("ADDDATE");
            entity.Property(e => e.Destination)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("DESTINATION");
            entity.Property(e => e.FltSeqNr)
                .HasPrecision(10)
                .HasColumnName("FLT_SEQ_NR");
            entity.Property(e => e.Issuedt)
                .HasColumnType("DATE")
                .HasColumnName("ISSUEDT");
            entity.Property(e => e.Paxdoc)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("PAXDOC");
            entity.Property(e => e.Pnr)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("PNR");
        });

        modelBuilder.Entity<AcsiPending>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("ACSI_PENDING");

            entity.Property(e => e.ActualArvArpCd)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("ACTUAL_ARV_ARP_CD");
            entity.Property(e => e.ActualDepArpCd)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("ACTUAL_DEP_ARP_CD");
            entity.Property(e => e.FltNr)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("FLT_NR");
            entity.Property(e => e.FltSeqNr)
                .HasPrecision(10)
                .HasColumnName("FLT_SEQ_NR");
        });

        modelBuilder.Entity<AcsiTkt>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("ACSI_TKTS");

            entity.Property(e => e.TktNr)
                .IsUnicode(false)
                .HasColumnName("TKT_NR");
        });

        modelBuilder.Entity<AdmNoshow>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("ADM_NOSHOW");

            entity.Property(e => e.Agentcode)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("AGENTCODE");
            entity.Property(e => e.Arrival)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("ARRIVAL");
            entity.Property(e => e.Cardocnum)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("CARDOCNUM");
            entity.Property(e => e.Carrier)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("CARRIER");
            entity.Property(e => e.Cls)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("CLS");
            entity.Property(e => e.Destination)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("DESTINATION");
            entity.Property(e => e.Docnum)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("DOCNUM");
            entity.Property(e => e.Flightdate)
                .HasColumnType("DATE")
                .HasColumnName("FLIGHTDATE");
            entity.Property(e => e.Flightno)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("FLIGHTNO");
            entity.Property(e => e.Fltseqnr)
                .HasColumnType("NUMBER")
                .HasColumnName("FLTSEQNR");
            entity.Property(e => e.Issuedate)
                .HasColumnType("DATE")
                .HasColumnName("ISSUEDATE");
            entity.Property(e => e.Origin)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("ORIGIN");
            entity.Property(e => e.Paxname)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("PAXNAME");
            entity.Property(e => e.Pnr)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("PNR");
        });

        modelBuilder.Entity<BookingFlightTest>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("BOOKING_FLIGHT_TEST");

            entity.Property(e => e.Departuredate)
                .HasPrecision(6)
                .HasColumnName("DEPARTUREDATE");
            entity.Property(e => e.Destination)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("DESTINATION");
            entity.Property(e => e.Flightnumber)
                .HasColumnType("NUMBER")
                .HasColumnName("FLIGHTNUMBER");
            entity.Property(e => e.Origin)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("ORIGIN");
            entity.Property(e => e.Status)
                .HasColumnType("NUMBER")
                .HasColumnName("STATUS");
        });

        modelBuilder.Entity<CsmSsr>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("CSM_SSR");

            entity.Property(e => e.BookingClass)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("BOOKING_CLASS");
            entity.Property(e => e.Destination)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("DESTINATION");
            entity.Property(e => e.FltDate)
                .HasColumnType("DATE")
                .HasColumnName("FLT_DATE");
            entity.Property(e => e.FltNr)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("FLT_NR");
            entity.Property(e => e.FltSeqNr)
                .HasPrecision(10)
                .HasColumnName("FLT_SEQ_NR");
            entity.Property(e => e.Origin)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("ORIGIN");
            entity.Property(e => e.PaxName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("PAX_NAME");
            entity.Property(e => e.ReqType)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("REQ_TYPE");
        });

        modelBuilder.Entity<CsmSsr1>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("CSM_SSR1");

            entity.Property(e => e.BookingClass)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("BOOKING_CLASS");
            entity.Property(e => e.Destination)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("DESTINATION");
            entity.Property(e => e.FltDate)
                .HasColumnType("DATE")
                .HasColumnName("FLT_DATE");
            entity.Property(e => e.FltNr)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("FLT_NR");
            entity.Property(e => e.FltSeqNr)
                .HasPrecision(10)
                .HasColumnName("FLT_SEQ_NR");
            entity.Property(e => e.Origin)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("ORIGIN");
            entity.Property(e => e.PaxName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("PAX_NAME");
            entity.Property(e => e.ReqType)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("REQ_TYPE");
        });

        modelBuilder.Entity<CuCdgSncf>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("CU_CDG_SNCF");

            entity.HasIndex(e => new { e.FltNo, e.FltDate, e.TktNo, e.IssueDate, e.Pnr, e.PaxName, e.SncfFltNo, e.SncfFltDate, e.SncfOrigin, e.SncfDestination, e.SncfFareBasis, e.SncfStatus, e.SncfClass }, "CU_CDG_SNCF_UK1").IsUnique();

            entity.Property(e => e.FltDate)
                .HasColumnType("DATE")
                .HasColumnName("FLT_DATE");
            entity.Property(e => e.FltNo)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("FLT_NO");
            entity.Property(e => e.IssueDate)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("ISSUE_DATE");
            entity.Property(e => e.PaxName)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("PAX_NAME");
            entity.Property(e => e.Pnr)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("PNR");
            entity.Property(e => e.SncfClass)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("SNCF_CLASS");
            entity.Property(e => e.SncfDestination)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("SNCF_DESTINATION");
            entity.Property(e => e.SncfFareBasis)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("SNCF_FARE_BASIS");
            entity.Property(e => e.SncfFltDate)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("SNCF_FLT_DATE");
            entity.Property(e => e.SncfFltNo)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("SNCF_FLT_NO");
            entity.Property(e => e.SncfOrigin)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("SNCF_ORIGIN");
            entity.Property(e => e.SncfStatus)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("SNCF_STATUS");
            entity.Property(e => e.TktNo)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("TKT_NO");
        });

        modelBuilder.Entity<DxbIb>(entity =>
        {
            entity.HasKey(e => e.Fltseqnr).HasName("DXB_IB_PK");

            entity.ToTable("DXB_IB");

            entity.Property(e => e.Fltseqnr)
                .HasColumnType("NUMBER")
                .HasColumnName("FLTSEQNR");
            entity.Property(e => e.Adddate)
                .HasDefaultValueSql("SYSDATE")
                .HasColumnType("DATE")
                .HasColumnName("ADDDATE");
            entity.Property(e => e.Dest)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("DEST");
            entity.Property(e => e.Inj)
                .HasColumnType("NUMBER")
                .HasColumnName("INJ");
            entity.Property(e => e.Iny)
                .HasColumnType("NUMBER")
                .HasColumnName("INY");
        });

        modelBuilder.Entity<EprCreate>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("EPR_CREATE");

            entity.Property(e => e.City)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("CITY");
            entity.Property(e => e.Dty)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("DTY");
            entity.Property(e => e.Epr)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("EPR");
            entity.Property(e => e.Name)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("NAME");
            entity.Property(e => e.Sine)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("SINE");
        });

        modelBuilder.Entity<EprGroup>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("EPR_GROUP");

            entity.Property(e => e.Epr)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("EPR");
            entity.Property(e => e.Grp)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("GRP");
            entity.Property(e => e.Ofc)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("OFC");
        });

        modelBuilder.Entity<EprReset>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("EPR_RESET");

            entity.Property(e => e.Auth)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("AUTH");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("EMAIL");
            entity.Property(e => e.Epr)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("EPR");
        });

        modelBuilder.Entity<EprUpdate>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("EPR_UPDATE");

            entity.Property(e => e.Epr)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("EPR");
        });

        modelBuilder.Entity<EprcOption>(entity =>
        {
            entity.HasKey(e => e.Opt).HasName("EPRC_OPTIONS_PK");

            entity.ToTable("EPRC_OPTIONS");

            entity.Property(e => e.Opt)
                .HasColumnType("NUMBER")
                .HasColumnName("OPT");
            entity.Property(e => e.Descoptions)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("DESCOPTIONS");
            entity.Property(e => e.Dien)
                .HasColumnType("NUMBER")
                .HasColumnName("DIEN");
            entity.Property(e => e.Dty)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("DTY");
            entity.Property(e => e.Keywords)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("KEYWORDS");
        });

        modelBuilder.Entity<EprcStaff>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("EPRC_STAFF");

            entity.Property(e => e.Opt)
                .HasColumnType("NUMBER")
                .HasColumnName("OPT");
            entity.Property(e => e.Staffno)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("STAFFNO");
            entity.Property(e => e.Station)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("STATION");
        });

        modelBuilder.Entity<ErrorList>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("ERROR_LIST");

            entity.Property(e => e.Brd)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("BRD");
            entity.Property(e => e.Class)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("CLASS");
            entity.Property(e => e.Fdate)
                .HasColumnType("DATE")
                .HasColumnName("FDATE");
            entity.Property(e => e.Fltnum)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("FLTNUM");
            entity.Property(e => e.Off)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("OFF");
        });

        modelBuilder.Entity<ExportTable>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("EXPORT_TABLE");

            entity.Property(e => e.Adddate)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ADDDATE");
            entity.Property(e => e.CCls)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("C_CLS");
            entity.Property(e => e.Destination)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("DESTINATION");
            entity.Property(e => e.DocNumber)
                .IsUnicode(false)
                .HasColumnName("DOC_NUMBER");
            entity.Property(e => e.FltSeqNr)
                .HasPrecision(10)
                .HasColumnName("FLT_SEQ_NR");
            entity.Property(e => e.MCls)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("M_CLS");
            entity.Property(e => e.Name)
                .IsUnicode(false)
                .HasColumnName("NAME");
            entity.Property(e => e.PaxType)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasDefaultValueSql("'ADT'")
                .IsFixedLength()
                .HasColumnName("PAX_TYPE");
            entity.Property(e => e.SeatNo)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("SEAT_NO");
            entity.Property(e => e.Sex)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("SEX");
            entity.Property(e => e.Status)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("STATUS");
            entity.Property(e => e.TktNr)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("TKT_NR");
        });

        modelBuilder.Entity<FfpFt>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("FFP_FT");

            entity.Property(e => e.Ffpno)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("FFPNO");
            entity.Property(e => e.FltSeqNr)
                .HasPrecision(10)
                .HasColumnName("FLT_SEQ_NR");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("NAME");
            entity.Property(e => e.Pnr)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("PNR");
        });

        modelBuilder.Entity<Fltno>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("FLTNO");

            entity.Property(e => e.Fltnr)
                .HasMaxLength(4)
                .IsUnicode(false)
                .HasColumnName("FLTNR");
        });

        modelBuilder.Entity<GfSector>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("GF_SECTOR");

            entity.Property(e => e.Datestr)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("DATESTR");
            entity.Property(e => e.Sector)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("SECTOR");
            entity.Property(e => e.Via)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("VIA");
            entity.Property(e => e.Via1)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("VIA1");
        });

        modelBuilder.Entity<InEprCpy2>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("IN_EPR_CPY2");

            entity.Property(e => e.Epr)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("EPR");
        });

        modelBuilder.Entity<Inepr>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("INEPR");

            entity.Property(e => e.Epr)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("EPR");
        });

        modelBuilder.Entity<MfcFlight>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("MFC_FLIGHTS");

            entity.Property(e => e.Child)
                .HasColumnType("NUMBER")
                .HasColumnName("CHILD");
            entity.Property(e => e.Destination)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("DESTINATION");
            entity.Property(e => e.Female)
                .HasColumnType("NUMBER")
                .HasColumnName("FEMALE");
            entity.Property(e => e.FltDate)
                .HasColumnType("DATE")
                .HasColumnName("FLT_DATE");
            entity.Property(e => e.FltSeqNr)
                .HasPrecision(10)
                .HasColumnName("FLT_SEQ_NR");
            entity.Property(e => e.Infant)
                .HasColumnType("NUMBER")
                .HasColumnName("INFANT");
            entity.Property(e => e.Male)
                .HasColumnType("NUMBER")
                .HasColumnName("MALE");
            entity.Property(e => e.Origin)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("ORIGIN");
        });

        modelBuilder.Entity<MyBidt>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("MY_BIDT");

            entity.Property(e => e.BookClass)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("BOOK_CLASS");
            entity.Property(e => e.Crs)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("CRS");
            entity.Property(e => e.Dest)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("DEST");
            entity.Property(e => e.FltDate)
                .HasColumnType("DATE")
                .HasColumnName("FLT_DATE");
            entity.Property(e => e.FltNo)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("FLT_NO");
            entity.Property(e => e.IataCode)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("IATA_CODE");
            entity.Property(e => e.Orig)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("ORIG");
            entity.Property(e => e.PaxName)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("PAX_NAME");
            entity.Property(e => e.Pnr)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("PNR");
            entity.Property(e => e.TransDate)
                .HasColumnType("DATE")
                .HasColumnName("TRANS_DATE");
        });

        modelBuilder.Entity<Mybidt1>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("MYBIDT");

            entity.Property(e => e.AddedBookings)
                .HasColumnType("NUMBER")
                .HasColumnName("ADDED_BOOKINGS");
            entity.Property(e => e.CancelledBookings)
                .HasColumnType("NUMBER")
                .HasColumnName("CANCELLED_BOOKINGS");
            entity.Property(e => e.CountryName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("COUNTRY_NAME");
            entity.Property(e => e.DistributionChannel)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("DISTRIBUTION_CHANNEL");
            entity.Property(e => e.IataNumber)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("IATA_NUMBER");
            entity.Property(e => e.NetBookings)
                .HasColumnType("NUMBER")
                .HasColumnName("NET_BOOKINGS");
            entity.Property(e => e.OfficeId)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("OFFICE_ID");
            entity.Property(e => e.OfficeName)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("OFFICE_NAME");
            entity.Property(e => e.RegionName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("REGION_NAME");
            entity.Property(e => e.TotalAda)
                .HasColumnType("NUMBER")
                .HasColumnName("TOTAL_ADA");
            entity.Property(e => e.TxDate)
                .HasColumnType("DATE")
                .HasColumnName("TX_DATE");
        });

        modelBuilder.Entity<PnrFinder>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("PNR_FINDER");

            entity.Property(e => e.FltSeqNr)
                .HasPrecision(10)
                .HasColumnName("FLT_SEQ_NR");
            entity.Property(e => e.MCls)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("M_CLS");
            entity.Property(e => e.Pnr)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("PNR");
            entity.Property(e => e.TktNr)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("TKT_NR");
        });

        modelBuilder.Entity<PrinterAv>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("PRINTER_AVS");

            entity.Property(e => e.Address)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ADDRESS");
        });

        modelBuilder.Entity<PrinterTree>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("PRINTER_TREE");

            entity.Property(e => e.Address)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ADDRESS");
        });

        modelBuilder.Entity<ResCatBooked>(entity =>
        {
            entity.HasKey(e => new { e.FltSeqNr, e.Class }).HasName("RES_CAT_BOOKED_PK");

            entity.ToTable("RES_CAT_BOOKED");

            entity.Property(e => e.FltSeqNr)
                .HasPrecision(10)
                .HasColumnName("FLT_SEQ_NR");
            entity.Property(e => e.Class)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("CLASS");
            entity.Property(e => e.AddDate)
                .HasDefaultValueSql("SYSDATE")
                .HasColumnType("DATE")
                .HasColumnName("ADD_DATE");
            entity.Property(e => e.BoardingPoint)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("BOARDING_POINT");
            entity.Property(e => e.Booked)
                .HasPrecision(3)
                .HasColumnName("BOOKED");
            entity.Property(e => e.Capacity)
                .HasPrecision(3)
                .HasColumnName("CAPACITY");
            entity.Property(e => e.Dest)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("DEST");
            entity.Property(e => e.FltDtGmt)
                .HasColumnType("DATE")
                .HasColumnName("FLT_DT_GMT");
            entity.Property(e => e.Pad)
                .HasPrecision(3)
                .HasColumnName("PAD");
        });

        modelBuilder.Entity<ResCatBooked1>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("RES_CAT_BOOKED1");

            entity.Property(e => e.AddDate)
                .HasColumnType("DATE")
                .HasColumnName("ADD_DATE");
            entity.Property(e => e.BoardingPoint)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("BOARDING_POINT");
            entity.Property(e => e.Booked)
                .HasPrecision(3)
                .HasColumnName("BOOKED");
            entity.Property(e => e.Capacity)
                .HasPrecision(3)
                .HasColumnName("CAPACITY");
            entity.Property(e => e.Class)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("CLASS");
            entity.Property(e => e.Dest)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("DEST");
            entity.Property(e => e.FltDtGmt)
                .HasColumnType("DATE")
                .HasColumnName("FLT_DT_GMT");
            entity.Property(e => e.FltSeqNr)
                .HasPrecision(10)
                .HasColumnName("FLT_SEQ_NR");
            entity.Property(e => e.Pad)
                .HasPrecision(3)
                .HasColumnName("PAD");
        });

        modelBuilder.Entity<ResCatBookedD3>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("RES_CAT_BOOKED_D3");

            entity.Property(e => e.AddDate)
                .HasColumnType("DATE")
                .HasColumnName("ADD_DATE");
            entity.Property(e => e.BoardingPoint)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("BOARDING_POINT");
            entity.Property(e => e.Booked)
                .HasPrecision(3)
                .HasColumnName("BOOKED");
            entity.Property(e => e.Capacity)
                .HasPrecision(3)
                .HasColumnName("CAPACITY");
            entity.Property(e => e.Class)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("CLASS");
            entity.Property(e => e.Dest)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("DEST");
            entity.Property(e => e.FltDtGmt)
                .HasColumnType("DATE")
                .HasColumnName("FLT_DT_GMT");
            entity.Property(e => e.FltSeqNr)
                .HasPrecision(10)
                .HasColumnName("FLT_SEQ_NR");
            entity.Property(e => e.Pad)
                .HasPrecision(3)
                .HasColumnName("PAD");
        });

        modelBuilder.Entity<ResCatBookedD31>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("RES_CAT_BOOKED_D31");

            entity.Property(e => e.AddDate)
                .HasColumnType("DATE")
                .HasColumnName("ADD_DATE");
            entity.Property(e => e.BoardingPoint)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("BOARDING_POINT");
            entity.Property(e => e.Booked)
                .HasPrecision(3)
                .HasColumnName("BOOKED");
            entity.Property(e => e.Capacity)
                .HasPrecision(3)
                .HasColumnName("CAPACITY");
            entity.Property(e => e.Class)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("CLASS");
            entity.Property(e => e.Dest)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("DEST");
            entity.Property(e => e.FltDtGmt)
                .HasColumnType("DATE")
                .HasColumnName("FLT_DT_GMT");
            entity.Property(e => e.FltSeqNr)
                .HasPrecision(10)
                .HasColumnName("FLT_SEQ_NR");
            entity.Property(e => e.Pad)
                .HasPrecision(3)
                .HasColumnName("PAD");
        });

        modelBuilder.Entity<ResCatError>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("RES_CAT_ERROR");

            entity.Property(e => e.Errorcount)
                .HasColumnType("NUMBER")
                .HasColumnName("ERRORCOUNT");
        });

        modelBuilder.Entity<ResCatSpml>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("RES_CAT_SPML");

            entity.HasIndex(e => new { e.FltSeqNr, e.Class, e.MealType, e.BoardingPoint, e.PaxName }, "SPML_UK1").IsUnique();

            entity.Property(e => e.BoardingPoint)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("BOARDING_POINT");
            entity.Property(e => e.Class)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("CLASS");
            entity.Property(e => e.FltSeqNr)
                .HasPrecision(10)
                .HasColumnName("FLT_SEQ_NR");
            entity.Property(e => e.MealDesc)
                .IsUnicode(false)
                .HasColumnName("MEAL_DESC");
            entity.Property(e => e.MealType)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("MEAL_TYPE");
            entity.Property(e => e.PaxName)
                .HasMaxLength(4000)
                .IsUnicode(false)
                .HasColumnName("PAX_NAME");
        });

        modelBuilder.Entity<ResCatSpml1>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("RES_CAT_SPML1");

            entity.Property(e => e.BoardingPoint)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("BOARDING_POINT");
            entity.Property(e => e.Class)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("CLASS");
            entity.Property(e => e.FltSeqNr)
                .HasPrecision(10)
                .HasColumnName("FLT_SEQ_NR");
            entity.Property(e => e.MealDesc)
                .IsUnicode(false)
                .HasColumnName("MEAL_DESC");
            entity.Property(e => e.MealType)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("MEAL_TYPE");
            entity.Property(e => e.PaxName)
                .IsUnicode(false)
                .HasColumnName("PAX_NAME");
        });

        modelBuilder.Entity<ResCatSpml2>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("RES_CAT_SPML2");

            entity.Property(e => e.BoardingPoint)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("BOARDING_POINT");
            entity.Property(e => e.Class)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("CLASS");
            entity.Property(e => e.FltSeqNr)
                .HasPrecision(10)
                .HasColumnName("FLT_SEQ_NR");
            entity.Property(e => e.MealDesc)
                .IsUnicode(false)
                .HasColumnName("MEAL_DESC");
            entity.Property(e => e.MealType)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("MEAL_TYPE");
            entity.Property(e => e.PaxName)
                .IsUnicode(false)
                .HasColumnName("PAX_NAME");
        });

        modelBuilder.Entity<SalesInd>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("SALES_IND");

            entity.Property(e => e.ActualDest)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("ACTUAL_DEST");
            entity.Property(e => e.Adddate)
                .HasColumnType("DATE")
                .HasColumnName("ADDDATE");
            entity.Property(e => e.CCls)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("C_CLS");
            entity.Property(e => e.Destination)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("DESTINATION");
            entity.Property(e => e.DocNumber)
                .IsUnicode(false)
                .HasColumnName("DOC_NUMBER");
            entity.Property(e => e.Farebasis)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("FAREBASIS");
            entity.Property(e => e.FltDate)
                .HasColumnType("DATE")
                .HasColumnName("FLT_DATE");
            entity.Property(e => e.FltNr)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("FLT_NR");
            entity.Property(e => e.FltSeqNr)
                .HasPrecision(10)
                .HasColumnName("FLT_SEQ_NR");
            entity.Property(e => e.Itin)
                .HasMaxLength(70)
                .IsUnicode(false)
                .HasColumnName("ITIN");
            entity.Property(e => e.LocalSales)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("LOCAL_SALES");
            entity.Property(e => e.MCls)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("M_CLS");
            entity.Property(e => e.Name)
                .IsUnicode(false)
                .HasColumnName("NAME");
            entity.Property(e => e.Origin)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("ORIGIN");
            entity.Property(e => e.PaxType)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("PAX_TYPE");
            entity.Property(e => e.Pos)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("POS");
            entity.Property(e => e.SeatNo)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("SEAT_NO");
            entity.Property(e => e.Sex)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("SEX");
            entity.Property(e => e.TktNr)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("TKT_NR");
        });

        modelBuilder.Entity<SixFsrFlight>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("SIX_FSR_FLIGHT");

            entity.Property(e => e.BookedpercabinF)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("BOOKEDPERCABIN_F");
            entity.Property(e => e.BookedpercabinJ)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("BOOKEDPERCABIN_J");
            entity.Property(e => e.BookedpercabinY)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("BOOKEDPERCABIN_Y");
            entity.Property(e => e.DateSent)
                .HasPrecision(6)
                .HasColumnName("DATE_SENT");
            entity.Property(e => e.Dcsflightlegid)
                .HasPrecision(12)
                .HasColumnName("DCSFLIGHTLEGID");
            entity.Property(e => e.Departuredatetimeutc)
                .HasPrecision(6)
                .HasColumnName("DEPARTUREDATETIMEUTC");
            entity.Property(e => e.FCapacity)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("F_CAPACITY");
            entity.Property(e => e.FileId)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("FILE_ID");
            entity.Property(e => e.FlightDeparturedate)
                .HasPrecision(6)
                .HasColumnName("FLIGHT_DEPARTUREDATE");
            entity.Property(e => e.FlightDestination)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("FLIGHT_DESTINATION");
            entity.Property(e => e.FlightFlightnumber)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("FLIGHT_FLIGHTNUMBER");
            entity.Property(e => e.FlightOrigin)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("FLIGHT_ORIGIN");
            entity.Property(e => e.FlightlegDestination)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("FLIGHTLEG_DESTINATION");
            entity.Property(e => e.FlightlegOrigin)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("FLIGHTLEG_ORIGIN");
            entity.Property(e => e.FltSeqNr)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("FLT_SEQ_NR");
            entity.Property(e => e.JCapacity)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("J_CAPACITY");
            entity.Property(e => e.PadpercabinF)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("PADPERCABIN_F");
            entity.Property(e => e.PadpercabinJ)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("PADPERCABIN_J");
            entity.Property(e => e.PadpercabinY)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("PADPERCABIN_Y");
            entity.Property(e => e.YCapacity)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("Y_CAPACITY");
        });

        modelBuilder.Entity<SixFsrPax>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("SIX_FSR_PAX");

            entity.Property(e => e.Cabincode)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("CABINCODE");
            entity.Property(e => e.DateSent)
                .HasPrecision(6)
                .HasColumnName("DATE_SENT");
            entity.Property(e => e.Dcspaxid)
                .HasColumnType("NUMBER")
                .HasColumnName("DCSPAXID");
            entity.Property(e => e.FileId)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("FILE_ID");
            entity.Property(e => e.Firstname)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("FIRSTNAME");
            entity.Property(e => e.FltSeqNr)
                .HasColumnType("NUMBER")
                .HasColumnName("FLT_SEQ_NR");
            entity.Property(e => e.Freetext)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("FREETEXT");
            entity.Property(e => e.Lastname)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("LASTNAME");
            entity.Property(e => e.Secondarytype)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("SECONDARYTYPE");
            entity.Property(e => e.Title)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("TITLE");
        });

        modelBuilder.Entity<SixFsrPaxSeg>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("SIX_FSR_PAX_SEG");

            entity.Property(e => e.Bookingclass)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("BOOKINGCLASS");
            entity.Property(e => e.DateSent)
                .HasPrecision(6)
                .HasColumnName("DATE_SENT");
            entity.Property(e => e.Dcspaxid)
                .HasColumnType("NUMBER")
                .HasColumnName("DCSPAXID");
            entity.Property(e => e.Dcspaxsegmentid)
                .HasColumnType("NUMBER")
                .HasColumnName("DCSPAXSEGMENTID");
            entity.Property(e => e.FileId)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("FILE_ID");
            entity.Property(e => e.Origin)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("ORIGIN");
        });

        modelBuilder.Entity<SixFsrPaxSer>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("SIX_FSR_PAX_SER");

            entity.Property(e => e.DateSent)
                .HasPrecision(6)
                .HasColumnName("DATE_SENT");
            entity.Property(e => e.Dcspaxid)
                .HasColumnType("NUMBER")
                .HasColumnName("DCSPAXID");
            entity.Property(e => e.Dcspaxserviceid)
                .HasColumnType("NUMBER")
                .HasColumnName("DCSPAXSERVICEID");
            entity.Property(e => e.FileId)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("FILE_ID");
            entity.Property(e => e.Servicecode)
                .HasMaxLength(5)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("SERVICECODE");
            entity.Property(e => e.Servicetype)
                .HasMaxLength(5)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("SERVICETYPE");
        });

        modelBuilder.Entity<SixFsrfbFlight>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("SIX_FSRFB_FLIGHT");

            entity.Property(e => e.DateSent)
                .HasPrecision(6)
                .HasColumnName("DATE_SENT");
            entity.Property(e => e.Departuredate)
                .HasPrecision(6)
                .HasComment("/flight")
                .HasColumnName("DEPARTUREDATE");
            entity.Property(e => e.Destination)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength()
                .HasComment("/flight")
                .HasColumnName("DESTINATION");
            entity.Property(e => e.FileId)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("FILE_ID");
            entity.Property(e => e.Flightnumber)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasComment("/flight")
                .HasColumnName("FLIGHTNUMBER");
            entity.Property(e => e.Origin)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength()
                .HasComment("/flight")
                .HasColumnName("ORIGIN");
            entity.Property(e => e.Status)
                .HasComment("/flight")
                .HasColumnType("NUMBER")
                .HasColumnName("STATUS");
        });

        modelBuilder.Entity<SixFsrfbFlightcount>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("SIX_FSRFB_FLIGHTCOUNT");

            entity.Property(e => e.Boarded)
                .HasComment("/flight/leg/flightCountFlightLink/flightCount/flightCountBreakdown")
                .HasColumnType("NUMBER")
                .HasColumnName("BOARDED");
            entity.Property(e => e.Booked)
                .HasComment("/flight/leg/flightCountFlightLink/flightCount/flightCountBreakdown")
                .HasColumnType("NUMBER")
                .HasColumnName("BOOKED");
            entity.Property(e => e.BreakdownStatus)
                .HasComment("/flight/leg/flightCountFlightLink/flightCount/flightCountBreakdown")
                .HasColumnType("NUMBER")
                .HasColumnName("BREAKDOWN_STATUS");
            entity.Property(e => e.Countattribute)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasComment("/flight/leg/flightCountFlightLink/flightCount/flightCountBreakdown")
                .HasColumnName("COUNTATTRIBUTE");
            entity.Property(e => e.DateSent)
                .HasPrecision(6)
                .HasColumnName("DATE_SENT");
            entity.Property(e => e.Departuredatetimeutc)
                .HasPrecision(6)
                .HasComment("/flight/leg/flightCountFlightLink/flightCount")
                .HasColumnName("DEPARTUREDATETIMEUTC");
            entity.Property(e => e.FileId)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("FILE_ID");
            entity.Property(e => e.FlightCountStatus)
                .HasComment("/flight/leg/flightCountFlightLink/flightCount")
                .HasColumnType("NUMBER")
                .HasColumnName("FLIGHT_COUNT_STATUS");
            entity.Property(e => e.Flightnumber)
                .HasComment("/flight")
                .HasColumnType("NUMBER")
                .HasColumnName("FLIGHTNUMBER");
        });

        modelBuilder.Entity<SixFsrfbLeg>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("SIX_FSRFB_LEG");

            entity.Property(e => e.CabinCode)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasComment("flight/leg/legCabin")
                .HasColumnName("CABIN_CODE");
            entity.Property(e => e.Capacity)
                .HasComment("/flight/leg/legCabin")
                .HasColumnType("NUMBER")
                .HasColumnName("CAPACITY");
            entity.Property(e => e.DateSent)
                .HasPrecision(6)
                .HasColumnName("DATE_SENT");
            entity.Property(e => e.DepartureDate)
                .HasPrecision(6)
                .HasComment("/flight/leg")
                .HasColumnName("DEPARTURE_DATE");
            entity.Property(e => e.Destination)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength()
                .HasComment("/flight/leg")
                .HasColumnName("DESTINATION");
            entity.Property(e => e.FileId)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("FILE_ID");
            entity.Property(e => e.Flightnumber)
                .HasComment("/flight")
                .HasColumnType("NUMBER")
                .HasColumnName("FLIGHTNUMBER");
            entity.Property(e => e.LegCabinStatus)
                .HasComment("/flight/leg/legCabin")
                .HasColumnType("NUMBER")
                .HasColumnName("LEG_CABIN_STATUS");
            entity.Property(e => e.LegStatus)
                .HasComment("/flight/leg")
                .HasColumnType("NUMBER")
                .HasColumnName("LEG_STATUS");
            entity.Property(e => e.Origin)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength()
                .HasComment("/flight/leg")
                .HasColumnName("ORIGIN");
        });

        modelBuilder.Entity<SixFsrfbSegment>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("SIX_FSRFB_SEGMENT");

            entity.Property(e => e.AddingDate)
                .HasPrecision(6)
                .HasComment("flight/segment/segmentCabin/segmentLink/bookingNameItem/bookingName/serviceLine")
                .HasColumnName("ADDING_DATE");
            entity.Property(e => e.CabinCode)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasComment("flight/segment/segmentCabin/segmentLink/bookingNameItem")
                .HasColumnName("CABIN_CODE");
            entity.Property(e => e.DateSent)
                .HasPrecision(6)
                .HasColumnName("DATE_SENT");
            entity.Property(e => e.DepartureDate)
                .HasPrecision(6)
                .HasComment("flight/segment")
                .HasColumnName("DEPARTURE_DATE");
            entity.Property(e => e.Destination)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength()
                .HasComment("flight/segment")
                .HasColumnName("DESTINATION");
            entity.Property(e => e.FileId)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("FILE_ID");
            entity.Property(e => e.FreeText)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasComment("flight/segment/segmentCabin/segmentLink/bookingNameItem/bookingName/serviceLine")
                .HasColumnName("FREE_TEXT");
            entity.Property(e => e.ItemStatus)
                .HasComment("flight/segment/segmentCabin/segmentLink/bookingNameItem")
                .HasColumnType("NUMBER")
                .HasColumnName("ITEM_STATUS");
            entity.Property(e => e.OperatingBookingClass)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasComment("flight/segment/segmentCabin/segmentLink/bookingNameItem")
                .HasColumnName("OPERATING_BOOKING_CLASS");
            entity.Property(e => e.Origin)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength()
                .HasComment("flight/segment")
                .HasColumnName("ORIGIN");
            entity.Property(e => e.RawName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasComment("flight/segment/segmentCabin/segmentLink/bookingNameItem/bookingName")
                .HasColumnName("RAW_NAME");
            entity.Property(e => e.SecondaryType)
                .HasMaxLength(6)
                .IsUnicode(false)
                .IsFixedLength()
                .HasComment("flight/segment/segmentCabin/segmentLink/bookingNameItem/bookingName/serviceLine")
                .HasColumnName("SECONDARY_TYPE");
            entity.Property(e => e.SegmentStatus)
                .HasComment("flight/segment")
                .HasColumnType("NUMBER")
                .HasColumnName("SEGMENT_STATUS");
            entity.Property(e => e.ServiceLineId)
                .HasComment("flight/segment/segmentCabin/segmentLink/bookingNameItem/bookingName/serviceLine")
                .HasColumnType("NUMBER")
                .HasColumnName("SERVICE_LINE_ID");
            entity.Property(e => e.ServiceLineState)
                .HasComment("flight/segment/segmentCabin/segmentLink/bookingNameItem/bookingName/serviceLine")
                .HasColumnType("NUMBER")
                .HasColumnName("SERVICE_LINE_STATE");
            entity.Property(e => e.ServiceLineTypeCode)
                .HasMaxLength(6)
                .IsUnicode(false)
                .IsFixedLength()
                .HasComment("flight/segment/segmentCabin/segmentLink/bookingNameItem/bookingName/serviceLine")
                .HasColumnName("SERVICE_LINE_TYPE_CODE");
        });

        modelBuilder.Entity<SixPtc>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("SIX_PTC");

            entity.Property(e => e.DateSent)
                .HasPrecision(6)
                .HasColumnName("DATE_SENT");
            entity.Property(e => e.Dcsflightlegid)
                .HasColumnType("NUMBER")
                .HasColumnName("DCSFLIGHTLEGID");
            entity.Property(e => e.Destination)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("DESTINATION");
            entity.Property(e => e.FileId)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("FILE_ID");
            entity.Property(e => e.FltSeqNr)
                .HasColumnType("NUMBER")
                .HasColumnName("FLT_SEQ_NR");
            entity.Property(e => e.Operatingcarrier)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("OPERATINGCARRIER");
            entity.Property(e => e.Operatingflightnumber)
                .HasColumnType("NUMBER")
                .HasColumnName("OPERATINGFLIGHTNUMBER");
            entity.Property(e => e.Origin)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("ORIGIN");
            entity.Property(e => e.Scheduleddeparturedatetime)
                .HasPrecision(6)
                .HasColumnName("SCHEDULEDDEPARTUREDATETIME");
            entity.Property(e => e.Stafftravelcount)
                .HasColumnType("NUMBER")
                .HasColumnName("STAFFTRAVELCOUNT");
        });

        modelBuilder.Entity<SsciDacPax>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("SSCI_DAC_PAX");

            entity.Property(e => e.Country)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("COUNTRY");
            entity.Property(e => e.Dob)
                .HasColumnType("DATE")
                .HasColumnName("DOB");
            entity.Property(e => e.Fltdate)
                .HasColumnType("DATE")
                .HasColumnName("FLTDATE");
            entity.Property(e => e.Fltseqnr)
                .HasColumnType("NUMBER")
                .HasColumnName("FLTSEQNR");
            entity.Property(e => e.Gender)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("GENDER");
            entity.Property(e => e.Paxname)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PAXNAME");
            entity.Property(e => e.Ppno)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("PPNO");
            entity.Property(e => e.Seatno)
                .HasMaxLength(6)
                .IsUnicode(false)
                .HasColumnName("SEATNO");
        });

        modelBuilder.Entity<SsciDeporteeOld>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("SSCI_DEPORTEE_OLD");

            entity.Property(e => e.FltSeqNr)
                .HasColumnType("NUMBER")
                .HasColumnName("FLT_SEQ_NR");
            entity.Property(e => e.PaxName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("PAX_NAME");
            entity.Property(e => e.Seat)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("SEAT");
        });

        modelBuilder.Entity<SsciPax>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("SSCI_PAX");

            entity.Property(e => e.AddDate)
                .HasDefaultValueSql("SYSDATE")
                .HasColumnType("DATE")
                .HasColumnName("ADD_DATE");
            entity.Property(e => e.Class)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("CLASS");
            entity.Property(e => e.EdCode)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("ED_CODE");
            entity.Property(e => e.FltSeqNr)
                .HasPrecision(10)
                .HasColumnName("FLT_SEQ_NR");
            entity.Property(e => e.PaxName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("PAX_NAME");
            entity.Property(e => e.Port)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("PORT");
            entity.Property(e => e.RLoc)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("R_LOC");
            entity.Property(e => e.Seat)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("SEAT");
            entity.Property(e => e.Type)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("TYPE");
            entity.Property(e => e.Vcr)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("VCR");
        });

        modelBuilder.Entity<SsciPending>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("SSCI_PENDING");

            entity.Property(e => e.ActualArvArpCd)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("ACTUAL_ARV_ARP_CD");
            entity.Property(e => e.ActualDepArpCd)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("ACTUAL_DEP_ARP_CD");
            entity.Property(e => e.FltNr)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("FLT_NR");
            entity.Property(e => e.FltSeqNr)
                .HasPrecision(10)
                .HasColumnName("FLT_SEQ_NR");
        });

        modelBuilder.Entity<SsciPf>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("SSCI_PFS");

            entity.Property(e => e.Arrival)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("ARRIVAL");
            entity.Property(e => e.Category)
                .HasMaxLength(5)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("CATEGORY");
            entity.Property(e => e.Cls)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("CLS");
            entity.Property(e => e.FfpNo)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("FFP_NO");
            entity.Property(e => e.FltSeqNr)
                .HasPrecision(10)
                .HasColumnName("FLT_SEQ_NR");
            entity.Property(e => e.PaxName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PAX_NAME");
            entity.Property(e => e.Rloc)
                .HasMaxLength(6)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("RLOC");
        });

        modelBuilder.Entity<SsciSumm>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("SSCI_SUMM");

            entity.Property(e => e.Authj)
                .HasPrecision(3)
                .HasColumnName("AUTHJ");
            entity.Property(e => e.Authy)
                .HasPrecision(3)
                .HasColumnName("AUTHY");
            entity.Property(e => e.Availj)
                .HasPrecision(3)
                .HasColumnName("AVAILJ");
            entity.Property(e => e.Availy)
                .HasPrecision(3)
                .HasColumnName("AVAILY");
            entity.Property(e => e.Bookedj)
                .HasPrecision(3)
                .HasColumnName("BOOKEDJ");
            entity.Property(e => e.Bookedy)
                .HasPrecision(3)
                .HasColumnName("BOOKEDY");
            entity.Property(e => e.Destination)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("DESTINATION");
            entity.Property(e => e.FltDate)
                .HasColumnType("DATE")
                .HasColumnName("FLT_DATE");
            entity.Property(e => e.FltNr)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("FLT_NR");
            entity.Property(e => e.FltSeqNr)
                .HasPrecision(10)
                .HasColumnName("FLT_SEQ_NR");
            entity.Property(e => e.Jumpj)
                .HasPrecision(3)
                .HasColumnName("JUMPJ");
            entity.Property(e => e.Jumpy)
                .HasPrecision(3)
                .HasColumnName("JUMPY");
            entity.Property(e => e.Lclonj)
                .HasPrecision(3)
                .HasColumnName("LCLONJ");
            entity.Property(e => e.Lclony)
                .HasPrecision(3)
                .HasColumnName("LCLONY");
            entity.Property(e => e.Lclrvj)
                .HasPrecision(3)
                .HasColumnName("LCLRVJ");
            entity.Property(e => e.Lclrvy)
                .HasPrecision(3)
                .HasColumnName("LCLRVY");
            entity.Property(e => e.Origin)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("ORIGIN");
            entity.Property(e => e.Otldj)
                .HasPrecision(3)
                .HasColumnName("OTLDJ");
            entity.Property(e => e.Otldy)
                .HasPrecision(3)
                .HasColumnName("OTLDY");
            entity.Property(e => e.Thrurvj)
                .HasPrecision(3)
                .HasColumnName("THRURVJ");
            entity.Property(e => e.Thrurvy)
                .HasPrecision(3)
                .HasColumnName("THRURVY");
            entity.Property(e => e.Tlobj)
                .HasPrecision(3)
                .HasColumnName("TLOBJ");
            entity.Property(e => e.Tloby)
                .HasPrecision(3)
                .HasColumnName("TLOBY");
        });

        modelBuilder.Entity<SsciWc>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("SSCI_WC");

            entity.Property(e => e.FltSeqNr)
                .HasColumnType("NUMBER")
                .HasColumnName("FLT_SEQ_NR");
            entity.Property(e => e.PaxName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("PAX_NAME");
            entity.Property(e => e.Seat)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("SEAT");
        });

        modelBuilder.Entity<Table1>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("TABLE1");

            entity.Property(e => e.Column1)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("COLUMN1");
        });

        modelBuilder.Entity<Table2>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("TABLE2");

            entity.Property(e => e.Column1)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("COLUMN1");
        });

        modelBuilder.Entity<TmpAdm>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("TMP_ADM");

            entity.Property(e => e.Cnttickets)
                .HasColumnType("NUMBER")
                .HasColumnName("CNTTICKETS");
            entity.Property(e => e.Fltseqnr)
                .HasColumnType("NUMBER")
                .HasColumnName("FLTSEQNR");
        });

        modelBuilder.Entity<TmpAdmTicket>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("TMP_ADM_TICKETS");

            entity.Property(e => e.Agentcode)
                .HasMaxLength(8)
                .IsUnicode(false)
                .HasColumnName("AGENTCODE");
            entity.Property(e => e.Arrival)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("ARRIVAL");
            entity.Property(e => e.Cardocnum)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("CARDOCNUM");
            entity.Property(e => e.Cls)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("CLS");
            entity.Property(e => e.Docnum)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("DOCNUM");
            entity.Property(e => e.FltDate)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("FLT_DATE");
            entity.Property(e => e.FltNr)
                .HasMaxLength(4)
                .IsUnicode(false)
                .HasColumnName("FLT_NR");
            entity.Property(e => e.Fltseqnr)
                .HasColumnType("NUMBER")
                .HasColumnName("FLTSEQNR");
            entity.Property(e => e.Issuedate)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("ISSUEDATE");
            entity.Property(e => e.Pnr)
                .HasMaxLength(9)
                .IsUnicode(false)
                .HasColumnName("PNR");
        });

        modelBuilder.Entity<TmpDatum>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("TMP_DATA");

            entity.Property(e => e.Cls)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("CLS");
            entity.Property(e => e.Cnt)
                .HasColumnType("NUMBER")
                .HasColumnName("CNT");
            entity.Property(e => e.Fltseqnr)
                .HasColumnType("NUMBER")
                .HasColumnName("FLTSEQNR");
        });

        modelBuilder.Entity<TmpDocMissing>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("TMP_DOC_MISSING");

            entity.Property(e => e.FltSeqNr)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("FLT_SEQ_NR");
            entity.Property(e => e.SeatNo)
                .HasMaxLength(26)
                .IsUnicode(false)
                .HasColumnName("SEAT_NO");
            entity.Property(e => e.TktNr)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("TKT_NR");
        });

        modelBuilder.Entity<TmpFlownDay>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("TMP_FLOWN_DAY");

            entity.Property(e => e.Cpnno)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("CPNNO");
            entity.Property(e => e.Docno)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("DOCNO");
            entity.Property(e => e.Farebasis)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("FAREBASIS");
            entity.Property(e => e.Flowndate)
                .HasColumnType("DATE")
                .HasColumnName("FLOWNDATE");
            entity.Property(e => e.Flownsector)
                .HasMaxLength(6)
                .IsUnicode(false)
                .HasColumnName("FLOWNSECTOR");
            entity.Property(e => e.Fltno)
                .HasMaxLength(6)
                .IsUnicode(false)
                .HasColumnName("FLTNO");
            entity.Property(e => e.Fltseqnr)
                .HasColumnType("NUMBER")
                .HasColumnName("FLTSEQNR");
            entity.Property(e => e.Itineary)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("ITINEARY");
            entity.Property(e => e.Orgdest)
                .HasMaxLength(6)
                .IsUnicode(false)
                .HasColumnName("ORGDEST");
            entity.Property(e => e.Paxtype)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("PAXTYPE");
            entity.Property(e => e.Pnrnbr)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("PNRNBR");
            entity.Property(e => e.Poscity)
                .HasMaxLength(45)
                .IsUnicode(false)
                .HasColumnName("POSCITY");
            entity.Property(e => e.Poscntry)
                .HasMaxLength(45)
                .IsUnicode(false)
                .HasColumnName("POSCNTRY");
            entity.Property(e => e.Proratedcpnamt)
                .HasColumnType("NUMBER")
                .HasColumnName("PRORATEDCPNAMT");
            entity.Property(e => e.Proratedyq)
                .HasColumnType("NUMBER")
                .HasColumnName("PRORATEDYQ");
            entity.Property(e => e.Rbd)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("RBD");
            entity.Property(e => e.Rficcode)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("RFICCODE");
            entity.Property(e => e.Saledate)
                .HasColumnType("DATE")
                .HasColumnName("SALEDATE");
            entity.Property(e => e.Tktdcabinclass)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("TKTDCABINCLASS");
        });

        modelBuilder.Entity<TmpFltSeq>(entity =>
        {
            entity.HasKey(e => e.Fltseqno).HasName("TMP_FLT_SEQ_PK");

            entity.ToTable("TMP_FLT_SEQ");

            entity.Property(e => e.Fltseqno)
                .HasColumnType("NUMBER")
                .HasColumnName("FLTSEQNO");
            entity.Property(e => e.Fltdate)
                .HasColumnType("DATE")
                .HasColumnName("FLTDATE");
        });

        modelBuilder.Entity<TmpFltseqSeat>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("TMP_FLTSEQ_SEAT");

            entity.Property(e => e.Fltseqno)
                .HasColumnType("NUMBER")
                .HasColumnName("FLTSEQNO");
            entity.Property(e => e.Seatno)
                .HasMaxLength(6)
                .IsUnicode(false)
                .HasColumnName("SEATNO");
        });

        modelBuilder.Entity<TmpIraqdetail>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("TMP_IRAQDETAILS");

            entity.Property(e => e.DocNumber)
                .IsUnicode(false)
                .HasColumnName("DOC_NUMBER");
            entity.Property(e => e.FltDate)
                .HasColumnType("DATE")
                .HasColumnName("FLT_DATE");
            entity.Property(e => e.FltNr)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("FLT_NR");
            entity.Property(e => e.FltSeqNr)
                .HasPrecision(10)
                .HasColumnName("FLT_SEQ_NR");
            entity.Property(e => e.Name)
                .IsUnicode(false)
                .HasColumnName("NAME");
            entity.Property(e => e.OrgDest)
                .HasMaxLength(6)
                .IsUnicode(false)
                .HasColumnName("ORG_DEST");
            entity.Property(e => e.Sex)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("SEX");
            entity.Property(e => e.TktNr)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("TKT_NR");
        });

        modelBuilder.Entity<TmpJPaxPak>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("TMP_J_PAX_PAK");

            entity.Property(e => e.Natly)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("NATLY");
            entity.Property(e => e.Passno)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("PASSNO");
            entity.Property(e => e.Tktno)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("TKTNO");
        });

        modelBuilder.Entity<TmpSeqno>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("TMP_SEQNO");

            entity.Property(e => e.Seqno)
                .HasColumnType("NUMBER")
                .HasColumnName("SEQNO");
        });

        modelBuilder.Entity<TmpSeqnr>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("TMP_SEQNR");

            entity.Property(e => e.Dest)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("DEST");
            entity.Property(e => e.Fltdate)
                .HasColumnType("DATE")
                .HasColumnName("FLTDATE");
            entity.Property(e => e.Fltnr)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("FLTNR");
            entity.Property(e => e.Fltseqnr)
                .HasColumnType("NUMBER")
                .HasColumnName("FLTSEQNR");
            entity.Property(e => e.Org)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("ORG");
        });

        modelBuilder.Entity<TmpTktnr1day>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("TMP_TKTNR_1DAY");

            entity.Property(e => e.FltSeqNr)
                .HasPrecision(10)
                .HasColumnName("FLT_SEQ_NR");
            entity.Property(e => e.TktNr)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("TKT_NR");
        });

        modelBuilder.Entity<VAcsiPaxManifest>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("V_ACSI_PAX_MANIFEST");

            entity.Property(e => e.Adddate)
                .HasColumnType("DATE")
                .HasColumnName("ADDDATE");
            entity.Property(e => e.Bagcount)
                .HasColumnType("NUMBER")
                .HasColumnName("BAGCOUNT");
            entity.Property(e => e.Bagwt)
                .HasColumnType("NUMBER")
                .HasColumnName("BAGWT");
            entity.Property(e => e.CCls)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("C_CLS");
            entity.Property(e => e.Destination)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("DESTINATION");
            entity.Property(e => e.Dob)
                .HasColumnType("DATE")
                .HasColumnName("DOB");
            entity.Property(e => e.DocNumber)
                .IsUnicode(false)
                .HasColumnName("DOC_NUMBER");
            entity.Property(e => e.Docissuedt)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("DOCISSUEDT");
            entity.Property(e => e.Filename)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("FILENAME");
            entity.Property(e => e.FltSeqNr)
                .HasPrecision(10)
                .HasColumnName("FLT_SEQ_NR");
            entity.Property(e => e.MCls)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("M_CLS");
            entity.Property(e => e.Name)
                .IsUnicode(false)
                .HasColumnName("NAME");
            entity.Property(e => e.PaxType)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("PAX_TYPE");
            entity.Property(e => e.Paxboardpoint)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("PAXBOARDPOINT");
            entity.Property(e => e.Paxdoc)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("PAXDOC");
            entity.Property(e => e.Paxnat)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("PAXNAT");
            entity.Property(e => e.Pnr)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("PNR");
            entity.Property(e => e.SeatNo)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("SEAT_NO");
            entity.Property(e => e.Sex)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("SEX");
            entity.Property(e => e.Splcode)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("SPLCODE");
            entity.Property(e => e.Splsrvc)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("SPLSRVC");
            entity.Property(e => e.Status)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("STATUS");
            entity.Property(e => e.TktNr)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("TKT_NR");
        });

        modelBuilder.Entity<VBookedInfo>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("V_BOOKED_INFO");

            entity.Property(e => e.AddDate)
                .HasColumnType("DATE")
                .HasColumnName("ADD_DATE");
            entity.Property(e => e.BoardingPoint)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("BOARDING_POINT");
            entity.Property(e => e.Booked)
                .HasPrecision(3)
                .HasColumnName("BOOKED");
            entity.Property(e => e.Capacity)
                .HasPrecision(3)
                .HasColumnName("CAPACITY");
            entity.Property(e => e.Class)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("CLASS");
            entity.Property(e => e.Dest)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("DEST");
            entity.Property(e => e.FltDtGmt)
                .HasColumnType("DATE")
                .HasColumnName("FLT_DT_GMT");
            entity.Property(e => e.FltSeqNr)
                .HasPrecision(10)
                .HasColumnName("FLT_SEQ_NR");
            entity.Property(e => e.Pad)
                .HasPrecision(3)
                .HasColumnName("PAD");
        });

        modelBuilder.Entity<VCheckinReport>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("V_CHECKIN_REPORT");

            entity.Property(e => e.CCls)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("C_CLS");
            entity.Property(e => e.Dob)
                .HasColumnType("DATE")
                .HasColumnName("DOB");
            entity.Property(e => e.DocNumber)
                .IsUnicode(false)
                .HasColumnName("DOC_NUMBER");
            entity.Property(e => e.Doctype)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("DOCTYPE");
            entity.Property(e => e.Expdt)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("EXPDT");
            entity.Property(e => e.FltSeqNr)
                .HasPrecision(10)
                .HasColumnName("FLT_SEQ_NR");
            entity.Property(e => e.Isscntry)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ISSCNTRY");
            entity.Property(e => e.Name)
                .IsUnicode(false)
                .HasColumnName("NAME");
            entity.Property(e => e.Paxnat)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("PAXNAT");
            entity.Property(e => e.Placissue)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("PLACISSUE");
            entity.Property(e => e.Sex)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("SEX");
        });

        modelBuilder.Entity<VFltDatum>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("V_FLT_DATA");

            entity.Property(e => e.ActualArvArpCd)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("ACTUAL_ARV_ARP_CD");
            entity.Property(e => e.ActualDepArpCd)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("ACTUAL_DEP_ARP_CD");
            entity.Property(e => e.Adddate)
                .HasColumnType("DATE")
                .HasColumnName("ADDDATE");
            entity.Property(e => e.AlnCd)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("ALN_CD");
            entity.Property(e => e.FltNr)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("FLT_NR");
            entity.Property(e => e.FltSeqNr)
                .HasColumnType("NUMBER")
                .HasColumnName("FLT_SEQ_NR");
            entity.Property(e => e.Isscntry)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("ISSCNTRY");
            entity.Property(e => e.PassengerPhone)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PASSENGER_PHONE");
            entity.Property(e => e.PaxName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("PAX_NAME");
            entity.Property(e => e.SchArvDt)
                .HasColumnType("DATE")
                .HasColumnName("SCH_ARV_DT");
            entity.Property(e => e.SchDepDt)
                .HasColumnType("DATE")
                .HasColumnName("SCH_DEP_DT");
        });

        modelBuilder.Entity<VPaxAllDetail>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("V_PAX_ALL_DETAILS");

            entity.Property(e => e.ActualArvArpCd)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("ACTUAL_ARV_ARP_CD");
            entity.Property(e => e.ActualArvDt)
                .HasColumnType("DATE")
                .HasColumnName("ACTUAL_ARV_DT");
            entity.Property(e => e.ActualDepArpCd)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("ACTUAL_DEP_ARP_CD");
            entity.Property(e => e.ActualDepDt)
                .HasColumnType("DATE")
                .HasColumnName("ACTUAL_DEP_DT");
            entity.Property(e => e.AlnCd)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("ALN_CD");
            entity.Property(e => e.ArvStationName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("ARV_STATION_NAME");
            entity.Property(e => e.Boarded)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("BOARDED");
            entity.Property(e => e.CnclCd)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("CNCL_CD");
            entity.Property(e => e.DepDt)
                .HasColumnType("DATE")
                .HasColumnName("DEP_DT");
            entity.Property(e => e.DepStationName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("DEP_STATION_NAME");
            entity.Property(e => e.Destination)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("DESTINATION");
            entity.Property(e => e.DisruptedFlag)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("DISRUPTED_FLAG");
            entity.Property(e => e.Dob)
                .HasColumnType("DATE")
                .HasColumnName("DOB");
            entity.Property(e => e.DocNumber)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("DOC_NUMBER");
            entity.Property(e => e.FfpFlag)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("FFP_FLAG");
            entity.Property(e => e.Ffpnum)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("FFPNUM");
            entity.Property(e => e.FltNr)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("FLT_NR");
            entity.Property(e => e.FltSeqNr)
                .HasColumnType("NUMBER")
                .HasColumnName("FLT_SEQ_NR");
            entity.Property(e => e.LegSeqNr)
                .HasColumnType("NUMBER")
                .HasColumnName("LEG_SEQ_NR");
            entity.Property(e => e.MCls)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("M_CLS");
            entity.Property(e => e.ManifestDate)
                .HasColumnType("DATE")
                .HasColumnName("MANIFEST_DATE");
            entity.Property(e => e.ManifestTktNr)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("MANIFEST_TKT_NR");
            entity.Property(e => e.PassengerEmail)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("PASSENGER_EMAIL");
            entity.Property(e => e.PassengerPhone)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("PASSENGER_PHONE");
            entity.Property(e => e.PaxClass)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("PAX_CLASS");
            entity.Property(e => e.PaxName)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("PAX_NAME");
            entity.Property(e => e.PaxType)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("PAX_TYPE");
            entity.Property(e => e.Paxboardpoint)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("PAXBOARDPOINT");
            entity.Property(e => e.Paxdoc)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PAXDOC");
            entity.Property(e => e.Paxnat)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("PAXNAT");
            entity.Property(e => e.Pnr)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("PNR");
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
            entity.Property(e => e.SeatNo)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("SEAT_NO");
            entity.Property(e => e.Sex)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("SEX");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("STATUS");
        });

        modelBuilder.Entity<VmslFlightList>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("VMSL_FLIGHT_LIST");

            entity.Property(e => e.Brd)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("BRD");
            entity.Property(e => e.Fdate)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("FDATE");
            entity.Property(e => e.Fltnum)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("FLTNUM");
            entity.Property(e => e.Off)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("OFF");
            entity.Property(e => e.Total)
                .IsUnicode(false)
                .HasColumnName("TOTAL");
        });

        modelBuilder.Entity<VwPaxManufest>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VW_PAX_MANUFEST");

            entity.Property(e => e.Dest)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("DEST");
            entity.Property(e => e.Fltdate)
                .HasColumnType("DATE")
                .HasColumnName("FLTDATE");
            entity.Property(e => e.Fltnr)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("FLTNR");
            entity.Property(e => e.Fltseqnr)
                .HasColumnType("NUMBER")
                .HasColumnName("FLTSEQNR");
            entity.Property(e => e.Name)
                .IsUnicode(false)
                .HasColumnName("NAME");
            entity.Property(e => e.Org)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("ORG");
            entity.Property(e => e.PaxClass)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("PAX_CLASS");
            entity.Property(e => e.PaxDestination)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("PAX_DESTINATION");
            entity.Property(e => e.PaxOrigin)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("PAX_ORIGIN");
            entity.Property(e => e.SeatNo)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("SEAT_NO");
            entity.Property(e => e.Sex)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("SEX");
            entity.Property(e => e.Status)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("STATUS");
            entity.Property(e => e.TktNr)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("TKT_NR");
            entity.Property(e => e.TlxPart)
                .HasMaxLength(6)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("TLX_PART");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
