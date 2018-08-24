using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;

namespace com.chatclube.Repository.Config
{
    public partial class ChatClubeContext : DbContext
    {
        public ChatClubeContext()
        {
            Database.EnsureCreated();
        }

        public ChatClubeContext(DbContextOptions<ChatClubeContext> options)
            : base(options)
        {
        }

        public DbSet<Sala> Sala { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        { 
            if (!optionsBuilder.IsConfigured)
            {
                String databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "chatclube.db");
                optionsBuilder.UseSqlite($"Filename={databasePath}");

                // iOS
                //var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "..", "Library", "banco.db")

            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Sala>(entity =>
            {
                entity.HasKey(e => e.IdSala);

                entity.Property(e => e.IdSala)
                    .HasColumnType("INT")
                    .ValueGeneratedNever();

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasColumnType("VARCHAR(30)");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
