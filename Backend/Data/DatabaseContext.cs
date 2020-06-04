using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Backend.Models;

namespace Backend.Data
{
    public partial class DatabaseContext : DbContext
    {
        public DatabaseContext()
        {
        }

        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<BayData> BayData { get; set; }
        public virtual DbSet<CostPriceGas> CostPriceGas { get; set; }
        public virtual DbSet<ExitGateData> ExitGateData { get; set; }
        public virtual DbSet<Gas> Gas { get; set; }
        public virtual DbSet<InboundWbdata> InboundWbdata { get; set; }
        public virtual DbSet<OutboundWbdata> OutboundWbdata { get; set; }
        public virtual DbSet<Popaper> Popaper { get; set; }
        public virtual DbSet<SaleOfficeData> SaleOfficeData { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BayData>(entity =>
            {
                entity.HasKey(e => e.ServiceId);

                entity.Property(e => e.ServiceId)
                    .HasColumnName("Service_ID")
                    .HasMaxLength(50);

                entity.Property(e => e.DateIn)
                    .HasColumnName("Date_In")
                    .HasColumnType("date");

                entity.Property(e => e.DateOut)
                    .HasColumnName("Date_Out")
                    .HasColumnType("date");

                entity.Property(e => e.GasId)
                    .IsRequired()
                    .HasColumnName("Gas_ID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PoNo)
                    .IsRequired()
                    .HasColumnName("PO_no")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TimeIn).HasColumnName("Time_In");

                entity.Property(e => e.TimeOut).HasColumnName("Time_Out");

                entity.HasOne(d => d.Gas)
                    .WithMany(p => p.BayData)
                    .HasForeignKey(d => d.GasId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__BayData__Gas_ID__693CA210");

                entity.HasOne(d => d.PoNoNavigation)
                    .WithMany(p => p.BayData)
                    .HasForeignKey(d => d.PoNo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__BayData__PO_no__66603565");
            });

            modelBuilder.Entity<CostPriceGas>(entity =>
            {
                entity.HasKey(e => e.GasPriceId);

                entity.Property(e => e.GasPriceId)
                    .HasColumnName("Gas_Price_ID")
                    .HasMaxLength(50);

                entity.Property(e => e.Date).HasColumnType("date");

                entity.Property(e => e.GasId)
                    .IsRequired()
                    .HasColumnName("Gas_ID")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ExitGateData>(entity =>
            {
                entity.HasKey(e => e.ServiceId);

                entity.Property(e => e.ServiceId)
                    .HasColumnName("Service_ID")
                    .HasMaxLength(50);

                entity.Property(e => e.DateIn)
                    .HasColumnName("Date_In")
                    .HasColumnType("date");

                entity.Property(e => e.PoNo)
                    .IsRequired()
                    .HasColumnName("PO_no")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TimeIn).HasColumnName("Time_In");

                entity.HasOne(d => d.PoNoNavigation)
                    .WithMany(p => p.ExitGateData)
                    .HasForeignKey(d => d.PoNo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ExitGateD__PO_no__68487DD7");
            });

            modelBuilder.Entity<Gas>(entity =>
            {
                entity.Property(e => e.GasId)
                    .HasColumnName("Gas_ID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.GasType)
                    .HasColumnName("Gas_Type")
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .IsFixedLength();
            });

            modelBuilder.Entity<InboundWbdata>(entity =>
            {
                entity.HasKey(e => e.ServiceId);

                entity.ToTable("InboundWBData");

                entity.Property(e => e.ServiceId)
                    .HasColumnName("Service_ID")
                    .HasMaxLength(50);

                entity.Property(e => e.DateIn)
                    .HasColumnName("Date_In")
                    .HasColumnType("date");

                entity.Property(e => e.DateOut)
                    .HasColumnName("Date_Out")
                    .HasColumnType("date");

                entity.Property(e => e.PoNo)
                    .IsRequired()
                    .HasColumnName("PO_no")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TimeIn).HasColumnName("Time_In");

                entity.Property(e => e.TimeOut).HasColumnName("Time_Out");

                entity.HasOne(d => d.PoNoNavigation)
                    .WithMany(p => p.InboundWbdata)
                    .HasForeignKey(d => d.PoNo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__InboundWB__PO_no__656C112C");
            });

            modelBuilder.Entity<OutboundWbdata>(entity =>
            {
                entity.HasKey(e => e.ServiceId);

                entity.ToTable("OutboundWBData");

                entity.Property(e => e.ServiceId)
                    .HasColumnName("Service_ID")
                    .HasMaxLength(50);

                entity.Property(e => e.DateIn)
                    .HasColumnName("Date_In")
                    .HasColumnType("date");

                entity.Property(e => e.DateOut)
                    .HasColumnName("Date_Out")
                    .HasColumnType("date");

                entity.Property(e => e.PoNo)
                    .IsRequired()
                    .HasColumnName("PO_no")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TimeIn).HasColumnName("Time_In");

                entity.Property(e => e.TimeOut).HasColumnName("Time_Out");

                entity.HasOne(d => d.PoNoNavigation)
                    .WithMany(p => p.OutboundWbdata)
                    .HasForeignKey(d => d.PoNo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__OutboundW__PO_no__6754599E");
            });

            modelBuilder.Entity<Popaper>(entity =>
            {
                entity.HasKey(e => e.PoNo);

                entity.ToTable("POPaper");

                entity.Property(e => e.PoNo)
                    .HasColumnName("PO_no")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CustomerId).HasColumnName("Customer_ID");

                entity.Property(e => e.Date).HasColumnType("date");

                entity.Property(e => e.Item)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PaymentNo)
                    .IsRequired()
                    .HasColumnName("Payment_no")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TruckId)
                    .IsRequired()
                    .HasColumnName("Truck_ID")
                    .HasMaxLength(50);

                entity.Property(e => e.UnitPriceId)
                    .HasColumnName("Unit_Price_ID")
                    .HasMaxLength(50);

                entity.HasOne(d => d.ItemNavigation)
                    .WithMany(p => p.Popaper)
                    .HasForeignKey(d => d.Item)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__POPaper__Item__6C190EBB");

                entity.HasOne(d => d.UnitPrice)
                    .WithMany(p => p.Popaper)
                    .HasForeignKey(d => d.UnitPriceId)
                    .HasConstraintName("FK__POPaper__Unit_Pr__6D0D32F4");
            });

            modelBuilder.Entity<SaleOfficeData>(entity =>
            {
                entity.HasKey(e => e.ServiceId);

                entity.Property(e => e.ServiceId)
                    .HasColumnName("Service_ID")
                    .HasMaxLength(50);

                entity.Property(e => e.DateIn)
                    .HasColumnName("Date_In")
                    .HasColumnType("date");

                entity.Property(e => e.DateOut)
                    .HasColumnName("Date_Out")
                    .HasColumnType("date");

                entity.Property(e => e.PoNo)
                    .IsRequired()
                    .HasColumnName("PO_no")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TimeIn).HasColumnName("Time_In");

                entity.Property(e => e.TimeOut).HasColumnName("Time_Out");

                entity.HasOne(d => d.PoNoNavigation)
                    .WithMany(p => p.SaleOfficeData)
                    .HasForeignKey(d => d.PoNo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__SaleOffic__PO_no__6477ECF3");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
