using MediatR;
using FinanceManager.Domain.Entities;

namespace FinanceManager.Application.Transactions.Queries.GetTransactionsByUser;

public record GetTransactionsByUserQuery(Guid UserId) : IRequest<IEnumerable<Transaction>>;