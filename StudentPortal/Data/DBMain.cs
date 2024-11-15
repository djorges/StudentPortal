using Microsoft.EntityFrameworkCore;
using MySql.EntityFrameworkCore.Extensions;
using StudentPortal.Entities;

namespace StudentPortal.Data
{
    public class DBMain : DbContext
    {
        public DBMain(DbContextOptions<DBMain> options)
            : base(options)
        {
        }

        public DbSet<Empleado> Empleados { get; set; }
        public DbSet<Perfil> Perfiles { get; set; }
        public DbSet<Estudiante> Estudiantes { get; set; }
        public DbSet<Profesor> Profesores { get; set; }
        public DbSet<Curso> Cursos { get; set; }
        public DbSet<CursoEstudiante> CursoEstudiantes { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Perfil>( tb => {
                tb.HasKey(col => col.Id);
                tb.Property(col => col.Id).ValueGeneratedOnAdd();
                tb.Property(col => col.Nombre).IsRequired().HasMaxLength(50);
                tb.ToTable("perfiles");
                tb.HasData(
                    new Perfil { Id = 1, Nombre = "Administrativo" },
                    new Perfil { Id = 2, Nombre = "Profesor" },
                    new Perfil { Id = 3, Nombre = "Investigador"},
                    new Perfil { Id = 4, Nombre = "Bibliotecario"},
                    new Perfil { Id = 5, Nombre = "Academico" }
                );
            });
            modelBuilder.Entity<Empleado>( tb => {
                tb.HasKey(col => col.Id);
                tb.Property(col => col.Id).ValueGeneratedOnAdd();
                tb.Property(col => col.Nombre).IsRequired().HasMaxLength(50);
                tb.HasOne(col => col.Perfil).WithMany(p => p.Empleados).HasForeignKey(col => col.IdPerfil);
                tb.ToTable("empleados");
            });

            modelBuilder.Entity<CursoEstudiante>()
                .HasKey(ce => new { ce.EstudianteId, ce.CursoId });

            modelBuilder.Entity<CursoEstudiante>()
                .HasOne(ce => ce.Estudiante)
                .WithMany(e => e.CursosEstudiantes)
                .HasForeignKey(ce => ce.EstudianteId);

            modelBuilder.Entity<CursoEstudiante>()
                .HasOne(ce => ce.Curso)
                .WithMany(c => c.CursosEstudiantes)
                .HasForeignKey(ce => ce.CursoId);

            modelBuilder.Entity<Curso>()
                .HasOne(c => c.Profesor)
                .WithMany(p => p.Cursos)
                .HasForeignKey(c => c.ProfesorId);
        }
    }
}
