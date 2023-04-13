using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Clinica.TdTablas
{
    public partial class dbapiContext : DbContext
    {
        public dbapiContext()
        {
        }

        public dbapiContext(DbContextOptions<dbapiContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Attendance> Attendances { get; set; } = null!;
        public virtual DbSet<Evaluation> Evaluations { get; set; } = null!;
        public virtual DbSet<Patient> Patients { get; set; } = null!;
        public virtual DbSet<Producto> Productos { get; set; } = null!;
        public virtual DbSet<Recurrencium> Recurrencia { get; set; } = null!;
        public virtual DbSet<Rol> Rols { get; set; } = null!;
        public virtual DbSet<Therapy> Therapies { get; set; } = null!;
        public virtual DbSet<Therapy2> Therapy2s { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<Usuario> Usuarios { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Attendance>(entity =>
            {
                entity.HasKey(e => e.IdAsistencias)
                    .HasName("PK__Attendan__E2B8D1AC720A3327");

                entity.ToTable("Attendance");

                entity.Property(e => e.IdAsistencias).HasColumnName("idAsistencias");

                entity.Property(e => e.FechaFinal).HasColumnType("datetime");

                entity.Property(e => e.FechaInicio).HasColumnType("datetime");

                entity.Property(e => e.Remarks)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("remarks");
            });

            modelBuilder.Entity<Evaluation>(entity =>
            {
                entity.ToTable("Evaluation");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IdPatients)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.IdTherapy)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Price)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Patient>(entity =>
            {
                entity.HasKey(e => e.IdPatients)
                    .HasName("PK__patients__DF7A7FD8257F2B26");

                entity.ToTable("patients");

                entity.Property(e => e.Age).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.Course)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DateOfBirth)
                    .HasColumnType("datetime")
                    .HasColumnName("Date_of_birth");

                entity.Property(e => e.Diagnosis)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.EducationalInstitution)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Educational_institution");

                entity.Property(e => e.FamilyMembersConcerns)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("Family_members_concerns");

                entity.Property(e => e.FamilySettings)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Family_settings");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NumberMothers)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Other)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ParentOrGuardianPhoneNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Parent_or_guardian_phone_number");

                entity.Property(e => e.ParentsName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Parents_Name");

                entity.Property(e => e.Recommendations)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Sex)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SpecificMedicalCondition)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("Specific_medical_condition");

                entity.Property(e => e.TherapiesOrServiceYouWillReceiveAtTheCenter)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Therapies_or_service_you_will_receive_at_the_center");

                entity.Property(e => e.WhoRefers)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Who_refers");
            });

            modelBuilder.Entity<Producto>(entity =>
            {
                entity.HasKey(e => e.IdProducto)
                    .HasName("PK__producto__098892101DC8E520");

                entity.ToTable("producto");

                entity.Property(e => e.Categoria)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CodigoBarra)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Marca)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Precio)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Recurrencium>(entity =>
            {
                entity.HasKey(e => e.IdRecurrencia)
                    .HasName("PK__recurren__D2D9B8818B3A9493");

                entity.ToTable("recurrencia");

                entity.Property(e => e.Dias)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FechaInicio).HasColumnType("datetime");

                entity.Property(e => e.Frecuencia)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdEvaluationNavigation)
                    .WithMany(p => p.Recurrencia)
                    .HasForeignKey(d => d.IdEvaluation)
                    .HasConstraintName("FK__recurrenc__IdEva__793DFFAF");
            });

            modelBuilder.Entity<Rol>(entity =>
            {
                entity.HasKey(e => e.IdRol)
                    .HasName("PK__Rol__2A49584C131BE8C0");

                entity.ToTable("Rol");

                entity.Property(e => e.IdRol).ValueGeneratedNever();

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("descripcion");
            });

            modelBuilder.Entity<Therapy>(entity =>
            {
                entity.HasKey(e => e.IdTherapy)
                    .HasName("PK__therapy__87DD5B7CACE2BC46");

                entity.ToTable("therapy");

                entity.Property(e => e.Description)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Label)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Porcentaje).HasColumnName("porcentaje");
            });

            modelBuilder.Entity<Therapy2>(entity =>
            {
                entity.HasKey(e => e.IdTherapy)
                    .HasName("PK__therapy2__87DD5B7C8B6FAAB1");

                entity.ToTable("therapy2");

                entity.Property(e => e.Description)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Label)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Value)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.IdUser)
                    .HasName("PK__users__B7C92638179DB345");

                entity.ToTable("users");

                entity.Property(e => e.Apellido)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("apellido");

                entity.Property(e => e.Direccion)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("direccion");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Names)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Telefono)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("telefono");

                entity.HasOne(d => d.IdRolNavigation)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.IdRol)
                    .HasConstraintName("FK__users__IdRol__03BB8E22");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("usuario");

                entity.Property(e => e.Label)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Names)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Value)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
