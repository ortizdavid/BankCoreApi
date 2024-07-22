namespace BankCoreApi.Exceptions
{
    public class UnauthorizedException : ApiException
    {
        public UnauthorizedException(string message) : base(message) 
        {
            StatusCode = 401;
        }
    }
}