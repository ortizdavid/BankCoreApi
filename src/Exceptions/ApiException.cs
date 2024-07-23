namespace BankCoreApi.Exceptions
{
    public class ApiException : Exception
    {
        public int StatusCode { get; set; }
        
        public ApiException(string message) : base(message) {}
    }
}