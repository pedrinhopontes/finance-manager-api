using FinanceManager.Domain.Exceptions;

namespace FinanceManager.Domain.Entities;

public class User
{
    public const int NameMaxLength = 100;
    public const int PasswordMinLength = 6;

    public Guid Id { get; private set; }
    public string Name { get; private set; } = null!;
    public string Email { get; private set; } = null!;
    public string PasswordHash { get; private set; } = null!;

    protected User() { }

    public User(string name, string email, string passwordHash)
    {
        Id = Guid.NewGuid();
        UpdateName(name);
        Email = email;
        SetPasswordHash(passwordHash);
    }

    public void UpdateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainValidationException("Nome não pode ser vazio.");

        name = name.Trim();

        if (name.Length > NameMaxLength)
            throw new DomainValidationException(
                $"Nome não pode ter mais de {NameMaxLength} caracteres.");

        Name = name;
    }

    /// <summary>
    /// Troca o hash da senha. A verificação da senha atual e a geração do
    /// hash ficam na camada de aplicação: o domínio não conhece o algoritmo.
    /// </summary>
    public void SetPasswordHash(string passwordHash)
    {
        if (string.IsNullOrWhiteSpace(passwordHash))
            throw new DomainValidationException("Hash da senha não pode ser vazio.");

        PasswordHash = passwordHash;
    }
}
