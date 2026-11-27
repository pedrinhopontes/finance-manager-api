using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using FinanceManager.API.Contracts;
using FinanceManager.Application.Users.Commands.ChangePassword;
using FinanceManager.Application.Users.Commands.UpdateProfile;
using FinanceManager.Application.Users.Queries.GetProfile;

namespace FinanceManager.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
[Produces("application/json")]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>Id do usuário autenticado, lido da claim do JWT.</summary>
    private Guid GetUserId()
    {
        var claim = User.FindFirstValue(ClaimTypes.NameIdentifier);

        return Guid.TryParse(claim, out var userId)
            ? userId
            : throw new UnauthorizedAccessException("Token sem claim de usuário válida.");
    }

    /// <summary>Dados do perfil do usuário autenticado.</summary>
    [HttpGet("profile")]
    [ProducesResponseType<UserProfileDto>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetProfile()
    {
        var profile = await _mediator.Send(new GetProfileQuery(GetUserId()));
        return Ok(profile);
    }

    /// <summary>Altera o nome do usuário autenticado.</summary>
    [HttpPut("profile")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateProfile([FromBody] UpdateProfileRequest request)
    {
        await _mediator.Send(new UpdateProfileCommand(GetUserId(), request.Name));
        return NoContent();
    }

    /// <summary>Troca a senha do usuário autenticado.</summary>
    [HttpPut("password")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest request)
    {
        await _mediator.Send(new ChangePasswordCommand(
            GetUserId(),
            request.CurrentPassword,
            request.NewPassword));

        return NoContent();
    }
}
