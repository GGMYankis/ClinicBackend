using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Clinica.SqlTables
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

        public virtual DbSet<Abono> Abonos { get; set; } = null!;
        public virtual DbSet<AbonosTerapia> AbonosTerapias { get; set; } = null!;
        public virtual DbSet<Attendance> Attendances { get; set; } = null!;
        public virtual DbSet<Consultorio> Consultorios { get; set; } = null!;
        public virtual DbSet<Evaluation> Evaluations { get; set; } = null!;
        public virtual DbSet<IdtherapistIdtherapy> IdtherapistIdtherapies { get; set; } = null!;
        public virtual DbSet<Inversion> Inversions { get; set; } = null!;
        public virtual DbSet<PagosRecurrente> PagosRecurrentes { get; set; } = null!;
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
            modelBuilder.Entity<Abono>(entity =>
            {
                entity.HasKey(e => e.IdBono)
                    .HasName("PK__Abono__D8053001342879F6");

                entity.ToTable("Abono");

                entity.Property(e => e.IdBono).HasColumnName("idBono");

                entity.Property(e => e.Description)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.Property(e => e.Fecha).HasColumnType("datetime");

                entity.Property(e => e.IdTherapy).HasColumnName("idTherapy");

                entity.Property(e => e.Monto).HasColumnType("decimal(10, 2)");
            });

            modelBuilder.Entity<AbonosTerapia>(entity =>
            {
                entity.Property(e => e.Fecha).HasColumnType("datetime");

                entity.Property(e => e.MontoPagado).HasColumnType("decimal(18, 0)");
            });

            modelBuilder.Entity<Attendance>(entity =>
            {
                entity.HasKey(e => e.IdAsistencias)
                    .HasName("PK__Attendan__E2B8D1AC97F11714");

                entity.ToTable("Attendance");

                entity.Property(e => e.IdAsistencias).HasColumnName("idAsistencias");

                entity.Property(e => e.FechaFinal).HasColumnType("datetime");

                entity.Property(e => e.FechaInicio).HasColumnType("datetime");

                entity.Property(e => e.Remarks)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("remarks");

                entity.Property(e => e.TipoAsistencias)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Consultorio>(entity =>
            {
                entity.HasKey(e => e.IdConsultorio)
                    .HasName("PK__Consulto__287E8160F15510DB");

                entity.ToTable("Consultorio");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Evaluation>(entity =>
            {
                entity.ToTable("Evaluation");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.HasOne(d => d.IdConsultorioNavigation)
                    .WithMany(p => p.Evaluations)
                    .HasForeignKey(d => d.IdConsultorio)
                    .HasConstraintName("FK__Evaluatio__IdCon__54CB950F");
            });

            modelBuilder.Entity<IdtherapistIdtherapy>(entity =>
            {
                entity.ToTable("idtherapist_idtherapy");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Idterapeuta).HasColumnName("idterapeuta");

                entity.Property(e => e.Idtherapia).HasColumnName("idtherapia");
            });

            modelBuilder.Entity<Inversion>(entity =>
            {
                entity.HasKey(e => e.IdAccounting)
                    .HasName("PK__inversio__68C5F826A81A5825");

                entity.ToTable("inversion");

                entity.Property(e => e.Amount).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.DateOfInvestment)
                    .HasColumnType("datetime")
                    .HasColumnName("Date_of_investment");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.EndDate)
                    .HasColumnType("datetime")
                    .HasColumnName("end_date");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<PagosRecurrente>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("pagos_recurrentes");

                entity.Property(e => e.Fecha)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha");

                entity.Property(e => e.IdPaciente).HasColumnName("idPaciente");

                entity.Property(e => e.IdTherapia).HasColumnName("idTherapia");

                entity.Property(e => e.Monto).HasColumnType("decimal(10, 2)");
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

                entity.Property(e => e.FechaIngreso)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("fechaIngreso");

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
                    .HasName("PK__recurren__D2D9B8816906D261");

                entity.ToTable("recurrencia");

                entity.Property(e => e.Dias)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FechaInicio).HasColumnType("datetime");

                entity.Property(e => e.Frecuencia)
                    .HasMaxLength(100)
                    .IsUnicode(false);
               // entity.Ignore(e => e.Dias);

                entity.HasOne(d => d.IdEvaluationNavigation)
                    .WithMany(p => p.Recurrencia)
                    .HasForeignKey(d => d.IdEvaluation)
                    .HasConstraintName("FK__recurrenc__IdEva__43A1090D");
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
                    .HasName("PK__therapy__87DD5B7C0AC607B2");

                entity.ToTable("therapy");

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

            modelBuilder.Entity<Therapy2>(entity =>
            {
                entity.HasKey(e => e.IdTherapy)
                    .HasName("PK__therapy2__87DD5B7C8B6FAAB1");

                entity.ToTable("therapy2");

                entity.Property(e => e.Description)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("email");

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
                    .HasName("PK__users__B7C9263853D2ED5E");

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

                entity.Property(e => e.Label)
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
                    .HasConstraintName("FK__users__IdRol__2334397B");
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
