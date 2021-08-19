using Newtonsoft.Json;
using Rebb.Client.Core.Controllers.Base;
using Rebb.Client.Core.Exceptions;
using Rebb.Client.Core.Models;
using Rebb.Client.Core.Models.Result;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Rebb.Client.Core.Controllers
{
    public sealed class LoginController : ApiController
    {
        /// <summary>
        /// Validar login
        /// </summary>
        /// <param name="login">Dados do login</param>
        /// <returns></returns>
#nullable enable
        public async Task<ValidLoginResult?> ValidLoginAsync(Login? login)
        {
#nullable disable
            if (login == null)
            {
                return null;
            }

            HttpRequestMessage request = login.AutenticatedRequest(DefaultRequest);
            request.RequestUri = new Uri(Host.AbsoluteUri + "/Login/ValidLogin");

            HttpResponseMessage response = await SendAsync(request);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                return null;
            }

            string responseContent = await response.Content.ReadAsStringAsync();
            ValidLoginResult result = JsonConvert.DeserializeObject<ValidLoginResult>(responseContent);

            return result;
        }

        /// <summary>
        /// Primeiro para da autentição 
        /// </summary>
        /// <param name="user">Email ou nome de usuario.</param>
        /// <returns>Resultado do primeiro passo.</returns>
        public async Task<FirstStepResult> FirstStepAsync(string user)
        {
            Uri uri = new Uri(Host.AbsoluteUri + $"/Login/FirstStep?redirect=false&user={Uri.EscapeUriString(user)}");
            HttpResponseMessage response = await GetAsync(uri.AbsoluteUri);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new LoginException("user", "User invalid!");
            }

            string responseContent = await response.Content.ReadAsStringAsync();
            FirstStepResult result = JsonConvert.DeserializeObject<FirstStepResult>(responseContent);

            return result;
        }
        /// <summary>
        /// Segundo passo da autenticação
        /// </summary>
        /// <param name="pwd">Senha</param>
        /// <param name="key">Chave do primeiro passo</param>
        /// <param name="fs_id">ID do primeiro passo</param>
        /// <returns>Tokens de autenticação</returns>
        public async Task<AuthenticationResult> SecondStepAsync(string pwd, string key, int fs_id)
        {

            Uri uri = new Uri(Host.AbsoluteUri + $"/Login/SecondStep?redirect=false" +
                $"&pwd={Uri.EscapeUriString(pwd)}" +
                $"&key={Uri.EscapeUriString(key)}" +
                $"&fs_id={fs_id}" +
                $"&set_cookie=false");

            HttpResponseMessage response = await GetAsync(uri.AbsoluteUri);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new LoginException("pwd", "Password invalid or incorrect for this user");
            }

            string responseContent = await response.Content.ReadAsStringAsync();
            AuthenticationResult result = JsonConvert.DeserializeObject<AuthenticationResult>(responseContent);

            return result;
        }
        /// <summary>
        /// Login automatico
        /// </summary>
        /// <param name="user">Usuario</param>
        /// <param name="pwd">Senha</param>
        /// <returns>Tokens de login</returns>
        public async Task<Login> LoginAsync(string user, string pwd)
        {
            FirstStepResult firstStep = await FirstStepAsync(user);
            AuthenticationResult authentication = await SecondStepAsync(pwd, firstStep.Key, firstStep.ID);
            Login login = new Login
            {
                AccountKey = authentication.AccountKey,
                AuthenticationToken = authentication.Token,
                FirstStepKey = firstStep.Key
            };

            return login;
        }

    }
}