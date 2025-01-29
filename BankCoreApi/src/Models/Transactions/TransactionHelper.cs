namespace BankCoreApi.Models.Transactions;

public class TransactionHelper
{
    public static string GenerateTransactionCode()
    {
        string timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
        string randomString = Guid.NewGuid().ToString().Substring(0, 6);
        string code = $"{timestamp}{randomString}";
        return $"TXN{code}";
    }
}