namespace DigitalProductsApi.src.Application.DTOs.Users
{
    public class RegisterUserDto
    {
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string? Email { get; set; }

    }
}
