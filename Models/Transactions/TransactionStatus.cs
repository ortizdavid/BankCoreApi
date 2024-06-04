namespace BankCoreApi.Models.Transactions
{
    public enum TransactionStatus
    {
        Pending = 1,
        Completed,
        Approved,
        Failed,
        PendingApproval,
        OnHold,
        Expired,
        Cancelled,
        Refunded
    }
}
