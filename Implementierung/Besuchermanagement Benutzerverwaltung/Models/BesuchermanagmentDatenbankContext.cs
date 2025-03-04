using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Besuchermanagement_Benutzerverwaltung.Models
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
        public virtual DbSet<Reservierung> Reservierungs { get; set; } = null!;
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
                    .HasName("PK__Benutzer__F4BE35752D73A8E1");

                entity.ToTable("Benutzer");

                entity.Property(e => e.IdBenutzer).HasColumnName("ID_Benutzer");

                entity.Property(e => e.Benutzername)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("EMail");

                entity.Property(e => e.Telefon)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Besucher>(entity =>
            {
                entity.HasKey(e => e.IdBesucher)
                    .HasName("PK__Besucher__5B1DEB25ACB1C286");

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

            modelBuilder.Entity<Reservierung>(entity =>
            {
                entity.HasKey(e => e.IdReservierung)
                    .HasName("PK__Reservie__5564930982DB49C7");

                entity.ToTable("Reservierung");

                entity.Property(e => e.IdReservierung).HasColumnName("ID_Reservierung");

                entity.Property(e => e.Nachname)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Ticket)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Vorname)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Status>(entity =>
            {
                entity.HasKey(e => e.IdStatus)
                    .HasName("PK__Status__5AC2A7340DEF7737");

                entity.ToTable("Status");

                entity.Property(e => e.IdStatus).HasColumnName("ID_Status");

                entity.Property(e => e.Bezeichnung)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Ticket>(entity =>
            {
                entity.HasKey(e => e.IdTicket)
                    .HasName("PK__Ticket__79F5DC0824529978");

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
