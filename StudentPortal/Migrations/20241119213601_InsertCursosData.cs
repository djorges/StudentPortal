using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace StudentPortal.Migrations
{
    /// <inheritdoc />
    public partial class InsertCursosData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "Puntuacion",
                table: "calificaciones",
                type: "float",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.InsertData(
                table: "calificaciones",
                columns: new[] { "CalificacionId", "Puntuacion", "TotalVotos" },
                values: new object[,]
                {
                    { 1, 4.5f, 1045 },
                    { 2, 4f, 909 },
                    { 3, 3.9f, 894 }
                });

            migrationBuilder.InsertData(
                table: "cursos",
                columns: new[] { "CursoId", "Aula", "CalificacionId", "CantInscriptos", "Codigo", "Creditos", "Descripcion", "Duracion", "EsObligatorio", "Horarios", "MaxInscriptos", "Nombre", "ProfesorId", "Sede" },
                values: new object[,]
                {
                    { 1, 505, 1, 30, "Cod: 8001", 8, "Introduce a los estudiantes a los fundamentos del diseño y análisis de algoritmos. Se enfoca en desarrollar habilidades para resolver problemas mediante la creación de soluciones computacionales eficientes. A lo largo del curso, se exploran conceptos básicos como..", "Cuatrimestral", true, "Martes y Jueves, 16:00-20:00", 30, "Algoritmos Computacionales I", 1, "Flores" },
                    { 2, 109, 2, 19, "Cod: 8085", 8, "Introduce a los estudiantes a los conceptos fundamentales del cálculo diferencial e integral, estableciendo las bases para el estudio riguroso de funciones de una variable real. Los temas principales incluyen el análisis de límites y continuidad, derivación ...", "Cuatrimestral", true, "Lunes y Miercoles, 10:00-14:00", 30, "Análisis Matemático I", 1, "Palermo" },
                    { 3, 405, 3, 8, "Cod: 6012", 6, "Profundiza en los conceptos de la química general y se enfoca en el estudio de fenómenos químicos a nivel molecular y macroscópico. Los temas incluyen la termodinámica química, equilibrio químico, cinética de las reacciones, electroquímica y propiedades de los gases, líquidos y sólidos.", "Cuatrimestral", false, "Miércoles y Sábados, 7:00-10:00", 30, "Química II", 2, "Puerto Madero" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "cursos",
                keyColumn: "CursoId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "cursos",
                keyColumn: "CursoId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "cursos",
                keyColumn: "CursoId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "calificaciones",
                keyColumn: "CalificacionId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "calificaciones",
                keyColumn: "CalificacionId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "calificaciones",
                keyColumn: "CalificacionId",
                keyValue: 3);

            migrationBuilder.AlterColumn<int>(
                name: "Puntuacion",
                table: "calificaciones",
                type: "int",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "float");
        }
    }
}
