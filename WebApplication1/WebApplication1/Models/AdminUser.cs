using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class AdminUser
    {
        public int Id { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
