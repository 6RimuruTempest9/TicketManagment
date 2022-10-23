using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement.Mvc;
using TicketManagement.CommonElements.Parsers.Json;
using TicketManagement.Web.Attributes;
using TicketManagement.Web.Extensions;
using TicketManagement.Web.FeatureFlags;
using TicketManagement.Web.HttpClients;
using TicketManagement.Web.HttpClients.Results;
using TicketManagement.Web.Models;
using TicketManagement.Web.ViewModels.Event;

namespace TicketManagement.Web.Controllers
{
    public class EventController : Controller
    {
        private readonly EventManagerHttpClient _eventManagerHttpClient;

        public EventController(EventManagerHttpClient eventManagerHttpClient)
        {
            _eventManagerHttpClient = eventManagerHttpClient;
        }

        [HttpGet]
        [FeatureGate(ReactRedirectFeatureFlags.React)]
        public async Task<IActionResult> GetAll()
        {
            var getAllEventsResult = await _eventManagerHttpClient.GetAllEventsAsync();

            if (getAllEventsResult.ResultType == ResultType.Failure)
            {
                return BadRequest();
            }

            return View(getAllEventsResult.EventModels);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        [FeatureGate(ReactRedirectFeatureFlags.React)]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [FeatureGate(ReactRedirectFeatureFlags.React)]
        public async Task<IActionResult> Create(CreateEventViewModel createEventViewModel)
        {
            var createEventResult = await _eventManagerHttpClient.CreateEventAsync(createEventViewModel);

            if (createEventResult.ResultType == ResultType.Failure)
            {
                return BadRequest();
            }

            return RedirectToAction("GetAll");
        }

        [HttpGet]
        [Authorize(Roles = "Admin, Manager")]
        [FeatureGate(ReactRedirectFeatureFlags.React)]
        public IActionResult Update(int id, UpdateEventViewModel updateEventViewModel)
        {
            updateEventViewModel.Id = id;

            return View(updateEventViewModel);
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Manager")]
        [FeatureGate(ReactRedirectFeatureFlags.React)]
        public async Task<IActionResult> Update(UpdateEventViewModel updateEventViewModel)
        {
            var updateEventResult = await _eventManagerHttpClient.UpdateEventAsync(updateEventViewModel);

            if (updateEventResult.ResultType == ResultType.Failure)
            {
                return BadRequest();
            }

            return RedirectToAction("GetAll");
        }

        [HttpGet]
        [Authorize(Roles = "Admin, Manager")]
        [FeatureGate(ReactRedirectFeatureFlags.React)]
        public async Task<IActionResult> Delete(int id)
        {
            var deleteEventByIdResult = await _eventManagerHttpClient.DeleteEventByIdAsync(id);

            if (deleteEventByIdResult.ResultType == ResultType.Failure)
            {
                return BadRequest();
            }

            return RedirectToAction("GetAll");
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        [FeatureGate(ReactRedirectFeatureFlags.React)]
        public IActionResult LoadThirdPartyEventsFromJson()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [FeatureGate(ReactRedirectFeatureFlags.React)]
        public async Task<IActionResult> LoadThirdPartyEventsFromJson(LoadJsonViewModel loadJsonModel)
        {
            var models = default(IList<ThirdPartyEvent>);

            var environment = HttpContext.RequestServices.GetService<IWebHostEnvironment>();

            var pathToFolderWithImager = Path.Combine(environment.WebRootPath, "images");

            using (var jsonParser = new JsonParser<ThirdPartyEvent>(loadJsonModel.File.OpenReadStream()))
            {
                models = await jsonParser.GetAllModelsAsync();
            }

            await LoadJsonViewModel.ConvertImagesFromBase64StringToPngAndSave(models, pathToFolderWithImager);

            return View("ImportedEvents", models);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        [FeatureGate(ReactRedirectFeatureFlags.React)]
        public IActionResult ImportedEvent(EventModel model)
        {
            var imageFileName = EventModel.GetImageFileName(model);

            var imageUrl = EventModel.GetLocalImageUrlByImageFileName(imageFileName);

            return View("Create", new CreateEventViewModel
            {
                Name = model.Name,
                Description = model.Description,
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                ImageUrl = imageUrl,
            });
        }
    }
}