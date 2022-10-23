using System;
using System.Net.Http;
using TicketManagement.Web.HttpClients.Options;

namespace TicketManagement.Web.HttpClients
{
    public class HttpClientBase : IDisposable
    {
        private readonly HttpClient _httpClient = new HttpClient();

        private bool _disposed;

        protected HttpClientBase(HttpClientOptions options)
        {
            _httpClient.BaseAddress = new Uri(options.BaseAddress);
        }

        protected HttpClient HttpClient => _httpClient;

        public void SetHeader(string name, string value)
        {
            _httpClient.DefaultRequestHeaders.Add(name, value);
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