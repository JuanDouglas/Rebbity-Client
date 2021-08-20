using System.Net.Http;

namespace Rebb.Client.Core.Models
{
    public class Login
    {
        public string AuthenticationToken { get; set; }
        public string AccountKey { get; set; }
        public string FirstStepKey { get; set; }

        public Login()
        {

        }

        public const string AuthenticationTokenHeader = "Authentication-Token";
        public const string AccountKeyHeader = "Account-Key";
        public const string FirstStepKeyHeader = "First-Step-Key";
        internal HttpRequestMessage AutenticatedRequest(HttpRequestMessage defaultRequest)
        {
            HttpRequestMessage request = defaultRequest;

            request.Headers.Add(AuthenticationTokenHeader, AuthenticationToken);
            request.Headers.Add(AccountKeyHeader, AccountKey);
            request.Headers.Add(FirstStepKeyHeader, FirstStepKey);
            return request;
        }

    }
}