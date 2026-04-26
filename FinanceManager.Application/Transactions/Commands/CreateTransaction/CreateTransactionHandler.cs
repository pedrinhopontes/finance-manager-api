using MediatR;
using FinanceManager.Domain.Entities;
using FinanceManager.Domain.Interfaces;

namespace FinanceManager.Application.Transactions.Commands.CreateTransaction;

public class CreateTransactionHandler : IRequestHandler<CreateTransactionCommand, Guid>
{
    private readonly ITransactionRepository _repository;

    public CreateTransactionHandler(ITransactionRepository repository)
    {
        _repository = repository;
    }

    public async Task<Guid> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
    {
        var transaction = new Transaction(
            request.Description,
            request.Amount,
            request.Type,
            request.Category,
            request.UserId
        );

        await _repository.AddAsync(transaction);
        return transaction.Id;
    }
}