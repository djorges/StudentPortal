using StudentPortal.Models;

namespace StudentPortal.Entities
{
    public class Estudiante
    {
        public int EstudianteId { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Username { get; set; }

        public string Correo { get; set; }
        public PrivacidadCorreo PrivacidadCorreo { get; set; }
        public Genero Genero { get; set; }
        public int Edad { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public bool Activo { get; set; }

        public int Dni { get; set; }
        public string Domicilio  { get; set; }
        public Nacionalidad Nacionalidad { get; set; }
        public int Telefono { get; set; }
        public string Descripcion { get; set; }
        public byte[]? FotoPerfil { get; set; }

        public string Clave { get; set; }
        public string ConfirmarClave { get; set; }
        public bool Restablecer { get; set; }
        public bool Confirmado { get; set; }
        public string Token { get; set; }

        //public List<CursoEstudiante> CursosEstudiantes { get; set; } = new List<CursoEstudiante>();
    }
}
