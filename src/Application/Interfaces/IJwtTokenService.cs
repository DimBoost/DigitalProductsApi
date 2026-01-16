namespace DigitalProductsApi.src.Application.Interfaces
{
    public interface IJwtTokenService
    {
        string GenerateToken(string userId, string role);
    }
}
