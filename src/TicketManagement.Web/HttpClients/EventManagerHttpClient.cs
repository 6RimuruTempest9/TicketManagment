using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using TicketManagement.Web.HttpClients.Options;
using TicketManagement.Web.HttpClients.Results;
using TicketManagement.Web.HttpClients.Results.EventManagerHttpClient;
using TicketManagement.Web.Models;
using TicketManagement.Web.ViewModels.Event;

namespace TicketManagement.Web.HttpClients
{
    public class EventManagerHttpClient : HttpClientBase
    {
        private readonly EventManagerHttpClientOptions _options;

        public EventManagerHttpClient(IOptions<EventManagerHttpClientOptions> options)
            : base(options.Value)
        {
            _options = options.Value;
        }

        public async Task<CreateEventResult> CreateEventAsync(CreateEventViewModel createEventViewModel)
        {
            var form = new MultipartFormDataContent
            {
                { new StringContent(createEventViewModel.LayoutId.ToString()), nameof(createEventViewModel.LayoutId) },
                { new StringContent(createEventViewModel.Name), nameof(createEventViewModel.Name) },
                { new StringContent(createEventViewModel.Description), nameof(createEventViewModel.Description) },
                { new StringContent(createEventViewModel.StartDate.ToString("O")), nameof(createEventViewModel.StartDate) },
                { new StringContent(createEventViewModel.EndDate.ToString("O")), nameof(createEventViewModel.EndDate) },
                { new StringContent(createEventViewModel.ImageUrl), nameof(createEventViewModel.ImageUrl) },
            };

            var response = await HttpClient.PostAsync(_options.CreateEventUrl, form);

            var createEventResult = new CreateEventResult(ResultType.Failure);

            if (response.IsSuccessStatusCode)
            {
                createEventResult = new CreateEventResult(ResultType.Success);
            }

            return createEventResult;
        }

        public async Task<UpdateEventResult> UpdateEventAsync(UpdateEventViewModel updateEventViewModel)
        {
            var form = new MultipartFormDataContent
            {
                { new StringContent(updateEventViewModel.Id.ToString()), nameof(updateEventViewModel.Id) },
                { new StringContent(updateEventViewModel.LayoutId.ToString()), nameof(updateEventViewModel.LayoutId) },
                { new StringContent(updateEventViewModel.Name), nameof(updateEventViewModel.Name) },
                { new StringContent(updateEventViewModel.Description), nameof(updateEventViewModel.Description) },
                { new StringContent(updateEventViewModel.StartDate.ToString("O")), nameof(updateEventViewModel.StartDate) },
                { new StringContent(updateEventViewModel.EndDate.ToString("O")), nameof(updateEventViewModel.EndDate) },
                { new StringContent(updateEventViewModel.ImageUrl), nameof(updateEventViewModel.ImageUrl) },
            };

            var response = await HttpClient.PostAsync(_options.UpdateEventUrl, form);

            var updateEventResult = new UpdateEventResult(ResultType.Failure);

            if (response.IsSuccessStatusCode)
            {
                updateEventResult = new UpdateEventResult(ResultType.Success);
            }

            return updateEventResult;
        }

        public async Task<DeleteEventByIdResult> DeleteEventByIdAsync(int eventId)
        {
            var url = _options.DeleteEventByIdUrl + eventId;

            var response = await HttpClient.GetAsync(url);

            var deleteEventResult = new DeleteEventByIdResult(ResultType.Failure);

            if (response.IsSuccessStatusCode)
            {
                deleteEventResult = new DeleteEventByIdResult(ResultType.Success);
            }

            return deleteEventResult;
        }

        public async Task<GetAllEventsResult> GetAllEventsAsync()
        {
            var response = await HttpClient.GetAsync(_options.GetAllEventsUrl);

            var getAllEventsResult = new GetAllEventsResult(ResultType.Failure);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                var eventModels = JsonConvert.DeserializeObject<IEnumerable<EventModel>>(content);

                getAllEventsResult = new GetAllEventsResult(ResultType.Success);

                getAllEventsResult.EventModels = eventModels;
            }

            return getAllEventsResult;
        }

        public async Task<GetEventByIdResult> GetEventByIdAsync(int eventId)
        {
            var url = _options.GetEventByIdUrl + eventId;

            var response = await HttpClient.GetAsync(url);

            var getEventByIdResult = new GetEventByIdResult(ResultType.Failure);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                var eventModel = JsonConvert.DeserializeObject<EventModel>(content);

                getEventByIdResult = new GetEventByIdResult(ResultType.Success);

                getEventByIdResult.EventModel = eventModel;
            }

            return getEventByIdResult;
        }

        public async Task<GetAllEventAreasByEventIdResult> GetAllEventAreasByEventIdAsync(int eventId)
        {
            var url = _options.GetAllEventAreasByEventIdUrl + eventId;

            var response = await HttpClient.GetAsync(url);

            var result = new GetAllEventAreasByEventIdResult(ResultType.Failure);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                var eventAreaModels = JsonConvert.DeserializeObject<IEnumerable<EventAreaModel>>(content);

                result = new GetAllEventAreasByEventIdResult(ResultType.Success);

                result.EventAreaModels = eventAreaModels;
            }

            return result;
        }

        public async Task<GetAllEventSeatsByEventAreaIdResult> GetAllEventSeatsByEventAreaIdAsync(int eventAreaId)
        {
            var url = _options.GetAllEventSeatsByEventAreaIdUrl + eventAreaId;

            var response = await HttpClient.GetAsync(url);

            var result = new GetAllEventSeatsByEventAreaIdResult(ResultType.Failure);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                var eventSeatModels = JsonConvert.DeserializeObject<IEnumerable<EventSeatModel>>(content);

                result = new GetAllEventSeatsByEventAreaIdResult(ResultType.Success);

                result.EventSeatModels = eventSeatModels;
            }

            return result;
        }

        public async Task<UpdateEventSeatStateByIdResult> UpdateEventSeatStateByIdAsync(int eventSeatId, EventSeatState eventSeatState)
        {
            var form = new MultipartFormDataContent
            {
                { new StringContent(eventSeatState.ToString()), nameof(eventSeatState) },
            };

            var url = _options.UpdateEventSeatStateByIdUrl + eventSeatId;

            var response = await HttpClient.PostAsync(url, form);

            var getEventByIdResult = new UpdateEventSeatStateByIdResult(ResultType.Failure);

            if (response.IsSuccessStatusCode)
            {
                getEventByIdResult = new UpdateEventSeatStateByIdResult(ResultType.Success);
            }

            return getEventByIdResult;
        }
    }
}