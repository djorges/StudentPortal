﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using StudentPortal.Data;

#nullable disable

namespace StudentPortal.Migrations
{
    [DbContext(typeof(DBMain))]
    [Migration("20241210033941_AddExamenEstudianteRel")]
    partial class AddExamenEstudianteRel
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("StudentPortal.Entities.Calificacion", b =>
                {
                    b.Property<int>("CalificacionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<float>("Puntuacion")
                        .HasColumnType("float");

                    b.Property<int>("TotalVotos")
                        .HasColumnType("int");

                    b.HasKey("CalificacionId");

                    b.ToTable("calificaciones", (string)null);

                    b.HasData(
                        new
                        {
                            CalificacionId = 1,
                            Puntuacion = 4.5f,
                            TotalVotos = 1045
                        },
                        new
                        {
                            CalificacionId = 2,
                            Puntuacion = 4f,
                            TotalVotos = 909
                        },
                        new
                        {
                            CalificacionId = 3,
                            Puntuacion = 3.9f,
                            TotalVotos = 894
                        });
                });

            modelBuilder.Entity("StudentPortal.Entities.Curso", b =>
                {
                    b.Property<int>("CursoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("Aula")
                        .HasColumnType("int");

                    b.Property<int>("CalificacionId")
                        .HasColumnType("int");

                    b.Property<int>("CantInscriptos")
                        .HasColumnType("int");

                    b.Property<string>("Codigo")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("Creditos")
                        .HasColumnType("int");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Duracion")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("EsObligatorio")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Horarios")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("MaxInscriptos")
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<int>("ProfesorId")
                        .HasColumnType("int");

                    b.Property<string>("Sede")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("CursoId");

                    b.HasIndex("CalificacionId")
                        .IsUnique();

                    b.HasIndex("ProfesorId");

                    b.ToTable("cursos", (string)null);

                    b.HasData(
                        new
                        {
                            CursoId = 1,
                            Aula = 505,
                            CalificacionId = 1,
                            CantInscriptos = 30,
                            Codigo = "Cod: 8001",
                            Creditos = 8,
                            Descripcion = "Introduce a los estudiantes a los fundamentos del diseño y análisis de algoritmos. Se enfoca en desarrollar habilidades para resolver problemas mediante la creación de soluciones computacionales eficientes. A lo largo del curso, se exploran conceptos básicos como..",
                            Duracion = "Cuatrimestral",
                            EsObligatorio = true,
                            Horarios = "Martes y Jueves, 16:00-20:00",
                            MaxInscriptos = 30,
                            Nombre = "Algoritmo Computacional I",
                            ProfesorId = 1,
                            Sede = "Flores"
                        },
                        new
                        {
                            CursoId = 2,
                            Aula = 109,
                            CalificacionId = 2,
                            CantInscriptos = 19,
                            Codigo = "Cod: 8085",
                            Creditos = 8,
                            Descripcion = "Introduce a los estudiantes a los conceptos fundamentales del cálculo diferencial e integral, estableciendo las bases para el estudio riguroso de funciones de una variable real. Los temas principales incluyen el análisis de límites y continuidad, derivación ...",
                            Duracion = "Cuatrimestral",
                            EsObligatorio = true,
                            Horarios = "Lunes y Miercoles, 10:00-14:00",
                            MaxInscriptos = 30,
                            Nombre = "Análisis Matemático I",
                            ProfesorId = 1,
                            Sede = "Palermo"
                        },
                        new
                        {
                            CursoId = 3,
                            Aula = 405,
                            CalificacionId = 3,
                            CantInscriptos = 8,
                            Codigo = "Cod: 6012",
                            Creditos = 6,
                            Descripcion = "Profundiza en los conceptos de la química general y se enfoca en el estudio de fenómenos químicos a nivel molecular y macroscópico. Los temas incluyen la termodinámica química, equilibrio químico, cinética de las reacciones, electroquímica y propiedades de los gases, líquidos y sólidos.",
                            Duracion = "Cuatrimestral",
                            EsObligatorio = false,
                            Horarios = "Miércoles y Sábados, 7:00-10:00",
                            MaxInscriptos = 30,
                            Nombre = "Química II",
                            ProfesorId = 2,
                            Sede = "Puerto Madero"
                        });
                });

            modelBuilder.Entity("StudentPortal.Entities.CursoEstudiante", b =>
                {
                    b.Property<int>("EstudianteId")
                        .HasColumnType("int");

                    b.Property<int>("CursoId")
                        .HasColumnType("int");

                    b.Property<DateTime>("FechaInscripcion")
                        .HasColumnType("datetime(6)");

                    b.HasKey("EstudianteId", "CursoId");

                    b.HasIndex("CursoId");

                    b.ToTable("CursoEstudiantes");
                });

            modelBuilder.Entity("StudentPortal.Entities.Empleado", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("IdPerfil")
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<float>("Salario")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("IdPerfil");

                    b.ToTable("empleados", (string)null);
                });

