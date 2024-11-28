using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentPortal.Migrations
{
    /// <inheritdoc />
    public partial class ModifyEstudiantesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Descripcion",
                table: "estudiantes",
                type: "longtext",
                nullable: false);

            migrationBuilder.AddColumn<int>(
                name: "Dni",
                table: "estudiantes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Domicilio",
                table: "estudiantes",
                type: "longtext",
                nullable: false);

            migrationBuilder.AddColumn<byte[]>(
                name: "FotoPerfil",
                table: "estudiantes",
                type: "longblob",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Nacionalidad",
                table: "estudiantes",
                type: "longtext",
                nullable: false);

            migrationBuilder.AddColumn<string>(
                name: "PrivacidadCorreo",
                table: "estudiantes",
                type: "longtext",
                nullable: false);

            migrationBuilder.AddColumn<int>(
                name: "Telefono",
                table: "estudiantes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "cursos",
                keyColumn: "CursoId",
                keyValue: 1,
                column: "Nombre",
                value: "Algoritmo Computacional I");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Descripcion",
                table: "estudiantes");

            migrationBuilder.DropColumn(
                name: "Dni",
                table: "estudiantes");

            migrationBuilder.DropColumn(
                name: "Domicilio",
                table: "estudiantes");

            migrationBuilder.DropColumn(
                name: "FotoPerfil",
                table: "estudiantes");

            migrationBuilder.DropColumn(
                name: "Nacionalidad",
                table: "estudiantes");

            migrationBuilder.DropColumn(
                name: "PrivacidadCorreo",
                table: "estudiantes");

            migrationBuilder.DropColumn(
                name: "Telefono",
                table: "estudiantes");

            migrationBuilder.UpdateData(
                table: "cursos",
                keyColumn: "CursoId",
                keyValue: 1,
                column: "Nombre",
                value: "Algoritmos Computacionales I");
        }
    }
}
