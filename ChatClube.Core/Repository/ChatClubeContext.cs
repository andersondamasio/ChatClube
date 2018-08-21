using Microsoft.EntityFrameworkCore;
using System;
using System.IO;

namespace com.chatclube.Repository
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

            public virtual DbSet<Salas> Salas { get; set; }
        private const string databaseName = "chatclube.db";
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            String databasePath = "";
            if (!optionsBuilder.IsConfigured)
            {
                //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), databaseName);
            }
            // Specify that we will use sqlite and the path of the database here
            optionsBuilder.UseSqlite($"Filename={databasePath}");
            //optionsBuilder.UseSqlite("data source=F:\\Database\\ChatClube.SQLITE");
        }
            

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                modelBuilder.Entity<Salas>(entity =>
                {
                    entity.HasKey(e => e.sa_idSala);

                    entity.Property(e => e.sa_idSala)
                        .HasColumnType("INT")
                        .ValueGeneratedNever();

                    entity.Property(e => e.sa_nome)
                        .IsRequired()
                        .HasColumnType("VARCHAR(30)");
                });

                OnModelCreatingPartial(modelBuilder);
            }

            partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
        }
    }
