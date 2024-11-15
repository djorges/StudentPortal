namespace StudentPortal.Entities
{
    public class Curso
    {
        public int CursoId { get; set; }
        public string Nombre { get; set; }
        public string Codigo { get; set; }
        public int Creditos { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public bool EsObligatorio { get; set; }


        public int ProfesorId { get; set; } 
        public Profesor Profesor { get; set; }

        public List<CursoEstudiante> CursosEstudiantes { get; set; } = new List<CursoEstudiante>();
    }
}
