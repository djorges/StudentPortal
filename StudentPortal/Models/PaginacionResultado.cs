namespace StudentPortal.Models
{
    public class PaginacionResultado<T>
    {
        public List<T> Elementos { get; set; } = new List<T>();
        public int PaginaActual { get; set; }
        public int TotalPaginas { get; set; }
        public int TotalElementos { get; set; }
        public Dictionary<string, object?> Filtros { get; set; } = new();
    }
}
