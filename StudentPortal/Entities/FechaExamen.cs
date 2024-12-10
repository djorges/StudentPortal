namespace StudentPortal.Entities
{
    public class FechaExamen
    {
        public int FechaExamenId { get; set; }
        public DateTime Fecha { get; set; }

        public int ExamenId { get; set; }
        public Examen Examen { get; set; }
    }
}
