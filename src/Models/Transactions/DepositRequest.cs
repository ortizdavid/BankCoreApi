using System.ComponentModel.DataAnnotations;

namespace BankCoreApi.Models.Transactions
{
    public class DepositRequest
    {
        [Required]
        [StringLength(18, ErrorMessage = "Account Number must have {1} characters.")]
        public string? AccountNumber { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        [StringLength(3, ErrorMessage = "Currency must have {1} characters.")]
        public string? Currency { get; set; }
    }
}