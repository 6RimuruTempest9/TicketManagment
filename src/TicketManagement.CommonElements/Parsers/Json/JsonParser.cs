using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TicketManagement.CommonElements.Parsers.Json
{
    public class JsonParser<TModel> : IDisposable
    {
        private readonly Stream _stream;

        private bool _disposed;

        public JsonParser(Stream stream)
        {
            _stream = stream ?? throw new ArgumentNullException(nameof(stream));
        }

        public async Task<IList<TModel>> GetAllModelsAsync()
        {
            var content = string.Empty;

            using (var streamReader = new StreamReader(_stream))
            {
                content = await streamReader.ReadToEndAsync();
            }

            return JsonConvert.DeserializeObject<IList<TModel>>(content);
        }

        public void Close()
        {
            _stream.Close();
        }

        public void Dispose()
        {
            Dispose(disposing: true);

            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _stream.Dispose();
                }

                _disposed = true;
            }
        }
    }
}