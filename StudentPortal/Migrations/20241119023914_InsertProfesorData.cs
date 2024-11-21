using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace StudentPortal.Migrations
{
    /// <inheritdoc />
    public partial class InsertProfesorData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "NivelEstudio",
                table: "profesores",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Genero",
                table: "profesores",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<float>(
                name: "PorcentajeAlumnosAprobados",
                table: "profesores",
                type: "float",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<int>(
                name: "PuntuacionEncuestas",
                table: "profesores",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Trayectoria",
                table: "profesores",
                type: "longtext",
                nullable: false);

            migrationBuilder.AddColumn<int>(
                name: "MaxInscriptos",
                table: "cursos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "profesores",
                columns: new[] { "ProfesorId", "Activo", "Apellido", "Especialidad", "FechaNacimiento", "Genero", "NivelEstudio", "Nombre", "PorcentajeAlumnosAprobados", "PuntuacionEncuestas", "Salario", "Trayectoria", "imageUrl" },
                values: new object[,]
                {
                    { 1, true, "Herrera", "Matemáticas", new DateTime(1980, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Femenino", "Doctorado", "Sonia", 85.4f, 1890, 75000.5f, "10 años y 6 meses", "profe3.jpg" },
                    { 2, true, "Sambrano", "Física", new DateTime(1985, 8, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Masculino", "Maestria", "Héctor", 80.1f, 980, 68000f, "15 años y 6 meses", "profe1.jpg" },
                    { 3, true, "Ortega", "Química", new DateTime(1975, 3, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Femenino", "Doctorado", "Alison", 76.6f, 796, 62000f, "9 años y 11 meses", "profe2.jpg" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "profesores",
                keyColumn: "ProfesorId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "profesores",
                keyColumn: "ProfesorId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "profesores",
                keyColumn: "ProfesorId",
                keyValue: 3);

            migrationBuilder.DropColumn(
                name: "PorcentajeAlumnosAprobados",
                table: "profesores");

            migrationBuilder.DropColumn(
                name: "PuntuacionEncuestas",
                table: "profesores");

            migrationBuilder.DropColumn(
                name: "Trayectoria",
                table: "profesores");

            migrationBuilder.DropColumn(
                name: "MaxInscriptos",
                table: "cursos");

            migrationBuilder.AlterColumn<int>(
                name: "NivelEstudio",
                table: "profesores",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext");

            migrationBuilder.AlterColumn<int>(
                name: "Genero",
                table: "profesores",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext");
        }
    }
}
