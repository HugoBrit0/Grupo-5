namespace WebApplication1.Models
{
    public class HomeViewModel
    {
        public IEnumerable<Anuncio> Anuncios { get; set; }
        public IEnumerable<Anuncio> AnunciosDestaque { get; set; }
    }
}
