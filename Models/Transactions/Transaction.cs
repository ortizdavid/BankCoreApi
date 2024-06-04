
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BankCoreApi.Helpers;
using BankCoreApi.Models.Accounts;

namespace BankCoreApi.Models.Transactions
{
    [Table("transactions")]
    public class Transaction : IModel
    {
        [Key]
        [Column("transaction_id")]
        public int TransactionId { get; set; }

        [Required]
        [Column("account_id")]
        public int AccountId { get; set; }

        [Required]
        [Column("code")]
        public string? Code { get; }  

        [Required]
        [Column("transaction_type_id")]
        public TransactionType TransactionType { get; set; } 

        [Required]
        [Column("transaction_status_id")]
        public TransactionStatus TransactionStatus { get; set; }

        [Required]
        [Column("balance_before")]
        public decimal BalanceBefore { get; }
        
        [Required]
        [Column("balance_after")]
        public decimal BalanceAfter { get; }

        [Column("currency")]
        public string? Currency { get; set; }

        [Column("description")]
        public string? Description { get; set; }

        [Column("unique_id")]
        public Guid UniqueId { get; } = Encryption.GenerateUUID();

        [Column("transaction_date")]
        public DateTime TransactionDate { get; set; } = DateTime.UtcNow;
        
        [Column("created_at")]
        public DateTime CreatedAt { get ; set; } = DateTime.UtcNow;

        [Column("updated_at")]
        public DateTime UpdatedAt { get ; set; } = DateTime.UtcNow;
    }
}