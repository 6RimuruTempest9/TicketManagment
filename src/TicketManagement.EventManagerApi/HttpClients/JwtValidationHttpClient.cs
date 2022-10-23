using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace TicketManagement.EventManagerApi.HttpClients
{
    public class JwtValidationHttpClient : IDisposable
    {
        private readonly HttpClient _httpClient = new HttpClient();

        private readonly JsonSerializerOptions _options;

        private bool _disposed;

        public JwtValidationHttpClient(IConfiguration configuration)
        {
            _httpClient.BaseAddress = new Uri(configuration["UserApiAddress"]);

            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        public async Task<bool> IsJwtValid(string jwt)
        {
            var form = new MultipartFormDataContent
            {
                { new StringContent(jwt), nameof(jwt) },
            };

            var response = await _httpClient.PostAsync("auth/jwtValidation", form);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            return false;
        }

        public async Task<IEnumerable<string>> GetUserRolesByJwt(string jwt)
        {
            var form = new MultipartFormDataContent
            {
                { new StringContent(jwt), nameof(jwt) },
            };

            var response = await _httpClient.PostAsync("users/getRoles", form);

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            var roles = JsonSerializer.Deserialize<IEnumerable<string>>(content, _options);

            return roles;
        }

        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                _httpClient.Dispose();
            }

            _disposed = true;
        }
    }
}