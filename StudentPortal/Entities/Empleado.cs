using StudentPortal.Entities;
using System.ComponentModel.DataAnnotations;

namespace StudentPortal.Entities
{
    public class Empleado
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public float Salario { get; set; }

        public int IdPerfil { get; set; }
        public virtual Perfil Perfil { get; set; }
    }
}
