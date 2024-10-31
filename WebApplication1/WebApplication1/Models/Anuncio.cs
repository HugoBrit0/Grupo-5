using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Anuncio
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Titulo { get; set; }

        [Required]
        public string Descricao { get; set; }

        public string? ImagePath { get; set; } // Torna a propriedade opcional
    }
}
