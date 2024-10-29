// Caminho: Models/Car.cs
namespace YourNamespace.Models
{
    public class Car
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string MainImageUrl { get; set; }

        // Campo para armazenar a imagem adicional
        public string AdditionalImageUrl { get; set; }
    }
}
