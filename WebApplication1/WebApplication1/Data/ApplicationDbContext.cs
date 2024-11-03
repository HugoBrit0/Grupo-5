using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using BCrypt.Net;
using System;
using System.Linq;

namespace WebApplication1.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Anuncio> Anuncios { get; set; }
        public DbSet<AdminUser> AdminUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configurações adicionais de modelo, se necessário
        }

        public void SeedAnuncios()
        {
            if (!Anuncios.Any())
            {
                Anuncios.AddRange(
                    new Anuncio
                    {
                        Titulo = "Carro Teste 1",
                        Descricao = "Este é um carro de teste para verificar a funcionalidade.",
                        EstadoCarro = "Usado",
                        Quilometragem = 50000,
                        Telefone = "123-456-7890",
                        ImagePath = "/images/carro1.jpg"
                    },
                    new Anuncio
                    {
                        Titulo = "Carro Teste 2",
                        Descricao = "Outro carro de teste para nossa aplicação.",
                        EstadoCarro = "Novo",
                        Quilometragem = 0,
                        Telefone = "098-765-4321",
                        ImagePath = "/images/carro2.jpg"
                    }
                );
                SaveChanges();
            }
        }

        public void SeedAdminUser()
        {
            if (!AdminUsers.Any())
            {
                AdminUsers.Add(new AdminUser
                {
                    Username = "admin",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin123"),
                    Email = "admin@example.com",
                    CreatedAt = DateTime.UtcNow
                });
                SaveChanges();
            }
        }
    }
}
