using System;
using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using TicketManagement.Web.HttpClients.Options;
using TicketManagement.Web.HttpClients.Results;
using TicketManagement.Web.HttpClients.Results.UserManagerHttpClient;
using TicketManagement.Web.Models;

namespace TicketManagement.Web.HttpClients
{
    public class UserManagerHttpClient : HttpClientBase
    {
        private readonly UserManagerHttpClientOptions _options;

        public UserManagerHttpClient(IOptions<UserManagerHttpClientOptions> options)
            : base(options.Value)
        {
            _options = options.Value;
        }

        public async Task<GetUserByJwtResult> GetUserByJwtAsync(string jwt)
        {
            var form = new MultipartFormDataContent
            {
                { new StringContent(jwt), nameof(jwt) },
            };

            var response = await HttpClient.PostAsync(_options.GetUserByJwtUrl, form);

            var result = new GetUserByJwtResult(ResultType.Failure);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                var userModel = JsonConvert.DeserializeObject<UserModel>(content);

                result = new GetUserByJwtResult(ResultType.Success);

                result.UserModel = userModel;
            }

            return result;
        }

        public async Task<UpdateUserResult> UpdateUserAsync(UserModel userModel)
        {
            var form = new MultipartFormDataContent
            {
                { new StringContent(userModel.Id), nameof(userModel.Id) },
                { new StringContent(userModel.Email), nameof(userModel.Email) },
                { new StringContent(userModel.FirstName), nameof(userModel.FirstName) },
                { new StringContent(userModel.LastName), nameof(userModel.LastName) },
                { new StringContent(userModel.Language), nameof(userModel.Language) },
                { new StringContent(userModel.TimeZone), nameof(userModel.TimeZone) },
                { new StringContent(userModel.Balance.ToString()), nameof(userModel.Balance) },
            };

            var response = await HttpClient.PostAsync(_options.UpdateUserUrl, form);

            var result = new UpdateUserResult(ResultType.Failure);

            if (response.IsSuccessStatusCode)
            {
                result = new UpdateUserResult(ResultType.Success);
            }

            return result;
        }

        public async Task<ChangePasswordResult> ChangePasswordAsync(string jwt, string oldPassword, string newPassword)
        {
            var getUserByJwtResult = await GetUserByJwtAsync(jwt);

            if (getUserByJwtResult.ResultType == ResultType.Failure)
            {
                return new ChangePasswordResult(ResultType.Failure);
            }

            var userModel = getUserByJwtResult.UserModel;

            var form = new MultipartFormDataContent
            {
                { new StringContent(userModel.Id), "UserId" },
                { new StringContent(oldPassword), "OldPassword" },
                { new StringContent(newPassword), "NewPassword" },
            };

            var response = await HttpClient.PostAsync(_options.ChangePasswordUrl, form);

            var changePasswordResult = new ChangePasswordResult(ResultType.Failure);

            if (response.IsSuccessStatusCode)
            {
                changePasswordResult = new ChangePasswordResult(ResultType.Success);
            }

            return changePasswordResult;
        }

        public async Task<AddBalanceResult> AddBalanceAsync(string jwt, decimal additionalBalance)
        {
            var getUserByJwtResult = await GetUserByJwtAsync(jwt);

            if (getUserByJwtResult.ResultType == ResultType.Failure)
            {
                return new AddBalanceResult(ResultType.Failure);
            }

            var userModel = getUserByJwtResult.UserModel;

            userModel.Balance += additionalBalance;

            var updateUserResult = await UpdateUserAsync(userModel);

            if (updateUserResult.ResultType == ResultType.Failure)
            {
                return new AddBalanceResult(ResultType.Failure);
            }

            return new AddBalanceResult(ResultType.Success);
        }
    }
}