using System.ComponentModel.DataAnnotations;

namespace BankCoreApi.Models.Accounts;

public class ChangeAccountStatusRequest
{
    [Required]
    public string? AccountNumber { get; set; }

    [Required]
    public AccountStatus NewStatus { get; set; }
}

public class ChangeAccountTypeRequest
{
    [Required]
    public string? AccountNumber { get; set; }

    [Required]
    public AccountType NewType { get; set; }
}