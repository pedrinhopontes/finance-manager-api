using MediatR;
using FinanceManager.Domain.Interfaces;

namespace FinanceManager.Application.Auth.Queries.Login;

public class LoginHandler : IRequestHandler<LoginQuery, string?>
{
    private readonly IUserRepository _repository;
    private readonly IJwtService _jwtService;

    public LoginHandler(IUserRepository repository, IJwtService jwtService)
    {
        _repository = repository;
        _jwtService = jwtService;
    }

    public async Task<string?> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
        var user = await _repository.GetByEmailAsync(request.Email);

        if (user is null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            return null;

        return _jwtService.GenerateToken(user.Id, user.Email);
    }
}