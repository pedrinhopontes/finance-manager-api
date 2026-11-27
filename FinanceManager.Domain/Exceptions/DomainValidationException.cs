namespace FinanceManager.Domain.Exceptions;

/// <summary>Dados inválidos vindos do cliente. Traduzido em HTTP 400.</summary>
public class DomainValidationException : DomainException
{
    public DomainValidationException(string message) : base(message) { }
}
