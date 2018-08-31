using System;
using com.chatclube.SalaX;
using com.chatclube.UsuarioX;
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

                entity.Property(e => e.BSSIDWifi)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Cep)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Cidade).HasMaxLength(50);

                entity.Property(e => e.DataHora).HasColumnType("datetime");

                entity.Property(e => e.Estado).HasMaxLength(50);

                entity.Property(e => e.Latitude)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Longitude)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.NumeroMaxUsuarios)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasDefaultValueSql("((50))");

                entity.Property(e => e.Pais).HasMaxLength(50);

                entity.Property(e => e.Rua).HasMaxLength(50);

                entity.HasOne(d => d.IDUsuarioNavigation)
                    .WithMany(p => p.Sala)
                    .HasForeignKey(d => d.IDUsuario)
                    .HasConstraintName("FK_Sala_Usuario");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.IDUsuario);

                entity.Property(e => e.IDUsuario).ValueGeneratedNever();

                entity.Property(e => e.Apelido).HasMaxLength(50);

                entity.Property(e => e.BSSIdWifi)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Cidade).HasMaxLength(50);

                entity.Property(e => e.CidadeEstado).HasMaxLength(50);

                entity.Property(e => e.ConnectionID)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.DataHora).HasColumnType("datetime");

                entity.Property(e => e.DataHoraOnline).HasColumnType("datetime");

                entity.Property(e => e.DataHoraUltimaAtualizacao).HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Estado).HasMaxLength(50);

                entity.Property(e => e.IDProfile)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.IDsSalaNotificar).HasColumnType("text");

                entity.Property(e => e.Latitude)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Locale)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Longitude)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Pais).HasMaxLength(50);

                entity.Property(e => e.Sexo)
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Sobrenome).HasMaxLength(50);

                entity.Property(e => e.Token)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.VersaoAplicativo)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.IDSalaNavigation)
                    .WithMany(p => p.Usuario)
                    .HasForeignKey(d => d.IDSala)
                    .HasConstraintName("FK_Usuario_Sala");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
