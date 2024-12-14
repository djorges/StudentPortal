using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MySql.EntityFrameworkCore.Extensions;
using StudentPortal.Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;

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

        public DbSet<Examen> Examenes { get; set; }
        public DbSet<Profesor> Profesores { get; set; }
        public DbSet<Curso> Cursos { get; set; }
        public DbSet<Calificacion> Calificaciones { get; set; }
        public DbSet<Estudiante> Estudiantes { get; set; }
        public DbSet<CursoEstudiante> CursoEstudiantes { get; set; }
        public DbSet<ExamenEstudiante> ExamenEstudiantes { get; set; }
        public DbSet<FechaExamen> FechaExamen { get; set; }

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
                tb.Property(col => col.Nacionalidad).HasConversion<string>();
                tb.Property(col => col.PrivacidadCorreo).HasConversion<string>();
                tb.ToTable("estudiantes");
            });

            modelBuilder.Entity<CursoEstudiante>(entity =>
            {
                entity.HasKey(ce => new { ce.EstudianteId, ce.CursoId });
                entity.HasOne(ce => ce.Estudiante)
                      .WithMany(e => e.CursosEstudiantes)
                      .HasForeignKey(ce => ce.EstudianteId);
                entity.HasOne(ce => ce.Curso)
                      .WithMany(c => c.CursosEstudiantes)
                      .HasForeignKey(ce => ce.CursoId);
            });

            modelBuilder.Entity<ExamenEstudiante>(entity =>
            {
                entity.HasKey(ee => new { ee.ExamenId, ee.EstudianteId });
                entity.HasOne(ee => ee.Examen)
                    .WithMany(e => e.ExamenEstudiantes)
                    .HasForeignKey(ee => ee.ExamenId);
                entity.HasOne(ee => ee.Estudiante)
                    .WithMany(e => e.ExamenEstudiantes)
                    .HasForeignKey(ee => ee.EstudianteId);
            });

            modelBuilder.Entity<FechaExamen>()
               .HasOne(fe => fe.Examen)
               .WithMany(e => e.FechasDisponibles)
               .HasForeignKey(fe => fe.ExamenId);

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

            modelBuilder.Entity<Examen>(tb =>
            {
                tb.HasKey(col => col.ExamenId);
                tb.Property(col => col.ExamenId).ValueGeneratedOnAdd();
                tb.Property(col => col.Titulo).IsRequired().HasMaxLength(50);
                tb.Property(col => col.NotasProfesor)
                    .HasConversion(
                v => string.Join(',', v),
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries));
                tb.HasData(
                    new Examen
                    {
                        ExamenId = 1,
                        Titulo = "Integrador Fisica III",
                        PeriodoLectivo = "Agosto 2024",
                        Sede = "Flores",
                        Aula = 201,
                        CantInscriptos = 30,
                        MaxInscriptos = 30,
                        DuracionHoras = 2,
                        NotasProfesor = [
                            "Haber aprobado los dos parciales",
                            "Todos los TPs Aprobados",
                            "75% de asistencia a las clases"
                        ],
                        ProfesorId = 1,
                    },
                    new Examen
                    {
                        ExamenId = 2,
                        Titulo = "Integrador Matemáticas Avanzadas II",
                        PeriodoLectivo = "Noviembre 2024",
                        Sede = "Caballito",
                        Aula = 102,
                        CantInscriptos = 15,
                        MaxInscriptos = 25,
                        DuracionHoras = 3,
                        NotasProfesor = [
                            "TP final obligatorio aprobado",
                            "Participación en al menos 50% de las clases prácticas"
                        ],
                        ProfesorId = 2,
                    },
                    new Examen
                    {
                        ExamenId = 3,
                        Titulo = "Integrador Programación III",
                        PeriodoLectivo = "Diciembre 2024",
                        Sede = "Belgrano",
                        Aula = 305,
                        CantInscriptos = 20,
                        MaxInscriptos = 30,
                        DuracionHoras = 4,
                        NotasProfesor = [
                            "Haber entregado y aprobado el proyecto integrador",
                            "Participación en la evaluación grupal previa"
                        ],
                        ProfesorId = 3,
                    },
                    new Examen
                    {
                        ExamenId = 4,
                        Titulo = "Integrador Química III",
                        PeriodoLectivo = "Enero 2025",
                        Sede = "Microcentro",
                        Aula = 101,
                        CantInscriptos = 10,
                        MaxInscriptos = 20,
                        DuracionHoras = 2,
                        NotasProfesor = [
                            "Obligatorio completar el ensayo final",
                            "Revisión aprobada de las lecturas asignadas"
                        ],
                        ProfesorId = 1,
                    }
                );
                tb.ToTable("examenes");
            });

            modelBuilder.Entity<FechaExamen>().HasData(
                new FechaExamen { FechaExamenId = 1, ExamenId = 1, Fecha = new DateTime(2024, 12, 20, 12, 30, 00) },
                new FechaExamen { FechaExamenId = 2, ExamenId = 1, Fecha = new DateTime(2025, 01, 10, 11, 30, 00) },
                new FechaExamen { FechaExamenId = 3, ExamenId = 1, Fecha = new DateTime(2025, 02, 10, 09, 30, 00) },
                new FechaExamen { FechaExamenId = 4, ExamenId = 2, Fecha = new DateTime(2024, 11, 30, 10, 00, 00) },
                new FechaExamen { FechaExamenId = 5, ExamenId = 2, Fecha = new DateTime(2024, 12, 07, 14, 00, 00) },
                new FechaExamen { FechaExamenId = 6, ExamenId = 3, Fecha = new DateTime(2024, 12, 15, 09, 00, 00) },
                new FechaExamen { FechaExamenId = 7, ExamenId = 3, Fecha = new DateTime(2024, 12, 22, 13, 30, 00) },
                new FechaExamen { FechaExamenId = 8, ExamenId = 4, Fecha = new DateTime(2025, 01, 15, 11, 00, 00) },
                new FechaExamen { FechaExamenId = 9, ExamenId = 4, Fecha = new DateTime(2025, 01, 20, 14, 00, 00) }
            );
        }
    }
}
