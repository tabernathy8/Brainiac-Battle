using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using BrainiacBattle.Models;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using IdentityServer4.EntityFramework.Options;
using Microsoft.Extensions.Options;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace BrainiacBattle.Data
{
    public partial class BrainiacBattleContext : ApiAuthorizationDbContext<ApplicationUser>
    {
        public BrainiacBattleContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }

        public virtual DbSet<AccountGameStatistics> AccountGameStatistics { get; set; }
        public virtual DbSet<AccountCategoryStatistics> AccountCategoryStatistics { get; set; }
        public virtual DbSet<Accounts> Accounts { get; set; }
        public virtual DbSet<AccountsBadges> AccountsBadges { get; set; }
        public virtual DbSet<Badges> Badges { get; set; }
        public virtual DbSet<Benefits> Benefits { get; set; }
        public virtual DbSet<Categories> Categories { get; set; }
        public virtual DbSet<Games> Games { get; set; }
        public virtual DbSet<MultiplayerResult> MultiplayerResult { get; set; }
        public virtual DbSet<Results> Results { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Name=BrainiacBattleDB");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AccountGameStatistics>(entity =>
            {
                entity.HasKey(e => e.AccountGameStatisticId);

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.AccountGameStatistics)
                    .HasForeignKey(d => d.AccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AccountGameStatistics_Accounts");

                entity.HasOne(d => d.Game)
                    .WithMany(p => p.AccountGameStatistics)
                    .HasForeignKey(d => d.GameId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AccountGameStatistics_Games");
            });

            modelBuilder.Entity<AccountCategoryStatistics>(entity =>
            {
                entity.HasKey(e => e.AccountCategoryStatisticId);

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.AccountCategoryStatistics)
                    .HasForeignKey(d => d.AccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AccountCategoryStatistics_Accounts");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.AccountCategoryStatistics)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AccountCategoryStatistics_Games");
            });

            modelBuilder.Entity<Accounts>(entity =>
            {
                entity.HasKey(e => e.AccountId);

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50);
                    

                entity.HasOne(d => d.Game)
                    .WithMany(p => p.Accounts)
                    .HasForeignKey(d => d.CurrentGameId)
                    .HasConstraintName("FK_Accounts_Games");
            });

            modelBuilder.Entity<AccountsBadges>(entity =>
            {
                entity.HasKey(e => e.AccountBadgeId);

                entity.Property(e => e.DateAdded)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.AccountsBadges)
                    .HasForeignKey(d => d.AccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AccountsBadges_Accounts");

                entity.HasOne(d => d.Badge)
                    .WithMany(p => p.AccountsBadges)
                    .HasForeignKey(d => d.BadgeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AccountsBadges_Badges");
            });

            modelBuilder.Entity<Badges>(entity =>
            {
                entity.HasKey(e => e.BadgeId);

                entity.Property(e => e.Description).HasMaxLength(300);

                entity.Property(e => e.ImgSrc)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Game)
                    .WithMany(p => p.Badges)
                    .HasForeignKey(d => d.GameId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Badges_Games");
            });

            modelBuilder.Entity<Benefits>(entity =>
            {
                entity.HasKey(e => e.BenefitId);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Reference).HasMaxLength(100);

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Benefits)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Benefits_Categories");
            });

            modelBuilder.Entity<Categories>(entity =>
            {
                entity.HasKey(e => e.CategoryId);

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Games>(entity =>
            {
                entity.HasKey(e => e.GameId);

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.ImgSrc)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Games)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Games_Categories");
            });

            modelBuilder.Entity<MultiplayerResult>(entity =>
            {
                entity.Property(e => e.DateAdded)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsP1win).HasColumnName("IsP1Win");
            });

            modelBuilder.Entity<Results>(entity =>
            {
                entity.HasKey(e => e.ResultId);

                entity.Property(e => e.DateAdded)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.Results)
                    .HasForeignKey(d => d.AccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Results_Accounts");

                entity.HasOne(d => d.Game)
                    .WithMany(p => p.Results)
                    .HasForeignKey(d => d.GameId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Results_Games");

                entity.HasOne(d => d.MultiplayerResult)
                    .WithMany(p => p.Results)
                    .HasForeignKey(d => d.MultiplayerResultId)
                    .HasConstraintName("FK_Results_MultiplayerResult");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
