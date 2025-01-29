using System.Net;

namespace BankCoreApi.Exceptions;

public class NotFoundException : ApiException
{
    public NotFoundException(string message) : base(message) 
    {
        StatusCode = (int)HttpStatusCode.NotFound;
    }
}