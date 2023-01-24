using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace MiamiHeatRest.Models
{
    public partial class ScoutingReportsContext : DbContext
    {
        public ScoutingReportsContext()
        {
        }

        public ScoutingReportsContext(DbContextOptions<ScoutingReportsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<League> Leagues { get; set; }
        public virtual DbSet<Player> Players { get; set; }
        public virtual DbSet<ScoutingReport> ScoutingReports { get; set; }
        public virtual DbSet<Team> Teams { get; set; }
        public virtual DbSet<TeamPlayer> TeamPlayers { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=DESKTOP-LOLSMJK\\MSSQLSERVER01; Database=ScoutingReports; User ID=test; Password=test; TrustServerCertificate=True; Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<League>(entity =>
            {
                entity.HasKey(e => e.LeagueKey);

                entity.ToTable("League");

                entity.Property(e => e.LeagueKey).ValueGeneratedNever();

                entity.Property(e => e.Country).HasMaxLength(100);

                entity.Property(e => e.LeagueName).HasMaxLength(100);

                entity.Property(e => e.SearchDisplayFlag)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<Player>(entity =>
            {
                entity.HasKey(e => e.PlayerKey);

                entity.ToTable("Player");

                entity.Property(e => e.PlayerKey).ValueGeneratedNever();

                entity.Property(e => e.AgentName).HasMaxLength(200);

                entity.Property(e => e.AgentPhone).HasMaxLength(50);

                entity.Property(e => e.BirthDate).HasColumnType("date");

                entity.Property(e => e.BodyFat).HasColumnType("decimal(5, 2)");

                entity.Property(e => e.BodyFatSource)
                    .HasMaxLength(100)
                    .HasColumnName("BodyFat_Source");

                entity.Property(e => e.CommittedTo).HasMaxLength(200);

                entity.Property(e => e.CourtRunTime34)
                    .HasColumnType("decimal(5, 2)")
                    .HasColumnName("CourtRunTime_3_4");

                entity.Property(e => e.CourtRunTime34Source)
                    .HasMaxLength(100)
                    .HasColumnName("CourtRunTime_3_4_Source");

                entity.Property(e => e.DwhInsertDatetime)
                    .HasColumnType("datetime")
                    .HasColumnName("dwh_insert_datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DwhUpdateDatetime)
                    .HasColumnType("datetime")
                    .HasColumnName("dwh_update_datetime");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.GlplayerKey).HasColumnName("GLPlayerKey");

                entity.Property(e => e.Hand).HasMaxLength(10);

                entity.Property(e => e.HandLength).HasColumnType("decimal(5, 2)");

                entity.Property(e => e.HandWHSource)
                    .HasMaxLength(100)
                    .HasColumnName("Hand_W_H_Source");

                entity.Property(e => e.HandWidth).HasColumnType("decimal(5, 2)");

                entity.Property(e => e.Handedness).HasMaxLength(10);

                entity.Property(e => e.HandednessSource)
                    .HasMaxLength(100)
                    .HasColumnName("Handedness_Source");

                entity.Property(e => e.Height).HasColumnType("decimal(6, 4)");

                entity.Property(e => e.HeightSource)
                    .HasMaxLength(100)
                    .HasColumnName("Height_Source");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.StandingReach).HasColumnType("decimal(6, 4)");

                entity.Property(e => e.StandingReachSource)
                    .HasMaxLength(100)
                    .HasColumnName("StandingReach_Source");

                entity.Property(e => e.Urlphoto)
                    .HasMaxLength(250)
                    .HasColumnName("URLPhoto");

                entity.Property(e => e.VerticalJumpMax).HasColumnType("decimal(6, 4)");

                entity.Property(e => e.VerticalJumpMaxSource)
                    .HasMaxLength(100)
                    .HasColumnName("VerticalJumpMax_Source");

                entity.Property(e => e.VerticalJumpNoStep).HasColumnType("decimal(6, 4)");

                entity.Property(e => e.VerticalJumpNoStepSource)
                    .HasMaxLength(100)
                    .HasColumnName("VerticalJumpNoStep_Source");

                entity.Property(e => e.Weight).HasColumnType("decimal(6, 2)");

                entity.Property(e => e.WeightSource)
                    .HasMaxLength(100)
                    .HasColumnName("Weight_Source");

                entity.Property(e => e.Wing).HasColumnType("decimal(6, 4)");

                entity.Property(e => e.WingSource)
                    .HasMaxLength(100)
                    .HasColumnName("Wing_Source");
            });

            modelBuilder.Entity<ScoutingReport>(entity =>
            {
                entity.HasKey(e => new { e.PlayerKey, e.TeamKey, e.ScoutId });

                entity.ToTable("ScoutingReport");

                entity.Property(e => e.Comment).HasColumnType("text");

                entity.Property(e => e.DateCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<Team>(entity =>
            {
                entity.HasKey(e => e.TeamKey);

                entity.ToTable("Team");

                entity.HasIndex(e => new { e.TeamKey, e.LeagueKey }, "UK_DimTeam")
                    .IsUnique();

                entity.Property(e => e.TeamKey).ValueGeneratedNever();

                entity.Property(e => e.CoachName).HasMaxLength(100);

                entity.Property(e => e.Conference).HasMaxLength(100);

                entity.Property(e => e.CurrentNbateamFlg)
                    .HasColumnName("CurrentNBATeamFlg")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.LeagueKeyDomestic).HasColumnName("LeagueKey_Domestic");

                entity.Property(e => e.SubConference).HasMaxLength(100);

                entity.Property(e => e.TeamCity).HasMaxLength(100);

                entity.Property(e => e.TeamCountry).HasMaxLength(100);

                entity.Property(e => e.TeamName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.TeamNickname).HasMaxLength(100);

                entity.Property(e => e.Urlphoto)
                    .HasMaxLength(250)
                    .HasColumnName("URLPhoto");

                entity.HasOne(d => d.LeagueKeyNavigation)
                    .WithMany(p => p.TeamLeagueKeyNavigations)
                    .HasForeignKey(d => d.LeagueKey)
                    .HasConstraintName("FK_Team_League");

                entity.HasOne(d => d.LeagueKeyDomesticNavigation)
                    .WithMany(p => p.TeamLeagueKeyDomesticNavigations)
                    .HasForeignKey(d => d.LeagueKeyDomestic)
                    .HasConstraintName("FK_Team_LeagueDomestic");
            });

            modelBuilder.Entity<TeamPlayer>(entity =>
            {
                entity.HasKey(e => new { e.PlayerKey, e.TeamKey, e.SeasonKey });

                entity.ToTable("TeamPlayer");

                entity.Property(e => e.DwhInsertDatetime)
                    .HasColumnType("datetime")
                    .HasColumnName("dwh_insert_datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.PlayerKeyNavigation)
                    .WithMany(p => p.TeamPlayers)
                    .HasForeignKey(d => d.PlayerKey)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TeamPlayer_Player");

                entity.HasOne(d => d.TeamKeyNavigation)
                    .WithMany(p => p.TeamPlayers)
                    .HasForeignKey(d => d.TeamKey)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TeamPlayer_Team");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.AzureAdUserId)
                    .HasName("PK__User__76BABBB664B1F945");

                entity.ToTable("User");

                entity.Property(e => e.ActiveFlag)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
