using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

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
        public virtual DbSet<JewelryGold> JewelryGolds { get; set; } = null!;
        public virtual DbSet<JewelrySilver> JewelrySilvers { get; set; } = null!;
        public virtual DbSet<JoinAuction> JoinAuctions { get; set; } = null!;
        public virtual DbSet<Payment> Payments { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=(local);uid=sa;pwd=1234567890;database=JewelryAuction;TrustServerCertificate=True;");
            }
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

                entity.Property(e => e.DateofAuction)
                    .HasColumnType("date")
                    .HasColumnName("dateofAuction");

                entity.Property(e => e.Endtime).HasColumnName("endtime");

                entity.Property(e => e.JewelryGoldId).HasColumnName("jewelry_gold_id");

                entity.Property(e => e.JewelrySilverId).HasColumnName("jewelry_silver_id");

                entity.Property(e => e.Starttime).HasColumnName("starttime");

                entity.Property(e => e.Status)
                    .HasMaxLength(20)
                    .HasColumnName("status");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.Auctions)
                    .HasForeignKey(d => d.AccountId)
                    .HasConstraintName("FK__Auction__account__571DF1D5");

                entity.HasOne(d => d.JewelryGold)
                    .WithMany(p => p.Auctions)
                    .HasForeignKey(d => d.JewelryGoldId)
                    .HasConstraintName("FK__Auction__jewelry__59063A47");

                entity.HasOne(d => d.JewelrySilver)
                    .WithMany(p => p.Auctions)
                    .HasForeignKey(d => d.JewelrySilverId)
                    .HasConstraintName("FK__Auction__jewelry__5812160E");
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
                    .HasConstraintName("FK__AuctionRe__accou__656C112C");

                entity.HasOne(d => d.Joinauction)
                    .WithMany(p => p.AuctionResults)
                    .HasForeignKey(d => d.JoinauctionId)
                    .HasConstraintName("FK__AuctionRe__joina__6477ECF3");
            });

            modelBuilder.Entity<Bid>(entity =>
            {
                entity.ToTable("Bid");

                entity.Property(e => e.BidId).HasColumnName("bid_id");

                entity.Property(e => e.AccountId).HasColumnName("account_id");

                entity.Property(e => e.AuctionId).HasColumnName("auction_id");

                entity.Property(e => e.Datetime)
                    .HasColumnType("datetime")
                    .HasColumnName("datetime");

                entity.Property(e => e.Maxprice).HasColumnName("maxprice");

                entity.Property(e => e.Minprice).HasColumnName("minprice");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.Bids)
                    .HasForeignKey(d => d.AccountId)
                    .HasConstraintName("FK__Bid__account_id__5BE2A6F2");

                entity.HasOne(d => d.Auction)
                    .WithMany(p => p.Bids)
                    .HasForeignKey(d => d.AuctionId)
                    .HasConstraintName("FK__Bid__auction_id__5CD6CB2B");
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

                entity.Property(e => e.JewelryImg).HasColumnName("jewelry_img");

                entity.Property(e => e.Materials)
                    .HasMaxLength(200)
                    .HasColumnName("materials");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .HasColumnName("name");

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.Weight)
                    .HasMaxLength(200)
                    .HasColumnName("weight");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.JewelryGolds)
                    .HasForeignKey(d => d.AccountId)
                    .HasConstraintName("FK__JewelryGo__accou__5441852A");
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

                entity.Property(e => e.JewelryImg).HasColumnName("jewelry_img");

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

                entity.Property(e => e.Weight)
                    .HasMaxLength(200)
                    .HasColumnName("weight");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.JewelrySilvers)
                    .HasForeignKey(d => d.AccountId)
                    .HasConstraintName("FK__JewelrySi__accou__5165187F");
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
                    .HasConstraintName("FK__JoinAucti__accou__5FB337D6");

                entity.HasOne(d => d.Auction)
                    .WithMany(p => p.JoinAuctions)
                    .HasForeignKey(d => d.AuctionId)
                    .HasConstraintName("FK__JoinAucti__aucti__60A75C0F");

                entity.HasOne(d => d.Bid)
                    .WithMany(p => p.JoinAuctions)
                    .HasForeignKey(d => d.BidId)
                    .HasConstraintName("FK__JoinAucti__bid_i__619B8048");
            });

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.ToTable("Payment");

                entity.Property(e => e.PaymentId).HasColumnName("payment_id");

                entity.Property(e => e.AccountId).HasColumnName("account_id");

                entity.Property(e => e.Date)
                    .HasColumnType("datetime")
                    .HasColumnName("date");

                entity.Property(e => e.Fee).HasColumnName("fee");

                entity.Property(e => e.JewelryGoldId).HasColumnName("jewelry_gold_id");

                entity.Property(e => e.JewelrySilverId).HasColumnName("jewelry_silver_id");

                entity.Property(e => e.JoinauctionId).HasColumnName("joinauction_id");

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
                    .HasConstraintName("FK__Payment__account__68487DD7");

                entity.HasOne(d => d.JewelryGold)
                    .WithMany(p => p.Payments)
                    .HasForeignKey(d => d.JewelryGoldId)
                    .HasConstraintName("FK__Payment__jewelry__6A30C649");

                entity.HasOne(d => d.JewelrySilver)
                    .WithMany(p => p.Payments)
                    .HasForeignKey(d => d.JewelrySilverId)
                    .HasConstraintName("FK__Payment__jewelry__6B24EA82");

                entity.HasOne(d => d.Joinauction)
                    .WithMany(p => p.Payments)
                    .HasForeignKey(d => d.JoinauctionId)
                    .HasConstraintName("FK__Payment__joinauc__693CA210");
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

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
