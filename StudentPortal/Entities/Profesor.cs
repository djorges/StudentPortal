using StudentPortal.Models;

namespace StudentPortal.Entities
{
    public class Profesor
    {
        public int ProfesorId { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public Genero Genero { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Especialidad { get; set; }
        public NivelEstudio NivelEstudio { get; set; }
        public float Salario { get; set; }
        public bool Activo { get; set; }
        public string imageUrl { get; set; }

        public List<Curso> Cursos { get; set; } = new List<Curso>();
    }
}
