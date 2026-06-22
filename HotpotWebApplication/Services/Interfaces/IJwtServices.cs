namespace HotpotWebApplication.Services.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(int userid, string email, string role);
    }
}
