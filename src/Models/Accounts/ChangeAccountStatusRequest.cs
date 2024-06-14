using System.ComponentModel.DataAnnotations;

namespace BankCoreApi.Models.Accounts
{
    public class ChangeAccountStatusRequest
    {
        [Required]
        public string? AccountNumber { get; set; }

        [Required]
        public AccountStatus AccountStatus { get; set; }
    }
}