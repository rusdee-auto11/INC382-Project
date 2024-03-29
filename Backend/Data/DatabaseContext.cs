﻿using System;
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

        public virtual DbSet<Accounts> Accounts { get; set; }
        public virtual DbSet<BayData> BayData { get; set; }
        public virtual DbSet<CostPriceGas> CostPriceGas { get; set; }
        public virtual DbSet<CustomerInfo> CustomerInfo { get; set; }
        public virtual DbSet<ExitGateData> ExitGateData { get; set; }
        public virtual DbSet<Gas> Gas { get; set; }
        public virtual DbSet<InboundWbdata> InboundWbdata { get; set; }
        public virtual DbSet<OutboundWbdata> OutboundWbdata { get; set; }
        public virtual DbSet<Popaper> Popaper { get; set; }
        public virtual DbSet<SaleOfficeData> SaleOfficeData { get; set; }
        public virtual DbSet<Transactions> Transactions { get; set; }
        public virtual DbSet<Truck> Truck { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Accounts>(entity =>
            {
                entity.HasKey(e => e.AccountId);

                entity.Property(e => e.AccountId)
                    .HasColumnName("Account_ID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.AccountName)
                    .IsRequired()
                    .HasColumnName("Account_name")
                    .HasMaxLength(50);

                entity.Property(e => e.AccountType)
                    .IsRequired()
                    .HasColumnName("Account_type")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<BayData>(entity =>
            {
                entity.HasKey(e => e.ServiceId);

                entity.Property(e => e.ServiceId)
                    .HasColumnName("Service_ID")
                    .HasMaxLength(50);

                entity.Property(e => e.Bay)
                    .IsRequired()
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

                entity.Property(e => e.ServiceTime).HasColumnName("Service_Time");

                entity.Property(e => e.TimeIn).HasColumnName("Time_In");

                entity.Property(e => e.TimeOut).HasColumnName("Time_Out");
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

            modelBuilder.Entity<CustomerInfo>(entity =>
            {
                entity.HasKey(e => e.CustomerId);

                entity.Property(e => e.CustomerId)
                    .HasColumnName("Customer_ID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.PhoneNo)
                    .IsRequired()
                    .HasColumnName("Phone_no")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TaxPayerId)
                    .IsRequired()
                    .HasColumnName("Tax_payer_ID")
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
            });

            modelBuilder.Entity<Gas>(entity =>
            {
                entity.Property(e => e.GasId)
                    .HasColumnName("Gas_ID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.GasType)
                    .HasColumnName("Gas_Type")
                    .HasMaxLength(50);
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

                entity.Property(e => e.ServiceTime).HasColumnName("Service_Time");

                entity.Property(e => e.TimeIn).HasColumnName("Time_In");

                entity.Property(e => e.TimeOut).HasColumnName("Time_Out");
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

                entity.Property(e => e.ServiceTime).HasColumnName("Service_Time");

                entity.Property(e => e.TimeIn).HasColumnName("Time_In");

                entity.Property(e => e.TimeOut).HasColumnName("Time_Out");
            });

            modelBuilder.Entity<Popaper>(entity =>
            {
                entity.HasKey(e => e.PoNo);

                entity.ToTable("POPaper");

                entity.Property(e => e.PoNo)
                    .HasColumnName("PO_no")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CustomerId)
                    .IsRequired()
                    .HasColumnName("Customer_ID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Date).HasColumnType("date");

                entity.Property(e => e.InvoiceNo)
                    .IsRequired()
                    .HasColumnName("Invoice_no")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Item)
                    .IsRequired()
                    .HasMaxLength(50);

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

                entity.Property(e => e.ServiceTime).HasColumnName("Service_Time");

                entity.Property(e => e.TimeIn).HasColumnName("Time_In");

                entity.Property(e => e.TimeOut).HasColumnName("Time_Out");
            });

            modelBuilder.Entity<Transactions>(entity =>
            {
                entity.HasKey(e => e.TransactionId)
                    .HasName("PK__Transact__9A8D56257D59B51A");

                entity.Property(e => e.TransactionId).HasColumnName("Transaction_ID");

                entity.Property(e => e.Date).HasColumnType("date");

                entity.Property(e => e.Description).HasMaxLength(250);

                entity.Property(e => e.PoNo)
                    .HasColumnName("PO_no")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.RefNo)
                    .HasColumnName("Ref_no")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Type)
                    .HasMaxLength(25)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Truck>(entity =>
            {
                entity.Property(e => e.TruckId)
                    .HasColumnName("Truck_ID")
                    .HasMaxLength(50);

                entity.Property(e => e.TruckDriverName)
                    .HasColumnName("TruckDriver_Name")
                    .HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
