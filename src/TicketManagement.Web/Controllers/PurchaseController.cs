using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement.Mvc;
using TicketManagement.BusinessLogic.Services;
using TicketManagement.DataAccess.Entities;
using TicketManagement.Web.Attributes;
using TicketManagement.Web.FeatureFlags;
using TicketManagement.Web.HttpClients;
using TicketManagement.Web.HttpClients.Results;
using TicketManagement.Web.Models;

namespace TicketManagement.Web.Controllers
{
    public class PurchaseController : Controller
    {
        private readonly IServiceAsync<Purchase> _purchaseService;
        private readonly UserManagerHttpClient _userManagerHttpClient;
        private readonly EventManagerHttpClient _eventManagerHttpClient;

        public PurchaseController(IServiceAsync<Purchase> purchaseService,
            UserManagerHttpClient userManagerHttpClient,
            EventManagerHttpClient eventManagerHttpClient)
        {
            _purchaseService = purchaseService;
            _userManagerHttpClient = userManagerHttpClient;
            _eventManagerHttpClient = eventManagerHttpClient;
        }

        [HttpPost]
        [Authorize]
        [FeatureGate(ReactRedirectFeatureFlags.React)]
        public async Task<IActionResult> BuyTickets(PurchaseModel purchaseModel)
        {
            if (purchaseModel.EventSeatIds.Any())
            {
                var jwt = HttpContext.Session.GetString("JWT");

                if (string.IsNullOrEmpty(jwt))
                {
                    return BadRequest();
                }

                var time = DateTime.Now;

                var getUserByJwtResult = await _userManagerHttpClient.GetUserByJwtAsync(jwt);

                if (getUserByJwtResult.ResultType == ResultType.Failure)
                {
                    return BadRequest();
                }

                var userModel = getUserByJwtResult.UserModel;

                var purchaseCost = purchaseModel.TicketPrice * purchaseModel.EventSeatIds.Count();

                if (userModel.Balance < purchaseCost)
                {
                    return BadRequest();
                }

                userModel.Balance -= purchaseCost;

                await _userManagerHttpClient.UpdateUserAsync(userModel);

                foreach (var eventSeatId in purchaseModel.EventSeatIds)
                {
                    var updateEventSeatStateByIdResult
                        = await _eventManagerHttpClient.UpdateEventSeatStateByIdAsync(eventSeatId, Models.EventSeatState.Busy);

                    if (updateEventSeatStateByIdResult.ResultType == ResultType.Failure)
                    {
                        return BadRequest();
                    }

                    var purchase = new Purchase
                    {
                        UserId = userModel.Id,
                        EventSeatId = eventSeatId,
                        Time = time,
                        Price = purchaseModel.TicketPrice,
                    };

                    await _purchaseService.InsertAsync(purchase);
                }
            }

            return RedirectToAction("GetAll", "Event");
        }
    }
}