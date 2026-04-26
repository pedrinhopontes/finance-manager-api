namespace FinanceManager.Domain.Entities;

public class Transaction
{
    public Guid Id { get; private set; }
    public string Description { get; private set; } = null!;
    public decimal Amount { get; private set; }
    public TransactionType Type { get; private set; }
    public DateTime Date { get; private set; }
    public string Category { get; private set; } = null!;
    public Guid UserId { get; private set; }

    protected Transaction() { }

    public Transaction(string description, decimal amount, TransactionType type, string category, Guid userId)
    {
        Id = Guid.NewGuid();
        Description = description;
        Amount = amount;
        Type = type;
        Category = category;
        UserId = userId;
        Date = DateTime.UtcNow;
    }

    public void Update(string description, decimal amount, string category)
    {
        Description = description;
        Amount = amount;
        Category = category;
    }
}