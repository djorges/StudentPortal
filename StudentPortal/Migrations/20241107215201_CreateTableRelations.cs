using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace StudentPortal.Migrations
{
    /// <inheritdoc />
    public partial class CreateTableRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Estudiantes",
                columns: table => new
                {
                    EstudianteId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(type: "longtext", nullable: false),
                    Apellido = table.Column<string>(type: "longtext", nullable: false),
                    Genero = table.Column<int>(type: "int", nullable: false),
                    FechaNacimiento = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Email = table.Column<string>(type: "longtext", nullable: false),
                    FechaInscripcion = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Activo = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estudiantes", x => x.EstudianteId);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Profesores",
                columns: table => new
                {
                    ProfesorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(type: "longtext", nullable: false),
                    Apellido = table.Column<string>(type: "longtext", nullable: false),
                    Genero = table.Column<int>(type: "int", nullable: false),
                    FechaNacimiento = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Especialidad = table.Column<string>(type: "longtext", nullable: false),
                    NivelEstudio = table.Column<int>(type: "int", nullable: false),
                    Salario = table.Column<float>(type: "float", nullable: false),
                    Activo = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profesores", x => x.ProfesorId);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Cursos",
                columns: table => new
                {
                    CursoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(type: "longtext", nullable: false),
                    Codigo = table.Column<string>(type: "longtext", nullable: false),
                    Creditos = table.Column<int>(type: "int", nullable: false),
                    FechaInicio = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    FechaFin = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    EsObligatorio = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    ProfesorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cursos", x => x.CursoId);
                    table.ForeignKey(
                        name: "FK_Cursos_Profesores_ProfesorId",
                        column: x => x.ProfesorId,
                        principalTable: "Profesores",
                        principalColumn: "ProfesorId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "CursoEstudiantes",
                columns: table => new
                {
                    EstudianteId = table.Column<int>(type: "int", nullable: false),
                    CursoId = table.Column<int>(type: "int", nullable: false),
                    FechaInscripcion = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CursoEstudiantes", x => new { x.EstudianteId, x.CursoId });
                    table.ForeignKey(
                        name: "FK_CursoEstudiantes_Cursos_CursoId",
                        column: x => x.CursoId,
                        principalTable: "Cursos",
                        principalColumn: "CursoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CursoEstudiantes_Estudiantes_EstudianteId",
                        column: x => x.EstudianteId,
                        principalTable: "Estudiantes",
                        principalColumn: "EstudianteId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_CursoEstudiantes_CursoId",
                table: "CursoEstudiantes",
                column: "CursoId");

            migrationBuilder.CreateIndex(
                name: "IX_Cursos_ProfesorId",
                table: "Cursos",
                column: "ProfesorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CursoEstudiantes");

            migrationBuilder.DropTable(
                name: "Cursos");

            migrationBuilder.DropTable(
                name: "Estudiantes");

            migrationBuilder.DropTable(
                name: "Profesores");
        }
    }
}
