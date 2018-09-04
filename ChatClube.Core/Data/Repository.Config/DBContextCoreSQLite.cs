using com.chatclube.Repository.Config;
using com.chatclube.SalaX;
using com.chatclube.UsuarioX;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;

namespace com.chatclube.Data.Repository.Config
{
    public partial class DBContextCoreSQLite : DbContext
    {
        public static IList<string> Logs = null;

        private static ILoggerFactory LoggerFactory => new LoggerFactory().AddConsole(LogLevel.Trace);

        public static DbContextOptions<DBContextCoreSQLite> dbContextOptions;

        public DBContextCoreSQLite()
        {
         

                //Database.EnsureCreated();
        }


        public DBContextCoreSQLite(DbContextOptions<DBContextCoreSQLite> options)
            : base(options)
        {

          

        }

        public DbSet<Sala> Sala { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured)
            {
               
            }
            optionsBuilder.UseLoggerFactory(LoggerFactory);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (Database.IsSqlite())
                modelBuilder = GetModelSQLite();
            else modelBuilder = GetModelSQLServer();

            base.OnModelCreating(modelBuilder);
        }


        private ModelBuilder GetModelSQLServer()
        {
            var convention = new Microsoft.EntityFrameworkCore.Metadata.Conventions.ConventionSet();
            ModelBuilder modelBuilder = new ModelBuilder(convention);

            modelBuilder.Entity<Sala>(entity =>
            {
                entity.HasKey(e => e.IDSala);

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

                entity.Property(e => e.NumeroMaxUsuarios).HasDefaultValueSql("((50))");

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

            return modelBuilder;
        }

        private ModelBuilder GetModelSQLite()
        {
            var convention = new Microsoft.EntityFrameworkCore.Metadata.Conventions.ConventionSet();
            ModelBuilder modelBuilder = new ModelBuilder(convention);
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

            return modelBuilder;
        }


        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);


        private class CustomLoggerProvider : ILoggerProvider
        {
            public ILogger CreateLogger(string categoryName) => new SampleLogger();

            private class SampleLogger : ILogger
            {
                public void Log<TState>(
                    LogLevel logLevel,
                    EventId eventId,
                    TState state,
                    Exception exception,
                    Func<TState, Exception, string> formatter)
                {
                    if (eventId.Id == RelationalEventId.CommandExecuting.Id)
                    {
                        var log = formatter(state, exception);
                        Logs.Add(log);
                    }
                }

                public bool IsEnabled(LogLevel logLevel) => true;

                public IDisposable BeginScope<TState>(TState state) => null;

            }

            public void Dispose() { }
        }
    }
}
