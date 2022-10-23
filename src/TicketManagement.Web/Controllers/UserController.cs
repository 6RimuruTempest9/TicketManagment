using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.FeatureManagement.Mvc;
using TicketManagement.BusinessLogic.Services;
using TicketManagement.DataAccess.Entities;
using TicketManagement.Web.Attributes;
using TicketManagement.Web.FeatureFlags;
using TicketManagement.Web.HttpClients;
using TicketManagement.Web.Models;
using TicketManagement.Web.ViewModels.User;

namespace TicketManagement.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManagerHttpClient _userManagerHttpClient;
        private readonly IServiceAsync<Purchase> _purchaseService;

        public UserController(UserManagerHttpClient userManagerHttpClient,
            IServiceAsync<Purchase> purchaseService)
        {
            _userManagerHttpClient = userManagerHttpClient;
            _purchaseService = purchaseService;
        }

        [HttpGet]
        [Authorize]
        [FeatureGate(ReactRedirectFeatureFlags.React)]
        public async Task<IActionResult> Profile()
        {
            var jwt = HttpContext.Session.GetString("JWT");

            var result = await _userManagerHttpClient.GetUserByJwtAsync(jwt);

            if (result.ResultType == HttpClients.Results.ResultType.Failure)
            {
                return BadRequest();
            }

            return View(result.UserModel);
        }

        [HttpGet]
        [Authorize]
        [FeatureGate(ReactRedirectFeatureFlags.React)]
        public async Task<IActionResult> Edit()
        {
            var jwt = HttpContext.Session.GetString("JWT");

            var getUserByJwtResult = await _userManagerHttpClient.GetUserByJwtAsync(jwt);

            if (getUserByJwtResult.ResultType == HttpClients.Results.ResultType.Failure)
            {
                return BadRequest();
            }

            return View(getUserByJwtResult.UserModel);
        }

        [HttpPost]
        [Authorize]
        [FeatureGate(ReactRedirectFeatureFlags.React)]
        public async Task<IActionResult> Edit(UserModel model)
        {
            var result = await _userManagerHttpClient.UpdateUserAsync(model);

            if (result.ResultType == HttpClients.Results.ResultType.Failure)
            {
                return BadRequest();
            }

            return RedirectToAction("Profile", "User");
        }

        [HttpGet]
        [Authorize]
        [FeatureGate(ReactRedirectFeatureFlags.React)]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        [FeatureGate(ReactRedirectFeatureFlags.React)]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            var jwt = HttpContext.Session.GetString("JWT");

            var result = await _userManagerHttpClient.ChangePasswordAsync(jwt, model.OldPassword, model.NewPassword);

            if (result.ResultType == HttpClients.Results.ResultType.Failure)
            {
                return BadRequest();
            }

            return RedirectToAction("Profile", "User");
        }

        [HttpGet]
        [Authorize]
        [FeatureGate(ReactRedirectFeatureFlags.React)]
        public IActionResult AddBalance()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        [FeatureGate(ReactRedirectFeatureFlags.React)]
        public async Task<IActionResult> AddBalance(AddBalanceViewModel model)
        {
            var jwt = HttpContext.Session.GetString("JWT");

            var addBalanceResult = await _userManagerHttpClient.AddBalanceAsync(jwt, model.AdditionalBalance);

            if (addBalanceResult.ResultType == HttpClients.Results.ResultType.Failure)
            {
                return BadRequest();
            }

            return RedirectToAction("Profile", "User");
        }

        [HttpGet]
        [Authorize]
        [FeatureGate(ReactRedirectFeatureFlags.React)]
        public async Task<IActionResult> History()
        {
            var jwt = HttpContext.Session.GetString("JWT");

            var getUserByJwtResult = await _userManagerHttpClient.GetUserByJwtAsync(jwt);

            if (getUserByJwtResult.ResultType == HttpClients.Results.ResultType.Failure)
            {
                return BadRequest();
            }

            var userModel = getUserByJwtResult.UserModel;

            var purchases = await _purchaseService.GetAll()
                .Where(purchase => purchase.UserId == userModel.Id)
                .GroupBy(purchase => purchase.Time)
                .Select(gp => new PurchaseViewModel
                {
                    Time = gp.Key,
                    Amount = gp.Sum(purchase => purchase.Price),
                })
                .ToListAsync();

            return View(purchases);
        }
    }
}