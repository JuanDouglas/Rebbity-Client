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

        internal const string AuthenticationTokenHeader = "Authentication-Token";
        internal const string AccountKeyHeader = "Account-Key";
        internal const string FirstStepKeyHeader = "First-Step-Key";
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