using System.ComponentModel.DataAnnotations;

namespace BankCoreApi.Models.Accounts
{
    public class ChangeAccountTypeRequest
    {
        [Required]
        public string? AccountNumber { get; set; }

        [Required]
        public AccountType AccountType { get; set; }
    }
}