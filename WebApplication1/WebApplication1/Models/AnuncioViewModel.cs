using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class AnuncioViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O título é obrigatório.")]
        [Display(Name = "Título")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "A descrição é obrigatória.")]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "O estado do carro é obrigatório.")]
        [Display(Name = "Estado do Carro")]
        public string EstadoCarro { get; set; }

        [Required(ErrorMessage = "A quilometragem é obrigatória.")]
        [Range(0, int.MaxValue, ErrorMessage = "A quilometragem deve ser um número positivo.")]
        [Display(Name = "Quilometragem")]
        public int Quilometragem { get; set; }

        [Required(ErrorMessage = "O número de telefone é obrigatório.")]
        [Phone(ErrorMessage = "Número de telefone inválido.")]
        [Display(Name = "Telefone")]
        public string Telefone { get; set; }

        [Display(Name = "Imagem")]
        public string ImagePath { get; set; }
    }
}
