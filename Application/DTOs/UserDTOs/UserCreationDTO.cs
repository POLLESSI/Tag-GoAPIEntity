namespace MyApi.Application.DTOs.UserDTOs   
{
    public class UserCreationDto
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}
