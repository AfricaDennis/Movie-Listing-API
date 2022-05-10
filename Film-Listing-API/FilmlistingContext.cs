using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Film_Listing_API
{
    public partial class FilmlistingContext : DbContext
    {
        public FilmlistingContext()
        {
        }

        public FilmlistingContext(DbContextOptions<FilmlistingContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Actor> Actors { get; set; }
        public virtual DbSet<Movie> Movies { get; set; }
        public virtual DbSet<MovieActor> MovieActors { get; set; }
        public virtual DbSet<MovieProducer> MovieProducers { get; set; }
        public virtual DbSet<Producer> Producers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=localhost\\SQLEXPRESS;Initial Catalog=Film-listing;User ID=scuser;Password=scuser");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Actor>(entity =>
            {
                entity.ToTable("Actor");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.BirthDate).HasColumnType("date");

                entity.Property(e => e.Image)
                    .IsRequired()
                    .HasMaxLength(650);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(250);
            });

            modelBuilder.Entity<Movie>(entity =>
            {
                entity.ToTable("Movie");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.Image)
                    .IsRequired()
                    .HasMaxLength(600);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.ReleaseDate).HasColumnType("date");

                entity.Property(e => e.Synopsis)
                    .IsRequired()
                    .HasMaxLength(555);
            });

            modelBuilder.Entity<MovieActor>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("MovieActor");

                entity.Property(e => e.ActorId).HasColumnName("ActorID");

                entity.Property(e => e.MovieId).HasColumnName("MovieID");

                entity.HasOne(d => d.Actor)
                    .WithMany()
                    .HasForeignKey(d => d.ActorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MovieActor_Actor");

                entity.HasOne(d => d.Movie)
                    .WithMany()
                    .HasForeignKey(d => d.MovieId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MovieActor_Movie");
            });

            modelBuilder.Entity<MovieProducer>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("MovieProducer");

                entity.Property(e => e.MovieId).HasColumnName("MovieID");

                entity.Property(e => e.ProducerId).HasColumnName("ProducerID");

                entity.HasOne(d => d.Movie)
                    .WithMany()
                    .HasForeignKey(d => d.MovieId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MovieProducer_Movie");

                entity.HasOne(d => d.Producer)
                    .WithMany()
                    .HasForeignKey(d => d.ProducerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MovieProducer_Producer");
            });

            modelBuilder.Entity<Producer>(entity =>
            {
                entity.ToTable("Producer");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.FundationDate).HasColumnType("date");

                entity.Property(e => e.Image)
                    .IsRequired()
                    .HasMaxLength(650);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(250);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
