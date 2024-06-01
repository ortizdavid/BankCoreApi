using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BankCoreApi.Helpers;

namespace BankCoreApi.Models.Accounts
{
    [Table("savings_accounts")]
    public class SavingsAccount : IAccount
    {
        [Key]
        [Column("account_id")]
        public int AccountId { get; }

        [Required]
        [Column("customer_id")]
        public int CustomerId { get; }

        [Required]
        [StringLength(18, ErrorMessage = "Account number must have {1} characters")]
        [Column("account_number")]
        public string? AccountNumber { get; }

        [Required]
        [StringLength(18, ErrorMessage = "IBAN must have {1} characters")]
        [Column("iban")]
        public string? Iban { get; }

        [Required]
        [Column("balance")]
        public decimal Balance { get; private set; } = 0.0M;

        [Required]
        public AccountStatus Status { get; set; } = AccountStatus.Pending;

        [Column("unique_id")]
        public Guid UniqueId { get; } = Encryption.GenerateUUID();
        
        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        [Column("created_at")]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;


        public void Deposit(decimal amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentOutOfRangeException("amount must be greater than 0");
            } 
            else
            {
                Balance += amount;
            }
        }

        public void Transfer(decimal amount, IAccount destination)
        {
            if (amount <= 0)
            {
                throw new ArgumentOutOfRangeException("amount must be greater than 0");
            }
            if (amount > Balance)
            {
                throw new InvalidOperationException("Insuficient balance");
            }
            Withdraw(amount);
            destination.Deposit(amount);
        }

        public void Withdraw(decimal amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentOutOfRangeException("amount must be greater than 0");
            }
            if (amount > Balance)
            {
                throw new InvalidOperationException("Insuficient balance");
            }
            Balance -= amount;
        }
    }
}