using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace EventApp.Models
{
    public partial class EvenAppContext : DbContext
    {
        public virtual DbSet<Events> Events { get; set; }

        public EvenAppContext(DbContextOptions<EvenAppContext>options):base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<Events>(entity =>
            {
                entity.HasKey(e => e.EventId);

                entity.Property(e => e.EventId).HasColumnName("eventId");

                entity.Property(e => e.EventAddress)
                    .HasColumnName("eventAddress")
                    .HasMaxLength(350)
                    .IsUnicode(false);

                entity.Property(e => e.EventContact)
                    .HasColumnName("eventContact")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.EventDate)
                    .HasColumnName("eventDate")
                    .HasColumnType("date");

                entity.Property(e => e.EventDescription)
                    .HasColumnName("eventDescription")
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.EventLink)
                    .HasColumnName("eventLink")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.EventTitle)
                    .HasColumnName("eventTitle")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Picture).HasColumnType("image");

                entity.Property(e => e.PictureType)
                    .HasColumnName("pictureType")
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });
        }
    }
}
