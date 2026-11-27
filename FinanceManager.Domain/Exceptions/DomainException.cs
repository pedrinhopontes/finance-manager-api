namespace FinanceManager.Domain.Exceptions;

/// <summary>
/// Base para erros previstos de regra de negócio. O middleware da API
/// traduz cada tipo derivado no status HTTP correspondente.
/// </summary>
public abstract class DomainException : Exception
{
    protected DomainException(string message) : base(message) { }
}
