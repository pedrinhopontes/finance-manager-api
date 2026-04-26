using MediatR;

namespace FinanceManager.Application.Transactions.Commands.DeleteTransaction;

public record DeleteTransactionCommand(Guid Id) : IRequest;