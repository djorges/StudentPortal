namespace StudentPortal.Entities
{
    public class Curso
    {
        public int CursoId { get; set; }
        public string Nombre { get; set; }
        public string Codigo { get; set; }
        public int Creditos { get; set; } 
        public bool EsObligatorio { get; set; }
        public string Sede { get; set; }
        public int Aula { get; set; }
        public string Horarios { get; set; }
        public int CantInscriptos { get; set; }
        public int MaxInscriptos { get; set; }
        public string Descripcion { get; set; }
        public string Duracion { get; set; }

        public int CalificacionId { get; set; }
        public Calificacion Calificacion { get; set; }

        public int ProfesorId { get; set; } 
        public Profesor Profesor { get; set; }

        public List<CursoEstudiante> CursosEstudiantes { get; set; } = new List<CursoEstudiante>();
    }
}
