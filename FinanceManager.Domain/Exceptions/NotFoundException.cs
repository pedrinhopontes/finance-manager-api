namespace FinanceManager.Domain.Exceptions;

/// <summary>Recurso não encontrado. Traduzido em HTTP 404.</summary>
public class NotFoundException : DomainException
{
    public NotFoundException(string message) : base(message) { }

    public static NotFoundException For(string entidade, Guid id)
        => new($"{entidade} com id '{id}' não foi encontrado.");
}
