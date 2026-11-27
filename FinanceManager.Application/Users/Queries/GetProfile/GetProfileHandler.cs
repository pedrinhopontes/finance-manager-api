using MediatR;
using FinanceManager.Domain.Exceptions;
using FinanceManager.Domain.Interfaces;

namespace FinanceManager.Application.Users.Queries.GetProfile;

public class GetProfileHandler : IRequestHandler<GetProfileQuery, UserProfileDto>
{
    private readonly IUserRepository _repository;

    public GetProfileHandler(IUserRepository repository)
    {
        _repository = repository;
    }

    public async Task<UserProfileDto> Handle(GetProfileQuery request, CancellationToken cancellationToken)
    {
        var user = await _repository.GetByIdAsync(request.UserId)
            ?? throw NotFoundException.For("Usuário", request.UserId);

        return new UserProfileDto(user.Id, user.Name, user.Email);
    }
}
