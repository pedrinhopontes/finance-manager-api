using MediatR;
using FinanceManager.Domain.Entities;

namespace FinanceManager.Application.Transactions.Commands.CreateTransaction;

public record CreateTransactionCommand(
    string Description,
    decimal Amount,
    TransactionType Type,
    string Category,
    Guid UserId
) : IRequest<Guid>;