using MediatR;
using FinanceManager.Domain.Exceptions;
using FinanceManager.Domain.Interfaces;

namespace FinanceManager.Application.Users.Commands.UpdateProfile;

public class UpdateProfileHandler : IRequestHandler<UpdateProfileCommand>
{
    private readonly IUserRepository _repository;

    public UpdateProfileHandler(IUserRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(UpdateProfileCommand request, CancellationToken cancellationToken)
    {
        var user = await _repository.GetByIdAsync(request.UserId)
            ?? throw NotFoundException.For("Usuário", request.UserId);

        user.UpdateName(request.Name);
        await _repository.UpdateAsync(user);
    }
}
