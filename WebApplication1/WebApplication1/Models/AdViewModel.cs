﻿using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class AdViewModel
    {
        [Required]
        [StringLength(100)]
        public string Titulo { get; set; }

        [Required]
        public string Descricao { get; set; }

        public string ImagePath { get; set; } // Para armazenar o caminho da imagem
    }
}
