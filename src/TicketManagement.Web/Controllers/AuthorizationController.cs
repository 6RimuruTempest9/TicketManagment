using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement.Mvc;
using TicketManagement.Web.Attributes;
using TicketManagement.Web.FeatureFlags;
using TicketManagement.Web.HttpClients;
using TicketManagement.Web.HttpClients.Results;
using TicketManagement.Web.ViewModels.User;

namespace TicketManagement.Web.Controllers
{
    public class AuthorizationController : Controller
    {
        private readonly AuthorizationHttpClient _authorizationHttpClient;

        public AuthorizationController(AuthorizationHttpClient authorizationHttpClient)
        {
            _authorizationHttpClient = authorizationHttpClient;
        }

        [HttpGet]
        [NonAuthorizeOnly]
        [FeatureGate(ReactRedirectFeatureFlags.React)]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [NonAuthorizeOnly]
        [FeatureGate(ReactRedirectFeatureFlags.React)]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            var registerResult = await _authorizationHttpClient.RegisterAsync(registerViewModel);

            if (registerResult.ResultType == ResultType.Failure)
            {
                return View("FailedRegister");
            }

            HttpContext.Session.SetString("JWT", registerResult.Jwt);

            return View("SuccessRegister");
        }

        [HttpGet]
        [NonAuthorizeOnly]
        [FeatureGate(ReactRedirectFeatureFlags.React)]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [NonAuthorizeOnly]
        [FeatureGate(ReactRedirectFeatureFlags.React)]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            var loginResult = await _authorizationHttpClient.LoginAsync(loginViewModel);

            if (loginResult.ResultType == ResultType.Failure)
            {
                return View("FailedLogin");
            }

            HttpContext.Session.SetString("JWT", loginResult.Jwt);

            return View("SuccessLogin");
        }

        [HttpGet]
        [Authorize]
        [FeatureGate(ReactRedirectFeatureFlags.React)]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("JWT");

            return RedirectToAction("GetAll", "Event");
        }
    }
}