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

        
        public DbSet<Profesor> Profesores { get; set; }
        public DbSet<Curso> Cursos { get; set; }
        public DbSet<Calificacion> Calificaciones { get; set; }
        public DbSet<Estudiante> Estudiantes { get; set; }
        //public DbSet<CursoEstudiante> CursoEstudiantes { get; set; }


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

            modelBuilder.Entity<Estudiante>(tb => {
                tb.HasKey(col => col.EstudianteId);
                tb.Property(col => col.EstudianteId).ValueGeneratedOnAdd();
                tb.Property(col => col.Nombre).IsRequired().HasMaxLength(50);
                tb.Property(col => col.Genero).HasConversion<string>();
                tb.ToTable("estudiantes");
            });
            /*modelBuilder.Entity<CursoEstudiante>()
                .HasKey(ce => new { ce.EstudianteId, ce.CursoId });

            modelBuilder.Entity<CursoEstudiante>()
                .HasOne(ce => ce.Estudiante)
                .WithMany(e => e.CursosEstudiantes)
                .HasForeignKey(ce => ce.EstudianteId);

            modelBuilder.Entity<CursoEstudiante>()
                .HasOne(ce => ce.Curso)
                .WithMany(c => c.CursosEstudiantes)
                .HasForeignKey(ce => ce.CursoId);
            */

            modelBuilder.Entity<Profesor>(tb =>
            {
                tb.HasKey(col => col.ProfesorId);
                tb.Property(col => col.ProfesorId).ValueGeneratedOnAdd();
                tb.Property(col => col.Genero).HasConversion<string>();
                tb.Property(col => col.NivelEstudio).HasConversion<string>();
                tb.HasData(
                    new Profesor
                    {
                        ProfesorId = 1,
                        Nombre = "Sonia",
                        Apellido = "Herrera",
                        Genero = Genero.Femenino,
                        FechaNacimiento = new DateTime(1980, 5, 15),
                        Especialidad = "Matemáticas",
                        NivelEstudio = NivelEstudio.Doctorado,
                        Salario = 75000.50f,
                        Activo = true,
                        imageUrl = "profe3.jpg",
                        Trayectoria = "10 años y 6 meses",
                        PorcentajeAlumnosAprobados = 85.4f,
                        PuntuacionEncuestas = 1890
                    },
                    new Profesor
                    {
                        ProfesorId = 2,
                        Nombre = "Héctor",
                        Apellido = "Sambrano",
                        Genero = Genero.Masculino,
                        FechaNacimiento = new DateTime(1985, 8, 22),
                        Especialidad = "Física",
                        NivelEstudio = NivelEstudio.Maestria,
                        Salario = 68000.00f,
                        Activo = true,
                        imageUrl = "profe1.jpg",
                        Trayectoria = "15 años y 6 meses",
                        PorcentajeAlumnosAprobados = 80.1f,
                        PuntuacionEncuestas = 980
                    },
                    new Profesor
                    {
                        ProfesorId = 3,
                        Nombre = "Alison",
                        Apellido = "Ortega",
                        Genero = Genero.Femenino,
                        FechaNacimiento = new DateTime(1975, 3, 30),
                        Especialidad = "Química",
                        NivelEstudio = NivelEstudio.Doctorado,
                        Salario = 62000.00f,
                        Activo = true,
                        imageUrl = "profe2.jpg",
                        Trayectoria = "9 años y 11 meses",
                        PorcentajeAlumnosAprobados = 76.6f,
                        PuntuacionEncuestas = 796
                    }
                );
                tb.ToTable("profesores");
            });

            modelBuilder.Entity<Calificacion>( tb =>
            {
                tb.HasKey(col => col.CalificacionId);
                tb.Property(col => col.CalificacionId).ValueGeneratedOnAdd();
                tb.HasData(
                    new Calificacion
                    {
                        CalificacionId = 1,
                        Puntuacion = 4.5f,
                        TotalVotos = 1045
                    },
                    new Calificacion
                    {
                        CalificacionId = 2,
                        Puntuacion = 4.0f,
                        TotalVotos = 909
                    },
                    new Calificacion
                    {
                        CalificacionId = 3,
                        Puntuacion = 3.9f,
                        TotalVotos = 894
                    }
                );
                tb.ToTable("calificaciones");
            });

            modelBuilder.Entity<Curso>(tb =>
            {
                tb.HasKey(col => col.CursoId);
                tb.Property(col => col.CursoId).ValueGeneratedOnAdd();
                tb.Property(col => col.Nombre).IsRequired().HasMaxLength(50);
                tb.HasOne(col => col.Profesor).WithMany(p => p.Cursos).HasForeignKey(c => c.ProfesorId);
                tb.HasOne(col => col.Calificacion).WithOne(p => p.Curso).HasForeignKey<Curso>(c => c.CalificacionId);
                tb.HasData(
                     new Curso
                     {
                         CursoId = 1,
                         Nombre = "Algoritmo Computacional I",
                         Codigo = "Cod: 8001",
                         Creditos = 8,
                         EsObligatorio = true,
                         Sede = "Flores",
                         Aula = 505,
                         Horarios = "Martes y Jueves, 16:00-20:00",
                         CantInscriptos = 30,
                         MaxInscriptos = 30,
                         Descripcion = "Introduce a los estudiantes a los fundamentos del diseño y análisis de algoritmos. Se enfoca en desarrollar habilidades para resolver problemas mediante la creación de soluciones computacionales eficientes. A lo largo del curso, se exploran conceptos básicos como..",
                         Duracion = "Cuatrimestral",
                         CalificacionId = 1,
                         ProfesorId = 1,
                     },
                     new Curso
                     {
                         CursoId = 2,
                         Nombre = "Análisis Matemático I",
                         Codigo = "Cod: 8085",
                         Creditos = 8,
                         EsObligatorio = true,
                         Sede = "Palermo",
                         Aula = 109,
                         Horarios = "Lunes y Miercoles, 10:00-14:00",
                         CantInscriptos = 19,
                         MaxInscriptos = 30,
                         Descripcion = "Introduce a los estudiantes a los conceptos fundamentales del cálculo diferencial e integral, estableciendo las bases para el estudio riguroso de funciones de una variable real. Los temas principales incluyen el análisis de límites y continuidad, derivación ...",
                         Duracion = "Cuatrimestral",
                         CalificacionId = 2,
                         ProfesorId = 1,
                     },
                      new Curso
                      {
                          CursoId = 3,
                          Nombre = "Química II",
                          Codigo = "Cod: 6012",
                          Creditos = 6,
                          EsObligatorio = false,
                          Sede = "Puerto Madero",
                          Aula = 405,
                          Horarios = "Miércoles y Sábados, 7:00-10:00",
                          CantInscriptos = 8,
                          MaxInscriptos = 30,
                          Descripcion = "Profundiza en los conceptos de la química general y se enfoca en el estudio de fenómenos químicos a nivel molecular y macroscópico. Los temas incluyen la termodinámica química, equilibrio químico, cinética de las reacciones, electroquímica y propiedades de los gases, líquidos y sólidos.",
                          Duracion = "Cuatrimestral",
                          CalificacionId = 3,
                          ProfesorId = 2,
                      }
                );
                tb.ToTable("cursos");
            });

            /*modelBuilder.Entity<Estudiante>(tb =>
            {
                tb.HasKey(e => e.EstudianteId);
                tb.Property(p => p.EstudianteId).ValueGeneratedOnAdd();
                tb.ToTable("estudiantes");
            });*/
        }
    }
}
