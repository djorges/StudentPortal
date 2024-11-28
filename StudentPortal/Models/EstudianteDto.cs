using Microsoft.EntityFrameworkCore.Metadata.Internal;
using StudentPortal.Entities;
using System.ComponentModel.DataAnnotations;

namespace StudentPortal.Models
{
    public class EstudianteDto
    {
        public int EstudianteId { get; set; }

        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Username { get; set; }
        [Required(ErrorMessage = "El correo del usuario es requerido")]
        public string Correo { get; set; }
        public PrivacidadCorreo PrivacidadCorreo { get; set; }
        public Genero Genero { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public int Dni { get; set; }
        public string? Domicilio { get; set; }
        public Nacionalidad Nacionalidad { get; set; }
        [Required(ErrorMessage = "El teléfono del usuario es requerido")]
        public int Telefono { get; set; }
        public string? Descripcion { get; set; }

        public IFormFile? FotoPerfil { get; set; } //request
        public byte[]? FotoPerfilBytes { get; set; } //response

        public bool Activo { get; set; }
        public int Edad { get; set; }

        public string? Clave { get; set; }
        public string? ConfirmarClave { get; set; }
        public bool Restablecer { get; set; }
        public bool Confirmado { get; set; }
        public string? Token { get; set; }
    }
}
