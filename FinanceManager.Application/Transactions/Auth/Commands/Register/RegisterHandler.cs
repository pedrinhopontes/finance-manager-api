using MediatR;
using FinanceManager.Domain.Entities;
using FinanceManager.Domain.Interfaces;

namespace FinanceManager.Application.Auth.Commands.Register;

public class RegisterHandler : IRequestHandler<RegisterCommand, Guid>
{
    private readonly IUserRepository _repository;

    public RegisterHandler(IUserRepository repository)
    {
        _repository = repository;
    }

    public async Task<Guid> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);
        var user = new User(request.Name, request.Email, passwordHash);
        await _repository.AddAsync(user);
        return user.Id;
    }
}