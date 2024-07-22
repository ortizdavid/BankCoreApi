namespace BankCoreApi.Exceptions
{
    public class ConflictException : ApiException
    {
        public ConflictException(string message) : base(message) 
        {
            StatusCode = 400;
        }
    }
}