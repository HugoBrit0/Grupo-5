using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class AdViewModel
    {
        [Required(ErrorMessage = "O título é obrigatório.")]
        [StringLength(100)]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "A descrição é obrigatória.")]
        public string Descricao { get; set; }

        public string ImagePath { get; set; }

        // Construtor para inicializar as propriedades
        public AdViewModel()
        {
            Titulo = string.Empty;
            Descricao = string.Empty;
            ImagePath = string.Empty;
        }
    }
}
