using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TicketManagement.BusinessLogic.Extensions;
using TicketManagement.BusinessLogic.Services;
using TicketManagement.DataAccess.Entities;
using TicketManagement.ReactWeb.Models;

namespace TicketManagement.ReactWeb.Controllers
{
    public class EventController : Controller
    {
        private readonly LayoutService _layoutService;

        public EventController(IServiceAsync<Layout> layoutService)
        {
            _layoutService = (LayoutService)layoutService;
        }

        [HttpGet]
        public IActionResult GetAvailableLayouts()
        {
            var groupedAvailableLayouts = _layoutService.GetGroupedAvailableLayoutModel();

            return Ok(groupedAvailableLayouts);
        }
    }
}