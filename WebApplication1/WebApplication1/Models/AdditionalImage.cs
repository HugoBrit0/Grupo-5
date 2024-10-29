using System.ComponentModel.DataAnnotations;

namespace YourNamespace.Models
{
    public class AdditionalImage
    {
        public int Id { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        public int CarId { get; set; }
        //public Car Car { get; set; } // Navegação para o Carro
    }
}
