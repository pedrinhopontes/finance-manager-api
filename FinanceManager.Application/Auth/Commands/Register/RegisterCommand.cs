using MediatR;

namespace FinanceManager.Application.Auth.Commands.Register;

public record RegisterCommand(string Name, string Email, string Password) : IRequest<Guid>;