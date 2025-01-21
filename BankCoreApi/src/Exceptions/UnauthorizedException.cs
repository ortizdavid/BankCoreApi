using System.Net;

namespace BankCoreApi.Exceptions
{
    public class UnauthorizedException : ApiException
    {
        public UnauthorizedException(string message) : base(message) 
        {
            StatusCode = (int)HttpStatusCode.Unauthorized;
        }
    }
}