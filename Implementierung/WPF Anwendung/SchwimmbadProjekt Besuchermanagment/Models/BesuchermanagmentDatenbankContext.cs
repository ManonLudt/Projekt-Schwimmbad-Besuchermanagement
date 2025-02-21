using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Schwimmbad_Besuchermanagment.Models
{
    public partial class BesuchermanagmentDatenbankContext : DbContext
    {
        public BesuchermanagmentDatenbankContext()
        {
        }

        public BesuchermanagmentDatenbankContext(DbContextOptions<BesuchermanagmentDatenbankContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Benutzer> Benutzers { get; set; } = null!;
        public virtual DbSet<Besucher> Besuchers { get; set; } = null!;
        public virtual DbSet<Status> Statuses { get; set; } = null!;
        public virtual DbSet<Ticket> Tickets { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=BesuchermanagmentDatenbank;Integrated Security=SSPI");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Benutzer>(entity =>
            {
                entity.HasKey(e => e.IdBenutzer)
                    .HasName("PK__Benutzer__F4BE357579F7B671");

                entity.ToTable("Benutzer");

                entity.Property(e => e.IdBenutzer).HasColumnName("ID_Benutzer");

                entity.Property(e => e.Benutzername)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Besucher>(entity =>
            {
                entity.HasKey(e => e.IdBesucher)
                    .HasName("PK__Besucher__5B1DEB2538760BC5");

                entity.ToTable("Besucher");

                entity.Property(e => e.IdBesucher).HasColumnName("ID_Besucher");

                entity.Property(e => e.Nachname)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Vorname)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Status>(entity =>
            {
                entity.HasKey(e => e.IdStatus)
                    .HasName("PK__Status__5AC2A7343D6504D4");

                entity.ToTable("Status");

                entity.Property(e => e.IdStatus).HasColumnName("ID_Status");

                entity.Property(e => e.Bezeichnung)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Ticket>(entity =>
            {
                entity.HasKey(e => e.IdTicket)
                    .HasName("PK__Ticket__79F5DC08FF694B5C");

                entity.ToTable("Ticket");

                entity.Property(e => e.IdTicket).HasColumnName("ID_Ticket");

                entity.Property(e => e.Bezeichnung)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Preis).HasColumnType("decimal(18, 0)");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
