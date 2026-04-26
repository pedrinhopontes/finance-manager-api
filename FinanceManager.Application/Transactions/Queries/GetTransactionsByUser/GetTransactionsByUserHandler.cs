using MediatR;
using FinanceManager.Domain.Entities;
using FinanceManager.Domain.Interfaces;

namespace FinanceManager.Application.Transactions.Queries.GetTransactionsByUser;

public class GetTransactionsByUserHandler : IRequestHandler<GetTransactionsByUserQuery, IEnumerable<Transaction>>
{
    private readonly ITransactionRepository _repository;

    public GetTransactionsByUserHandler(ITransactionRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Transaction>> Handle(GetTransactionsByUserQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetByUserIdAsync(request.UserId);
    }
}