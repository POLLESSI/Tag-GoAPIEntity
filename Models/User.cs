using System.ComponentModel.DataAnnotations;

namespace MyApi.Models
{
    public class User
    {   
        [Key]
        [Required]
        public int Id { get; set; }
            
        [Required]
        public required string Name { get; set; }

        [Required]
        public required string Email { get; set; }

        [Required]
        public required string Password { get; set; }

        [Required]
        public required string Role { get; set; }
    }
}
