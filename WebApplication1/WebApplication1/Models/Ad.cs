﻿namespace WebApplication1.Models
{
    public class Ad
    {
        public int Id { get; set; }
        public string Marca { get; set; }
        public int Quilometragem { get; set; }
        public int Ano { get; set; }
        public string Estado { get; set; }
        public string ImagePath { get; set; } // Opcional, caso necessário
    }
}
