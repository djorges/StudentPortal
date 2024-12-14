namespace StudentPortal.Entities
{
    public class Examen
    {
        public int ExamenId { get; set; }
        public string Titulo { get; set; }
        public string PeriodoLectivo { get; set; }
        public string Sede { get; set; }
        public short Aula { get; set; }
        public int CantInscriptos { get; set; }
        public int MaxInscriptos { get; set; }

        public DateTime? FechaSeleccionada { get; set; } //TODO: delete
        public short DuracionHoras { get; set; }
        public string[]? NotasProfesor { get; set; }

        public int ProfesorId { get; set; }
        public Profesor Profesor { get; set; }

        public List<FechaExamen> FechasDisponibles { get; set; } = new List<FechaExamen>();
        public List<ExamenEstudiante> ExamenEstudiantes { get; set; } = new List<ExamenEstudiante>();
    }
}
