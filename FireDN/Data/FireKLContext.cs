using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace FireDN.Data
{
    public partial class FireKLContext : DbContext
    {
        public FireKLContext()
        {
        }

        public FireKLContext(DbContextOptions<FireKLContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Esp> Esps { get; set; } = null!;
        public virtual DbSet<FireD> FireDs { get; set; } = null!;
        public virtual DbSet<Humi> Humis { get; set; } = null!;
        public virtual DbSet<NguoiDung> NguoiDungs { get; set; } = null!;
        public virtual DbSet<Sensor> Sensors { get; set; } = null!;
        public virtual DbSet<Smoke> Smokes { get; set; } = null!;
        public virtual DbSet<Temp> Temps { get; set; } = null!;
        public virtual DbSet<TgEsp> TgEsps { get; set; } = null!;
        public virtual DbSet<TgSensor> TgSensors { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=FireKL;Integrated Security=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Esp>(entity =>
            {
                entity.ToTable("ESP");

                entity.Property(e => e.EspId)
                    .ValueGeneratedNever()
                    .HasColumnName("Esp_ID");

                entity.Property(e => e.EspN)
                    .HasMaxLength(255)
                    .HasColumnName("Esp_N");
            });

            modelBuilder.Entity<FireD>(entity =>
            {
                entity.ToTable("FireD");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.FireDId).HasColumnName("FireD_ID");

                entity.Property(e => e.ReadTime).HasColumnType("datetime");

                entity.Property(e => e.Statistic).HasMaxLength(255);
            });

            modelBuilder.Entity<Humi>(entity =>
            {
                entity.ToTable("Humi");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.HumiId).HasColumnName("Humi_ID");

                entity.Property(e => e.ReadTime).HasColumnType("datetime");

                entity.Property(e => e.Statistic).HasMaxLength(255);
            });

            modelBuilder.Entity<NguoiDung>(entity =>
            {
                entity.HasKey(e => e.Iduser)
                    .HasName("PK__NguoiDun__B6EF7F6C6B244AFB");

                entity.ToTable("NguoiDung");

                entity.Property(e => e.Iduser).HasColumnName("IDuser");

                entity.Property(e => e.PassWord)
                    .HasMaxLength(100)
                    .HasColumnName("Pass_word");

                entity.Property(e => e.RoleUser)
                    .HasMaxLength(100)
                    .HasColumnName("Role_user");

                entity.Property(e => e.Username).HasMaxLength(100);
            });

            modelBuilder.Entity<Sensor>(entity =>
            {
                entity.ToTable("Sensor");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.SensorN)
                    .HasMaxLength(255)
                    .HasColumnName("Sensor_N");
            });

            modelBuilder.Entity<Smoke>(entity =>
            {
                entity.ToTable("Smoke");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.ReadTime).HasColumnType("datetime");

                entity.Property(e => e.SmokeId).HasColumnName("Smoke_ID");

                entity.Property(e => e.Statistic).HasMaxLength(255);
            });

            modelBuilder.Entity<Temp>(entity =>
            {
                entity.ToTable("Temp");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.ReadTime).HasColumnType("datetime");

                entity.Property(e => e.Statistic).HasMaxLength(255);

                entity.Property(e => e.TempId).HasColumnName("Temp_ID");
            });

            modelBuilder.Entity<TgEsp>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("TG_ESP");

                entity.Property(e => e.Alerts).HasMaxLength(255);

                entity.Property(e => e.EspId).HasColumnName("Esp_ID");

                entity.Property(e => e.FireDId).HasColumnName("FireD_ID");

                entity.Property(e => e.HumiId).HasColumnName("Humi_ID");

                entity.Property(e => e.Record).HasColumnType("datetime");

                entity.Property(e => e.SmokeId).HasColumnName("Smoke_ID");

                entity.Property(e => e.TempId).HasColumnName("Temp_ID");

                entity.HasOne(d => d.Esp)
                    .WithMany()
                    .HasForeignKey(d => d.EspId)
                    .HasConstraintName("FK_ESP");

                entity.HasOne(d => d.FireD)
                    .WithMany()
                    .HasForeignKey(d => d.FireDId)
                    .HasConstraintName("FK_FireD");

                entity.HasOne(d => d.Humi)
                    .WithMany()
                    .HasForeignKey(d => d.HumiId)
                    .HasConstraintName("FK_Humi");

                entity.HasOne(d => d.Smoke)
                    .WithMany()
                    .HasForeignKey(d => d.SmokeId)
                    .HasConstraintName("FK_Smoke");

                entity.HasOne(d => d.Temp)
                    .WithMany()
                    .HasForeignKey(d => d.TempId)
                    .HasConstraintName("FK_Temp");
            });

            modelBuilder.Entity<TgSensor>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("TG_Sensor");

                entity.Property(e => e.EspId).HasColumnName("Esp_ID");

                entity.Property(e => e.TgSid).HasColumnName("TG_SID");

                entity.HasOne(d => d.Esp)
                    .WithMany()
                    .HasForeignKey(d => d.EspId)
                    .HasConstraintName("FK_esp_id");

                entity.HasOne(d => d.TgS)
                    .WithMany()
                    .HasForeignKey(d => d.TgSid)
                    .HasConstraintName("FK_TG_SID");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
