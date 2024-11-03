using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class AdViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O título é obrigatório.")]
        [StringLength(100)]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "A descrição é obrigatória.")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "O estado do carro é obrigatório.")]
        public string EstadoCarro { get; set; }

        [Required(ErrorMessage = "A quilometragem é obrigatória.")]
        [Range(0, int.MaxValue, ErrorMessage = "A quilometragem deve ser um número positivo.")]
        public int Quilometragem { get; set; }

        [Required(ErrorMessage = "O número de telefone é obrigatório.")]
        [Phone(ErrorMessage = "Número de telefone inválido.")]
        public string Telefone { get; set; }

        public string? ImagePath { get; set; }
    }
}
