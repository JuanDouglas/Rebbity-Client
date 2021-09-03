using Android.Util;
using Newtonsoft.Json;
using Rebb.Client.Core.Exceptions;
using Rebb.Client.Core.Models.Result;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Rebb.Client.Core.Controllers.Base
{
    public abstract class ApiController
    {
        private static string[] Domains => new string[] { "rebbity-api.azurewebsites.net", "api.rebbity.com", "192.168.1.64:5001" };
        private static string _testedDomain;
        private static string TestedDomain
        {
            get
            {
                if (_testedDomain == null)
                {
                    foreach (string domain in Domains)
                    {
                        try
                        {
                            HttpClient httpClient = HttpClient;
                            Task<HttpResponseMessage> awaitResponse = httpClient.GetAsync($"https://{domain}/api/Server/Status");
                            awaitResponse.Wait();
                            HttpResponseMessage response = awaitResponse.Result;
                            if (response.StatusCode == HttpStatusCode.OK)
                            {
                                _testedDomain = domain;
                                break;
                            }
                        }
                        catch (Exception e)
                        {

                        }
                    }
                }
                return _testedDomain;
            }
        }
        public static Uri Host => new Uri($"https://{TestedDomain}/api");
        public static string UserAgent { get; set; }

        protected internal virtual string DefaultHost => Host.AbsoluteUri + "/Login";
        protected internal static HttpClient HttpClient
        {
            get
            {
                HttpClient client = new HttpClient
                {
                    Timeout = TimeSpan.FromSeconds(15)
                };
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                return client;
            }
        }
        protected internal static async Task<HttpResponseMessage> SendAsync(HttpRequestMessage message)
        {
            try
            {
                using HttpClient client = HttpClient;
                HttpResponseMessage response = await client.SendAsync(message).ConfigureAwait(false);
                if (response.StatusCode == HttpStatusCode.BadRequest)
                {
                    string responseContent = await response.Content.ReadAsStringAsync();
                    ValidationErrorsResult result = JsonConvert.DeserializeObject<ValidationErrorsResult>(responseContent);
                    ValidationErrorsException exception = new ValidationErrorsException(result);
                    throw exception;
                }

                return response;
            }
            catch (Exception e)
            {
                throw;
            }

            throw new NotImplementedException();
        }
        protected internal static async Task<HttpResponseMessage> GetAsync(string uri)
        {
            return await SendAsync(new HttpRequestMessage(HttpMethod.Get, uri));
        }

        protected internal virtual async Task<T> SendJsonObject<T>(object obj, HttpMethod method, Uri uri)
        {
            return await SendJsonObject<T>(obj, method, uri, DefaultRequest);
        }
        protected internal virtual async Task<T> SendJsonObject<T>(object obj, HttpMethod method, Uri uri, HttpRequestMessage defaultRequest)
        {
            string jsonString = JsonConvert.SerializeObject(obj);
            HttpRequestMessage request = defaultRequest;
            request.Method = method;
            request.RequestUri = uri;
            request.Content = new StringContent(jsonString, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                string responseContent = await response.Content.ReadAsStringAsync();
                T resp = JsonConvert.DeserializeObject<T>(responseContent);
                return resp;
            }

            throw new NotImplementedException();
        }
        protected internal static HttpRequestMessage DefaultRequest
        {
            get
            {
                HttpRequestMessage request = new HttpRequestMessage();
                request.Headers.Add("User-Agent", UserAgent);
                return request;
            }
        }
    }
}