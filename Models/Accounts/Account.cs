using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BankCoreApi.Helpers;

namespace BankCoreApi.Models.Accounts
{
    [Table("accounts")]
    public class Account : IAccount, IModel
    {
        [Key]
        [Column("account_id")]
        public int AccountId { get; set; }

        [Required]
        [Column("customer_id")]
        public int CustomerId { get; set; }

        [Required]
        [Column("account_status_id")]
        public int AccountStatusId { get; set; }

        [Required]
        [Column("account_type_id")]
        public int AccountTypeId { get; set; }

        [Required]
        [StringLength(18, ErrorMessage = "Account number must have {1} characters")]
        [Column("account_number")]
        public string? AccountNumber { get; set; }

        [Required]
        [StringLength(31, ErrorMessage = "IBAN must have {1} characters")]
        [Column("iban")]
        public string? Iban { get; set; }

        [Column("holder_name")]
        public string? HolderName { get; set; }

        [Required]
        [Column("balance")]
        public decimal Balance { get; private set; } = 0.0M;

        [Column("currency")]
        public string? Currency { get; set; }

        [Column("unique_id")]
        public Guid UniqueId { get; } = Encryption.GenerateUUID();
        
        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        [Column("updated_at")]
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