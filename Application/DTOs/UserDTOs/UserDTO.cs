namespace MyApi.Application.DTOs.UserDTOs  
{
    public class UserDto
    {
        public int Id { get; set; }
        public required string? Name { get; set; }
        public required string Email { get; set; }
        public required string? Role { get; set; }
    }
}
