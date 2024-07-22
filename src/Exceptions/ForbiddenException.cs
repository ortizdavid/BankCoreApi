namespace BankCoreApi.Exceptions
{
    public class ForbiddenException : ApiException
    {
        public ForbiddenException(string message) : base(message) 
        {
            StatusCode = 403;
        }
    }
}