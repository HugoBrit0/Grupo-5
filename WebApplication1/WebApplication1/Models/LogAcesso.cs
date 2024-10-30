using System;

namespace WebApplication1.Models
{
    public class LogAcesso
    {
        public int Id { get; set; }
        public string Usuario { get; set; }
        public DateTime DataHora { get; set; }
        public int AdminId { get; set; } // Usar AdminId se for uma relação

        // Referência à classe Admin
        public Admin Admin { get; set; }
    }
}
