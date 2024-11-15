using StudentPortal.Models;

namespace StudentPortal.Entities
{
    public class Estudiante
    {
        public int EstudianteId { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public Genero Genero { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Email { get; set; }
        public DateTime FechaInscripcion { get; set; }
        public bool Activo { get; set; }

        public List<CursoEstudiante> CursosEstudiantes { get; set; } = new List<CursoEstudiante>();
    }
}
