namespace StudentPortal.Models
{
    public class FiltrosDto
    {
        private string? _busquedaNombre;
        public string? BusquedaNombre
        {
            get => _busquedaNombre;
            set => _busquedaNombre = value is not null ? Uri.EscapeDataString(value) : null;
        }

        private string? _sede;
        public string? Sede
        {
            get => _sede;
            set => _sede = value is not null ? Uri.EscapeDataString(value) : null;
        }

        public int? Creditos { get; set; }

        public bool? Obligatorio { get; set; }

        private string? _duracion;
        public string? Duracion
        {
            get => _duracion;
            set => _duracion = value is not null ? Uri.EscapeDataString(value) : null;
        }
    }
}
