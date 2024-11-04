using StudentPortal.Models;

namespace StudentPortal.Entities
{
    public class Perfil
    {
        public int Id { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection <Empleado> Empleados { get; set; }
    }
}
