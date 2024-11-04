using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Anuncio
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O título é obrigatório")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "A descrição é obrigatória")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "O estado do carro é obrigatório")]
        public string EstadoCarro { get; set; }

        [Required(ErrorMessage = "A quilometragem é obrigatória")]
        [Range(0, int.MaxValue, ErrorMessage = "A quilometragem deve ser um número positivo")]
        public int Quilometragem { get; set; }

        [Required(ErrorMessage = "O telefone é obrigatório")]
        [Phone(ErrorMessage = "Telefone inválido")]
        public string Telefone { get; set; }

        public string? ImagePath { get; set; }
    }
}