using MediatR;

namespace FinanceManager.Application.Users.Commands.UpdateProfile;

public record UpdateProfileCommand(Guid UserId, string Name) : IRequest;