using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankCoreApi.Models.Transactions
{
    [Table("view_transaction_data")]
    public class TransactionData
    {
        [Column("transaction_id")]
        public int TransactionId { get; }

        [Column("unique_id")]
        public string? UniqueId { get; }

        [Column("code")]
        public string? Code { get; }

        [Column("amount")]
        public decimal Amount { get; }

        [Column("balance_before")]
        public decimal BalanceBefore { get; }

        [Column("balance_after")]
        public decimal BalanceAfter { get; }

        [Column("currency")]
        public string? Currency { get; }

        [Column("description")]
        public string? Description { get; }

        [Column("transaction_date")]
        public DateTime TransactionDate { get; }

        [Column("created_at")]
        public DateTime CreatedAt { get; }

        [Column("updated_at")]
        public DateTime UpdatedAt { get; }

        [Column("account_id")]
        public int AccountId { get; }

        [Column("account_number")]
        public string? AccountNumber { get; }

        [Column("iban")]
        public string? Iban { get; }

        [Column("customer_name")]
        public string? CustomerName { get; }

        [Column("identification_number")]
        public string? IdenficationNumber { get; }

        [Column("type_id")]
        public int TypeId { get; }

        [Column("type_name")]
        public string? TypeName { get; }

        [Column("status_id")]
        public int StatusId { get; }

        [Column("status_name")]
        public string? StatusName { get; }
    }
}

   