namespace DigitalProductsApi.src.Application.DTOs.Users
{
    public class AuthResultDto
    {
        public string Token { get; set; } = null!;
        public DateTime Expiration { get; set; }
        public string UserId { get; set; } = null!;
        public string? Role { get; set; }

    }
}
