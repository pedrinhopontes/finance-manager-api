using MediatR;

namespace FinanceManager.Application.Users.Queries.GetProfile;

public record GetProfileQuery(Guid UserId) : IRequest<UserProfileDto>;

public record UserProfileDto(Guid Id, string Name, string Email);