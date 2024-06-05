
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BankCoreApi.Helpers;

namespace BankCoreApi.Models.Transactions
{
    public class Transaction : IModel
    {
        [Key]
        public int TransactionId { get; set; }
        public int AccountId { get; set; }
        public string? Code { get; set; }  
        public TransactionType TransactionType { get; set; } 
        public TransactionStatus TransactionStatus { get; set; }
        public decimal Amount { get; set; }
        public decimal BalanceBefore { get; set; }
        public decimal BalanceAfter { get; set; }
        public string? Currency { get; set; }
        public string? Description { get; set; }
        public Guid UniqueId { get; } = Encryption.GenerateUUID();
        public DateTime TransactionDate { get; set; } = DateTime.UtcNow;
        public DateTime CreatedAt { get ; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get ; set; } = DateTime.UtcNow;
        
    }
}