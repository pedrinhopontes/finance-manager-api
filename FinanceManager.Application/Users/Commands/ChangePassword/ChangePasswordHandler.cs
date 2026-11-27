using MediatR;
using FinanceManager.Domain.Entities;
using FinanceManager.Domain.Exceptions;
using FinanceManager.Domain.Interfaces;

namespace FinanceManager.Application.Users.Commands.ChangePassword;

public class ChangePasswordHandler : IRequestHandler<ChangePasswordCommand>
{
    private readonly IUserRepository _repository;

    public ChangePasswordHandler(IUserRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await _repository.GetByIdAsync(request.UserId)
            ?? throw NotFoundException.For("Usuário", request.UserId);

        if (string.IsNullOrWhiteSpace(request.CurrentPassword))
            throw new DomainValidationException("Informe a senha atual.");

        // Confirma a identidade antes de trocar: um token roubado não deve
        // ser suficiente para tomar a conta.
        if (!BCrypt.Net.BCrypt.Verify(request.CurrentPassword, user.PasswordHash))
            throw new DomainValidationException("Senha atual incorreta.");

        if (string.IsNullOrWhiteSpace(request.NewPassword)
            || request.NewPassword.Length < User.PasswordMinLength)
            throw new DomainValidationException(
                $"A nova senha deve ter ao menos {User.PasswordMinLength} caracteres.");

        if (BCrypt.Net.BCrypt.Verify(request.NewPassword, user.PasswordHash))
            throw new DomainValidationException("A nova senha deve ser diferente da atual.");

        user.SetPasswordHash(BCrypt.Net.BCrypt.HashPassword(request.NewPassword));
        await _repository.UpdateAsync(user);
    }
}
