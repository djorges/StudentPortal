using StudentPortal.Entities;

namespace StudentPortal.Models
{
    public class EmpleadoDto
    {

        public int Id { get; set; }
        public string Nombre { get; set; }
        public float Salario { get; set; }

        public int IdPerfil { get; set; }
        public string NombrePerfil { get; set; }
    }
}
