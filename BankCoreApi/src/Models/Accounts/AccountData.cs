namespace BankCoreApi.Models.Accounts;

public class AccountData
{
    public int AccountId { get; }
    public Guid UniqueId { get; }
    public string? AccountNumber { get; }
    public string? Iban { get; }
    public decimal Balance { get; }
    public string? Currency { get; }
    public DateTime CreatedAt { get; }
    public DateTime UpdatedAt { get; }
    public int CustomerId { get; }
    public string? CustomerName { get; }
    public string? IdentificationNumber { get; }
    public int TypeId { get; }
    public string? TypeName { get; }
    public int StatusId { get; }
    public string? StatusName { get; }
}
