﻿namespace StudentPortal.Models
{
    public class EstudianteDto
    {
        public int EstudianteId { get; set; }
        public string Nombre { get; set; }
        public string Correo { get; set; }
        public int Edad { get; set; }
        public Genero Genero { get; set; }

        public string Clave { get; set; }
        public string ConfirmarClave { get; set; }
        public bool Restablecer { get; set; }
        public bool Confirmado { get; set; }
        public string Token { get; set; }
    }
}