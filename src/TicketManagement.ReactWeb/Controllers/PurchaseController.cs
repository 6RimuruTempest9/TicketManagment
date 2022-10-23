using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TicketManagement.BusinessLogic.Services;
using TicketManagement.DataAccess.Entities;

namespace TicketManagement.ReactWeb.Controllers
{
    public class PurchaseController : Controller
    {
        private readonly IServiceAsync<Purchase> _purchaseService;

        public PurchaseController(IServiceAsync<Purchase> purchaseService)
        {
            _purchaseService = purchaseService;
        }

        [HttpPost]
        public async Task<IActionResult> BuyTicket(
            [FromForm] string userId,
            [FromForm] decimal ticketPrice,
            [FromForm] int seatId)
        {
            var purchase = new Purchase
            {
                UserId = userId,
                EventSeatId = seatId,
                Time = DateTime.Now,
                Price = ticketPrice,
            };

            await _purchaseService.InsertAsync(purchase);

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> History([FromForm] string userId)
        {
            var purchases = await _purchaseService.GetAll()
                .Where(purchase => purchase.UserId == userId)
                .GroupBy(purchase => purchase.Time)
                .Select(gp => new
                {
                    Time = gp.Key,
                    Amount = gp.Sum(purchase => purchase.Price),
                })
                .ToListAsync();

            return Ok(purchases);
        }
    }
}