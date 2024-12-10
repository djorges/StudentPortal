namespace StudentPortal.Entities
{
    public class ExamenEstudiante
    {
        public int EstudianteId { get; set; }
        public Estudiante Estudiante { get; set; }

        public int ExamenId { get; set; }
        public Examen Examen { get; set; }

        public DateTime FechaInscripcion { get; set; }
    }
}
