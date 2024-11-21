using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace StudentPortal.Migrations
{
    /// <inheritdoc />
    public partial class CreateCursosTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "calificaciones",
                columns: table => new
                {
                    CalificacionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Puntuacion = table.Column<int>(type: "int", nullable: false),
                    TotalVotos = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_calificaciones", x => x.CalificacionId);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "perfiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_perfiles", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "profesores",
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
                    Activo = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    imageUrl = table.Column<string>(type: "longtext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_profesores", x => x.ProfesorId);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "empleados",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    Salario = table.Column<float>(type: "float", nullable: false),
                    IdPerfil = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_empleados", x => x.Id);
                    table.ForeignKey(
                        name: "FK_empleados_perfiles_IdPerfil",
                        column: x => x.IdPerfil,
                        principalTable: "perfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "cursos",
                columns: table => new
                {
                    CursoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    Codigo = table.Column<string>(type: "longtext", nullable: false),
                    Creditos = table.Column<int>(type: "int", nullable: false),
                    EsObligatorio = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Sede = table.Column<string>(type: "longtext", nullable: false),
                    Aula = table.Column<int>(type: "int", nullable: false),
                    Horarios = table.Column<string>(type: "longtext", nullable: false),
                    CantInscriptos = table.Column<int>(type: "int", nullable: false),
                    Descripcion = table.Column<string>(type: "longtext", nullable: false),
                    Duracion = table.Column<string>(type: "longtext", nullable: false),
                    CalificacionId = table.Column<int>(type: "int", nullable: false),
                    ProfesorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cursos", x => x.CursoId);
                    table.ForeignKey(
                        name: "FK_cursos_calificaciones_CalificacionId",
                        column: x => x.CalificacionId,
                        principalTable: "calificaciones",
                        principalColumn: "CalificacionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_cursos_profesores_ProfesorId",
                        column: x => x.ProfesorId,
                        principalTable: "profesores",
                        principalColumn: "ProfesorId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.InsertData(
                table: "perfiles",
                columns: new[] { "Id", "Nombre" },
                values: new object[,]
                {
                    { 1, "Administrativo" },
                    { 2, "Profesor" },
                    { 3, "Investigador" },
                    { 4, "Bibliotecario" },
                    { 5, "Academico" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_cursos_CalificacionId",
                table: "cursos",
                column: "CalificacionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_cursos_ProfesorId",
                table: "cursos",
                column: "ProfesorId");

            migrationBuilder.CreateIndex(
                name: "IX_empleados_IdPerfil",
                table: "empleados",
                column: "IdPerfil");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "cursos");

            migrationBuilder.DropTable(
                name: "empleados");

            migrationBuilder.DropTable(
                name: "calificaciones");

            migrationBuilder.DropTable(
                name: "profesores");

            migrationBuilder.DropTable(
                name: "perfiles");
        }
    }
}