            modelBuilder.Entity("StudentPortal.Entities.Estudiante", b =>
                {
                    b.Property<int>("EstudianteId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<bool>("Activo")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Apellido")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Clave")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("Confirmado")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("ConfirmarClave")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Correo")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("Dni")
                        .HasColumnType("int");

                    b.Property<string>("Domicilio")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("Edad")
                        .HasColumnType("int");

                    b.Property<DateTime>("FechaNacimiento")
                        .HasColumnType("datetime(6)");

                    b.Property<byte[]>("FotoPerfil")
                        .HasColumnType("longblob");

                    b.Property<string>("Genero")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Nacionalidad")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("PrivacidadCorreo")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("Restablecer")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("Telefono")
                        .HasColumnType("int");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("EstudianteId");

                    b.ToTable("estudiantes", (string)null);
                });

            modelBuilder.Entity("StudentPortal.Entities.Examen", b =>
                {
                    b.Property<int>("ExamenId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<short>("Aula")
                        .HasColumnType("smallint");

                    b.Property<int>("CantInscriptos")
                        .HasColumnType("int");

                    b.Property<short>("DuracionHoras")
                        .HasColumnType("smallint");

                    b.Property<DateTime?>("FechaSeleccionada")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("MaxInscriptos")
                        .HasColumnType("int");

                    b.Property<string>("NotasProfesor")
                        .HasColumnType("longtext");

                    b.Property<string>("PeriodoLectivo")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("ProfesorId")
                        .HasColumnType("int");

                    b.Property<string>("Sede")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.HasKey("ExamenId");

                    b.HasIndex("ProfesorId");

                    b.ToTable("examenes", (string)null);

                    b.HasData(
                        new
                        {
                            ExamenId = 1,
                            Aula = (short)201,
                            CantInscriptos = 30,
                            DuracionHoras = (short)2,
                            MaxInscriptos = 30,
                            NotasProfesor = "Haber aprobado los dos parciales,Todos los TPs Aprobados,75% de asistencia a las clases",
                            PeriodoLectivo = "Agosto 2024",
                            ProfesorId = 1,
                            Sede = "Flores",
                            Titulo = "Integrador Fisica III"
                        },
                        new
                        {
                            ExamenId = 2,
                            Aula = (short)102,
                            CantInscriptos = 15,
                            DuracionHoras = (short)3,
                            MaxInscriptos = 25,
                            NotasProfesor = "TP final obligatorio aprobado,Participación en al menos 50% de las clases prácticas",
                            PeriodoLectivo = "Noviembre 2024",
                            ProfesorId = 2,
                            Sede = "Caballito",
                            Titulo = "Integrador Matemáticas Avanzadas II"
                        },
                        new
                        {
                            ExamenId = 3,
                            Aula = (short)305,
                            CantInscriptos = 20,
                            DuracionHoras = (short)4,
                            MaxInscriptos = 30,
                            NotasProfesor = "Haber entregado y aprobado el proyecto integrador,Participación en la evaluación grupal previa",
                            PeriodoLectivo = "Diciembre 2024",
                            ProfesorId = 3,
                            Sede = "Belgrano",
                            Titulo = "Integrador Programación III"
                        },
                        new
                        {
                            ExamenId = 4,
                            Aula = (short)101,
                            CantInscriptos = 10,
                            DuracionHoras = (short)2,
                            MaxInscriptos = 20,
                            NotasProfesor = "Obligatorio completar el ensayo final,Revisión aprobada de las lecturas asignadas",
                            PeriodoLectivo = "Enero 2025",
                            ProfesorId = 1,
                            Sede = "Microcentro",
                            Titulo = "Integrador Química III"
                        });
                });

            modelBuilder.Entity("StudentPortal.Entities.ExamenEstudiante", b =>
                {
                    b.Property<int>("ExamenId")
                        .HasColumnType("int");

                    b.Property<int>("EstudianteId")
                        .HasColumnType("int");

                    b.Property<DateTime>("FechaInscripcion")
                        .HasColumnType("datetime(6)");

                    b.HasKey("ExamenId", "EstudianteId");

                    b.HasIndex("EstudianteId");

                    b.ToTable("ExamenEstudiante");
                });

            modelBuilder.Entity("StudentPortal.Entities.FechaExamen", b =>
                {
                    b.Property<int>("FechaExamenId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("ExamenId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("datetime(6)");

                    b.HasKey("FechaExamenId");

                    b.HasIndex("ExamenId");

                    b.ToTable("FechaExamen");

                    b.HasData(
                        new
                        {
                            FechaExamenId = 1,
                            ExamenId = 1,
                            Fecha = new DateTime(2024, 12, 20, 12, 30, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            FechaExamenId = 2,
                            ExamenId = 1,
                            Fecha = new DateTime(2025, 1, 10, 11, 30, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            FechaExamenId = 3,
                            ExamenId = 1,
                            Fecha = new DateTime(2025, 2, 10, 9, 30, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            FechaExamenId = 4,
                            ExamenId = 2,
                            Fecha = new DateTime(2024, 11, 30, 10, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            FechaExamenId = 5,
                            ExamenId = 2,
                            Fecha = new DateTime(2024, 12, 7, 14, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            FechaExamenId = 6,
                            ExamenId = 3,
                            Fecha = new DateTime(2024, 12, 15, 9, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            FechaExamenId = 7,
                            ExamenId = 3,
                            Fecha = new DateTime(2024, 12, 22, 13, 30, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            FechaExamenId = 8,
                            ExamenId = 4,
                            Fecha = new DateTime(2025, 1, 15, 11, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            FechaExamenId = 9,
                            ExamenId = 4,
                            Fecha = new DateTime(2025, 1, 20, 14, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("StudentPortal.Entities.Perfil", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.HasKey("Id");

                    b.ToTable("perfiles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Nombre = "Administrativo"
                        },
                        new
                        {
                            Id = 2,
                            Nombre = "Profesor"
                        },
                        new
                        {
                            Id = 3,
                            Nombre = "Investigador"
                        },
                        new
                        {
                            Id = 4,
                            Nombre = "Bibliotecario"
                        },
                        new
                        {
                            Id = 5,
                            Nombre = "Academico"
                        });
                });

            modelBuilder.Entity("StudentPortal.Entities.Profesor", b =>
                {
                    b.Property<int>("ProfesorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<bool>("Activo")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Apellido")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Especialidad")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("FechaNacimiento")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Genero")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("NivelEstudio")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<float>("PorcentajeAlumnosAprobados")
                        .HasColumnType("float");

                    b.Property<int>("PuntuacionEncuestas")
                        .HasColumnType("int");

                    b.Property<float>("Salario")
                        .HasColumnType("float");

                    b.Property<string>("Trayectoria")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("imageUrl")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("ProfesorId");

                    b.ToTable("profesores", (string)null);

                    b.HasData(
                        new
                        {
                            ProfesorId = 1,
                            Activo = true,
                            Apellido = "Herrera",
                            Especialidad = "Matemáticas",
                            FechaNacimiento = new DateTime(1980, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Genero = "Femenino",
                            NivelEstudio = "Doctorado",
                            Nombre = "Sonia",
                            PorcentajeAlumnosAprobados = 85.4f,
                            PuntuacionEncuestas = 1890,
                            Salario = 75000.5f,
                            Trayectoria = "10 años y 6 meses",
                            imageUrl = "profe3.jpg"
                        },
                        new
                        {
                            ProfesorId = 2,
                            Activo = true,
                            Apellido = "Sambrano",
                            Especialidad = "Física",
                            FechaNacimiento = new DateTime(1985, 8, 22, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Genero = "Masculino",
                            NivelEstudio = "Maestria",
                            Nombre = "Héctor",
                            PorcentajeAlumnosAprobados = 80.1f,
                            PuntuacionEncuestas = 980,
                            Salario = 68000f,
                            Trayectoria = "15 años y 6 meses",
                            imageUrl = "profe1.jpg"
                        },
                        new
                        {
                            ProfesorId = 3,
                            Activo = true,
                            Apellido = "Ortega",
                            Especialidad = "Química",
                            FechaNacimiento = new DateTime(1975, 3, 30, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Genero = "Femenino",
                            NivelEstudio = "Doctorado",
                            Nombre = "Alison",
                            PorcentajeAlumnosAprobados = 76.6f,
                            PuntuacionEncuestas = 796,
                            Salario = 62000f,
                            Trayectoria = "9 años y 11 meses",
                            imageUrl = "profe2.jpg"
                        });
                });

            modelBuilder.Entity("StudentPortal.Entities.Curso", b =>
                {
                    b.HasOne("StudentPortal.Entities.Calificacion", "Calificacion")
                        .WithOne("Curso")
                        .HasForeignKey("StudentPortal.Entities.Curso", "CalificacionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StudentPortal.Entities.Profesor", "Profesor")
                        .WithMany("Cursos")
                        .HasForeignKey("ProfesorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Calificacion");

                    b.Navigation("Profesor");
                });

            modelBuilder.Entity("StudentPortal.Entities.CursoEstudiante", b =>
                {
                    b.HasOne("StudentPortal.Entities.Curso", "Curso")
                        .WithMany("CursosEstudiantes")
                        .HasForeignKey("CursoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StudentPortal.Entities.Estudiante", "Estudiante")
                        .WithMany("CursosEstudiantes")
                        .HasForeignKey("EstudianteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Curso");

                    b.Navigation("Estudiante");
                });

            modelBuilder.Entity("StudentPortal.Entities.Empleado", b =>
                {
                    b.HasOne("StudentPortal.Entities.Perfil", "Perfil")
                        .WithMany("Empleados")
                        .HasForeignKey("IdPerfil")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Perfil");
                });

            modelBuilder.Entity("StudentPortal.Entities.Examen", b =>
                {
                    b.HasOne("StudentPortal.Entities.Profesor", "Profesor")
                        .WithMany()
                        .HasForeignKey("ProfesorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Profesor");
                });

            modelBuilder.Entity("StudentPortal.Entities.ExamenEstudiante", b =>
                {
                    b.HasOne("StudentPortal.Entities.Estudiante", "Estudiante")
                        .WithMany("ExamenEstudiantes")
                        .HasForeignKey("EstudianteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StudentPortal.Entities.Examen", "Examen")
                        .WithMany("ExamenEstudiantes")
                        .HasForeignKey("ExamenId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Estudiante");

                    b.Navigation("Examen");
                });

            modelBuilder.Entity("StudentPortal.Entities.FechaExamen", b =>
                {
                    b.HasOne("StudentPortal.Entities.Examen", "Examen")
                        .WithMany("FechasDisponibles")
                        .HasForeignKey("ExamenId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Examen");
                });

            modelBuilder.Entity("StudentPortal.Entities.Calificacion", b =>
                {
                    b.Navigation("Curso")
                        .IsRequired();
                });

            modelBuilder.Entity("StudentPortal.Entities.Curso", b =>
                {
                    b.Navigation("CursosEstudiantes");
                });

            modelBuilder.Entity("StudentPortal.Entities.Estudiante", b =>
                {
                    b.Navigation("CursosEstudiantes");

                    b.Navigation("ExamenEstudiantes");
                });

            modelBuilder.Entity("StudentPortal.Entities.Examen", b =>
                {
                    b.Navigation("ExamenEstudiantes");

                    b.Navigation("FechasDisponibles");
                });

            modelBuilder.Entity("StudentPortal.Entities.Perfil", b =>
                {
                    b.Navigation("Empleados");
                });

            modelBuilder.Entity("StudentPortal.Entities.Profesor", b =>
                {
                    b.Navigation("Cursos");
                });
#pragma warning restore 612, 618
        }
    }
}
