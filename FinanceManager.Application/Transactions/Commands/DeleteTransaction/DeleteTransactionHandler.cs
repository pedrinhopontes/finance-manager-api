using MediatR;
using FinanceManager.Domain.Interfaces;

namespace FinanceManager.Application.Transactions.Commands.DeleteTransaction;

public class DeleteTransactionHandler : IRequestHandler<DeleteTransactionCommand>
{
    private readonly ITransactionRepository _repository;

    public DeleteTransactionHandler(ITransactionRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(DeleteTransactionCommand request, CancellationToken cancellationToken)
    {
        await _repository.DeleteAsync(request.Id);
    }
}