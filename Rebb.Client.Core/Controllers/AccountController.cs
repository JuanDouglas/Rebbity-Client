using Newtonsoft.Json;
using Rebb.Client.Core.Controllers.Base;
using Rebb.Client.Core.Models;
using Rebb.Client.Core.Models.Result;
using Rebb.Client.Core.Models.Upload;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Rebb.Client.Core.Controllers
{
    public sealed class AccountController : ApiController
    {
        /// <summary>
        /// Cria uma nova conta de usuário.
        /// </summary>
        /// <param name="upload">Dados da conta</param>
        /// <returns>Dados salvos da conta</returns>
        public async Task<AccountResult> CreateAccount(AccountUpload upload)
        {
            Uri uri = new Uri(Host.AbsoluteUri + "/Account/Create");
            AccountResult result = await SendJsonObject<AccountResult>(upload, HttpMethod.Put, uri);
            return result;
        }
        /// <summary>
        /// Obtém a conta do login especificado.
        /// </summary>
        /// <param name="login">Login do usuário.</param>
        /// <returns>Dados da conta</returns>
        public async Task<AccountResult> MyAccount(Login login)
        {
            Uri uri = new Uri(Host + "/Account/MyAccount");

            HttpRequestMessage request = login.AutenticatedRequest(DefaultRequest);
            request.RequestUri = uri;

            HttpResponseMessage response = await SendAsync(request);
            string responseContent = await response.Content.ReadAsStringAsync();
            AccountResult accountResult = JsonConvert.DeserializeObject<AccountResult>(responseContent);

            return accountResult;
        }

        /// <summary>
        /// Enviar código de confirmação para o email do login.
        /// </summary>
        /// <returns></returns>
        public async Task SendEmailConfirmation(Login login)
        {
            HttpRequestMessage request = login.AutenticatedRequest(DefaultRequest);
            request.RequestUri = new Uri(Host.AbsoluteUri + "/Account/SendEmailConfirmation");
            request.Method = HttpMethod.Put;

            HttpResponseMessage response = await SendAsync(request);


        }
        /// <summary>
        /// Confirmar e-mail com o codigo
        /// </summary>
        /// <param name="code">Codigo de confirmacao</param>
        /// <param name="login">Login da conta que sera confrimada</param>
        /// <returns>True se foi confirmada ou False se o codigo estiver errado</returns>
        public async Task<bool> ConfirmEmail(int code, string email, Login login)
        {
            HttpRequestMessage request = login.AutenticatedRequest(DefaultRequest);
            request.Method = HttpMethod.Put;
            request.RequestUri = new Uri(Host.AbsoluteUri + $"/Account/ConfirmEmail?number={code}&email={Uri.EscapeUriString(email)}");

            HttpResponseMessage response = await SendAsync(request);
            return response.IsSuccessStatusCode;
        }
    }
}