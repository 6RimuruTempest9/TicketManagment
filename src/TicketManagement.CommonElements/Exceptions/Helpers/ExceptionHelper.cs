using System;
using System.Threading.Tasks;

namespace TicketManagement.CommonElements.Exceptions.Helpers
{
    public class ExceptionHelper
    {
        public T ExecuteWithServiceException<T>(Func<T> action, string message)
        {
            try
            {
                return action.Invoke();
            }
            catch (Exception ex)
            {
                throw new ServiceException(message, ex);
            }
        }

        public async Task ExecuteWithServiceExceptionAsync(Func<Task> action, string message)
        {
            try
            {
                await action.Invoke();
            }
            catch (Exception ex)
            {
                throw new ServiceException(message, ex);
            }
        }

        public async Task<T> ExecuteWithServiceExceptionAsync<T>(Func<Task<T>> action, string message)
        {
            try
            {
                return await action.Invoke();
            }
            catch (Exception ex)
            {
                throw new ServiceException(message, ex);
            }
        }

        public T ExecuteWithEFRepositoryException<T>(Func<T> action, string message)
        {
            try
            {
                return action.Invoke();
            }
            catch (Exception ex)
            {
                throw new EFRepositoryException(message, ex);
            }
        }

        public async Task ExecuteWithEFRepositoryExceptionAsync(Func<Task> action, string message)
        {
            try
            {
                await action.Invoke();
            }
            catch (Exception ex)
            {
                throw new EFRepositoryException(message, ex);
            }
        }

        public async Task<T> ExecuteWithEFRepositoryExceptionAsync<T>(Func<Task<T>> action, string message)
        {
            try
            {
                return await action.Invoke();
            }
            catch (Exception ex)
            {
                throw new EFRepositoryException(message, ex);
            }
        }
    }
}