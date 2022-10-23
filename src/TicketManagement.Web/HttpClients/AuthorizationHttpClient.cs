using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using TicketManagement.Web.HttpClients.Options;
using TicketManagement.Web.HttpClients.Results;
using TicketManagement.Web.HttpClients.Results.AuthorizationHttpClient;
using TicketManagement.Web.Models;
using TicketManagement.Web.ViewModels.User;

namespace TicketManagement.Web.HttpClients
{
    public class AuthorizationHttpClient : HttpClientBase
    {
        private readonly AuthorizationHttpClientOptions _options;

        public AuthorizationHttpClient(IOptions<AuthorizationHttpClientOptions> options)
            : base(options.Value)
        {
            _options = options.Value;
        }

        public async Task<RegisterResult> RegisterAsync(RegisterViewModel registerViewModel)
        {
            var form = new MultipartFormDataContent
            {
                { new StringContent(registerViewModel.Email), nameof(registerViewModel.Email) },
                { new StringContent(registerViewModel.Password), nameof(registerViewModel.Password) },
                { new StringContent(registerViewModel.FirstName), nameof(registerViewModel.FirstName) },
                { new StringContent(registerViewModel.LastName), nameof(registerViewModel.LastName) },
                { new StringContent(registerViewModel.Language), nameof(registerViewModel.Language) },
                { new StringContent(registerViewModel.TimeZone), nameof(registerViewModel.TimeZone) },
            };

            var response = await HttpClient.PostAsync(_options.RegisterUrl, form);

            var registerResult = new RegisterResult(ResultType.Failure);

            if (response.IsSuccessStatusCode)
            {
                var jwt = await response.Content.ReadAsStringAsync();

                registerResult = new RegisterResult(ResultType.Success);

                registerResult.Jwt = jwt;
            }

            return registerResult;
        }

        public async Task<LoginResult> LoginAsync(LoginViewModel loginViewModel)
        {
            var form = new MultipartFormDataContent
            {
                { new StringContent(loginViewModel.Email), nameof(loginViewModel.Email) },
                { new StringContent(loginViewModel.Password), nameof(loginViewModel.Password) },
            };

            var response = await HttpClient.PostAsync(_options.LoginUrl, form);

            var loginResult = new LoginResult(ResultType.Failure);

            if (response.IsSuccessStatusCode)
            {
                var jwt = await response.Content.ReadAsStringAsync();

                loginResult = new LoginResult(ResultType.Success);

                loginResult.Jwt = jwt;
            }

            return loginResult;
        }

        public async Task<RequestRolesResult> RequestRolesAsync(string jwt)
        {
            var form = new MultipartFormDataContent
            {
                { new StringContent(jwt), nameof(jwt) },
            };

            var response = await HttpClient.PostAsync(_options.RequestRolesUrl, form);

            var requestRolesResult = new RequestRolesResult(ResultType.Failure);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                var roles = JsonConvert.DeserializeObject<IEnumerable<string>>(content);

                requestRolesResult = new RequestRolesResult(ResultType.Success);

                requestRolesResult.Roles = roles;
            }

            return requestRolesResult;
        }
    }
}