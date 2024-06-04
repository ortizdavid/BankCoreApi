using System.ComponentModel.DataAnnotations;

namespace BankCoreApi.Models.Accounts
{
    public class AccountRequest
    {
        [Required]
        [StringLength(150, ErrorMessage = "Customer name can't be longer than {1} characters.")]
        public string? CustomerName { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "Identification number can't be longer than {1} characters.")]
        public string? IdentificationNumber { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [StringLength(150, ErrorMessage = "Email must be between {2} and {1} characters.", MinimumLength = 10)]
        public string? Email { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "Phone number can't be longer than {1} characters.")]
        public string? Phone { get; set; }

        [Required]
        [StringLength(200, ErrorMessage = "Address can't be longer than {1} characters.")]
        public string? Address { get; set; }

        [Required]
        public AccountType AccountTypeId { get; set; }

        [Required]
        [StringLength(3, ErrorMessage = "Currency code can't be longer than {1} characters.")]
        public string? Currency { get; set; }
    }
}