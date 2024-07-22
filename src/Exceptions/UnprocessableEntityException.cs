namespace BankCoreApi.Exceptions
{
    public class UnprocessableEntityException : ApiException
    {
        public UnprocessableEntityException(string message) : base(message) 
        {
            StatusCode = 422;
        }
    }
}