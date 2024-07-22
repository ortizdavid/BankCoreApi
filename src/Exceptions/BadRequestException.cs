namespace BankCoreApi.Exceptions
{
    public class BadRequestException : ApiException
    {
        public BadRequestException( string message) : base(message) 
        {
            StatusCode = 400;
        }
    }
}