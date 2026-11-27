using MediatR;

namespace FinanceManager.Application.Auth.Queries.Login;

public record LoginQuery(string Email, string Password) : IRequest<string?>;