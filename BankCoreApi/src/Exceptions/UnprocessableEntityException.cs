using System.Net;

namespace BankCoreApi.Exceptions;

public class UnprocessableEntityException : ApiException
{
    public UnprocessableEntityException(string message) : base(message) 
    {
        StatusCode = (int)HttpStatusCode.UnprocessableEntity;
    }
}