using System.ComponentModel.DataAnnotations;

namespace BankCoreApi.Models.Accounts
{
    public class CreateAccountRequest
    {
        [Required]
        public int CustomerId { get; set; }

        [Required]
        public AccountType AccountType { get; set; }
        
        [Required]
        public string? Currency { get; set; }
    }
}