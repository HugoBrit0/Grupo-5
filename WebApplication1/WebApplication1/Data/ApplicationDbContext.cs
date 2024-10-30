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

        // Aqui deve estar a DbSet para o seu modelo Administrador
        public DbSet<Administrador> Administrador { get; set; } // <-- Certifique-se de que esta linha está presente
    }
}
