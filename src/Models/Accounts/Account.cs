using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BankCoreApi.Helpers;

namespace BankCoreApi.Models.Accounts
{
    public class Account : IAccount, IModel
    {
        [Key]
        public int AccountId { get; set; }

        [Required]
        public int CustomerId { get; set; }

        [Required]
        public AccountStatus AccountStatus { get; set; }

        [Required]
        public AccountType AccountType { get; set; }

        [Required]
        [StringLength(18, ErrorMessage = "Account number must have {1} characters")]
        public string? AccountNumber { get; set; }

        [Required]
        [StringLength(31, ErrorMessage = "IBAN must have {1} characters")]
        public string? Iban { get; set; }

        public string? HolderName { get; set; }

        [Required]
        public decimal Balance { get; private set; } = 0.0M;

        public string? Currency { get; set; }

        public Guid UniqueId { get; } = Encryption.GenerateUUID();
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;


        // Deposit if account is Active
        public void Deposit(decimal amount)
        {
            ValidateAmount(amount);
            CheckAccountStatus();
            Balance += amount;
        }

        // Transfer if both accounts are Active
        public void Transfer(decimal amount, IAccount destination)
        {
            ValidateAmount(amount);
            CheckAccountStatus();
            destination.CheckAccountStatus();
            if (amount > Balance)
            {
                throw new InvalidOperationException($"Insufficient funds in account '{AccountNumber}'.");
            }
            Withdraw(amount);
            destination.Deposit(amount);
        }

        // Withdraw if account is Active
        public void Withdraw(decimal amount)
        {
            ValidateAmount(amount);
            CheckAccountStatus();
            if (amount > Balance)
            {
                throw new InvalidOperationException($"Insufficient funds in account '{AccountNumber}'.");
            }
            Balance -= amount;
        }

        // Helper method to validate the amount
        public void ValidateAmount(decimal amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentOutOfRangeException("amount must be greater than 0");
            }
        }

        // Helper method to check the account status
        public void CheckAccountStatus()
        {
            if (AccountStatus != AccountStatus.Active)
            {
                throw new InvalidOperationException($"Account is not active. Status: {AccountStatus.ToString()}");
            }
        }

    }
}