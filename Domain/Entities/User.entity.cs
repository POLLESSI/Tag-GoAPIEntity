using System.ComponentModel.DataAnnotations;

namespace MyApi.Domain.Entities
{
    public class UserEntity
    {   
        [Key]
        public int Id { get; set; }
            
        public string? Name { get; set; }

        [Required]
        public required string Email { get; set; }

        [Required]
        public required string Password { get; set; }

        [Required]
        public required string Role { get; set; }
    }
}
