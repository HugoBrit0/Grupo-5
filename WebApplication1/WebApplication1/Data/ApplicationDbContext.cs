using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<AdminUser> AdminUsers { get; set; }
        public DbSet<Anuncio> Anuncios { get; set; } // Adiciona o DbSet para Anuncios

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Dados iniciais para AdminUser
            modelBuilder.Entity<AdminUser>().HasData(
                new AdminUser
                {
                    Id = 1,
                    Username = "admin",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin123"),
                    Email = "admin@example.com"
                }
            );

            // Dados iniciais para Anuncio
            modelBuilder.Entity<Anuncio>().HasData(
                new Anuncio
                {
                    Id = 1,
                    Titulo = "Primeiro Anúncio",
                    Descricao = "Descrição do primeiro anúncio.",
                    ImagePath = null // Inicializa como nulo ou adicione um caminho padrão se necessário.
                },
                new Anuncio
                {
                    Id = 2,
                    Titulo = "Segundo Anúncio",
                    Descricao = "Descrição do segundo anúncio.",
                    ImagePath = null // Inicializa como nulo ou adicione um caminho padrão se necessário.
                }
            );
        }
    }
}
