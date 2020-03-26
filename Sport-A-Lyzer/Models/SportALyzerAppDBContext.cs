using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Sport_A_Lyzer.Models
{
    public partial class SportALyzerAppDbContext : DbContext
    {
        public SportALyzerAppDbContext()
        {
        }

        public SportALyzerAppDbContext(DbContextOptions<SportALyzerAppDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<FoulTypes> FoulTypes { get; set; }
        public virtual DbSet<Fouls> Fouls { get; set; }
        public virtual DbSet<GameEventTypes> GameEventTypes { get; set; }
        public virtual DbSet<GameEvents> GameEvents { get; set; }
        public virtual DbSet<Games> Games { get; set; }
        public virtual DbSet<GoalTypes> GoalTypes { get; set; }
        public virtual DbSet<Goals> Goals { get; set; }
        public virtual DbSet<GoalsEvents> GoalsEvents { get; set; }
        public virtual DbSet<Players> Players { get; set; }
        public virtual DbSet<Sports> Sports { get; set; }
        public virtual DbSet<Teams> Teams { get; set; }
        public virtual DbSet<Tournaments> Tournaments { get; set; }
        public virtual DbSet<Translations> Translations { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=SportALyzerAppDb;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FoulTypes>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Description).HasMaxLength(255);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Uikey)
                    .IsRequired()
                    .HasColumnName("UIKey")
                    .HasMaxLength(20)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<Fouls>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Description).HasMaxLength(255);

                entity.Property(e => e.FoulTypeId).HasColumnName("FoulTypeID");

                entity.Property(e => e.PlayerId).HasColumnName("PlayerID");

                entity.Property(e => e.TeamId).HasColumnName("TeamID");

                entity.Property(e => e.TimeStamp).HasColumnType("datetime");

                entity.HasOne(d => d.FoulType)
                    .WithMany(p => p.Fouls)
                    .HasForeignKey(d => d.FoulTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Fouls_FoulTypes");

                entity.HasOne(d => d.Player)
                    .WithMany(p => p.Fouls)
                    .HasForeignKey(d => d.PlayerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Fouls_Players");

                entity.HasOne(d => d.Team)
                    .WithMany(p => p.Fouls)
                    .HasForeignKey(d => d.TeamId)
                    .HasConstraintName("FK_Fouls_Teams");
            });

            modelBuilder.Entity<GameEventTypes>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Description).HasMaxLength(255);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Uikey)
                    .IsRequired()
                    .HasColumnName("UIKey")
                    .HasMaxLength(20)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<GameEvents>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.EventTypeId).HasColumnName("EventTypeID");

                entity.Property(e => e.GameId).HasColumnName("GameID");

                entity.Property(e => e.PlayerId).HasColumnName("PlayerID");

                entity.Property(e => e.TeamId).HasColumnName("TeamID");

                entity.Property(e => e.TimeStamp).HasColumnType("datetime");

                entity.HasOne(d => d.EventType)
                    .WithMany(p => p.GameEvents)
                    .HasForeignKey(d => d.EventTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GameEvents_GameEventTypes");

                entity.HasOne(d => d.Game)
                    .WithMany(p => p.GameEvents)
                    .HasForeignKey(d => d.GameId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GameEvents_Games");

                entity.HasOne(d => d.Player)
                    .WithMany(p => p.GameEvents)
                    .HasForeignKey(d => d.PlayerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GameEvents_Players");

                entity.HasOne(d => d.Team)
                    .WithMany(p => p.GameEvents)
                    .HasForeignKey(d => d.TeamId)
                    .HasConstraintName("FK_GameEvents_Teams");
            });

            modelBuilder.Entity<Games>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.AwayTeamId).HasColumnName("AwayTeamID");

                entity.Property(e => e.EndTime).HasColumnType("datetime");

                entity.Property(e => e.HomeTeamId).HasColumnName("HomeTeamID");

                entity.Property(e => e.StartTime).HasColumnType("datetime");

                entity.Property(e => e.TournamentId).HasColumnName("TournamentID");

                entity.HasOne(d => d.AwayTeam)
                    .WithMany(p => p.GamesAwayTeam)
                    .HasForeignKey(d => d.AwayTeamId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Games_Teams_Away");

                entity.HasOne(d => d.HomeTeam)
                    .WithMany(p => p.GamesHomeTeam)
                    .HasForeignKey(d => d.HomeTeamId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Games_Teams_Home");

                entity.HasOne(d => d.Tournament)
                    .WithMany(p => p.Games)
                    .HasForeignKey(d => d.TournamentId)
                    .HasConstraintName("FK_Games_Tournaments");
            });

            modelBuilder.Entity<GoalTypes>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Description).HasMaxLength(255);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.SportId).HasColumnName("SportID");

                entity.Property(e => e.Uikey)
                    .IsRequired()
                    .HasColumnName("UIKey")
                    .HasMaxLength(20)
                    .HasDefaultValueSql("('')");

                entity.HasOne(d => d.Sport)
                    .WithMany(p => p.GoalTypes)
                    .HasForeignKey(d => d.SportId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GoalTypes_Sports");
            });

            modelBuilder.Entity<Goals>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.GameId).HasColumnName("GameID");

                entity.Property(e => e.GoalTypeId).HasColumnName("GoalTypeID");

                entity.Property(e => e.PlayerId).HasColumnName("PlayerID");

                entity.Property(e => e.TeamId).HasColumnName("TeamID");

                entity.Property(e => e.TimeStamp).HasColumnType("datetime");

                entity.HasOne(d => d.Game)
                    .WithMany(p => p.Goals)
                    .HasForeignKey(d => d.GameId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Goals_Games");

                entity.HasOne(d => d.GoalType)
                    .WithMany(p => p.Goals)
                    .HasForeignKey(d => d.GoalTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Goals_GoalTypes");

                entity.HasOne(d => d.Player)
                    .WithMany(p => p.Goals)
                    .HasForeignKey(d => d.PlayerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Goals_Players");

                entity.HasOne(d => d.Team)
                    .WithMany(p => p.Goals)
                    .HasForeignKey(d => d.TeamId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Goals_Teams");
            });

            modelBuilder.Entity<GoalsEvents>(entity =>
            {
                entity.HasKey(e => new { e.GameEventId, e.GoalId })
                    .HasName("PK__GoalsEve__01FD8314BB6F3857");

                entity.Property(e => e.GameEventId).HasColumnName("GameEventID");

                entity.Property(e => e.GoalId).HasColumnName("GoalID");

                entity.HasOne(d => d.GameEvent)
                    .WithMany(p => p.GoalsEvents)
                    .HasForeignKey(d => d.GameEventId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GoalsEvents_GameEvents");

                entity.HasOne(d => d.Goal)
                    .WithMany(p => p.GoalsEvents)
                    .HasForeignKey(d => d.GoalId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GoalsEvents_Goals");
            });

            modelBuilder.Entity<Players>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.FirstName)
                    .HasMaxLength(100)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.TeamId).HasColumnName("TeamID");

                entity.HasOne(d => d.Team)
                    .WithMany(p => p.Players)
                    .HasForeignKey(d => d.TeamId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Players_Teams");
            });

            modelBuilder.Entity<Sports>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Description).HasMaxLength(255);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Uikey)
                    .IsRequired()
                    .HasColumnName("UIKey")
                    .HasMaxLength(20)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<Teams>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<Tournaments>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.EndTime).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsFixedLength();

                entity.Property(e => e.StartTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<Translations>(entity =>
            {
                entity.HasIndex(e => new { e.SportId, e.Uikey });

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.SportId).HasColumnName("SportID");

                entity.Property(e => e.Translation)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Uikey)
                    .IsRequired()
                    .HasColumnName("UIKey")
                    .HasMaxLength(20);

                entity.HasOne(d => d.Sport)
                    .WithMany(p => p.Translations)
                    .HasForeignKey(d => d.SportId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Translations_Sports");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
