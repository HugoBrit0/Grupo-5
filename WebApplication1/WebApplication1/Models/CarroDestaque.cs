using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class CarroDestaque
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public byte[] ImagemData { get; set; }
    }
}
