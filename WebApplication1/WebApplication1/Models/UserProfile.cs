using System;
using Microsoft.AspNetCore.Identity;

namespace WebApplication1.Models
{
    public class UserProfile
    {
        public int Id { get; set; }
        public string IdentityUserId { get; set; }
        public IdentityUser IdentityUser { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public DateTime DateJoined { get; set; }

        // Adicionando a propriedade para a senha
        public string Password { get; set; } // Armazenando a senha em texto simples (não recomendado)
    }
}