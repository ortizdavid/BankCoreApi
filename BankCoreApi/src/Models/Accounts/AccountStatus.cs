namespace BankCoreApi.Models.Accounts;

public enum AccountStatus
{
    Active = 1,
    Inactive,
    Dormant,
    Closed,
    Suspended,
    Pending,
    Overdrawn,
    Frozen,
    Restricted,
    Verified,
    Unverified
}