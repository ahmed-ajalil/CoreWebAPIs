using System;
using System.Collections.Generic;
using CoreWebAPIs.Models;
using Microsoft.EntityFrameworkCore;

namespace CoreWebAPIs.Context 
{
    public partial class EBriefingDbContext : DbContext
    {
        public EBriefingDbContext()
        {
        }

        public EBriefingDbContext(DbContextOptions<EBriefingDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AirportCode> AirportCodes { get; set; }

        public virtual DbSet<OtpFlightInfo> OtpFlightInfos { get; set; }

        public virtual DbSet<OtpFlightStatus> OtpFlightStatuses { get; set; }

        public virtual DbSet<otpGatesdep> otpGatesdep { get; set; }

        public virtual DbSet<AiportTimeZone> AiportTimeZones { get; set; }

        public virtual DbSet<OtpPassengerDetails> OtpPassengerDetail { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
            => optionsBuilder.UseSqlServer("Server=ebriefingdb.database.windows.net;Database=eBriefingDB;User ID=CSM;Password=Puzzle#888;TrustServerCertificate=True;");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AirportCode>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__AirportC__3214EC074C394D62");

                entity.Property(e => e.AirportName)
                    .HasMaxLength(250)
                    .IsUnicode(false);
                entity.Property(e => e.City)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.Code)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.Country)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<otpGatesdep>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.FltDt)
                    .HasColumnType("datetime")
                    .HasColumnName("FltDt");
                entity.Property(e => e.FltNr)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("FltNr");
                entity.Property(e => e.Dep)
                     .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Dep");
                entity.Property(e => e.Arr)
                     .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Arr");
                entity.Property(e => e.Gat)
                     .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Gat");
                entity.Property(e => e.Bstpln)
                    .HasColumnType("datetime")
                    .HasColumnName("Bstpln");
                entity.Property(e => e.Gclpln)
                    .HasColumnType("datetime")
                    .HasColumnName("Gclpln");
            });

            modelBuilder.Entity<OtpFlightInfo>(entity =>
            {
                entity.HasKey(e => e.SequenceNumber).HasName("PK__OtpFligh__7625BE5C11F58518");

                entity.ToTable("OtpFlightInfo");

                entity.Property(e => e.SequenceNumber)
                    .ValueGeneratedNever()
                    .HasColumnName("sequence_Number");
                entity.Property(e => e.ActualArrivalAirport)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasColumnName("actual_Arrival_Airport");
                entity.Property(e => e.ActualDepartureAirport)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasColumnName("actual_Departure_Airport");
                entity.Property(e => e.FlightDate).HasColumnName("flight_Date");
                entity.Property(e => e.FlightNumber)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("flight_Number");
                entity.Property(e => e.ScheduledArrivalDateTime)
                    .HasColumnType("datetime")
                    .HasColumnName("scheduled_Arrival_DateTime");
                entity.Property(e => e.ScheduledDepartureDateTime)
                    .HasColumnType("datetime")
                    .HasColumnName("scheduled_Departure_DateTime");
                entity.Property(e => e.ActualArrivalDateTime)
                   .HasColumnType("datetime")
                   .HasColumnName("actual_Arrival_DateTime");
                entity.Property(e => e.ActualDepartureDateTime)
                    .HasColumnType("datetime")
                    .HasColumnName("actual_Departure_DateTime");
                entity.Property(e => e.PublishArrivalDateTime)
                   .HasColumnType("datetime")
                   .HasColumnName("publish_Arrival_DateTime");
                entity.Property(e => e.PublishDepartureDateTime)
                    .HasColumnType("datetime")
                    .HasColumnName("publish_Departure_DateTime");
                entity.Property(e => e.Status)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("status");
                entity.Property(e => e.CurrentStatus)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("current_Status");
                entity.Property(e => e.LegSequenceNumber)
                    .HasColumnName("Leg_Sequence_Number");

                // Configure one-to-one relationship
                entity.HasOne(f => f.OtpPassengerDetail)
            .WithOne(p => p.FlightSequenceNumberNavigation)
            .HasForeignKey<OtpPassengerDetails>(p => p.FlightSequenceNumber)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_OtpPassengerDetail_OtpFlightInfo");
            });

            modelBuilder.Entity<OtpPassengerDetails>(entity =>
            {
                entity.HasKey(e => e.FlightSequenceNumber).HasName("PK__OtpPassengerDetail__SequenceNumber");

                entity.ToTable("OtpPassengerDetails");

                entity.Property(e => e.FlightSequenceNumber).HasColumnName("flight_Sequence_Number");
                entity.Property(e => e.Total).HasColumnName("total");
                entity.Property(e => e.First).HasColumnName("first");
                entity.Property(e => e.Business).HasColumnName("business");
                entity.Property(e => e.Coach).HasColumnName("coach");
                entity.Property(e => e.Adult).HasColumnName("adult");
                entity.Property(e => e.Child).HasColumnName("child");
                entity.Property(e => e.Infant).HasColumnName("infant");
                entity.Property(e => e.SeatConfig).HasMaxLength(50).IsUnicode(false).HasColumnName("seatConfig");
                entity.Property(e => e.YSeatConfig).HasMaxLength(50).IsUnicode(false).HasColumnName("ySeatConfig");
                entity.Property(e => e.JSeatConfig).HasMaxLength(50).IsUnicode(false).HasColumnName("jSeatConfig");
                entity.Property(e => e.BookedJ).HasColumnName("bookedJ");
                entity.Property(e => e.BookedY).HasColumnName("bookedY");

                // Configure the inverse navigation property
                entity.HasOne(d => d.FlightSequenceNumberNavigation)
                      .WithOne(p => p.OtpPassengerDetail)
                      .HasForeignKey<OtpPassengerDetails>(d => d.FlightSequenceNumber)
                      .OnDelete(DeleteBehavior.ClientSetNull)
                      .HasConstraintName("FK_OtpPassengerDetail_OtpFlightInfo");
            });

            modelBuilder.Entity<OtpFlightStatus>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__OtpFligh__3214EC07025805AF");

                entity.ToTable("OtpFlightStatus");

                entity.Property(e => e.ActualAirborne).HasColumnType("datetime");
                entity.Property(e => e.ActualLanding).HasColumnType("datetime");
                entity.Property(e => e.ActualOffblocks).HasColumnType("datetime");
                entity.Property(e => e.ActualOnblocks).HasColumnType("datetime");
                entity.Property(e => e.CancelledTime)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.CarrierCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.CnclCd)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.DepGates)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.EstArvDt).HasColumnType("datetime");
                entity.Property(e => e.EstDepDt).HasColumnType("datetime");
                entity.Property(e => e.FltDt).HasColumnType("datetime");
                entity.Property(e => e.FltManipCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.FltNr)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.FltSeqNr)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.GrdReturn)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.LastUpdated).HasColumnType("datetime");
                entity.Property(e => e.LatestArvArpCd)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.LatestArvDt).HasColumnType("datetime");
                entity.Property(e => e.LatestDepArpCd)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.LatestDepDt).HasColumnType("datetime");
                entity.Property(e => e.LatestEqpCd)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.LatestEqpCdType)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.LatestTailNr)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.LegStatus)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.LocalRemarks)
                    .HasMaxLength(255)
                    .IsUnicode(false);
                entity.Property(e => e.OpSuffix)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.PreEndStatus)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.PreStartStatus)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.PubSchArvDt).HasColumnType("datetime");
                entity.Property(e => e.PubSchDepDt).HasColumnType("datetime");
                entity.Property(e => e.SchArvArpCd)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.SchArvDt).HasColumnType("datetime");
                entity.Property(e => e.SchDepArpCd)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.SchDepDt).HasColumnType("datetime");
                entity.Property(e => e.SchEqpCd)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.SchTailNr)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.SvcType)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<AiportTimeZone>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.AirportCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TimeDiff)
                    .IsRequired(false);
            });


            OnModelCreatingPartial(modelBuilder);
        }


        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}


