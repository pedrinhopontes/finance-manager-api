using MediatR;
using Microsoft.AspNetCore.Mvc;
using FinanceManager.Application.Auth.Commands.Register;
using FinanceManager.Application.Auth.Queries.Login;

namespace FinanceManager.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterCommand command)
    {
        var id = await _mediator.Send(command);
        return CreatedAtAction(nameof(Register), new { id }, id);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginQuery query)
    {
        var token = await _mediator.Send(query);
        if (token is null)
            return Unauthorized("Email ou senha inválidos.");
        return Ok(new { token });
    }
}