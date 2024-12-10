using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace StudentPortal.Migrations
{
    /// <inheritdoc />
    public partial class AddExamenEstudianteRel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "examenes",
                columns: table => new
                {
                    ExamenId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Titulo = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    PeriodoLectivo = table.Column<string>(type: "longtext", nullable: false),
                    Sede = table.Column<string>(type: "longtext", nullable: false),
                    Aula = table.Column<short>(type: "smallint", nullable: false),
                    CantInscriptos = table.Column<int>(type: "int", nullable: false),
                    MaxInscriptos = table.Column<int>(type: "int", nullable: false),
                    FechaSeleccionada = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DuracionHoras = table.Column<short>(type: "smallint", nullable: false),
                    NotasProfesor = table.Column<string>(type: "longtext", nullable: true),
                    ProfesorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_examenes", x => x.ExamenId);
                    table.ForeignKey(
                        name: "FK_examenes_profesores_ProfesorId",
                        column: x => x.ProfesorId,
                        principalTable: "profesores",
                        principalColumn: "ProfesorId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ExamenEstudiante",
                columns: table => new
                {
                    EstudianteId = table.Column<int>(type: "int", nullable: false),
                    ExamenId = table.Column<int>(type: "int", nullable: false),
                    FechaInscripcion = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamenEstudiante", x => new { x.ExamenId, x.EstudianteId });
                    table.ForeignKey(
                        name: "FK_ExamenEstudiante_estudiantes_EstudianteId",
                        column: x => x.EstudianteId,
                        principalTable: "estudiantes",
                        principalColumn: "EstudianteId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExamenEstudiante_examenes_ExamenId",
                        column: x => x.ExamenId,
                        principalTable: "examenes",
                        principalColumn: "ExamenId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "FechaExamen",
                columns: table => new
                {
                    FechaExamenId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Fecha = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ExamenId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FechaExamen", x => x.FechaExamenId);
                    table.ForeignKey(
                        name: "FK_FechaExamen_examenes_ExamenId",
                        column: x => x.ExamenId,
                        principalTable: "examenes",
                        principalColumn: "ExamenId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.InsertData(
                table: "examenes",
                columns: new[] { "ExamenId", "Aula", "CantInscriptos", "DuracionHoras", "FechaSeleccionada", "MaxInscriptos", "NotasProfesor", "PeriodoLectivo", "ProfesorId", "Sede", "Titulo" },
                values: new object[,]
                {
                    { 1, (short)201, 30, (short)2, null, 30, "Haber aprobado los dos parciales,Todos los TPs Aprobados,75% de asistencia a las clases", "Agosto 2024", 1, "Flores", "Integrador Fisica III" },
                    { 2, (short)102, 15, (short)3, null, 25, "TP final obligatorio aprobado,Participación en al menos 50% de las clases prácticas", "Noviembre 2024", 2, "Caballito", "Integrador Matemáticas Avanzadas II" },
                    { 3, (short)305, 20, (short)4, null, 30, "Haber entregado y aprobado el proyecto integrador,Participación en la evaluación grupal previa", "Diciembre 2024", 3, "Belgrano", "Integrador Programación III" },
                    { 4, (short)101, 10, (short)2, null, 20, "Obligatorio completar el ensayo final,Revisión aprobada de las lecturas asignadas", "Enero 2025", 1, "Microcentro", "Integrador Química III" }
                });

            migrationBuilder.InsertData(
                table: "FechaExamen",
                columns: new[] { "FechaExamenId", "ExamenId", "Fecha" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2024, 12, 20, 12, 30, 0, 0, DateTimeKind.Unspecified) },
                    { 2, 1, new DateTime(2025, 1, 10, 11, 30, 0, 0, DateTimeKind.Unspecified) },
                    { 3, 1, new DateTime(2025, 2, 10, 9, 30, 0, 0, DateTimeKind.Unspecified) },
                    { 4, 2, new DateTime(2024, 11, 30, 10, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, 2, new DateTime(2024, 12, 7, 14, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 6, 3, new DateTime(2024, 12, 15, 9, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 7, 3, new DateTime(2024, 12, 22, 13, 30, 0, 0, DateTimeKind.Unspecified) },
                    { 8, 4, new DateTime(2025, 1, 15, 11, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 9, 4, new DateTime(2025, 1, 20, 14, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_examenes_ProfesorId",
                table: "examenes",
                column: "ProfesorId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamenEstudiante_EstudianteId",
                table: "ExamenEstudiante",
                column: "EstudianteId");

            migrationBuilder.CreateIndex(
                name: "IX_FechaExamen_ExamenId",
                table: "FechaExamen",
                column: "ExamenId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExamenEstudiante");

            migrationBuilder.DropTable(
                name: "FechaExamen");

            migrationBuilder.DropTable(
                name: "examenes");
        }
    }
}
