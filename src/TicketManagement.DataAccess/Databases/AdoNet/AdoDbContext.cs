using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using TicketManagement.DataAccess.Models;

namespace TicketManagement.DataAccess.Databases.AdoNet
{
    internal class AdoDbContext : IDisposable, IAsyncDisposable
    {
        private readonly SqlConnection _connection;

        public AdoDbContext(string connectionString)
        {
            if (connectionString == null)
            {
                throw new ArgumentNullException(nameof(connectionString));
            }

            _connection = new SqlConnection(connectionString);
        }

        ~AdoDbContext()
        {
            Dispose(disposing: false);
        }

        public Task ExecuteNonQueryAsync(SqlCommand command)
        {
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            return ExecuteNonQueryInternalAsync(command);
        }

        private async Task ExecuteNonQueryInternalAsync(SqlCommand command)
        {
            try
            {
                await _connection.OpenAsync();

                await command.ExecuteNonQueryAsync();
            }
            finally
            {
                await _connection.CloseAsync();
            }
        }

        public Task<TResult> ExecuteScalarAsync<TResult>(SqlCommand command)
        {
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            return ExecuteScalarInternalAsync<TResult>(command);
        }

        private async Task<TResult> ExecuteScalarInternalAsync<TResult>(SqlCommand command)
        {
            var result = default(TResult);

            try
            {
                await _connection.OpenAsync();

                result = (TResult)await command.ExecuteScalarAsync();
            }
            finally
            {
                await _connection.CloseAsync();
            }

            return result;
        }

        public Task<IEnumerable<TModel>> ExecuteQueryAsync<TModel>(SqlCommand command)
            where TModel : IModel, new()
        {
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            return ExecuteQueryInternalAsync<TModel>(command);
        }

        public async Task<IEnumerable<TModel>> ExecuteQueryInternalAsync<TModel>(SqlCommand command)
            where TModel : IModel, new()
        {
            var result = new List<TModel>();

            var instanceType = typeof(TModel);

            try
            {
                await _connection.OpenAsync();

                var reader = await command.ExecuteReaderAsync();

                var instance = (IModel)Activator.CreateInstance(instanceType);

                var properties = instanceType.GetProperties().ToList();

                while (await reader.ReadAsync())
                {
                    var instanceClone = (TModel)instance.Clone();

                    properties.ForEach(property =>
                    {
                        property.SetValue(instanceClone, reader[property.Name] is DBNull ? null : reader[property.Name]);
                    });

                    result.Add(instanceClone);
                }
            }
            finally
            {
                await _connection.CloseAsync();
            }

            return result;
        }

        public void Dispose()
        {
            Dispose(disposing: true);

            GC.SuppressFinalize(this);
        }

        public async ValueTask DisposeAsync()
        {
            await DisposeAsyncCore().ConfigureAwait(false);

            Dispose(disposing: false);

            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                (_connection as IDisposable)?.Dispose();
            }
        }

        protected virtual async ValueTask DisposeAsyncCore()
        {
            if (_connection != null)
            {
                await _connection.DisposeAsync().ConfigureAwait(false);
            }
        }
    }
}