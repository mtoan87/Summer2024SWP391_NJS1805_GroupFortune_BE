﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

namespace DAL.Models
{
    public partial class JewelryAuctionContext : DbContext
    {
        public JewelryAuctionContext()
        {
        }

        public JewelryAuctionContext(DbContextOptions<JewelryAuctionContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; } = null!;
        public virtual DbSet<AccountWallet> AccountWallets { get; set; } = null!;
        public virtual DbSet<Auction> Auctions { get; set; } = null!;
        public virtual DbSet<AuctionResult> AuctionResults { get; set; } = null!;
        public virtual DbSet<Bid> Bids { get; set; } = null!;
        public virtual DbSet<BidRecord> BidRecords { get; set; } = null!;
        public virtual DbSet<JewelryGold> JewelryGolds { get; set; } = null!;
        public virtual DbSet<JewelryGoldDiamond> JewelryGoldDiamonds { get; set; } = null!;
        public virtual DbSet<JewelrySilver> JewelrySilvers { get; set; } = null!;
        public virtual DbSet<JoinAuction> JoinAuctions { get; set; } = null!;
        public virtual DbSet<Payment> Payments { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<WalletTransaction> WalletTransactions { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(this.GetConnectionString());
            }
        }

        private string GetConnectionString()
        {
            IConfiguration config = new ConfigurationBuilder()
                                        .SetBasePath(Directory.GetCurrentDirectory())
                                        .AddJsonFile("appsettings.json", true, true).Build();
            return config["ConnectionStrings:DefaultConnectionStringDB"];
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.ToTable("Account");

                entity.Property(e => e.AccountId).HasColumnName("account_id");

                entity.Property(e => e.AccountEmail)
                    .HasMaxLength(100)
                    .HasColumnName("account_email");

                entity.Property(e => e.AccountName)
                    .HasMaxLength(100)
                    .HasColumnName("account_name");

                entity.Property(e => e.AccountPassword)
                    .HasMaxLength(100)
                    .HasColumnName("account_password");

                entity.Property(e => e.AccountPhone)
                    .HasMaxLength(100)
                    .HasColumnName("account_phone");

                entity.Property(e => e.RoleId).HasColumnName("role_id");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Accounts)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK__Account__role_id__4BAC3F29");
            });

            modelBuilder.Entity<AccountWallet>(entity =>
            {
                entity.ToTable("AccountWallet");

                entity.Property(e => e.AccountwalletId).HasColumnName("accountwallet_id");

                entity.Property(e => e.AccountId).HasColumnName("account_id");

                entity.Property(e => e.BankName)
                    .HasMaxLength(100)
                    .HasColumnName("bank_name");

                entity.Property(e => e.BankNo).HasColumnName("bank_no");

                entity.Property(e => e.Budget).HasColumnName("budget");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.AccountWallets)
                    .HasForeignKey(d => d.AccountId)
                    .HasConstraintName("FK__AccountWa__accou__4E88ABD4");
            });

            modelBuilder.Entity<Auction>(entity =>
            {
                entity.ToTable("Auction");

                entity.Property(e => e.AuctionId).HasColumnName("auction_id");

                entity.Property(e => e.AccountId).HasColumnName("account_id");

                entity.Property(e => e.Endtime)
                    .HasColumnType("datetime")
                    .HasColumnName("endtime");

                entity.Property(e => e.JewelryGoldId).HasColumnName("jewelry_gold_id");

                entity.Property(e => e.JewelryGolddiaId).HasColumnName("jewelry_golddia_id");

                entity.Property(e => e.JewelrySilverId).HasColumnName("jewelry_silver_id");

                entity.Property(e => e.Starttime)
                    .HasColumnType("datetime")
                    .HasColumnName("starttime");

                entity.Property(e => e.Status)
                    .HasMaxLength(20)
                    .HasColumnName("status");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.Auctions)
                    .HasForeignKey(d => d.AccountId)
                    .HasConstraintName("FK__Auction__account__5CD6CB2B");

                entity.HasOne(d => d.JewelryGold)
                    .WithMany(p => p.Auctions)
                    .HasForeignKey(d => d.JewelryGoldId)
                    .HasConstraintName("FK__Auction__jewelry__5EBF139D");

                entity.HasOne(d => d.JewelryGolddia)
                    .WithMany(p => p.Auctions)
                    .HasForeignKey(d => d.JewelryGolddiaId)
                    .HasConstraintName("FK__Auction__jewelry__5FB337D6");

                entity.HasOne(d => d.JewelrySilver)
                    .WithMany(p => p.Auctions)
                    .HasForeignKey(d => d.JewelrySilverId)
                    .HasConstraintName("FK__Auction__jewelry__5DCAEF64");
            });

            modelBuilder.Entity<AuctionResult>(entity =>
            {
                entity.ToTable("AuctionResult");

                entity.Property(e => e.AuctionresultId).HasColumnName("auctionresult_id");

                entity.Property(e => e.AccountId).HasColumnName("account_id");

                entity.Property(e => e.Date)
                    .HasColumnType("datetime")
                    .HasColumnName("date");

                entity.Property(e => e.JoinauctionId).HasColumnName("joinauction_id");

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.Status)
                    .HasMaxLength(50)
                    .HasColumnName("status");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.AuctionResults)
                    .HasForeignKey(d => d.AccountId)
                    .HasConstraintName("FK__AuctionRe__accou__6EF57B66");

                entity.HasOne(d => d.Joinauction)
                    .WithMany(p => p.AuctionResults)
                    .HasForeignKey(d => d.JoinauctionId)
                    .HasConstraintName("FK__AuctionRe__joina__6E01572D");
            });

            modelBuilder.Entity<Bid>(entity =>
            {
                entity.ToTable("Bid");

                entity.Property(e => e.BidId).HasColumnName("bid_id");

                entity.Property(e => e.AuctionId).HasColumnName("auction_id");

                entity.Property(e => e.Datetime)
                    .HasColumnType("datetime")
                    .HasColumnName("datetime");

                entity.Property(e => e.Maxprice).HasColumnName("maxprice");

                entity.Property(e => e.Minprice).HasColumnName("minprice");

                entity.HasOne(d => d.Auction)
                    .WithMany(p => p.Bids)
                    .HasForeignKey(d => d.AuctionId)
                    .HasConstraintName("FK__Bid__auction_id__628FA481");
            });

            modelBuilder.Entity<BidRecord>(entity =>
            {
                entity.ToTable("Bid_record");

                entity.Property(e => e.BidRecordId).HasColumnName("bid_record_id");

                entity.Property(e => e.AccountId).HasColumnName("account_id");

                entity.Property(e => e.BidAmount).HasColumnName("bid_amount");

                entity.Property(e => e.BidId).HasColumnName("bid_id");

                entity.Property(e => e.BidStep).HasColumnName("bid_step");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.BidRecords)
                    .HasForeignKey(d => d.AccountId)
                    .HasConstraintName("FK__Bid_recor__accou__656C112C");

                entity.HasOne(d => d.Bid)
                    .WithMany(p => p.BidRecords)
                    .HasForeignKey(d => d.BidId)
                    .HasConstraintName("FK__Bid_recor__bid_i__66603565");
            });

            modelBuilder.Entity<JewelryGold>(entity =>
            {
                entity.ToTable("JewelryGold");

                entity.Property(e => e.JewelryGoldId).HasColumnName("jewelry_gold_id");

                entity.Property(e => e.AccountId).HasColumnName("account_id");

                entity.Property(e => e.Category)
                    .HasMaxLength(100)
                    .HasColumnName("category");

                entity.Property(e => e.Description)
                    .HasMaxLength(500)
                    .HasColumnName("description");

                entity.Property(e => e.GoldAge)
                    .HasMaxLength(100)
                    .HasColumnName("gold_age");

                entity.Property(e => e.JewelryImg)
                    .IsUnicode(false)
                    .HasColumnName("jewelry_img");

                entity.Property(e => e.Materials)
                    .HasMaxLength(200)
                    .HasColumnName("materials");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .HasColumnName("name");

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.Shipment)
                    .HasMaxLength(100)
                    .HasColumnName("shipment");

                entity.Property(e => e.Status)
                    .HasMaxLength(100)
                    .HasColumnName("status");

                entity.Property(e => e.Weight)
                    .HasMaxLength(200)
                    .HasColumnName("weight");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.JewelryGolds)
                    .HasForeignKey(d => d.AccountId)
                    .HasConstraintName("FK__JewelryGo__accou__571DF1D5");
            });

            modelBuilder.Entity<JewelryGoldDiamond>(entity =>
            {
                entity.HasKey(e => e.JewelryGolddiaId)
                    .HasName("PK__JewelryG__97244312DC3DB40A");

                entity.ToTable("JewelryGoldDiamond");

                entity.Property(e => e.JewelryGolddiaId).HasColumnName("jewelry_golddia_id");

                entity.Property(e => e.AccountId).HasColumnName("account_id");

                entity.Property(e => e.Carat)
                    .HasMaxLength(100)
                    .HasColumnName("carat");

                entity.Property(e => e.Category)
                    .HasMaxLength(100)
                    .HasColumnName("category");

                entity.Property(e => e.Clarity)
                    .HasMaxLength(100)
                    .HasColumnName("clarity");

                entity.Property(e => e.Description)
                    .HasMaxLength(500)
                    .HasColumnName("description");

                entity.Property(e => e.GoldAge)
                    .HasMaxLength(100)
                    .HasColumnName("gold_age");

                entity.Property(e => e.JewelryImg)
                    .IsUnicode(false)
                    .HasColumnName("jewelry_img");

                entity.Property(e => e.Materials)
                    .HasMaxLength(200)
                    .HasColumnName("materials");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .HasColumnName("name");

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.Shipment)
                    .HasMaxLength(100)
                    .HasColumnName("shipment");

                entity.Property(e => e.Status)
                    .HasMaxLength(100)
                    .HasColumnName("status");

                entity.Property(e => e.Weight)
                    .HasMaxLength(200)
                    .HasColumnName("weight");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.JewelryGoldDiamonds)
                    .HasForeignKey(d => d.AccountId)
                    .HasConstraintName("FK__JewelryGo__accou__59FA5E80");
            });

            modelBuilder.Entity<JewelrySilver>(entity =>
            {
                entity.ToTable("JewelrySilver");

                entity.Property(e => e.JewelrySilverId).HasColumnName("jewelry_silver_id");

                entity.Property(e => e.AccountId).HasColumnName("account_id");

                entity.Property(e => e.Category)
                    .HasMaxLength(100)
                    .HasColumnName("category");

                entity.Property(e => e.Description)
                    .HasMaxLength(500)
                    .HasColumnName("description");

                entity.Property(e => e.JewelryImg)
                    .IsUnicode(false)
                    .HasColumnName("jewelry_img");

                entity.Property(e => e.Materials)
                    .HasMaxLength(200)
                    .HasColumnName("materials");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .HasColumnName("name");

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.Purity)
                    .HasMaxLength(100)
                    .HasColumnName("purity");

                entity.Property(e => e.Shipment)
                    .HasMaxLength(100)
                    .HasColumnName("shipment");

                entity.Property(e => e.Status)
                    .HasMaxLength(100)
                    .HasColumnName("status");

                entity.Property(e => e.Weight)
                    .HasMaxLength(200)
                    .HasColumnName("weight");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.JewelrySilvers)
                    .HasForeignKey(d => d.AccountId)
                    .HasConstraintName("FK__JewelrySi__accou__5441852A");
            });

            modelBuilder.Entity<JoinAuction>(entity =>
            {
                entity.ToTable("JoinAuction");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AccountId).HasColumnName("account_id");

                entity.Property(e => e.AuctionId).HasColumnName("auction_id");

                entity.Property(e => e.BidId).HasColumnName("bid_id");

                entity.Property(e => e.Joindate)
                    .HasColumnType("datetime")
                    .HasColumnName("joindate");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.JoinAuctions)
                    .HasForeignKey(d => d.AccountId)
                    .HasConstraintName("FK__JoinAucti__accou__693CA210");

                entity.HasOne(d => d.Auction)
                    .WithMany(p => p.JoinAuctions)
                    .HasForeignKey(d => d.AuctionId)
                    .HasConstraintName("FK__JoinAucti__aucti__6A30C649");

                entity.HasOne(d => d.Bid)
                    .WithMany(p => p.JoinAuctions)
                    .HasForeignKey(d => d.BidId)
                    .HasConstraintName("FK__JoinAucti__bid_i__6B24EA82");
            });

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.ToTable("Payment");

                entity.Property(e => e.PaymentId).HasColumnName("payment_id");

                entity.Property(e => e.AccountId).HasColumnName("account_id");

                entity.Property(e => e.AuctionResultId).HasColumnName("auction_result_id");

                entity.Property(e => e.Date)
                    .HasColumnType("datetime")
                    .HasColumnName("date");

                entity.Property(e => e.Fee).HasColumnName("fee");

                entity.Property(e => e.Paymentmethod)
                    .HasMaxLength(100)
                    .HasColumnName("paymentmethod");

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.Status)
                    .HasMaxLength(100)
                    .HasColumnName("status");

                entity.Property(e => e.Totalprice).HasColumnName("totalprice");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.Payments)
                    .HasForeignKey(d => d.AccountId)
                    .HasConstraintName("FK__Payment__account__71D1E811");

                entity.HasOne(d => d.AuctionResult)
                    .WithMany(p => p.Payments)
                    .HasForeignKey(d => d.AuctionResultId)
                    .HasConstraintName("FK__Payment__auction__72C60C4A");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Role");

                entity.Property(e => e.RoleId)
                    .ValueGeneratedNever()
                    .HasColumnName("role_id");

                entity.Property(e => e.RoleName)
                    .HasMaxLength(50)
                    .HasColumnName("role_name");
            });

            modelBuilder.Entity<WalletTransaction>(entity =>
            {
                entity.HasKey(e => e.TransactionId)
                    .HasName("PK__wallet_t__85C600AF435A6DDB");

                entity.ToTable("wallet_transaction");

                entity.Property(e => e.TransactionId).HasColumnName("transaction_id");

                entity.Property(e => e.AccountwalletId).HasColumnName("accountwallet_id");

                entity.Property(e => e.Amount).HasColumnName("amount");

                entity.Property(e => e.DateTime)
                    .HasColumnType("datetime")
                    .HasColumnName("date_time");

                entity.HasOne(d => d.Accountwallet)
                    .WithMany(p => p.WalletTransactions)
                    .HasForeignKey(d => d.AccountwalletId)
                    .HasConstraintName("FK__wallet_tr__accou__5165187F");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
