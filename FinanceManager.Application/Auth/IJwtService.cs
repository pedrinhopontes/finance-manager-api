namespace FinanceManager.Application.Auth;

public interface IJwtService
{
    string GenerateToken(Guid userId, string email);
}