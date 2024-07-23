using System.Net;

namespace BankCoreApi.Exceptions
{
    public class ForbiddenException : ApiException
    {
        public ForbiddenException(string message) : base(message) 
        {
            StatusCode = (int)HttpStatusCode.Forbidden;
        }
    }
}