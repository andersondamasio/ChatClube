using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace com.chatclube
{
    public partial class ChatClubeContext : DbContext
    {
        public ChatClubeContext()
        {
        }

        public ChatClubeContext(DbContextOptions<ChatClubeContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Sala> Sala { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Sala>(entity =>
            {
                entity.HasKey(e => e.IDSala);

                entity.Property(e => e.IDSala).ValueGeneratedNever();

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.IDUsuario);

                entity.Property(e => e.IDUsuario).ValueGeneratedNever();

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Sobrenome).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
