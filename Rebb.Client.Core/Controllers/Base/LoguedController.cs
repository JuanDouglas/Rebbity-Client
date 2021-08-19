using Rebb.Client.Core.Models;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Rebb.Client.Core.Controllers.Base
{
    public class LoguedController : ApiController
    {
        public static Login Login { get; set; }
        protected internal HttpRequestMessage LoguedRequest
        {
            get
            {
                return Login.AutenticatedRequest(DefaultRequest);
            }
        }
        protected internal override async Task<T> SendJsonObject<T>(object obj, HttpMethod method, Uri uri)
        {
            return await SendJsonObject<T>(obj, method, uri, false);
        }
        protected internal virtual async Task<T> SendJsonObject<T>(object obj, HttpMethod method, Uri uri, bool logued)
        {
            if (logued)
                return await SendJsonObject<T>(obj, method, uri, LoguedRequest);

            return await base.SendJsonObject<T>(obj, method, uri);
        }
    }
}