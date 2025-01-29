namespace BankCoreApi.Models.Transactions;

public class TransactionData
{
    public int TransactionId { get; }
    public Guid UniqueId { get; }
    public string? Code { get; }
    public decimal Amount { get; }
    public decimal BalanceBefore { get; }
    public decimal BalanceAfter { get; }
    public string? Currency { get; }
    public string? Description { get; }
    public DateTime TransactionDate { get; }
    public DateTime CreatedAt { get; }
    public DateTime UpdatedAt { get; }

    public int SourceAccountId { get; }
    public string? SourceAccountNumber { get; }
    public string? SourceIban { get; }

    public int DestinationAccountId { get; }
    public string? DestinationAccountNumber { get; }
    public string? DestinationIban { get; }

    public string? CustomerName { get; }
    public string? IdentificationNumber { get; }

    public int TypeId { get; }
    public string? TypeName { get; }

    public int StatusId { get; }
    public string? StatusName { get; }
}
