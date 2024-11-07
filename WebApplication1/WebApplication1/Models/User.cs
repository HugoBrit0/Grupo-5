   using System.ComponentModel.DataAnnotations;

   namespace WebApplication1.Models
   {
       public class User
       {
           [Key]
           public int Id { get; set; }

           [Required]
           [EmailAddress]
           public string Email { get; set; }

           [Required]
           [DataType(DataType.Password)]
           public string Password { get; set; }

           public string PhotoPath { get; set; }

           public string Username { get; set; }
           public string FirstName { get; set; }
           public string LastName { get; set; }
           public string PasswordHash { get; set; }
           public string ProfilePicture { get; set; }
       }
   }