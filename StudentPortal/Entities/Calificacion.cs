namespace StudentPortal.Entities
{
    public class Calificacion
    {
        public int CalificacionId { get; set; }
        public float Puntuacion { get; set; }
        public int TotalVotos { get; set; }

        public Curso Curso { get; set; }
    }
}
